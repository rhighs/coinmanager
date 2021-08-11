using System;
using System.Linq;
using System.Collections.Generic;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.EF;
using CoinManager.Models.GUI;

namespace CoinManager.GUI
{
    public class Transaction : Panel
    {
         public string Name { get; } = "Transaction";
        public Transaction()
        {
             Func<TableLayout> createLayout = () =>
            {
                var t = new TableLayout
                {
                    Spacing = new Size(1, 1),
				    Padding = new Padding(10, 10, 10, 10), 
				
                };
                var filter =
				(
					new TableRow(
				        new TableCell(new Label { Text = "Filter" }, true),
                        new TableCell(new DropDown { Items = { "All", "Running" } }, true)
					)
                );
                var items = 
                (
                    new TableRow(
                        new TableCell(new ListBox())
                        
		            )
                );
                t.Rows.Add(filter);
                t.Rows.Add(items);
                return t;
            };
            Content = createLayout();
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
            //var i = Content.Items.items();
        }
    }

    public class CoinsList : Scrollable
    {
        public string Name { get; } = "Coins list";

        private CMDbContext db;
        private TableLayout table;
        private const int BUTTON_WIDTH = 50;

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
                                var dialog = new OpenFileDialog();
                                if(dialog.ShowDialog(this) == DialogResult.Ok)
                                {
                                    Console.WriteLine("test");
                                }
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
