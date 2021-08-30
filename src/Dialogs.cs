using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

using Eto.Forms;
using Eto.Drawing;
using CoinManager.EF;
using CoinManager.Models.GUI;
namespace CoinManager.GUI
{
    public class CryptoDialog : Panel
    {
        private CMDbContext db;
        private readonly Size BUTTON_SIZE = new Size(400, 50);

        public CryptoDialog(string cryptoId)
        {
            db = CMDbContext.Instance;

            var table = new TableLayout();
            var stack = new StackLayout();
            var image = Task.Run(async () => 
            {
                return await GetImage(cryptoId);
            }).Result;

            var crypto = db.Crypto.Find(cryptoId);

            var buySellButton = new Button
            {
                Text = "Compra/Vendi",
                Size = BUTTON_SIZE,
                Command = new Command((sender, e) =>
                {
                    var dialog = new Dialog
                    {
                        Size = new Size(Size.Width, Size.Height/2),
                        Content = new BuySellDialog(crypto)
                    };
                    dialog.ShowModal();
                })
            };

            var loanButton = new Button
            {
                Text = "Prestito",
                Size = BUTTON_SIZE,
                Command = new Command((sender, e) => 
                {
                    var dialog = new Dialog
                    {
                        Size = new Size(Size.Width, Size.Height/2),
                        Content = new LoanDialog(crypto)
                    };
                    dialog.ShowModal();
                })
            };

            stack.Items.Add(new Label{ Text = crypto.Name + $" ({crypto.Symbol.ToUpper()})"});
            stack.Items.Add(new Label{ Text = $"Posizione: {crypto.MarketCapRank.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Prezzo: ${crypto.CurrentPrice.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Capitale di mercato: ${crypto.MarketCap.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Volume di mercato: ${crypto.TotalVolume.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Qtà in circolo: {crypto.CirculatingSupply.ToString()}" });
            table.Rows.Add(new TableRow{ Cells = { image, stack } });
            table.Rows.Add(new TableRow{ Cells = { null, buySellButton } });
            table.Rows.Add(new TableRow{ Cells = { null, loanButton } });
            table.Padding = new Padding(20, 20);
            table.Spacing = new Size(20, 20);
            Content = table;
        }

        private async Task<ImageView> GetImage(string cryptoId)
        {
            var http = new HttpClient();
            var url = db.Crypto.Find(cryptoId).ImageUrl;
            var res = await http.GetAsync(url);
            var stream = await res.Content.ReadAsStreamAsync();
            var memStream = new MemoryStream();
            await stream.CopyToAsync(memStream);
            memStream.Position = 0;

            return new ImageView
            {
                Image = new Bitmap(memStream),
                Size = new Size(100, 100)
            };
        }
    }

    public class LoanDialog : Panel
    {
        private Crypto _crypto;
        private readonly Size TABLE_SPACING = new Size(20, 20);
        private readonly Padding TABLE_PADDING = new Padding(20);
        private TextBox cryptoQty;
        private TextBox advance;
        private Label expireDate;
        private Label errorMessage;
        private Button confirmButton;
        private double advanceValue;
        private double loanValue;
        private double loanQty;

        public LoanDialog(Crypto crypto)
        {
            _crypto = crypto;

            var inputs = CreateInputs(_crypto.Symbol.ToUpper(), "USDT");
            confirmButton = CreateButton("Conferma");

            var cryptoLabels = new TableRow
            {
                Cells =
                {
                    new TableCell(new Label { Text = $"Qtà richiesta ({_crypto.Symbol.ToUpper()})" }, true),
                    new Label { Text = "Anticipo (USDT)" },
                }
            };

            var qtyRow = new TableRow
            {
                Cells = 
                {
                    new TableCell(inputs.Item1, true),
                    inputs.Item2,
                }
            };

            var infoLabels = new TableRow
            {
                Cells = 
                {
                    new TableCell(expireDate, true),
                    null
                }
            };

            var buttonRow = new TableRow
            {
                Cells = 
                {
                    new TableCell(errorMessage, true),
                    new TableCell(confirmButton),
                }
            };

            Content = CreateTable(cryptoLabels, qtyRow, infoLabels, buttonRow);
        }

