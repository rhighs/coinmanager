using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoinManager.EF
{
    /*
     * Case sensisivity with postgres *matters*, so when you create a model representing a relation,
     * make sure to call its member fields with the same casing given in the schema.
     * Notice how the "crypto" DbSet is written in lowercase, thats because the relation crypto has
     * the same casing in the db, as you can tell the EF parser wont make any adjustment on its own,
     * you must provide the correct casing ...but as you might also have noticed, the class name for
     * the representing model can have any desired casing, in this case i'll just stick with the c#
     * standards.
     */
    public class CMDbContext : DbContext
    {
        public DbSet<Crypto> Crypto { get; set; }
        public DbSet<UserStandard> UserStandard { get; set; }
        public DbSet<UserMiner> UserMiner { get; set; }
        public DbSet<MinerSessions> MinerSessions { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<RunningTransaction> RunningTransaction { get; set; }
        public DbSet<Buy> Buy{ get; set; }
        public DbSet<Loan> Loan { get; set; }
        private string connectionString;

        public CMDbContext(string host, string dbName, string username, string password) 
        {
            connectionString = $"Host={host};Database={dbName};Username={username};Password={password}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(connectionString);
    }

    [Table("userstandard")]
    public class UserStandard
    {
        [Column("id")]
        public int Id              { get; set; }
        [Column("username")]
        public string Username     { get; set; }
        [Column("password")]
        public string Password     { get; set; }
    }
 
    [Table("userminer")]
    public class UserMiner
    {
        [Column("id")]
        public int Id              { get; set; }
        [Column("miningpower")]
        public int Miningpower     { get; set; }
    }

    [Table("minersessions")]
    public class MinerSessions
    {
        [Column("id")]
        public int Id              { get; set; }
        [Column("minerid")]
        public int MinerId         { get; set; }
        [Column("transactionid")]
        public int TransactionId   { get; set; }
    }

    [Table("wallet")]
    public class Wallet
    {
        [Key]
        [Column("userid")]
        public int UserId          { get; set; }
        [Column("cryptoid")]
        public string CryptoId     { get; set; }
        [Column("quantity")]
        public double Quantity     { get; set; }
    }

    [Table("crypto")]
    public class Crypto
    {
        [Column("id")]
        public string Id                { get; set; }
        [Column("name")]
        public string Name              { get; set; }
        [Column("symbol")]
        public string Symbol            { get; set; }
        [Column("currentprice")]
        public double CurrentPrice      { get; set; }
        [Column("imageurl")]
        public string ImageUrl          { get; set; }
        [Column("marketcap")]
        public double Marketcap         { get; set; }
        [Column("marketcaprank")]
        public double MarketcapRrank     { get; set; }
        [Column("circulatingsupply")]
        public int CirculatingSupply    { get; set; }
        [Column("totalvolume")]
        public double TotalVolume       { get; set; }
    }

    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public int Id                   { get; set; }
        [Column("sourceid")]
        public int SourceId             { get; set; }
        [Column("dstinationid")]
        public int DestinationId        { get; set; }
        [Column("cryptoid")]
        public string CryptoId          { get; set; }
        [Column("startdate")]
        public DateTime StartDate       { get; set; }
        [Column("finishdate")]
        public DateTime FinishDate      { get; set; }
        [Column("cryptoquantity")]
        public double CryptoQuantity    { get; set; }
        [Column("state")]
        public int State                { get; set; }
    }

    [Table("runningtransaction")]
    public class RunningTransaction
    {
        [Key]
        [Column("transactionid")]
        public int TransactionId        { get; set; }
        [Column("startdate")]
        public DateTime StartDate       { get; set; }
        [Column("finishdate")]
        public DateTime FinishDate      { get; set; }
        [Column("idminer")]
        public int MinerId              { get; set; }
    }

    [Table("buy")]
    public class Buy
    {
        [Column("id")]
        public int Id                   { get; set; }
        [Column("userid")]
        public int UserId               { get; set; }
        [Column("cryptoid")]
        public string CryptoId          { get; set; }
        [Column("basecryptoid")]
        public string BaseCryptoId      { get; set; }
        [Column("basequantity")]
        public double BaseQuantity      { get; set; }
        [Column("buyquantity")]
        public double BuyQuantity       { get; set; }
    }

    [Table("loan")]
    public class Loan
    {
        [Column("id")]
        public int Id                   { get; set; }
        [Column("userid")]
        public int UserId               { get; set; }
        [Column("crytpo")]
        public string CryptoId          { get; set; }
        [Column("advancecryptoid")]
        public string AdvanceCryptoId   { get; set; }
        [Column("loanquantity")]
        public double LoanQuantity      { get; set; }
        [Column("advance")]
        public double Advance           { get; set; }
        [Column("expiredate")]
        public DateTime ExpireDate      { get; set; }
    }
}
