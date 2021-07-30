using System;
using System.Collections.Generic;
using System.Net.Http;

using Eto.Forms;
using Eto.Drawing;

using CoinManager.DB;

namespace CoinManager.GUI
{
    public class CoinsTable : TableLayout 
    {
        private int padding = 20;

        private string priceFormatting = "{0:N2}";

        private string mainCoin = "$";

        private int spacing = 5;

        private Command postCommand;

        public CoinsTable() : base()
        {
            Rows.Add(
                new TableRow(
                        new TableCell(new Label { Text = "Coin Symbol" }, true),
                        new TableCell(new Label { Text = "Coin Name" }, true),
                        new Label { Text = "Current price" }
                        )
            );
            Spacing = new Size(spacing, spacing);
            Padding = new Padding(padding);

            postCommand = new Command(async (sender, e) =>
                {
                    string url = "https://httpbin.org/post";
                    var connString = "Host=localhost;Username=rob;Password=rob;Database=test";
                    var db = new DbHelper(connString);
                    using(var client = new HttpClient())
                    {
                        var res = await client.PostAsync(url, null);
                        var strRes = await res.Content.ReadAsStringAsync();
                        await db.Connect();
                        Console.WriteLine(strRes);
                    }
                }
            );
        }

        public void AddRows(List<Tuple<string, string, float>> coinInfo)
        {
            coinInfo.ForEach(c =>
                {
                    var button = new Button
                    {
                        Text = c.Item1.ToUpper(),
                        Command = postCommand
                    };
                    var row = new TableRow(
                        new TableCell(button, true),
                        new TableCell(new Label { Text = c.Item2 }, true),
                        new TableCell(new Label { Text = (string.Format(priceFormatting, c.Item3 ) + mainCoin)}, true)
                    );
                    if(c == coinInfo[coinInfo.Count - 1])
                    {
                        row.ScaleHeight = true;
                    }
                    Rows.Add(row);
                }
           );
        }
    }
}
