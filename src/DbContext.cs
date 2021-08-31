using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoinManager.EF
{
    /*
     * Case sensisivity with postgres *matters*, so when you create a model representing a relation,
     * make sure to call its member fields with the same casing given in the schema.
     * Notice how the "Crypto" DbSet is written in pascal case, thats because the relation crypto has
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

        public static CMDbContext Instance;
        public static UserStandard LoggedUser;

        public CMDbContext(string host, string dbName, string username, string password) 
        {
            connectionString = $"Host={host};Database={dbName};Username={username};Password={password}";
            Instance = this;
            LoggedUser = new UserStandard{Username = username, Password = password};
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                .HasKey(w => new { w.UserId , w.CryptoId });
        }
    }

    public class UserStandard
    {
        public int Id              { get; set; }
        public string Username     { get; set; }
        public string Password     { get; set; }
    }
 
    public class UserMiner
    {
        public int Id              { get; set; }
        public int Miningpower     { get; set; }
    }

    public class MinerSessions
    {
        public int Id              { get; set; }
        public int MinerId         { get; set; }
        public int TransactionId   { get; set; }
    }

    public class Wallet
    {
        [Key]
        public int UserId          { get; set; }
        [Key]
        public string CryptoId     { get; set; }
        public double Quantity     { get; set; }
    }

    public class Crypto
    {
        public string Id                { get; set; }
        public string Name              { get; set; }
        public string Symbol            { get; set; }
        public double CurrentPrice      { get; set; }
        public string ImageUrl          { get; set; }
        public double MarketCap         { get; set; }
        public int MarketCapRank        { get; set; }
        public long CirculatingSupply   { get; set; }
        public double TotalVolume       { get; set; }
    }

    public class Transaction
    {
        public int Id                   { get; set; }
        public int SourceId             { get; set; }
        public int DestinationId        { get; set; }
        public string CryptoId          { get; set; }
        public DateTime StartDate       { get; set; }
        public DateTime FinishDate      { get; set; }
        public double CryptoQuantity    { get; set; }
        public int State                { get; set; }
    }

    public class RunningTransaction
    {
        [Key]
        public int TransactionId        { get; set; }
        public DateTime StartDate       { get; set; }
        public DateTime FinishDate      { get; set; }
        public int MinerId              { get; set; }
    }

    public class Buy
    {
        public int Id                   { get; set; }
        public int UserId               { get; set; }
        public string CryptoId          { get; set; }
        public string BaseCryptoId      { get; set; }
        public double BaseQuantity      { get; set; }
        public double BuyQuantity       { get; set; }
    }

    public class Loan
    {
        public int Id                   { get; set; }
        public int UserId               { get; set; }
        public string CryptoId          { get; set; }
        public string AdvanceCryptoId   { get; set; }
        public double LoanQuantity      { get; set; }
        public double Advance           { get; set; }
        public DateTime ExpireDate      { get; set; }
    }
}
