using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

/*
 * This namespace is just huge, the reason behind lies in the response of the coingecko api,
 * when asking for more information about a cryptocurrency.
 * Since in c# there is no easy way to parse a json into a dictionary (as say, in python), you need
 * to declare a c# class which, via its properties, creates the best suitable object for a successful
 * json parsing..
 *
 * ps. i did not create it on my own, i used an online generator: https://json2csharp.com
 */

namespace CoinManager.FullCoin
{
    public class Platforms
    {
        [JsonPropertyName("binance-smart-chain")]
        public string BinanceSmartChain { get; set; }

        [JsonPropertyName("huobi-token")]
        public string HuobiToken { get; set; }
        public string tomochain { get; set; }
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

    public class Description
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

    public class ReposUrl
    {
        public List<string> github { get; set; }
        public List<object> bitbucket { get; set; }
    }

    public class Links
    {
        public List<string> homepage { get; set; }
        public List<string> blockchain_site { get; set; }
        public List<string> official_forum_url { get; set; }
        public List<string> chat_url { get; set; }
        public List<string> announcement_url { get; set; }
        public string twitter_screen_name { get; set; }
        public string facebook_username { get; set; }
        public int bitcointalk_thread_identifier { get; set; }
        public string telegram_channel_identifier { get; set; }
        public string subreddit_url { get; set; }
        public ReposUrl repos_url { get; set; }
    }

    public class Image
    {
        public string thumb { get; set; }
        public string small { get; set; }
        public string large { get; set; }
    }

    public class IcoData
    {
        public DateTime ico_start_date { get; set; }
        public DateTime ico_end_date { get; set; }
        public string short_desc { get; set; }
        public object description { get; set; }
        public Links links { get; set; }
        public string softcap_currency { get; set; }
        public string hardcap_currency { get; set; }
        public string total_raised_currency { get; set; }
        public object softcap_amount { get; set; }
        public object hardcap_amount { get; set; }
        public object total_raised { get; set; }
        public string quote_pre_sale_currency { get; set; }
        public object base_pre_sale_amount { get; set; }
        public object quote_pre_sale_amount { get; set; }
        public string quote_public_sale_currency { get; set; }
        public double base_public_sale_amount { get; set; }
        public double quote_public_sale_amount { get; set; }
        public string accepting_currencies { get; set; }
        public string country_origin { get; set; }
        public object pre_sale_start_date { get; set; }
        public object pre_sale_end_date { get; set; }
        public string whitelist_url { get; set; }
        public object whitelist_start_date { get; set; }
        public object whitelist_end_date { get; set; }
        public string bounty_detail_url { get; set; }
        public object amount_for_sale { get; set; }
        public bool kyc_required { get; set; }
        public object whitelist_available { get; set; }
        public object pre_sale_available { get; set; }
        public bool pre_sale_ended { get; set; }
    }

    public class CurrentPrice
    {
        public double aed { get; set; }
        public int ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public int bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public int clp { get; set; }
        public double cny { get; set; }
        public int czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public int huf { get; set; }
        public int idr { get; set; }
        public double ils { get; set; }
        public int inr { get; set; }
        public int jpy { get; set; }
        public int krw { get; set; }
        public double kwd { get; set; }
        public int lkr { get; set; }
        public double ltc { get; set; }
        public int mmk { get; set; }
        public int mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public int nok { get; set; }
        public double nzd { get; set; }
        public int php { get; set; }
        public int pkr { get; set; }
        public double pln { get; set; }
        public int rub { get; set; }
        public double sar { get; set; }
        public int sek { get; set; }
        public double sgd { get; set; }
        public int thb { get; set; }
        public int @try { get; set; }
        public int twd { get; set; }
        public int uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public int vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public int xlm { get; set; }
        public int xrp { get; set; }
        public double yfi { get; set; }
        public int zar { get; set; }
        public int bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class Roi
    {
        public double times { get; set; }
        public string currency { get; set; }
        public double percentage { get; set; }
    }

