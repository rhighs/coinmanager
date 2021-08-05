CREATE DOMAIN CRIPTO_ID AS VARCHAR(20) NOT NULL;

CREATE TABLE UserStandard (
    Id Integer NOT NULL PRIMARY KEY,
    Username VARCHAR(30) NOT NULL,
    Password CHAR(20) NOT NULL
);

CREATE TABLE UserMiner (
    Id Integer NOT NULL PRIMARY KEY,
    MiningPower INTEGER NOT NULL,
    FOREIGN KEY (Id) REFERENCES UserStandard(Id)
);

CREATE TABLE Cripto (
    Id CRIPTO_ID PRIMARY KEY,
    Name VARCHAR(20) NOT NULL,
    Symbol VARCHAR(4) NOT NULL,
    CurrentPrice FLOAT NOT NULL,
    ImageUrl VARCHAR(200), 
    MarketCap FLOAT NOT NULL,
    MarketCapRank FLOAT NOT NULL,
    CirculatingSupply INTEGER,
    TotalVolume FLOAT
);

CREATE TABLE Wallet (
    UserId Integer NOT NULL,
    CriptoId CRIPTO_ID NOT NULL,
    Quantity FLOAT NOT NULL,
    PRIMARY KEY (UserId, CriptoId),
    FOREIGN KEY (UserId) REFERENCES UserStandard(Id),
    FOREIGN KEY (CriptoId) REFERENCES Cripto(Id)
);

CREATE TABLE Transaction (
    Id INTEGER NOT NULL PRIMARY KEY,
    SourceId Integer NOT NULL,
    DestitationId Integer NOT NULL,
    CriptoId CRIPTO_ID,
    StartDate TIMESTAMP WITH TIME ZONE,
    FinishDate TIMESTAMP WITH TIME ZONE,
    CriptoQuantity FLOAT NOT NULL,
    State INTEGER NOT NULL,
    CONSTRAINT ValidState CHECK ( State BETWEEN 1 AND 3 )
);

CREATE TABLE RunningTransaction (
    TransactionId INTEGER NOT NULL PRIMARY KEY,
    StartDate TIMESTAMP WITH TIME ZONE,
    TotalTime INTERVAL MINUTE TO SECOND,
    IdMiner INTEGER NOT NULL,
    FOREIGN KEY (IdMiner) REFERENCES UserMiner(Id),
    FOREIGN KEY (TransactionId) REFERENCES Transaction(Id)
);

CREATE TABLE Buy (
    Id INTEGER NOT NULL PRIMARY KEY,
    UserId INTEGER NOT NULL,
    CriptoId CRIPTO_ID,
    BaseCriptoId CRIPTO_ID,
    BaseQuantity FLOAT NOT NULL,
    BuyQuantity FLOAT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserStandard(Id),
    FOREIGN KEY (CriptoId) REFERENCES Cripto(Id),
    FOREIGN KEY (BaseCriptoId) REFERENCES Cripto(Id)
);

CREATE TABLE Loan (
    Id INTEGER NOT NULL PRIMARY KEY,
    UserId INTEGER NOT NULL,
    CriptoId CRIPTO_ID,
    AdvanceCriptoId CRIPTO_ID,
    LoanQuantity FLOAT NOT NULL,
    Advance FLOAT,
    ExpireDate TIMESTAMP WITH TIME ZONE,
    FOREIGN KEY (UserId) REFERENCES UserStandard(Id),
    FOREIGN KEY (CriptoId) REFERENCES Cripto(Id),
    FOREIGN KEY (AdvanceCriptoId) REFERENCES Cripto(Id)
);

CREATE TABLE MinerSessions (
    Id INTEGER NOT NULL PRIMARY KEY,
    MinerId INTEGER NOT NULL,
    TransactionId INTEGER not NULL,
    FOREIGN KEY (MinerId) REFERENCES UserMiner(Id),
    FOREIGN KEY (TransactionId) REFERENCES Transaction(Id)
);

