using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public static class RDPaths
    {
        public static string Base =                "https://random-data-api.com/api";
        public static string RandomUser =          Base + "/users/random_user";
    }
}

//this is just for the development phase, going to remove it on release
namespace CoinManager.FS
{
    static public class CMImages
    {
        static private string robPath = "/home/rob/repos/coinmanager/src/res/logo.png";
        static private string jsonPath ="/home/json/Scrivania/coinmanager/src/res/logo.png";
        static public string LogoPath
        {
            get 
            {
                return System.Environment.UserName == "json" 
                    ? jsonPath
                    : robPath;
            }
        }
    }
}


namespace CoinManager.Models
{
    namespace RD
    {
        public class Employment
        {
            public string title { get; set; }
            public string key_skill { get; set; }
        }

        public class Coordinates
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Address
        {
            public string city { get; set; }
            public string street_name { get; set; }
            public string street_address { get; set; }
            public string zip_code { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public Coordinates coordinates { get; set; }
        }

        public class CreditCard
        {
            public string cc_number { get; set; }
        }

        public class Subscription
        {
            public string plan { get; set; }
            public string status { get; set; }
            public string payment_method { get; set; }
            public string term { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string uid { get; set; }
            public string password { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string avatar { get; set; }
            public string gender { get; set; }
            public string phone_number { get; set; }
            public string social_insurance_number { get; set; }
            public string date_of_birth { get; set; }
            public Employment employment { get; set; }
            public Address address { get; set; }
            public CreditCard credit_card { get; set; }
            public Subscription subscription { get; set; }
        }

    }

    namespace GUI
    {
        public class GuiCrypto : IEquatable<GuiCrypto>, IComparable<GuiCrypto>
        {
            private string symbol;
            private string name;

            public string Id { get; set; }
            public string Name {
                get { return name; }
                set { name = value.FirstUpper(); }
            }
            public double Price { get; set; }
            public int Rank { get; set; }
            public string Symbol {
                get { return symbol; }
                set { symbol = value.ToUpper(); }
            }

            public bool Equals(GuiCrypto? coin)
            {
                if(coin == null) return false;
                return coin.Rank == this.Rank;
            }

            public int CompareTo(GuiCrypto compareCoin)
            {
                if(compareCoin == null) return 1;
                else return this.Rank.CompareTo(compareCoin.Rank);
            }

        }

        public class GuiTransaction
        {
            public int Id { get; set; }
            public int SourceId { get; set; }
            public int DestinationId { get; set; }
            public string CryptoId { get; set; }
            public int State { get; set; }
            public DateTime StartDate{ get; set; }
            public DateTime FinishDate{ get; set; }
            public double CryptoQuantity{ get; set; }
            
        }

        public class GuiWallet
        {
            public int UserId { get; set; }
            public string CryptoId { get; set; }
            public double Quantity { get; set; }
            
        }

        public class GuiFriendship
        {
            public int UserId { get; set; }
            public int FriendId { get; set; }
        }

        public class GuiUser
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class GuiFriendRequest
        {
            public int SenderId{ get; set; }
            public int ReceiverId{ get; set; }
        }
    }

    namespace CG
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
            public int? market_cap_rank { get; set; }
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