    public class Ath
    {
        public double aed { get; set; }
        public int ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public int bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public int bnb { get; set; }
        public int brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public int clp { get; set; }
        public int cny { get; set; }
        public int czk { get; set; }
        public int dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public int hkd { get; set; }
        public int huf { get; set; }
        public int idr { get; set; }
        public double ils { get; set; }
        public int inr { get; set; }
        public int jpy { get; set; }
        public int krw { get; set; }
        public double kwd { get; set; }
        public int lkr { get; set; }
        public double ltc { get; set; }
        public int mmk { get; set; }
        public int mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public int nok { get; set; }
        public double nzd { get; set; }
        public int php { get; set; }
        public int pkr { get; set; }
        public double pln { get; set; }
        public int rub { get; set; }
        public double sar { get; set; }
        public int sek { get; set; }
        public double sgd { get; set; }
        public int thb { get; set; }
        public int @try { get; set; }
        public int twd { get; set; }
        public int uah { get; set; }
        public double usd { get; set; }
        public int vef { get; set; }
        public int vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public int xlm { get; set; }
        public int xrp { get; set; }
        public double yfi { get; set; }
        public int zar { get; set; }
        public int bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class AthChangePercentage
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class AthDate
    {
        public DateTime aed { get; set; }
        public DateTime ars { get; set; }
        public DateTime aud { get; set; }
        public DateTime bch { get; set; }
        public DateTime bdt { get; set; }
        public DateTime bhd { get; set; }
        public DateTime bmd { get; set; }
        public DateTime bnb { get; set; }
        public DateTime brl { get; set; }
        public DateTime btc { get; set; }
        public DateTime cad { get; set; }
        public DateTime chf { get; set; }
        public DateTime clp { get; set; }
        public DateTime cny { get; set; }
        public DateTime czk { get; set; }
        public DateTime dkk { get; set; }
        public DateTime dot { get; set; }
        public DateTime eos { get; set; }
        public DateTime eth { get; set; }
        public DateTime eur { get; set; }
        public DateTime gbp { get; set; }
        public DateTime hkd { get; set; }
        public DateTime huf { get; set; }
        public DateTime idr { get; set; }
        public DateTime ils { get; set; }
        public DateTime inr { get; set; }
        public DateTime jpy { get; set; }
        public DateTime krw { get; set; }
        public DateTime kwd { get; set; }
        public DateTime lkr { get; set; }
        public DateTime ltc { get; set; }
        public DateTime mmk { get; set; }
        public DateTime mxn { get; set; }
        public DateTime myr { get; set; }
        public DateTime ngn { get; set; }
        public DateTime nok { get; set; }
        public DateTime nzd { get; set; }
        public DateTime php { get; set; }
        public DateTime pkr { get; set; }
        public DateTime pln { get; set; }
        public DateTime rub { get; set; }
        public DateTime sar { get; set; }
        public DateTime sek { get; set; }
        public DateTime sgd { get; set; }
        public DateTime thb { get; set; }
        public DateTime @try { get; set; }
        public DateTime twd { get; set; }
        public DateTime uah { get; set; }
        public DateTime usd { get; set; }
        public DateTime vef { get; set; }
        public DateTime vnd { get; set; }
        public DateTime xag { get; set; }
        public DateTime xau { get; set; }
        public DateTime xdr { get; set; }
        public DateTime xlm { get; set; }
        public DateTime xrp { get; set; }
        public DateTime yfi { get; set; }
        public DateTime zar { get; set; }
        public DateTime bits { get; set; }
        public DateTime link { get; set; }
        public DateTime sats { get; set; }
    }

