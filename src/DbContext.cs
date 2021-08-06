using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        public DbSet<Crypto> crypto { get; set; }
        public DbSet<UserStandard> userstandard { get; set; }
        public DbSet<UserMiner> userminer { get; set; }
        public DbSet<MinerSessions> minersessions { get; set; }
        public DbSet<Wallet> wallet { get; set; }
        public DbSet<Transaction> transaction { get; set; }
        public DbSet<RunningTransaction> runningtransaction { get; set; }
        public DbSet<Buy> buy { get; set; }
        public DbSet<Loan> loan { get; set; }
        private string connectionString;

        public CMDbContext(string host, string dbName, string username, string password) 
        {
            connectionString = $"Host={host};Database={dbName};Username={username};Password={password}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(connectionString);
    }

    public class UserStandard
    {
        public int id              { get; set; }
        public string username     { get; set; }
        public string password     { get; set; }
    }
 
    public class UserMiner
    {
        public int id              { get; set; }
        public int miningpower     { get; set; }
    }

    public class MinerSessions
    {
        public int id              { get; set; }
        public int minerid         { get; set; }
        public int transactionid   { get; set; }
    }

    public class Wallet
    {
        [Key]
        public int userid          { get; set; }
        public string cryptoid     { get; set; }
        public double quantity     { get; set; }
    }

    public class Crypto
    {
        public string id                { get; set; }
        public string name              { get; set; }
        public string symbol            { get; set; }
        public double currentprice      { get; set; }
        public string imageurl          { get; set; }
        public double marketcap         { get; set; }
        public double marketcaprank     { get; set; }
        public int circulatingsupply    { get; set; }
        public double totalvolume       { get; set; }
    }

    public class Transaction
    {
        public int id                   { get; set; }
        public int sourceid             { get; set; }
        public int destinationid        { get; set; }
        public string cryptoid          { get; set; }
        public DateTime startdate       { get; set; }
        public DateTime finishdate      { get; set; }
        public double cryptoquantity    { get; set; }
        public int state                { get; set; }
    }

    public class RunningTransaction
    {
        [Key]
        public int transactionid        { get; set; }
        public DateTime startdate       { get; set; }
        public DateTime finishdate      { get; set; }
        public int idminer              { get; set; }
    }

    public class Buy
    {
        public int id                   { get; set; }
        public int userid               { get; set; }
        public string cryptoid          { get; set; }
        public string basecryptoid      { get; set; }
        public double basequantity      { get; set; }
        public double buyquantity       { get; set; }
    }

    public class Loan
    {
        public int id                   { get; set; }
        public int userid               { get; set; }
        public string cryptoid          { get; set; }
        public string advancecryptoid   { get; set; }
        public double loanquantity      { get; set; }
        public double advance           { get; set; }
        public DateTime expiredate      { get; set; }
    }
}
