using System;
using System.Collections.Generic;
using System.Resources;

using Eto.Forms;
using Eto.Drawing;

namespace CoinManager.GUI
{
    public class CoinsForm : Form
    {
        public CoinsForm(string title) : base()
        {
            Title = title;
            var coinsTable = new CoinsTable();
            coinsTable.AddRows(new List<Tuple<string, string, float>> {
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    });
            var tabs = new TabDrawer();
            tabs.AddPage("coins", coinsTable);

            var loginPage = new LoginPanel();
            Content = loginPage;
        }

        public void ChangeContent(Control control)
        {
            Content = control;
            Console.WriteLine("control chaged");
        }
    }

    public class LoginPanel : Panel
    {
        private PasswordBox pwd;
        private Command onSubmit;
        private Panel littleForm; 
        private TextBox username;
        private Button loginButton;
        static private Size inputSize = new Size(400, 40);
        static private string imagePath = "./res/logo.png";

        public LoginPanel()
        {
            username = new TextBox()
            {
                Size = inputSize
            };
            pwd = new PasswordBox()
            {
                Size = inputSize
            };

            onSubmit = new Command((sender, e) => {
                    Console.WriteLine("Login action triggered");
                    });

            loginButton = new Button()
            {
                Text = "Login",
                Command = new Command((sender, e) => {
                        Console.WriteLine(username.Text);
                        Console.WriteLine(pwd.Text);
                    }),
                Size = inputSize
            };

            var layout = new StackLayout()
            {
                Padding = new Padding(200, 100, 200, 100),
                Spacing = 10,
                AlignLabels = true
            };

            var bmp = new Bitmap(imagePath);
            var logo = new ImageView()
            {
                Image = bmp,
                Size = new Size(400, 200)
            };

            layout.Items.Add(logo);
            layout.Items.Add(new Label(){ Text = "Nome utente" });
            layout.Items.Add(username);
            layout.Items.Add(new Label(){ Text = "Password" });
            layout.Items.Add(pwd);
            layout.Items.Add(loginButton);
            Content = layout;
        }
    }
    
    public class TabDrawer : TabControl
    {
        public TabDrawer()
        {
            Size = new Size(1000, 100);
        }

        public void AddPage(string pageTitle, Control item)
        {
            var page = new TabPage()
            {
                Text = pageTitle, 
                Content = item
            };
            Pages.Add(page);
        }

        public void AddPages(Dictionary<string, Control> items)
        {
            foreach(var item in items)
            {
                var page = new TabPage()
                {
                    Text = item.Key,
                    Content = item.Value
                };
                Pages.Add(page);
            }
        }
    }
}
