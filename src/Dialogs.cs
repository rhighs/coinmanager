using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.EF;

namespace CoinManager.GUI
{
    public class CryptoDialog : Panel
    {
        private CMDbContext db;

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

            var button = new Button
            {
                Text = "Compra/Vendi",
                Size = new Size(400, 100),
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

            stack.Items.Add(new Label{ Text = crypto.Name + $" ({crypto.Symbol.ToUpper()})"});
            stack.Items.Add(new Label{ Text = $"Posizione: {crypto.MarketCapRank.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Prezzo: ${crypto.CurrentPrice.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Capitale di mercato: ${crypto.MarketCap.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Volume di mercato: ${crypto.TotalVolume.ToString()}" });
            stack.Items.Add(new Label{ Text = $"Qtà in circolo: {crypto.CirculatingSupply.ToString()}" });
            table.Rows.Add(new TableRow{ Cells = { image, stack } });
            table.Rows.Add(new TableRow{ Cells = { null, button } });
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
}
