using System;
using System.Linq;
using System.Collections.Generic;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.EF;
using CoinManager.Models.CG;
using CoinManager.Models.GUI;

namespace CoinManager.GUI
{
    public class Wallet : Panel
    {

        public Wallet()
        {
            Func<DynamicLayout> createLayout = () =>
            {
                var l = new DynamicLayout();
                var t = new TableLayout();
                l.Add(t);
                return l;
            };
            Content = createLayout();
        }
    }

    public class CoinsList : Scrollable
    {
        public string Name { get; private set; } = "Coins list";

        private TableLayout table;
        private const int BUTTON_WIDTH = 50;
        private CMDbContext db;

        public CoinsList()
        {
            db = CMDbContext.Instance;
            var coins = db.Crypto.Select(c => new Coin{
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

        public void UpdateList(List<Coin> coins)
        {
            table.RemoveAll();
            coins.ForEach(c =>
            {
                var button = new Button
                {
                    Text = "...",
                    Command = new Command((sender, e) =>
                            {
                                Console.WriteLine("Pop up window to open");
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