        private Button CreateButton(string buttonText)
        {
            return new Button 
            {
                Text = buttonText,
                Command = new Command((sender, e) => 
                {
                    if(loanValue > 0 && advanceValue >= 0)
                        Console.WriteLine($"{loanValue}$ di {_crypto.Symbol.ToUpper()} con {advanceValue}$ e {expireDate.Text}");
                })
            };
        }

        private Tuple<TextBox, TextBox> CreateInputs(string qtyPh, string advPh)
        {
            var cryptoQty = new TextBox { PlaceholderText = qtyPh };
            var advance = new TextBox { PlaceholderText = advPh };
            var defaultDate = DateTime.Now.AddDays(100);
            expireDate = new Label { Text = $"Scadenza: {defaultDate}" };
            errorMessage = new Label();
            int maxValue = 200000;

            cryptoQty.TextChanged += (sender, e) => 
            {
                int days;
                double qty;
                var now = DateTime.Now;
                var defaultDate = now.AddDays(100);

                var ok = double.TryParse(cryptoQty.Text, out qty);
                if(!ok) cryptoQty.Text = "";

                if(_crypto.CurrentPrice * qty > maxValue)
                {
                    errorMessage.Text = $"Stai chiedendo troppo, non sono\nconcessi prestiti sopra i {maxValue}$ di valore";
                    loanValue = maxValue;
                    cryptoQty.Text = (maxValue / _crypto.CurrentPrice).ToString();
                    return;
                }

                loanQty = qty;
                loanValue = _crypto.CurrentPrice * qty;
                days = Convert.ToInt32(_crypto.CurrentPrice * qty / (maxValue / 1000));
                var finalDate = now.AddDays(days);
                if(days == 0) finalDate = defaultDate;

                expireDate.Text = $"Scadenza: {finalDate}";
                errorMessage.Text = "";
            };

            advance.TextChanged += (sender, e) => 
            {
                double value;
                var ok = double.TryParse(advance.Text, out value);
                if(!ok)
                {
                    advance.Text = "";
                }
                if(value > loanValue/2) 
                {
                    advance.Text = "";
                    confirmButton.Enabled = false;
                    errorMessage.Text = "Non puoi anticipare più della\nmetà del valore richiesto!";
                    return;
                }
                confirmButton.Enabled = true;
                advanceValue = value;
            };

            return new Tuple<TextBox, TextBox>(cryptoQty, advance);
        }

        private TableLayout CreateTable(params TableRow[] rows)
        {
            var table = new TableLayout();
            table.Padding = TABLE_PADDING;
            table.Spacing = TABLE_SPACING;
            foreach(var row in rows) 
                table.Rows.Add(row);
            return table;
        }
    }

    public class BuySellDialog : Panel
    {
        private enum MarketAction {
            BUY, SELL
        }

        private Crypto _crypto;
        private CMDbContext db;
        private Button confirmButton;
        private DropDown changeAction;
        private TextBox cryptoQty1;
        private TextBox cryptoQty2;
        private bool buySelected;

        private readonly Size TABLE_SPACING = new Size(20, 20);
        private readonly Padding TABLE_PADDING = new Padding(20);

        public BuySellDialog(Crypto crypto)
        {
            _crypto = crypto;

            var inputsQty = CreateInputs();
            var cryptoName1 = new Label { Text = "Dollari" };
            var cryptoName2 = new Label { Text = _crypto.Name };
            confirmButton = CreateButton();
            changeAction = CreateDropDown();

            cryptoQty1 = inputsQty.Item1;
            cryptoQty2 = inputsQty.Item2;

            var labels = new TableRow 
            {
                Cells =
                {
                    new TableCell(cryptoName1, true),
                    new TableCell(cryptoName2, true),
                }
            };
            var inputs = new TableRow 
            {
                Cells =
                {
                    new TableCell(cryptoQty1, true),
                    new TableCell(cryptoQty2, true),
                }
            };
            var actions = new TableRow 
            {
                Cells =
                {
                    new TableCell(changeAction, true),
                    new TableCell(confirmButton, true)
                }
            };

            Content = CreateTable(labels, inputs, actions);
        }

