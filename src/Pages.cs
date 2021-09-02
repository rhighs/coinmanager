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
        //Da sistemare, dubbi sull'utilità, forse più semplice fare gli stati 0,1,2
        //No magic numbers
        enum DropItems
        {
            Completed = 1,
            Running = 2
        }

        private CMDbContext db;
        public string Name { get; } = "Transazioni";
        
        private const int BUTTON_WIDTH = 50;
        
        public Transaction()
        {
            var collection = new ObservableCollection<GuiTransaction>();
            var grid = new GridView { DataStore = collection };
            var filtersTable = new TableLayout
            {
                Padding = new Padding(20)
            };
            var dropDown = new DropDown{ Items = {
                DropItems.Completed.ToString(), DropItems.Running.ToString()
            }};

            var cmdAdd = new Command((sender, e) =>
            {
                collection.Clear();
                db = CMDbContext.Instance;
                var trans = db.Transaction.Select(t => new GuiTransaction
                {
                    Id = t.Id,
                    SourceId = t.SourceId,
                    DestinationId = t.DestinationId,
                    CryptoId = t.CryptoId,
                    StartDate = t.StartDate.Value,
                    FinishDate = t.FinishDate.Value,
                    CryptoQuantity = t.CryptoQuantity,
                    State = t.State
                }).ToList();
                trans.ForEach(t =>
                {
                    if(dropDown.SelectedIndex + 1 == t.State)
                        collection.Add(t);
                });
            });

            var buttonFilter = new Button
            {
                Text = "Apply",
                Command = cmdAdd,
                Width = BUTTON_WIDTH
            };
            
            var filterRow = new TableRow(
                    new TableCell(new Label() { Text = "Filtri"}, true),
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
        public string Name { get; } = "Portafoglio";

        private const int BUTTON_WIDTH = 50;
        private const int GRID_HEIGHT = 300;
        private readonly Size DIALOG_SIZE = new Size(600, 300);

        private CMDbContext db;
        private UserStandard user;

        private List<GuiWallet> guiWallets;

        private TableLayout layout = new TableLayout
        {
            Spacing = new Size(5, 5),
            Padding = new Padding(10, 10, 10, 10), 
        };

        private TableLayout transLayout = new TableLayout
        {
            Spacing = new Size(5, 5),
            Padding = new Padding(10, 10, 10, 10), 
        };

        protected override void OnShown(EventArgs e)
        {
            db = CMDbContext.Instance;
            layout.Size = new Size(Width, Height / 2);
            var collection = new ObservableCollection<GuiTransaction>();
            var grid = new GridView { DataStore = collection };
            user = CMDbContext.LoggedUser;

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
                        TableLayout.AutoSized(new Label { Text = "Le tue criptovalute" })
                    )
                );
                
                guiWallets = CreateWallet();
                var cryptoList = new TableRow { Cells = { new Scrollable { Content = layout } } };
                var dyn = new DynamicLayout();

                dyn.AddColumn(new Button
                {
                        Text = "Invia", 
                        Command = new Command((sender, e) =>
                        {
                            var content = new SendDialog(guiWallets)
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
                });

                var tenTrans = createTransList();
                tenTrans.ForEach(x => 
                {
                    var cont = 10;
                    if(cont !=0)
                        collection.Add(x);
                });

                foreach(var c in CreateColums())
                {
                    grid.Columns.Add(c);
                }

                var gridLabel = new Label { Text = "Ultime 10 transazioni" };
                grid.Height = GRID_HEIGHT;
                t.Rows.Add(new TableRow { Cells = { title      }, ScaleHeight = true });
                t.Rows.Add(new TableRow { Cells = { cryptoList }, ScaleHeight = true });
                t.Rows.Add(new TableRow { Cells = { dyn        }, ScaleHeight = true });
                t.Rows.Add(new TableRow { Cells = { gridLabel  }, ScaleHeight = true });
                t.Rows.Add(new TableRow { Cells = { grid       }, ScaleHeight = true });
                return t;
            };
            Content = createLayout();
        }

        private List<GuiWallet> CreateWallet()
        {
            var wallets = db.Wallet.ToList().Where(w => w.UserId == user.Id).ToList();
            var layout = new TableLayout();
            wallets.ForEach(w => 
            {
                var cryptoRow = new TableRow(
                    new TableCell(new Label() { Text = w.CryptoId}, true),
                    new TableCell(new Label() { Text = w.Quantity.ToString()}, true)
                );
                layout.Rows.Add(cryptoRow);
            });

            var guiWallets = wallets.Select(w => new GuiWallet
            {
                CryptoId = w.CryptoId,
                Quantity = w.Quantity
            }).ToList();

            this.layout = layout;
            return guiWallets;
        }

        private List<GuiTransaction> createTransList()
        {
            return db.Transaction.Select(c => new GuiTransaction{
                            Id = c.Id,
                            SourceId = c.SourceId,
                            DestinationId = c.DestinationId,
                            CryptoId = c.CryptoId,
                            StartDate = c.StartDate.Value,
                            FinishDate = c.FinishDate.Value,
                            CryptoQuantity = c.CryptoQuantity,
                            State = c.State
                    }).OrderByDescending(x => x.StartDate).ToList();
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

    public class Profile : Panel
    {
        private const int BUTTON_WIDTH = 150;
        private readonly Size DIALOG_SIZE = new Size(600, 300);
        public UserStandard logged;
        public CMDbContext db;
        public string Name { get; } = "Profile";

        static private Padding PANEL_PADDING = new Padding(10);
        static private Padding CONTENTS_PADDING = new Padding(10);
        static private Size IMAGE_SIZE = new Size(200, 200);
        static private string IMAGE_PATH = "./res/profile.png";

        protected override void OnShown(EventArgs e)
        {
            db = CMDbContext.Instance;
            logged = CMDbContext.LoggedUser;
            bool isMiner = false;
            var mainTable = new TableLayout();
            var lists = new TableLayout();
            

            lists.Rows.Add(new TableRow { Cells = { CreateFriendsList() }, ScaleHeight = true });
            if(!isMiner)
            {
                lists.Rows.Add(new TableRow { Cells = { CreateMinerSection() }, ScaleHeight = true });
            }

            var infoCell = new TableCell(CreateInfoStack(), true);
            var listsCell = new TableCell(lists, true);
            mainTable.Rows.Add(new TableRow { Cells = { infoCell, listsCell }, ScaleHeight = true });
            Padding = PANEL_PADDING;
            Content = mainTable;
        }

        private StackLayout CreateInfoStack()
        {
            
            var stack = new StackLayout();
            var image = new Bitmap(IMAGE_PATH);

            var requestButton = new Button
            {
                Text = "Send friend request",
                Command = new Command((sender, e) =>
                        {
                            var content = new FriendDialog
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

            var showReqButton = new Button
            {
                Text = "Not Accepted Request",
                Command = new Command((sender, e) =>
                        {
                            var content = new RequestDialog
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

            stack.Items.Add(new ImageView{Image = image, Size = IMAGE_SIZE});
            stack.Items.Add(new Label { Text = "Id: " + logged.Id });
            stack.Items.Add(new Label { Text = "Username: " + logged.Username, Size = new Size(50,50)});
            stack.Items.Add(requestButton);
            stack.Items.Add(showReqButton);
            return stack;
        }

        private GroupBox CreateFriendsList()
        {
            var group = new GroupBox();
            var friendsTable = new TableLayout();
            var friend = db.Friendship.Select(f => new GuiFriendship
                {
                    UserId = f.UserId,
                    FriendId = f.FriendId
                }).ToList();
            friend.ForEach(f => 
                {
                    if(f.UserId == logged.Id)
                    {
                        var id = new TableCell(new Label { Text = f.FriendId.ToString() }, true);
                        var username = new TableCell(new Label { Text = GetFriendName(f.FriendId) }, true);
                        var button = TableLayout.AutoSized(new Button 
                            { 
                                Text = "Remove", 
                                Tag = f.FriendId,
                                Command = new Command((sender, e) =>
                                {
                                    var deleted = new EF.Friendship
                                    {
                                        UserId = logged.Id,
                                        FriendId = f.FriendId
                                    };
                                    db.Friendship.Remove(deleted);
                                    db.SaveChanges();
                                })
                            });
                        
                        friendsTable.Rows.Add(new TableRow { Cells = { id, username, button }, ScaleHeight = false });
                    }
                });
            return CreateScrollableGroup("Amici", friendsTable);
        }

        public string GetFriendName(int FriendId)
        {
            var list = db.UserStandard.Select(u => new GuiUser
                {
                    Id = u.Id,
                    Username = u.Username,
                    Password = u.Password
                }).ToList();
            
            foreach (var l in list)
            {
                if(l.Id == FriendId)
                    return l.Username;
            };
            return "no friends";
        }

        public GroupBox CreateMinerSection()
        {
            var scroll = new Scrollable();
            var transactionsTable = new TableLayout();
            var runningTrans = db.RunningTransaction.ToList();

            foreach (var trans in runningTrans)
            {
                var confirmButton = new Button
                {
                    Text = "Conferma",
                    Command = new Command((sender, e) =>
                    {
                        CMDbContext.TransactionsTasks.EndTimer(trans.TransactionId, logged.Id);
                    })
                };
                var idLabel = TableLayout.AutoSized(new Label { Text = trans.TransactionId.ToString() });
                var button = TableLayout.AutoSized(confirmButton);
                transactionsTable.Rows.Add(new TableRow(idLabel, button));
            }

            scroll.Content = transactionsTable;
            return CreateScrollableGroup("Transazioni confermabili", scroll);
        }

        private GroupBox CreateScrollableGroup(string Title, Container container)
        {
            var group = new GroupBox();
            var scroll = new Scrollable();
            scroll.Content = container;
            scroll.Padding = CONTENTS_PADDING;
            group.Text = Title;
            group.Content = scroll;
            group.Padding = CONTENTS_PADDING;
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
