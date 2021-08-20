using System;
using System.Linq;
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
    public class Transaction : Scrollable
    {
        private CMDbContext db;
        public string Name { get; } = "Transaction";
        
        private const int BUTTON_WIDTH = 50;
        private TableLayout layout = new TableLayout
                {
                    Spacing = new Size(5, 5),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
        
        public Transaction()
        {
            var dropDown = new DropDown{ Items = {
                "All", "Running"
            }};
            var buttonFilter = new Button
                {
                    Text = "Apply",
                    /*Command = new Command((sender, e) =>
                            {
                                db = CMDbContext.Instance;
                                var trans = db.Transaction.Select(t => new GuiTransaction{
                                    Id = t.Id,
                                    SourceId = t.SourceId,
                                    DestinationId = t.DestinationId,
                                    CryptoId = t.CryptoId,
                                    //StartDate = t.StartDate,
                                    //FinishDate = t.FinishDate,
                                    CryptoQuantity = t.CryptoQuantity,
                                    State = t.State
                                }).ToList();
                                FillLayout(trans);
                                Console.WriteLine("click");
                                Content = layout;
                            }),*/
                    Width = BUTTON_WIDTH
                };
            var filterRow = new TableRow(
                new TableCell(new Label() { Text = "Filter"}, true),
                        new TableCell(dropDown, true),
                        new TableCell(buttonFilter, true)
                        
            );
            var row = new TableRow(
                new TableCell(new Label() { Text = "Id"}, true),
                        new TableCell(new Label() { Text = "DestinationId"}, true),
                        new TableCell(new Label() { Text = "CryptoId"}, true),
                        new TableCell(new Label() { Text = "CryptoQuantity"}, true),
                        new TableCell(new Label() { Text = "State"}, true)
                        
                        );
            layout.Rows.Add(filterRow);
            layout.Rows.Add(row);
            db = CMDbContext.Instance;
            var trans = db.Transaction.Select(t => new GuiTransaction{
                    Id = t.Id,
                    SourceId = t.SourceId,
                    DestinationId = t.DestinationId,
                    CryptoId = t.CryptoId,
                    //StartDate = t.StartDate,
                    //FinishDate = t.FinishDate,
                    CryptoQuantity = t.CryptoQuantity,
                    State = t.State
                }).ToList();
            FillLayout(trans);
            Content = layout;
            
        }
       public void FillLayout(List<GuiTransaction> trans)
        {
            trans.ForEach(t =>
            {
               var row = new TableRow(
                        new TableCell(new Label() { Text = t.Id.ToString()}, true),
                        new TableCell(new Label() { Text = t.DestinationId.ToString() }, true),
                        new TableCell(new Label() { Text = t.CryptoId.ToString()}, true),
                        new TableCell(new Label() { Text = t.CryptoQuantity.ToString()}, true),
                        new TableCell(new Label() { Text = t.State.ToString()}, true)
                        
                        );
                layout.Rows.Add(row);
            });
        }
    }
    
    
    public class Wallet : Panel
    {
        public string Name { get; } = "Wallet";
        private CMDbContext db;
        private TableLayout layout = new TableLayout
                {
                    Spacing = new Size(5, 5),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
        private const int BUTTON_WIDTH = 50;

        public Wallet()
        {
            Func<TableLayout> createLayout = () =>
            {
                var t = new TableLayout
                {
                    Spacing = new Size(5, 5),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
                var title =
				(
					new TableRow(
						new TableCell(new Label { Text = "Saved Crypto" }, true)
					)
                );
                
                db = CMDbContext.Instance;
                    var wallet = db.Wallet.Select(c => new GuiWallet{
                        CryptoId = c.CryptoId,
                        Quantity = c.Quantity
                        
                    }).ToList();
                    wallet.ForEach(w => 
                        {
                            var cryptoList = new TableRow(
                                new TableCell(new Label() { Text = w.CryptoId}, true),
                                new TableCell(new Label() { Text = w.Quantity.ToString()}, true)
                            );
                            layout.Rows.Add(cryptoList);
                            
                        }
                    );
                var cryptoList = new TableRow(
                    new Scrollable(){
                        Content = layout
                    }
                );
                var buttonRow = new TableRow(
                        TableLayout.AutoSized(new Button(){Text = "Send", Width = BUTTON_WIDTH}),
                        TableLayout.AutoSized(new Button(){Text = "Refresh", Width = BUTTON_WIDTH}) 
                    );
                var tenTrans = new TableRow(
                        new ListBox()
                    );
                t.Rows.Add(title);
                t.Rows.Add(cryptoList);
                t.Rows.Add(buttonRow);
                t.Rows.Add(tenTrans);
                return t;
            };
            Content = createLayout();
        }

        public void FillWallet(){}
    }

    public class CoinsList : Scrollable
    {
        public string Name { get; } = "Coins list";

        private CMDbContext db;
        private TableLayout table;
        private const int BUTTON_WIDTH = 50;
        private readonly Size DIALOG_SIZE = new Size(600, 400);

        public CoinsList()
        {
            db = CMDbContext.Instance;
            var coins = db.Crypto.Select(c => new GuiCrypto{
                    Name = c.Name,
                    Symbol = c.Symbol,
                    Id = c.Id,
                    Price = c.CurrentPrice,
                    Rank = c.MarketCapRank
                });
            table = new TableLayout();
            table.Spacing = new Size(10, 10);
            table.Padding = new Padding(10, 10, 10, 10);
            var coinsList = coins.ToList();
            coinsList.Sort();
            UpdateList(coinsList);
            Content = table;
        }

        public void UpdateList(List<GuiCrypto> coins)
        {
            table.RemoveAll();
            coins.ForEach(c =>
            {
                var button = new Button
                {
                    Text = "...",
                    Command = new Command((sender, e) =>
                            {
                                var content = new CryptoDialog(c.Id)
                                {
                                    Size = DIALOG_SIZE
                                };
                                var dialog = new Dialog()
                                {
                                    Size = content.Size,
                                    Content = content
                                };
                                dialog.ShowModal();
                            }),
                    Width = BUTTON_WIDTH
                };

                var row = new TableRow(
                        new TableCell(new Label() { Text = c.Symbol}, true),
                        new TableCell(new Label() { Text = c.Name }, true),
                        new TableCell(new Label() { Text = c.Price.ToString()}, true),
                        TableLayout.AutoSized(button) 
                        );
                table.Rows.Add(row);
            });
        }
    }

    public class CryptoDialog : Panel
    {
        private CMDbContext db;

        public CryptoDialog(string cryptoId)
        {
            db = CMDbContext.Instance;

            var c = new DynamicLayout();
            var image = Task.Run(async () => 
            {
                return await GetImage(cryptoId);
            }).Result;

            c.BeginVertical();
            c.BeginHorizontal();
            c.Add(image);
            c.Add(new Label{ Text = db.Crypto.Find(cryptoId).Name });
            c.Add(new Label{ Text = $"({db.Crypto.Find(cryptoId).Symbol.ToUpper()})" });
            c.EndHorizontal();
            c.EndVertical();

            c.BeginVertical();
            c.BeginHorizontal();
            c.Add(new TextArea());
            c.Add(new TextArea());
            c.EndHorizontal();
            c.EndVertical();

            Content = c;
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
}