        private Tuple<TextBox, TextBox> CreateInputs()
        {
            var c1 = new TextBox { PlaceholderText = "USDT" };
            var c2 = new TextBox { PlaceholderText = _crypto.Symbol.ToUpper() };

            System.EventHandler<System.EventArgs> cmdCheckValue = (sender, e) => 
            {
                double qty;
                var t = sender as TextBox;
                if (!t.Enabled) return;
                var ok = double.TryParse(t.Text, out qty);
                if(!ok) t.Text = "";
                confirmButton.Enabled = ok;
                if(t == c1)
                    c2.Text = (qty / _crypto.CurrentPrice).ToString();
                else
                    c1.Text = (qty * _crypto.CurrentPrice).ToString();
            };

            c2.Enabled = false;
            c1.TextChanged += cmdCheckValue;
            c2.TextChanged += cmdCheckValue;
            return new Tuple<TextBox, TextBox>(c1, c2);
        }

        private TableLayout CreateTable(params TableRow[] rows)
        {
            var table = new TableLayout();
            table.Padding = TABLE_PADDING;
            table.Spacing = TABLE_SPACING;
            foreach(var row in rows) 
                table.Rows.Add(row);
            return table;
        }

        private Button CreateButton()
        {
            var button = new Button
            {
                Text = "Conferma"
            };
            return button;
        }

        private DropDown CreateDropDown()
        {
            var dropDown = new DropDown
            {
                Items = 
                { 
                    new ListItem { Text = "Compra" },
                    new ListItem { Text = "Vendi" }
                }
            };
            var cmdConfirm = new Command((sender, e) =>
            {
                if(buySelected)
                {
                    Console.WriteLine($"Buying {cryptoQty2.Text} with {cryptoQty1.Text}");
                }
                else
                {
                    Console.WriteLine($"Selling {cryptoQty2.Text} for {cryptoQty1.Text}");
                }
            });

            dropDown.SelectedIndex = (int)MarketAction.BUY;
            confirmButton.Command = cmdConfirm; //buy set as default
            buySelected = dropDown.SelectedIndex == (int)MarketAction.BUY;

            dropDown.SelectedIndexChanged += (sender, e) => 
            {
                var table = this.Content as TableLayout;
                buySelected = dropDown.SelectedIndex == (int)MarketAction.BUY;
                confirmButton.Command = cmdConfirm;
                cryptoQty1.Enabled = buySelected;
                cryptoQty2.Enabled = !buySelected;
            };
            return dropDown;
        }
    }

    public class SendDialog : Panel
    {
        private CMDbContext db;

        public SendDialog(List<GuiWallet> wallet)
        {
            db = CMDbContext.Instance;

            var table = new TableLayout();
            var stack = new StackLayout();
            var dropDown = new DropDown();

            wallet.ForEach(w =>
                    {
                        dropDown.Items.Add(new ListItem { Text = w.CryptoId });
                    });
            
            var destinationRow = new TableRow
            (
                TableLayout.AutoSized(new Label { Text = "Destination" }),
                TableLayout.AutoSized(new DropDown())
            );
            var walletRow = new TableRow
            (
                 TableLayout.AutoSized(new Label { Text = "Crypto to send" }),
                 TableLayout.AutoSized(dropDown)
            );
            var textBox = new TextBox();
            var quantityRow = new TableRow
            (
                TableLayout.AutoSized(new Label { Text = "Quantity to send" }),
                TableLayout.AutoSized(textBox)
                //sarebbe una bella idea
                /*new Slider { 
                    MaxValue = wallet.Find(dropDown.SelectedValue).Quantity,
                    MinValue = 0,
                    Orientation = 0
                }*/
            );
            var cmdSend = new Command((sender, e) =>
                             {
                                if(dropDown.SelectedValue != null)
                                    MessageBox.Show("Send!", 0);
                            });
            var sendRow = new TableRow
            (
                TableLayout.AutoSized(new Button
                                        {
                                            Text = "Send",
                                            Command = cmdSend
                                        })
            );
            table.Rows.Add(destinationRow);
            table.Rows.Add(walletRow);
            table.Rows.Add(quantityRow);
            table.Rows.Add(sendRow);

            table.Padding = new Padding(20, 20);
            table.Spacing = new Size(20, 20);
            Content = table;
        }
    }
}
