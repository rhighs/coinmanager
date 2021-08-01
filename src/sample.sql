CREATE TABLE Coin (
    primary key Id VARCHAR(20) NOT NULL,
    Name VARCHAR(20) NOT NULL,
    Symbol VARCHAR(4) NOT NULL,
    TransTime FLOAT DEFAULT 1,
    Price FLOAT NOT NULL,
    ImageUrl VARCHAR(100)
)

CREATE TABLE CoinMarket (
    foreign key CoinId VARCHAR(20) NOT NULL,
    Mkt1h FLOAT[],
    Mkt24h FLOAT[],
    Mkt30g FLOAT[],
    MktCap FLOAT
)

CREATE TABLE Wallet (
    foreign key UserId Integer NOT NULL,
    foreign key CoindId VARCHAR(20) NOT NULL,
    Quantity FLOAT NOT NULL,
)

CREATE TABLE User (
    primary key Id Integer NOT NULL,
    Username VARCHAR(30) NOT NULL,
    Password CHAR(20) NOT NULL,
    MinerId Integer
)

CREATE TABLE Miner (
    foreign key UserId Integer NOT NULL,
    TransactionsDone Integer DEFAULT 0,
    MiningPower Integer NOT NULL
)

CREATE TABLE Transaction (
    primary key TransactionId Integer NOT NULL,
    foreign key SourceId Integer NOT NULL,
    foreign key DestitationId Integer NOT NULL,
    foreign key CoinId VARCHAR(20) NOT NULL,
    TimeRequired FLOAT NOT NULL,
    Taxes FLOAT NOT NULL,
    foreign key MinerId Integer NOT NULL,
)
CREATE TABLE Transaction (
    primary key TransactionId Integer NOT NULL,
    foreign key SourceId Integer NOT NULL,
    foreign key DestitationId Integer NOT NULL,
    foreign key CoinId VARCHAR(20) NOT NULL,
    TimeRequired FLOAT NOT NULL,
    Taxes FLOAT NOT NULL,
    foreign key MinerId Integer NOT NULL,
)
