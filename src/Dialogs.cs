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
        private Crypto _crypto;
        private CMDbContext db;
        private Button confirmButton;
        private DropDown changeAction;

        private readonly Size TABLE_SPACING = new Size(20, 20);
        private readonly Padding TABLE_PADDING = new Padding(20);

        public BuySellDialog(Crypto crypto)
        {
            _crypto = crypto;

            var cryptoName1 = new Label { Text = "Dollari" };
            var cryptoName2 = new Label { Text = _crypto.Name };
            var cryptoQty1 = new TextBox { PlaceholderText = "USD" };
            var cryptoQty2 = new TextBox { PlaceholderText = _crypto.Symbol.ToUpper() };

            confirmButton = CreateButton();
            changeAction = CreateDropDown();

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
            var buyCommand = new Command((sender, e) =>
            {
                Console.WriteLine("Compra");
            });
            var sellCommand = new Command((sender, e) =>
            {
                Console.WriteLine("Vendi");
            });
            dropDown.SelectedIndex = 0;
            confirmButton.Command = buyCommand; //buy set as default
            dropDown.SelectedIndexChanged += (sender, e) => 
            {
                confirmButton.Command = dropDown.SelectedIndex == 0 
                    ? buyCommand : sellCommand;
            };
            return dropDown;
        }
    }
}
