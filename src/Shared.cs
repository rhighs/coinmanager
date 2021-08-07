using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public class Localization
    {
        public string en { get; set; }
        public string de { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string it { get; set; }
        public string pl { get; set; }
        public string ro { get; set; }
        public string hu { get; set; }
        public string nl { get; set; }
        public string pt { get; set; }
        public string sv { get; set; }
        public string vi { get; set; }
        public string tr { get; set; }
        public string ru { get; set; }
        public string ja { get; set; }
        public string zh { get; set; }

        [JsonPropertyName("zh-tw")]
        public string ZhTw { get; set; }
        public string ko { get; set; }
        public string ar { get; set; }
        public string th { get; set; }
        public string id { get; set; }
    }

    public class Image
    {
        public string thumb { get; set; }
        public string small { get; set; }
    }

    public class CoinInfo
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public Localization localization { get; set; }
        public Image image { get; set; }
    }

    public class CoinMarket
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public double? current_price { get; set; }
        public double? market_cap { get; set; }
        public double? market_cap_rank { get; set; }
        public double? fully_diluted_valuation { get; set; }
        public double? total_volume { get; set; }
        public double? high_24h { get; set; }
        public double? low_24h { get; set; }
        public double? price_change_24h { get; set; }
        public double? price_change_percentage_24h { get; set; }
        public double? market_cap_change_24h { get; set; }
        public double? market_cap_change_percentage_24h { get; set; }
        public double? circulating_supply { get; set; }
        public double? total_supply { get; set; }
        public double? max_supply { get; set; }
        public double? ath { get; set; }
        public double? ath_change_percentage { get; set; }
        public DateTime? ath_date { get; set; }
        public double? atl { get; set; }
        public double? atl_change_percentage { get; set; }
        public DateTime? atl_date { get; set; }
        public object roi { get; set; }
        public DateTime? last_updated { get; set; }

        public override string ToString()
        {
            return "\"" + id + "\", \"" + name + "\", \"" + symbol + "\", " + current_price.ToString()
                + ", \"" + image + "\", " + market_cap.ToString() + ", " + market_cap_rank.ToString() 
                + ", " + circulating_supply.ToString() + ", " + total_volume.ToString();
        }
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
        public static string Ping =                Base + "/ping";
        public static string SimplePrice =         Base + "/simple/price";
        public static string SimpleWithAddress =   Base + "/simple/token/price/{0}";
        public static string CoinsList =           Base + "/coins/list";
        public static string CoinsMarket =         Base + "/coins/markets";
        public static string CoinsDataId =         Base + "/coins/{0}";
        public static string CoinHistoryId =       Base + "/coins/{0}/history";
        public static string Trending =            Base + "/search/trending";
    }
}