    public class Atl
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public int bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class AtlChangePercentage
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class AtlDate
    {
        public DateTime aed { get; set; }
        public DateTime ars { get; set; }
        public DateTime aud { get; set; }
        public DateTime bch { get; set; }
        public DateTime bdt { get; set; }
        public DateTime bhd { get; set; }
        public DateTime bmd { get; set; }
        public DateTime bnb { get; set; }
        public DateTime brl { get; set; }
        public DateTime btc { get; set; }
        public DateTime cad { get; set; }
        public DateTime chf { get; set; }
        public DateTime clp { get; set; }
        public DateTime cny { get; set; }
        public DateTime czk { get; set; }
        public DateTime dkk { get; set; }
        public DateTime dot { get; set; }
        public DateTime eos { get; set; }
        public DateTime eth { get; set; }
        public DateTime eur { get; set; }
        public DateTime gbp { get; set; }
        public DateTime hkd { get; set; }
        public DateTime huf { get; set; }
        public DateTime idr { get; set; }
        public DateTime ils { get; set; }
        public DateTime inr { get; set; }
        public DateTime jpy { get; set; }
        public DateTime krw { get; set; }
        public DateTime kwd { get; set; }
        public DateTime lkr { get; set; }
        public DateTime ltc { get; set; }
        public DateTime mmk { get; set; }
        public DateTime mxn { get; set; }
        public DateTime myr { get; set; }
        public DateTime ngn { get; set; }
        public DateTime nok { get; set; }
        public DateTime nzd { get; set; }
        public DateTime php { get; set; }
        public DateTime pkr { get; set; }
        public DateTime pln { get; set; }
        public DateTime rub { get; set; }
        public DateTime sar { get; set; }
        public DateTime sek { get; set; }
        public DateTime sgd { get; set; }
        public DateTime thb { get; set; }
        public DateTime @try { get; set; }
        public DateTime twd { get; set; }
        public DateTime uah { get; set; }
        public DateTime usd { get; set; }
        public DateTime vef { get; set; }
        public DateTime vnd { get; set; }
        public DateTime xag { get; set; }
        public DateTime xau { get; set; }
        public DateTime xdr { get; set; }
        public DateTime xlm { get; set; }
        public DateTime xrp { get; set; }
        public DateTime yfi { get; set; }
        public DateTime zar { get; set; }
        public DateTime bits { get; set; }
        public DateTime link { get; set; }
        public DateTime sats { get; set; }
    }

    public class MarketCap
    {
        public long aed { get; set; }
        public long ars { get; set; }
        public long aud { get; set; }
        public int bch { get; set; }
        public long bdt { get; set; }
        public long bhd { get; set; }
        public long bmd { get; set; }
        public int bnb { get; set; }
        public long brl { get; set; }
        public int btc { get; set; }
        public long cad { get; set; }
        public long chf { get; set; }
        public long clp { get; set; }
        public long cny { get; set; }
        public long czk { get; set; }
        public long dkk { get; set; }
        public long dot { get; set; }
        public long eos { get; set; }
        public int eth { get; set; }
        public long eur { get; set; }
        public long gbp { get; set; }
        public long hkd { get; set; }
        public long huf { get; set; }
        public long idr { get; set; }
        public long ils { get; set; }
        public long inr { get; set; }
        public long jpy { get; set; }
        public long krw { get; set; }
        public long kwd { get; set; }
        public long lkr { get; set; }
        public int ltc { get; set; }
        public long mmk { get; set; }
        public long mxn { get; set; }
        public long myr { get; set; }
        public long ngn { get; set; }
        public long nok { get; set; }
        public long nzd { get; set; }
        public long php { get; set; }
        public long pkr { get; set; }
        public long pln { get; set; }
        public long rub { get; set; }
        public long sar { get; set; }
        public long sek { get; set; }
        public long sgd { get; set; }
        public long thb { get; set; }
        public long @try { get; set; }
        public long twd { get; set; }
        public long uah { get; set; }
        public long usd { get; set; }
        public long vef { get; set; }
        public long vnd { get; set; }
        public long xag { get; set; }
        public int xau { get; set; }
        public long xdr { get; set; }
        public long xlm { get; set; }
        public long xrp { get; set; }
        public int yfi { get; set; }
        public long zar { get; set; }
        public long bits { get; set; }
        public long link { get; set; }
        public long sats { get; set; }
    }

    public class FullyDilutedValuation
    {
    }

    public class TotalVolume
    {
        public long aed { get; set; }
        public long ars { get; set; }
        public long aud { get; set; }
        public int bch { get; set; }
        public long bdt { get; set; }
        public long bhd { get; set; }
        public long bmd { get; set; }
        public int bnb { get; set; }
        public long brl { get; set; }
        public int btc { get; set; }
        public long cad { get; set; }
        public long chf { get; set; }
        public long clp { get; set; }
        public long cny { get; set; }
        public long czk { get; set; }
        public long dkk { get; set; }
        public int dot { get; set; }
        public long eos { get; set; }
        public int eth { get; set; }
        public long eur { get; set; }
        public long gbp { get; set; }
        public long hkd { get; set; }
        public long huf { get; set; }
        public long idr { get; set; }
        public long ils { get; set; }
        public long inr { get; set; }
        public long jpy { get; set; }
        public long krw { get; set; }
        public long kwd { get; set; }
        public long lkr { get; set; }
        public int ltc { get; set; }
        public long mmk { get; set; }
        public long mxn { get; set; }
        public long myr { get; set; }
        public long ngn { get; set; }
        public long nok { get; set; }
        public long nzd { get; set; }
        public long php { get; set; }
        public long pkr { get; set; }
        public long pln { get; set; }
        public long rub { get; set; }
        public long sar { get; set; }
        public long sek { get; set; }
        public long sgd { get; set; }
        public long thb { get; set; }
        public long @try { get; set; }
        public long twd { get; set; }
        public long uah { get; set; }
        public long usd { get; set; }
        public long vef { get; set; }
        public long vnd { get; set; }
        public int xag { get; set; }
        public int xau { get; set; }
        public long xdr { get; set; }
        public long xlm { get; set; }
        public long xrp { get; set; }
        public int yfi { get; set; }
        public long zar { get; set; }
        public long bits { get; set; }
        public int link { get; set; }
        public long sats { get; set; }
    }

