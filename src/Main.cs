using System;
using System.Threading.Tasks;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.Util;
using CoinManager.EF;

public class Startup
{
    public static void Main(string[] args)
    {
        const string TITLE = "CoinManager Desktop";
        var db = new CMDbContext("localhost", "coinmanager", "rob", "rob");
        new Eto.Forms.Application().Run(new CoinsForm(TITLE));
    }
}

