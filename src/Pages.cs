using System;
using System.Linq;
using System.Collections.Generic;

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
        private TableLayout layout = new TableLayout
                {
                    Spacing = new Size(5, 5),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
        public Transaction()
        {
            var row = new TableRow(
                new TableCell(new Label() { Text = "Id"}, true),
                        new TableCell(new Label() { Text = "DestinationId"}, true),
                        new TableCell(new Label() { Text = "CryptoId"}, true),
                        new TableCell(new Label() { Text = "CryptoQuantity"}, true),
                        new TableCell(new Label() { Text = "State"}, true)
                        );
            layout.Rows.Add(row);
            db = CMDbContext.Instance;
            var trans = db.Transaction.Select(t => new GuiTransaction
                {
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

        public Wallet()
        {
            Func<TableLayout> createLayout = () =>
            {
                var t = new TableLayout
                {
                    Spacing = new Size(5, 5),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
                var titles =
				(
					new TableRow(
						new TableCell(new Label { Text = "First half" }, true), 
						new TableCell(new Label { Text = "Second half" }, true)
					)
                );
                var items = 
                (
                    new TableRow(
                        new ListBox(),
			            new ListBox()
		            )
                );
                t.Rows.Add(titles);
                t.Rows.Add(items);
                return t;
            };
            Content = createLayout();
        }
    }

    public class CoinsList : Scrollable
    {
        public string Name { get; } = "Coins list";

        private CMDbContext db;
        private TableLayout table;
        private const int BUTTON_WIDTH = 50;
        private readonly Size DIALOG_SIZE = new Size(600, 300);

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
                                var dialog = new Dialog
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

}
