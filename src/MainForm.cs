using System;
using System.Collections.Generic;

using Eto.Forms;
using Eto.Drawing;

namespace CoinManager.GUI
{
    public class CoinsForm : Form
    {
        public CoinsForm(string title, Size windowSize) : base()
        {
            Title = title;
            Size = windowSize;
            var coinsTable = new CoinsTable();
            coinsTable.AddRows(new List<Tuple<string, string, float>> {
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f),
                    Tuple.Create("btc".ToUpper(), "bitcoin", 1.2f)
                    });
            var tabs = new TabDrawer();
            tabs.AddPage("coins", coinsTable);
            Content = tabs;
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
