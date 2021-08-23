using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        
        public Transaction()
        {

            var t = new GuiTransaction
            {
                Id = 12,
                   SourceId = 12,
                   DestinationId = 12,
                   CryptoId = "sku",
                   CryptoQuantity = 1231,
                   State = 1
            };

            var collection = new ObservableCollection<GuiTransaction>() ;
            var grid = new GridView { DataStore = collection };
            var filtersTable = new TableLayout
            {
                Padding = new Padding(20)
            };

            var cmdAdd = new Command((sender, e) =>
                             {
                                var t = new GuiTransaction
                                    {
                                        Id = 12,
                                        SourceId = 12,
                                        DestinationId = 12,
                                        CryptoId = "sku",
                                        CryptoQuantity = 1231,
                                        State = 1
                                     };
                                 collection.Add(t);
                            });

            var buttonFilter = new Button
            {
                Text = "Apply",
                Command = cmdAdd,
                Width = BUTTON_WIDTH
            };
            var dropDown = new DropDown{ Items = {
                "All", "Running"
            }};
            var filterRow = new TableRow(
                    new TableCell(new Label() { Text = "Filter"}, true),
                    new TableCell(dropDown, true),
                    new TableCell(buttonFilter, true)
                    );
            filtersTable.Rows.Add(filterRow);

            foreach(var c in CreateColums())
                grid.Columns.Add(c);

            var all = new TableLayout();
            all.Rows.Add(filtersTable);
            all.Rows.Add(grid);
            Content = all;
        }

        private List<GridColumn> CreateColums()
        {
            var list = new List<GridColumn>();
            foreach(var prop in typeof(GuiTransaction).GetProperties())
            {
                list.Add(new GridColumn 
                        { 
                            HeaderText = prop.Name.ToString(),
                            DataCell = new TextBoxCell 
                            { 
                                Binding = Binding.Property<GuiTransaction, string>
                                (p => prop.GetValue(p).ToString()),
                            },
                        });
            }
            return list;
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
                    });

                var dyn = new DynamicLayout();
                dyn.AddColumn(new Button(){Text = "Send", Width = BUTTON_WIDTH});
                dyn.AddColumn(new Button(){Text = "Refresh", Width = BUTTON_WIDTH});
                var tenTrans = new TableRow(
                        new ListBox()
                    );
                t.Rows.Add(title);
                t.Rows.Add(cryptoList);
                t.Rows.Add(dyn);
                t.Rows.Add(tenTrans);
                return t;
            };
            Content = createLayout();
        }

        public void FillWallet(){}
    }

    public class Profile : Panel
    {
        public string Name { get; private set; } = "Profile";
        public Profile()
        {
            var table = new TableLayout();
            var leftStack = new TableCell(CreateInfoStack(), true);
            var friends = new TableCell(CreateFriendsList(), true);
            table.Rows.Add(new TableRow { Cells = { leftStack, friends } });
            Padding = new Padding(20);
            Content = table;
        }

        private StackLayout CreateInfoStack()
        {
            var stack = new StackLayout();
            stack.Items.Add(new Label { Text = "Username" });
            stack.Items.Add(new Label { Text = "Email" });
            stack.Items.Add(new Label { Text = "Other data..." });
            return stack;
        }

        private GroupBox CreateFriendsList()
        {
            var group = new GroupBox();
            var scroll = new Scrollable();
            var friends = new TableLayout();
            scroll.Padding = new Padding(10);
            var button = new TableCell(new Button { Text = "Remove" }, true);
            var cell = new TableCell(new Label { Text = "friend data" }, true);
            var cell1 = new TableCell(new Label { Text = "friend data" }, true);
            friends.Rows.Add(new TableRow { Cells = { cell, cell1, button } });
            scroll.Content = friends;
            group.Text = "Amici";
            group.Content = scroll;
            group.Padding = new Padding(20);
            return group;
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
