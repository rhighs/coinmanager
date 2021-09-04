using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

using CoinManager.GUI;
using CoinManager.API;
using CoinManager.Util;
using CoinManager.EF;

public class Startup
{
    static CMDbContext db;
    const string GUI_TITLE = "CoinManager Desktop";
    const string DUMP_PATH = "./usersdump.txt";
    const string AUTH_PATH = "./dbauth.json";

    public static void Main(string[] args)
    {
        if(!File.Exists(AUTH_PATH))
        {
            Console.WriteLine($"{AUTH_PATH} mancante!");
            return;
        }

        var dbAuth = ReadJsonData(AUTH_PATH);
        db = new CMDbContext(dbAuth.host, dbAuth.dbname, dbAuth.user, dbAuth.password);

        bool shouldPop = db.Crypto.Count() == 0
                        || db.UserStandard.Count() == 0
                        || db.Wallet.Count() == 0
                        || db.Friendship.Count() == 0;

        if(shouldPop)
        {
            Console.WriteLine("Il tuo database sembra non contenere dei dati necessari per eseguire il programma...");
            Console.WriteLine("Veranno creati automaticamente, premere un tasto qualsiasi per continuare");
            Console.ReadKey();
            PopulateDatabase();
        }

        if(!File.Exists(DUMP_PATH))
        {
            UsersDump(DUMP_PATH);
        }

        new Eto.Forms.Application().Run(new CoinsForm(GUI_TITLE));
    }

    public static void UsersDump(string path)
    {
        var sr = new StreamWriter(path);
        db.UserStandard.ToList().ForEach(u => 
        {
            sr.WriteLine($"{u.Username}:{u.Password}");;
        });
        sr.Close();
    }

    public static DbAuth ReadJsonData(string path)
    {
        var sr = new StreamReader(path);
        var stringData = sr.ReadToEnd();
        return JsonSerializer.Deserialize<DbAuth>(stringData);
    }

    public static void PopulateDatabase()
    {
        Console.WriteLine("Inizio della popolazione del database...");
        var pop = new Populator();
        Task.Run(async () => 
        {
            if(db.Crypto.Count() == 0)
                await pop.GenerateCryptos(200);
            if(db.UserStandard.Count() == 0)
                await pop.GenerateUsers(100);
        }).Wait();
        if(db.Wallet.Count() == 0)
            pop.GenerateWallets(8);
        if(db.Friendship.Count() == 0)
            pop.GenerateFriendships();
        Console.WriteLine("Popolazione completata.");
    }
}

public class DbAuth
{
    public string host { get; set; }
    public string dbname { get; set; }
    public string user { get; set; }
    public string password { get; set; }
}