    public class High24h
    {
        public double aed { get; set; }
        public int ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public int bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public int clp { get; set; }
        public double cny { get; set; }
        public int czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public int huf { get; set; }
        public int idr { get; set; }
        public double ils { get; set; }
        public int inr { get; set; }
        public int jpy { get; set; }
        public int krw { get; set; }
        public double kwd { get; set; }
        public int lkr { get; set; }
        public double ltc { get; set; }
        public int mmk { get; set; }
        public int mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public int nok { get; set; }
        public double nzd { get; set; }
        public int php { get; set; }
        public int pkr { get; set; }
        public double pln { get; set; }
        public int rub { get; set; }
        public double sar { get; set; }
        public int sek { get; set; }
        public double sgd { get; set; }
        public int thb { get; set; }
        public int @try { get; set; }
        public int twd { get; set; }
        public int uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public int vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public int xlm { get; set; }
        public int xrp { get; set; }
        public double yfi { get; set; }
        public int zar { get; set; }
        public int bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class Low24h
    {
        public double aed { get; set; }
        public int ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public int bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public int clp { get; set; }
        public double cny { get; set; }
        public int czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public int huf { get; set; }
        public int idr { get; set; }
        public double ils { get; set; }
        public int inr { get; set; }
        public int jpy { get; set; }
        public int krw { get; set; }
        public double kwd { get; set; }
        public int lkr { get; set; }
        public double ltc { get; set; }
        public int mmk { get; set; }
        public int mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public int nok { get; set; }
        public double nzd { get; set; }
        public int php { get; set; }
        public int pkr { get; set; }
        public double pln { get; set; }
        public int rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public int thb { get; set; }
        public double @try { get; set; }
        public int twd { get; set; }
        public int uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public int vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public int xlm { get; set; }
        public int xrp { get; set; }
        public double yfi { get; set; }
        public int zar { get; set; }
        public int bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class PriceChange24hInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public int clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public int huf { get; set; }
        public int idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public int krw { get; set; }
        public double kwd { get; set; }
        public int lkr { get; set; }
        public double ltc { get; set; }
        public int mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public int ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public int pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public int vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public int sats { get; set; }
    }

    public class PriceChangePercentage1hInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage24hInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage7dInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage14dInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage30dInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage60dInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage200dInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class PriceChangePercentage1yInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class MarketCapChange24hInCurrency
    {
        public long aed { get; set; }
        public long ars { get; set; }
        public long aud { get; set; }
        public int bch { get; set; }
        public long bdt { get; set; }
        public long bhd { get; set; }
        public long bmd { get; set; }
        public int bnb { get; set; }
        public long brl { get; set; }
        public int btc { get; set; }
        public long cad { get; set; }
        public long chf { get; set; }
        public long clp { get; set; }
        public long cny { get; set; }
        public long czk { get; set; }
        public long dkk { get; set; }
        public int dot { get; set; }
        public long eos { get; set; }
        public int eth { get; set; }
        public long eur { get; set; }
        public long gbp { get; set; }
        public long hkd { get; set; }
        public long huf { get; set; }
        public long idr { get; set; }
        public long ils { get; set; }
        public long inr { get; set; }
        public long jpy { get; set; }
        public long krw { get; set; }
        public long kwd { get; set; }
        public long lkr { get; set; }
        public int ltc { get; set; }
        public long mmk { get; set; }
        public long mxn { get; set; }
        public long myr { get; set; }
        public long ngn { get; set; }
        public long nok { get; set; }
        public long nzd { get; set; }
        public long php { get; set; }
        public long pkr { get; set; }
        public long pln { get; set; }
        public long rub { get; set; }
        public long sar { get; set; }
        public long sek { get; set; }
        public long sgd { get; set; }
        public long thb { get; set; }
        public long @try { get; set; }
        public long twd { get; set; }
        public long uah { get; set; }
        public long usd { get; set; }
        public int vef { get; set; }
        public long vnd { get; set; }
        public int xag { get; set; }
        public int xau { get; set; }
        public long xdr { get; set; }
        public long xlm { get; set; }
        public double xrp { get; set; }
        public int yfi { get; set; }
        public long zar { get; set; }
        public long bits { get; set; }
        public double link { get; set; }
        public long sats { get; set; }
    }

