using Eto.Forms;
using Eto.Drawing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.Shared;

public class MyForm : Form
{
    private TableLayout table;

    public MyForm ()
    {
        Title = "My Cross-Platform App";
        ClientSize = new Size(1000, 800);
        Menu = CreateMenuBar();
        Menu.QuitItem = new Command((sender, e) => Application.Instance.Quit())
        {
            MenuText = "Quit",
                     Shortcut = Application.Instance.CommonModifier | Keys.Q
        };
        var aboutItem = new ButtonMenuItem { Text = "About..." };
        aboutItem.Click += (sender, e) => 
        {
            var dlg = new Dialog
            {
                Content = new Label { Text = "About my app..." },
                        ClientSize = new Size(200, 200)
            };
            dlg.ShowModal(this);
        };
        Menu.AboutItem = aboutItem;
        ToolBar = new ToolBar
        {
            Items = 
            {
                new MyCommand(),
                new SeparatorToolItem(),
                new ButtonToolItem { Text = "Click Me, ToolItem" }
            }
        };
        var table = new CoinsTable();
        table.AddRows(new List<Tuple<string, string, float>>(){
                Tuple.Create("btc", "bitcoin", 1.21f),
                Tuple.Create("btc", "bitcoin", 1.21f),
                Tuple.Create("btc", "bitcoin", 1.22f)
                });
        Content = table;
    }

    public MenuBar CreateMenuBar()
    {
        return new MenuBar(){
            Items =
            {
                new ButtonMenuItem
                {
                    Text = "&File",
                    Items =
                    {
                        new MyCommand(),
                        new ButtonMenuItem { Text = "Click me, MenuItem" }
                    }
                }
            }
        };
    }

    public TableLayout CreateTable()
    {
        var l = new TableLayout(ClientSize)
        {
            Rows = { new Label(), new TextBox(){ Text = "Bella" } }
        };
        return l;
    }
}

public class MyCommand : Command
{
    public MyCommand()
    {
        MenuText = "C&lick Me, command";
        ToolBarText = "Click Me";
        ToolTip = "Click Me";
        Shortcut = Application.Instance.CommonModifier | Keys.M;
    }

    protected override void OnEnabledChanged(System.EventArgs e)
    {
        base.OnExecuted(e);
        MessageBox.Show(
                Application.Instance.MainForm,
                "You clicked me!",
                "Tutorial 2",
                MessageBoxButtons.OK);
    }
}

public class Startup
{
    public static void Main(string[] args)
    {
        //new Eto.Forms.Application().Run(new CoinsForm("Bruh", new Size(1000, 600)));
        string vs = "usd";
        var client = new CoingeckoClient();
        List<SimpleCoin> list = null;
        Task.Run(async () => 
                {
                list = await client.GetCoinsList();                
                var mktList = await client.GetMarketData(vs, "bitcoin", "ethereum");
                mktList.ForEach((c) => 
                        {
                            Console.WriteLine(c.name);
                        });
                }).Wait();


    }
}

