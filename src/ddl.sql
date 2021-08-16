CREATE DOMAIN CRYPTO_ID AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRYPTO_NAME AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRYPTO_SYMBOL AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRYPTO_IMAGE AS VARCHAR(1000) NOT NULL;

CREATE TABLE "UserStandard" (
    "Id" Integer NOT NULL PRIMARY KEY,
    "Username" VARCHAR(30) NOT NULL,
    "Password" CHAR(20) NOT NULL
);

CREATE TABLE "UserMiner" (
    "Id" Integer NOT NULL PRIMARY KEY,
    "MiningPower" INTEGER NOT NULL,
    FOREIGN KEY ("Id") REFERENCES "UserStandard"("Id")
);

CREATE TABLE "Crypto" (
    "Id" CRYPTO_ID PRIMARY KEY,
    "Name" CRYPTO_NAME,
    "Symbol" CRYPTO_SYMBOL,
    "CurrentPrice" FLOAT NOT NULL,
    "ImageUrl" CRYPTO_IMAGE,
    "MarketCap" FLOAT NOT NULL,
    "MarketCapRank" INTEGER NOT NULL,
    "CirculatingSupply" BIGINT,
    "TotalVolume" FLOAT
);

CREATE TABLE "Wallet" (
    "UserId" Integer NOT NULL,
    "CryptoId" CRYPTO_ID NOT NULL,
    "Quantity" FLOAT NOT NULL,
    PRIMARY KEY ("UserId", "CryptoId"),
    FOREIGN KEY ("UserId") REFERENCES "UserStandard"("Id"),
    FOREIGN KEY ("CryptoId") REFERENCES "Crypto"("Id")
);

CREATE TABLE "Transaction" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "SourceId" Integer NOT NULL,
    "DestitationId" Integer NOT NULL,
    "CryptoId" CRYPTO_ID,
    "StartDate" TIMESTAMP WITH TIME ZONE,
    "FinishDate" TIMESTAMP WITH TIME ZONE,
    "CryptoQuantity" FLOAT NOT NULL,
    "State" INTEGER NOT NULL,
    CONSTRAINT ValidState CHECK ( "State" BETWEEN 1 AND 3 )
);

CREATE TABLE "RunningTransaction" (
    "TransactionId" INTEGER NOT NULL PRIMARY KEY,
    "StartDate" TIMESTAMP WITH TIME ZONE,
    "TotalTime" INTERVAL MINUTE TO SECOND,
    "MinerId" INTEGER NOT NULL,
    FOREIGN KEY ("MinerId") REFERENCES "UserMiner"("Id"),
    FOREIGN KEY ("TransactionId") REFERENCES "Transaction"("Id")
);

CREATE TABLE "Buy" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "UserId" INTEGER NOT NULL,
    "CryptoId" CRYPTO_ID,
    "BaseCryptoId" CRYPTO_ID,
    "BaseQuantity" FLOAT NOT NULL,
    "BuyQuantity" FLOAT NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES "UserStandard"("Id"),
    FOREIGN KEY ("CryptoId") REFERENCES "Crypto"("Id"),
    FOREIGN KEY ("BaseCryptoId") REFERENCES "Crypto"("Id")
);

CREATE TABLE "Loan" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "UserId" INTEGER NOT NULL,
    "CryptoId" CRYPTO_ID,
    "AdvanceCryptoId" CRYPTO_ID,
    "LoanQuantity" FLOAT NOT NULL,
    "Advance" FLOAT,
    "ExpireDate" TIMESTAMP WITH TIME ZONE,
    FOREIGN KEY ("UserId") REFERENCES "UserStandard"("Id"),
    FOREIGN KEY ("CryptoId") REFERENCES "Crypto"("Id"),
    FOREIGN KEY ("AdvanceCryptoId") REFERENCES "Crypto"("Id")
);

CREATE TABLE "MinerSessions" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "MinerId" INTEGER NOT NULL,
    "TransactionId" INTEGER not NULL,
    FOREIGN KEY ("MinerId") REFERENCES "UserMiner"("Id"),
    FOREIGN KEY ("TransactionId") REFERENCES "Transaction"("Id")
);