    public class MarketCapChangePercentage24hInCurrency
    {
        public double aed { get; set; }
        public double ars { get; set; }
        public double aud { get; set; }
        public double bch { get; set; }
        public double bdt { get; set; }
        public double bhd { get; set; }
        public double bmd { get; set; }
        public double bnb { get; set; }
        public double brl { get; set; }
        public double btc { get; set; }
        public double cad { get; set; }
        public double chf { get; set; }
        public double clp { get; set; }
        public double cny { get; set; }
        public double czk { get; set; }
        public double dkk { get; set; }
        public double dot { get; set; }
        public double eos { get; set; }
        public double eth { get; set; }
        public double eur { get; set; }
        public double gbp { get; set; }
        public double hkd { get; set; }
        public double huf { get; set; }
        public double idr { get; set; }
        public double ils { get; set; }
        public double inr { get; set; }
        public double jpy { get; set; }
        public double krw { get; set; }
        public double kwd { get; set; }
        public double lkr { get; set; }
        public double ltc { get; set; }
        public double mmk { get; set; }
        public double mxn { get; set; }
        public double myr { get; set; }
        public double ngn { get; set; }
        public double nok { get; set; }
        public double nzd { get; set; }
        public double php { get; set; }
        public double pkr { get; set; }
        public double pln { get; set; }
        public double rub { get; set; }
        public double sar { get; set; }
        public double sek { get; set; }
        public double sgd { get; set; }
        public double thb { get; set; }
        public double @try { get; set; }
        public double twd { get; set; }
        public double uah { get; set; }
        public double usd { get; set; }
        public double vef { get; set; }
        public double vnd { get; set; }
        public double xag { get; set; }
        public double xau { get; set; }
        public double xdr { get; set; }
        public double xlm { get; set; }
        public double xrp { get; set; }
        public double yfi { get; set; }
        public double zar { get; set; }
        public double bits { get; set; }
        public double link { get; set; }
        public double sats { get; set; }
    }

