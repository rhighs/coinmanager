using System;
using System.Collections.Generic;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.DbData;
using CoinManager.Shared;

namespace CoinManager.GUI
{
    public class Wallet : Panel
    {
        public User User { get; set; }

        public Wallet(User user)
        {
            User = user;
            Func<DynamicLayout> createLayout = () =>
            {
                var l = new DynamicLayout();
                l.Add(new Label(){ Text = User.Username });
                var t = new TableLayout();
                t.Rows.Add(new TableRow(new Label() { Text = "una cripto a caso" }));
                t.Rows.Add(new TableRow(new Label() { Text = "una cripto a caso" }));
                t.Rows.Add(new TableRow(new Label() { Text = "una cripto a caso" }));
                t.Rows.Add(new TableRow(new Label() { Text = "una cripto a caso" }));
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

        public CoinsList()
        {
            table = new TableLayout();
            table.Spacing = new Size(10, 10);
            table.Padding = new Padding(10, 10, 10, 10);

            Action tempFill = () => {
                var tempList = new List<SimpleCoin>();
                for(int i = 0; i < 100; i++)
                {
                    var simpleCoin = new SimpleCoin
                    {
                        name = "butcoin",
                        symbol = "btc",
                        id = "bitcoin"
                    };
                    tempList.Add(simpleCoin);
                }
                UpdateList(tempList);
            };

            tempFill();
            Content = table;
        }

        public void UpdateList(List<SimpleCoin> coins)
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
                        new TableCell(new Label() { Text = c.symbol }, true),
                        new TableCell(new Label() { Text = c.name }, true),
                        TableLayout.AutoSized(button) 
                        );
                table.Rows.Add(row);
            });
        }
    }
}
