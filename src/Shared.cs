using System;
using System.Collections.Generic;

namespace CoinManager.Shared
{
    public class SimpleCoin
    {
        private string _symbol;
        private string _name;

        public string symbol
        {
            get { return _symbol; }
            set { _symbol = value.ToUpper(); }
        }
        public string name
        {
            get { return _name; }
            set { _name = value.FirstUpper(); }
        }
        public string id { get; set; }

        public override string ToString()
        {
            return $"ID: {id} | {_symbol} - {_name}";
        }
    }

    public class CGList
    {
        public List<CryptoCoin> CoinsList { get; set; }
    }
    
    public static class StringExtensions
    {
        public static string FirstUpper(this string input) => 
        input switch
        {
            null => throw new NullReferenceException(nameof(input)),
            _ => input[0].ToString().ToUpper() + input.Substring(1) //default
        };
    }
}

namespace CoinManager.ApiData
{
    public static class CGPaths
    {
        public static string Base =                "https://api.coingecko.com/api/v3";
        public static string Ping =                "/ping";
        public static string SimplePrice =         "/simple/price";
        public static string SimpleWithAddress =   "/simple/token/price/{id}";
        public static string CoinsList =           "/coins/list";
        public static string CoinsMarket =         "/coins/market";
        public static string CoinsDataId =         "/coins/{id}";
        public static string Trending =            "/search/trending";
    }
}

namespace CoinManager.DbData
{
    public static class DbConnection
    {
        public static string ConnectionString = "";
    }
}