    public class MarketData
    {
        public CurrentPrice current_price { get; set; }
        public object total_value_locked { get; set; }
        public object mcap_to_tvl_ratio { get; set; }
        public object fdv_to_tvl_ratio { get; set; }
        public Roi roi { get; set; }
        public Ath ath { get; set; }
        public AthChangePercentage ath_change_percentage { get; set; }
        public AthDate ath_date { get; set; }
        public Atl atl { get; set; }
        public AtlChangePercentage atl_change_percentage { get; set; }
        public AtlDate atl_date { get; set; }
        public MarketCap market_cap { get; set; }
        public int market_cap_rank { get; set; }
        public FullyDilutedValuation fully_diluted_valuation { get; set; }
        public TotalVolume total_volume { get; set; }
        public High24h high_24h { get; set; }
        public Low24h low_24h { get; set; }
        public double price_change_24h { get; set; }
        public double price_change_percentage_24h { get; set; }
        public double price_change_percentage_7d { get; set; }
        public double price_change_percentage_14d { get; set; }
        public double price_change_percentage_30d { get; set; }
        public double price_change_percentage_60d { get; set; }
        public double price_change_percentage_200d { get; set; }
        public double price_change_percentage_1y { get; set; }
        public long market_cap_change_24h { get; set; }
        public double market_cap_change_percentage_24h { get; set; }
        public PriceChange24hInCurrency price_change_24h_in_currency { get; set; }
        public PriceChangePercentage1hInCurrency price_change_percentage_1h_in_currency { get; set; }
        public PriceChangePercentage24hInCurrency price_change_percentage_24h_in_currency { get; set; }
        public PriceChangePercentage7dInCurrency price_change_percentage_7d_in_currency { get; set; }
        public PriceChangePercentage14dInCurrency price_change_percentage_14d_in_currency { get; set; }
        public PriceChangePercentage30dInCurrency price_change_percentage_30d_in_currency { get; set; }
        public PriceChangePercentage60dInCurrency price_change_percentage_60d_in_currency { get; set; }
        public PriceChangePercentage200dInCurrency price_change_percentage_200d_in_currency { get; set; }
        public PriceChangePercentage1yInCurrency price_change_percentage_1y_in_currency { get; set; }
        public MarketCapChange24hInCurrency market_cap_change_24h_in_currency { get; set; }
        public MarketCapChangePercentage24hInCurrency market_cap_change_percentage_24h_in_currency { get; set; }
        public object total_supply { get; set; }
        public object max_supply { get; set; }
        public double circulating_supply { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class CommunityData
    {
        public object facebook_likes { get; set; }
        public int twitter_followers { get; set; }
        public double reddit_average_posts_48h { get; set; }
        public double reddit_average_comments_48h { get; set; }
        public int reddit_subscribers { get; set; }
        public int reddit_accounts_active_48h { get; set; }
        public object telegram_channel_user_count { get; set; }
    }

    public class CodeAdditionsDeletions4Weeks
    {
        public int additions { get; set; }
        public int deletions { get; set; }
    }

    public class DeveloperData
    {
        public int forks { get; set; }
        public int stars { get; set; }
        public int subscribers { get; set; }
        public int total_issues { get; set; }
        public int closed_issues { get; set; }
        public int pull_requests_merged { get; set; }
        public int pull_request_contributors { get; set; }
        public CodeAdditionsDeletions4Weeks code_additions_deletions_4_weeks { get; set; }
        public int commit_count_4_weeks { get; set; }
        public List<int> last_4_weeks_commit_activity_series { get; set; }
    }

    public class PublicInterestStats
    {
        public int alexa_rank { get; set; }
        public object bing_matches { get; set; }
    }

    public class Market
    {
        public string name { get; set; }
        public string identifier { get; set; }
        public bool has_trading_incentive { get; set; }
    }

    public class ConvertedLast
    {
        public double btc { get; set; }
        public double eth { get; set; }
        public double usd { get; set; }
    }

    public class ConvertedVolume
    {
        public double btc { get; set; }
        public double eth { get; set; }
        public long usd { get; set; }
    }

    public class Ticker
    {
        public string @base { get; set; }
        public string target { get; set; }
        public Market market { get; set; }
        public double last { get; set; }
        public double volume { get; set; }
        public ConvertedLast converted_last { get; set; }
        public ConvertedVolume converted_volume { get; set; }
        public string trust_score { get; set; }
        public double bid_ask_spread_percentage { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime last_traded_at { get; set; }
        public DateTime last_fetch_at { get; set; }
        public bool is_anomaly { get; set; }
        public bool is_stale { get; set; }
        public string trade_url { get; set; }
        public string token_info_url { get; set; }
        public string coin_id { get; set; }
        public string target_coin_id { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public object asset_platform_id { get; set; }
        public Platforms platforms { get; set; }
        public int block_time_in_minutes { get; set; }
        public string hashing_algorithm { get; set; }
        public List<string> categories { get; set; }
        public object public_notice { get; set; }
        public List<object> additional_notices { get; set; }
        public Localization localization { get; set; }
        public Description description { get; set; }
        public Links links { get; set; }
        public Image image { get; set; }
        public string country_origin { get; set; }
        public string genesis_date { get; set; }
        public double sentiment_votes_up_percentage { get; set; }
        public double sentiment_votes_down_percentage { get; set; }
        public IcoData ico_data { get; set; }
        public int market_cap_rank { get; set; }
        public int coingecko_rank { get; set; }
        public double coingecko_score { get; set; }
        public double developer_score { get; set; }
        public double community_score { get; set; }
        public double liquidity_score { get; set; }
        public double public_interest_score { get; set; }
        public MarketData market_data { get; set; }
        public CommunityData community_data { get; set; }
        public DeveloperData developer_data { get; set; }
        public PublicInterestStats public_interest_stats { get; set; }
        public List<object> status_updates { get; set; }
        public DateTime last_updated { get; set; }
        public List<Ticker> tickers { get; set; }
    }
}
