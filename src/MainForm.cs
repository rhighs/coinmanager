using System;
using System.Linq;
using System.Collections.Generic;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.EF;

namespace CoinManager.GUI
{
    public class CoinsForm : Form
    {
        public CoinsForm(string title) : base()
        {
            var wallet = new Wallet();
            var transaction = new Transaction();
            var tabs = new TabDrawer();
            var listTest = new TabDrawer();
            var coinsList = new CoinsList();
            Resizable = false;

            tabs.AddPage(coinsList.Name, coinsList);
            tabs.AddPage(wallet.Name, wallet);
            tabs.AddPage(transaction.Name, transaction);
            
            var loginPage = new LoginPanel();
            loginPage.CreateButton(new Command((sender, e) => {
                        Func<bool> verifyLogin = () => {
                            var dbc = CMDbContext.Instance;
                            var user = loginPage.Username.Text;
                            var password = loginPage.Password.Text;
                            return true;
                            return dbc
                                .UserStandard
                                .FirstOrDefault(u => u.Username == user && u.Password == password) != null;
                        };
                        if(verifyLogin())
                        {
                            Resizable = true;
                            Content = tabs;
                        }
                        loginPage.ErroreMessage.Text = "Hai sbagliato username o password, riprova.";
                        }));
            Title = title;
            Content = loginPage;
        }
    }

    public class LoginPanel : Panel
    {
        public PasswordBox Password { get; set; }
        public TextBox Username { get; set; }
        public Command OnSubmit { get; set; }
        public Label ErroreMessage { get; set; } =
            new Label() { Text = "" };

        private Button loginButton;
        private StackLayout layout;

        static private Size inputSize = new Size(400, 40);
        static private string imagePath = "./res/logo.png";
        //bro scusa ma a me va solo con il percorso assoluto
        //static private string imagePath ="/home/json/Scrivania/coinmanager/src/res/logo.png";
        static private Label usernameLabel = new Label(){ Text = "Nome utente" };
        static private Label passwordLabel = new Label(){ Text = "Password" };

        public LoginPanel()
        {
            Username = new TextBox()
            {
                Size = inputSize
            };
            Password = new PasswordBox()
            {
                Size = inputSize
            };
            var bmp = new Bitmap(imagePath);
            var logo = new ImageView()
            {
                Image = bmp,
                Size = new Size(400, 200)
            };
            Func<StackLayout> createLayout = () => 
            {
                var l = new StackLayout()
                {
                    Padding = new Padding(200, 50, 200, 100),
                    Spacing = 10,
                    AlignLabels = true
                };
                l.Items.Add(logo);
                l.Items.Add(usernameLabel);
                l.Items.Add(Username);
                l.Items.Add(passwordLabel);
                l.Items.Add(Password);
                l.Items.Add(ErroreMessage);
                return l;
            };
            Content = createLayout();
        }

        public void CreateButton(Command command)
        {
            OnSubmit = command;
            var layout = (Content as StackLayout);
            var last = layout.Items[layout.Items.Count - 1];
            if(last.Control is Button)
            {
                layout.Items.Remove(last);
            }
            loginButton = new Button()
            {
                Text = "Login",
                Command = OnSubmit,
                Size = inputSize
            };
            layout.Items.Add(loginButton);
        }
    }

    public class TabDrawer : TabControl
    {
        public TabDrawer()
        {
            Size = new Size(800, 600);
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
