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
        enum DropItems
        {
            Completate = 1,
            In_Corso = 2
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
            var dropDown = new DropDown { Items = { "Completate", "In corso" } };

            var cmdAdd = new Command((sender, e) =>
            {
                collection.Clear();
                db = CMDbContext.Instance;
                var trans = db.Transaction.Select(t => new GuiTransaction
                {
                    Id = t.Id,
                    Emittente = t.SourceId,
                    Destinatario = t.DestinationId,
                    CryptoId = t.CryptoId,
                    Inizio = t.StartDate.Value,
                    Fine = t.FinishDate.Value,
                    Quantità = t.CryptoQuantity,
                    Miner = t.MinerId.Value,
                    Stato = t.State
                }).ToList();
                trans.ForEach(t =>
                {
                    if(dropDown.SelectedIndex + 1 == t.Stato)
                        collection.Add(t);
                });
            });

            var buttonFilter = new Button
            {
                Text = "Applica",
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
        private readonly Padding DYNAMIC_PADDING = new Padding(700, 0, 0, 0);
        private readonly Padding ROWS_PADDING = new Padding(5);

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
                
                guiWallets = CreateWallet();
                var cryptos = new TableRow { Cells = { new Scrollable { Content = layout } } };
                var dyn = new DynamicLayout
                {
                    Padding = DYNAMIC_PADDING
                };

                dyn.AddAutoSized(new Button
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

                var tenTrans = CreateTransList();
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

                Func<Control, TableLayout> autosized = (control) => 
                {
                    var cell = TableLayout.AutoSized(control);
                    cell.Padding = new Padding(5);
                    return cell;
                };

                var cryptoListHeader = new TableLayout
                {
                    Padding = new Padding(5)
                };
                var header = new TableRow(
                    autosized(new Label { Text = "Cripto Id" }),
                    autosized(new Label { Text = "Quantità" })
                );
                foreach(var cell in header.Cells)
                {
                    cell.ScaleWidth = true;
                }
                cryptoListHeader.Rows.Add(header);

                var cryptoList = new TableLayout
                {
                    Rows = { cryptoListHeader, cryptos }
                };
                foreach(var row in cryptoList.Rows)
                {
                    foreach(var cell in row.Cells)
                    {
                        cell.ScaleWidth = true;
                    }
                }
                var cryptoGroupbox = new GroupBox
                {
                    Text = "Le tue criptovalute",
                    Content = cryptoList
                };

                var gridLabel = new Label { Text = "Ultime transazioni" };
                grid.Height = GRID_HEIGHT;
                t.Rows.Add(new TableRow { Cells = { cryptoGroupbox },               ScaleHeight = true  });
                t.Rows.Add(new TableRow { Cells = { new TableCell(dyn, false) },    ScaleHeight = false });
                t.Rows.Add(new TableRow { Cells = { gridLabel  },                   ScaleHeight = true  });
                t.Rows.Add(new TableRow { Cells = { grid       },                   ScaleHeight = true  });
                return t;
            };
            Content = createLayout();
        }

        private List<GuiWallet> CreateWallet()
        {
            var wallets = db.Wallet.ToList().Where(w => w.UserId == user.Id).ToList();
            var layout = new TableLayout
            {
                Padding = ROWS_PADDING,
            };

            int counter = 0;
            wallets.ForEach(w => 
            {
                bool dimColor = counter % 2 == 0;
                var magicWidth = (this.Width/2) - 30;
                var cryptoRow = new TableRow(
                    CreateAutosized(new Label { Text = w.CryptoId,              Width = magicWidth }, dimColor),
                    CreateAutosized(new Label { Text = w.Quantity.ToString(),   Width = magicWidth }, dimColor)
                    );
                foreach(var cell in cryptoRow.Cells)
                {
                    cell.ScaleWidth = true;
                }
                layout.Rows.Add(cryptoRow);
                counter++;
            });

            var guiWallets = wallets.Select(w => new GuiWallet
            {
                CryptoId = w.CryptoId,
                Quantity = w.Quantity
            }).ToList();

            this.layout = layout;
            return guiWallets;
        }

        private List<GuiTransaction> CreateTransList()
        {
            return db.Transaction.Select(c => new GuiTransaction
            {
                Id = c.Id,
                Emittente = c.SourceId,
                Destinatario = c.DestinationId,
                CryptoId = c.CryptoId,
                Inizio = c.StartDate.Value,
                Fine = c.FinishDate.Value,
                Quantità = c.CryptoQuantity,
                Stato = c.State
            }).OrderByDescending(x => x.Stato).ToList();
        }

        private TableLayout CreateAutosized(Control control, bool dimColor)
        {
            var cell = TableLayout.AutoSized(control);
            cell.Padding = ROWS_PADDING;
            cell.BackgroundColor = dimColor
                ? new Color(0, 0, 0, 0.1f)
                : new Color(0, 0, 0, 0);
            return cell;
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
        public UserStandard user;
        public CMDbContext db;
        public string Name { get; } = "Profilo";
        private readonly Size SPACING_SIZE = new Size(10, 10);
        private readonly int SPACING_SIZE_STACK = 20;
        static private Padding PANEL_PADDING = new Padding(10);
        static private Padding CONTENTS_PADDING = new Padding(10);
        static private Size IMAGE_SIZE = new Size(300, 300);
        static private string IMAGE_PATH = "./res/profile.png";

        protected override void OnShown(EventArgs e)
        {
            db = CMDbContext.Instance;
            user = CMDbContext.LoggedUser;
            bool isMiner = false;
            var mainTable = new TableLayout();
            var lists = new TableLayout();
            
            lists.Rows.Add(new TableRow { Cells = { CreateFriendsList() } });
            isMiner = db.UserMiner.FirstOrDefault(t => t.Id == user.Id) != null;

            if(isMiner)
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
            var stack = new StackLayout
            {
                Spacing = SPACING_SIZE_STACK
            };
            var image = new Bitmap(IMAGE_PATH);

            var requestButton = new Button
            {
                Text = "Invia richiesta di amicizia",
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
                Text = "Richieste in arrivo",
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
            stack.Items.Add(new Label { Text = "Id utente: " + user.Id });
            stack.Items.Add(new Label { Text = "Nome utente: " + user.Username, Size = new Size(50,50)});
            stack.Items.Add(requestButton);
            stack.Items.Add(showReqButton);
            return stack;
        }

        private GroupBox CreateFriendsList()
        {
            var group = new GroupBox();
            var friendsTable = new TableLayout
            {
                Spacing = SPACING_SIZE, 
                Padding = CONTENTS_PADDING
            };

            var friend = db.Friendship.Select(f => new GuiFriendship
                {
                    UserId = f.UserId,
                    FriendId = f.FriendId
                }).ToList();

            var headId = new TableCell(new Label { Text = "Id utente" }, true);
            var nameId = new TableCell(new Label { Text = "Nome utente" }, true);
            var buttonHead = new TableCell(new Label { Text = "" }, true);

            friendsTable.Rows.Add(new TableRow{ Cells = { headId, nameId, buttonHead }, ScaleHeight = false });
            friend.ForEach(f => 
                {
                    if(f.UserId == user.Id)
                    {
                        var id = new TableCell(new Label { Text = f.FriendId.ToString() }, true);
                        var username = new TableCell(new Label { Text = GetFriendName(f.FriendId) }, true);
                        var button = TableLayout.AutoSized(new Button 
                            { 
                                Text = "Rimuovi", 
                                Tag = f.FriendId,
                                Command = new Command((sender, e) =>
                                {
                                    var deleted = new EF.Friendship
                                    {
                                        UserId = user.Id,
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
            return "Nessun amico";
        }

        public GroupBox CreateMinerSection()
        {
            var transactionsTable = new TableLayout
            {
                Spacing = SPACING_SIZE,
                Padding = CONTENTS_PADDING 
            };

            var runningTrans = db.RunningTransaction.ToList();

            var headerId        = new TableCell { Control = new Label { Text = "Id Transazione", TextAlignment = TextAlignment.Left},  ScaleWidth = true };
            var headerTime      = new TableCell { Control = new Label { Text = "Tempo Totale", TextAlignment = TextAlignment.Center},  ScaleWidth = true };
            var headerButton    = new TableCell { Control = new Label { Text = "", TextAlignment = TextAlignment.Center},              ScaleWidth = true };

            transactionsTable.Rows.Add(new TableRow{ Cells = { headerId, headerTime, headerButton }, ScaleHeight = false});

            foreach (var trans in runningTrans)
            {

                var confirmButton = new Button
                {
                    Text = "Conferma",
                    Command = new Command((sender, e) =>
                    {
                        CMDbContext.TransactionsTasks.EndTimer(trans.TransactionId, user.Id);
                        var dialog = new Dialog
                            {
                                Padding = new Padding(20),
                                Content = new Label { Text = "Richiesta Inviata" }
                            };
                            dialog.ShowModal();
                    })
                };

                var idLabel         = new TableCell { Control = new Label { Text = trans.TransactionId.ToString(), TextAlignment = TextAlignment.Left }, ScaleWidth = true};
                var totalTimeLabel  = new TableCell { Control = new Label { Text = trans.TotalTime.ToString(), TextAlignment = TextAlignment.Center },   ScaleWidth = true};
                var button          = TableLayout.AutoSized(confirmButton);

                transactionsTable.Rows.Add(new TableRow { Cells = { idLabel, totalTimeLabel, button}, ScaleHeight = false});
            }

            return CreateScrollableGroup("Transazioni confermabili", transactionsTable, 200);
        }

        private GroupBox CreateScrollableGroup(string Title, Container container, int height = 0)
        {
            var group = new GroupBox();
            var scroll = new Scrollable();
            scroll.Content = container;
            scroll.Padding = CONTENTS_PADDING;
            if(height > 0)
                scroll.Height = height;
            group.Text = Title;
            group.Content = scroll;
            group.Padding = CONTENTS_PADDING;
            return group;
        }
    }

    public class CoinsList : Panel
    {
        public string Name { get; } = "Lista di cripto";

        private CMDbContext db;
        private TableLayout table;
        private const int BUTTON_WIDTH = 50;
        private readonly Size DIALOG_SIZE = new Size(600, 300);
        private readonly Padding ROWS_PADDING = new Padding(5);

        public CoinsList()
        {
            db = CMDbContext.Instance;
            var coins = db.Crypto.Select(c => new GuiCrypto
            {
                Name = c.Name,
                Symbol = c.Symbol,
                Id = c.Id,
                Price = c.CurrentPrice,
                Rank = c.MarketCapRank
            });

            table = new TableLayout();
            var tablePadding = new Padding(0, 10, 10, 0);
            table.Padding = tablePadding;
            var coinsList = coins.ToList();
            coinsList.Sort();
            UpdateList(coinsList);
            var scroll = new Scrollable { Content = table };


            var headerTable = new TableLayout();
            headerTable.Padding = tablePadding;

            var header = new TableRow(
                CreateAutosized(new Label { Text = "Simbolo" }),
                CreateAutosized(new Label { Text = "Nome completo" }),
                CreateAutosized(new Label { Text = "Prezzo ($)" })
            );

            foreach(var cell in header.Cells)
            {
                cell.ScaleWidth = true;
            }

            header.Cells.Add(CreateAutosized(new Label { Text = "Più informazioni" }));

            headerTable.Rows.Add(header);
            var dyn = new TableLayout
            {
                Rows = { new TableRow(headerTable), new TableRow(scroll) }
            };

            Content = dyn;
        }

        private TableLayout CreateAutosized(Control control, bool dimColor = false)
        {
            var cell = TableLayout.AutoSized(control);
            cell.Padding = ROWS_PADDING;
            cell.BackgroundColor = dimColor
                ? new Color(0, 0, 0, 0.1f)
                : new Color(0, 0, 0, 0);
            return cell;
        }

        public void UpdateList(List<GuiCrypto> coins)
        {
            table.RemoveAll();

            int counter = 0;
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

                bool dimColor = counter % 2 == 0;
                var row = new TableRow(
                        CreateAutosized(new Label { Text = c.Symbol }, dimColor),
                        CreateAutosized(new Label { Text = c.Name }, dimColor),
                        CreateAutosized(new Label { Text = c.Price.ToString() }, dimColor)
                        );
                foreach(var cell in row.Cells)
                {
                    cell.ScaleWidth = true;
                }

                row.Cells.Add(CreateAutosized(button, dimColor));
                table.Rows.Add(row);
                counter++;
            });
        }
    }

}
