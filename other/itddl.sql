CREATE DOMAIN CRIPTO_ID AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRIPTO_NAME AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRIPTO_SYMBOL AS VARCHAR(300) NOT NULL;
CREATE DOMAIN CRIPTO_IMAGE AS VARCHAR(1000) NOT NULL;

CREATE TABLE "UtenteStandard" (
    "Id" Integer NOT NULL PRIMARY KEY,
    "Username" VARCHAR(30) NOT NULL,
    "Password" CHAR(20) NOT NULL
);

CREATE TABLE "UtenteMiner" (
    "Id" Integer NOT NULL PRIMARY KEY,
    "PotenzaDiCalcolo" INTEGER NOT NULL,
    FOREIGN KEY ("Id") REFERENCES "UtenteStandard"("Id")
);

CREATE TABLE "Cripto" (
    "Id" CRIPTO_ID PRIMARY KEY,
    "Name" CRIPTO_NAME,
    "Simbolo" CRIPTO_SYMBOL,
    "Prezzo" FLOAT NOT NULL,
    "UrlImmagine" CRIPTO_IMAGE,
    "CapitaleDiMercato" FLOAT NOT NULL,
    "PosizioneClassifica" INTEGER NOT NULL,
    "QuantitàInCircolo" BIGINT,
    "VolumeTotale" FLOAT
);

CREATE TABLE "Portafoglio" (
    "IdUtente" Integer NOT NULL,
    "IdCripto" CRIPTO_ID NOT NULL,
    "Quantità" FLOAT NOT NULL,
    PRIMARY KEY ("IdUtente", "IdCripto"),
    FOREIGN KEY ("IdUtente") REFERENCES "UtenteStandard"("Id"),
    FOREIGN KEY ("IdCripto") REFERENCES "Cripto"("Id")
);

CREATE TABLE "Transazione" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "IdMittente" Integer NOT NULL,
    "IdDestinatario" Integer NOT NULL,
    "IdCripto" CRIPTO_ID,
    "DataInizio" TIMESTAMP WITH TIME ZONE,
    "DataFine" TIMESTAMP WITH TIME ZONE,
    "QuantitàCripto" FLOAT NOT NULL,
    "IdMiner" INTEGER,
    "Stato" INTEGER NOT NULL,
    FOREIGN KEY ("IdMiner") REFERENCES "UtenteMiner"("Id"),
    CONSTRAINT ValidState CHECK ( "Stato" BETWEEN 1 AND 3 )
);

CREATE TABLE "TransazioneInCorso" (
    "IdTransazione" INTEGER NOT NULL PRIMARY KEY,
    "DataInizio" TIMESTAMP WITH TIME ZONE,
    "TempoRichiesto" INTERVAL MINUTE TO SECOND,
    FOREIGN KEY ("IdTransazione") REFERENCES "Transazione"("Id")
);

CREATE TABLE "Acquisto" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "IdUtente" INTEGER NOT NULL,
    "IdCripto" CRIPTO_ID,
    "IdCriptoBase" CRIPTO_ID,
    "QuantitàBase" FLOAT NOT NULL,
    "QuantitàCriptoComprata" FLOAT NOT NULL,
    FOREIGN KEY ("IdUtente") REFERENCES "UtenteStandard"("Id"),
    FOREIGN KEY ("IdCripto") REFERENCES "Cripto"("Id"),
    FOREIGN KEY ("IdCriptoBase") REFERENCES "Cripto"("Id")
);

CREATE TABLE "Prestito" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "IdUtente" INTEGER NOT NULL,
    "IdCripto" CRIPTO_ID,
    "IdCriptoAnticipo" CRIPTO_ID,
    "QuantitàPrestito" FLOAT NOT NULL,
    "QuantitàAnticipo" FLOAT,
    "DataScadenza" TIMESTAMP WITH TIME ZONE,
    FOREIGN KEY ("IdUtente") REFERENCES "UtenteStandard"("Id"),
    FOREIGN KEY ("IdCripto") REFERENCES "Cripto"("Id"),
    FOREIGN KEY ("IdCriptoAnticipo") REFERENCES "Cripto"("Id")
);

CREATE TABLE "SessioniMiner" (
    "Id" INTEGER NOT NULL PRIMARY KEY,
    "IdMiner" INTEGER NOT NULL,
    "IdTransazione" INTEGER not NULL,
    FOREIGN KEY ("IdMiner") REFERENCES "UtenteMiner"("Id"),
    FOREIGN KEY ("IdTransazione") REFERENCES "Transazione"("Id")
);

CREATE TABLE "Amicizia" (
    "IdUtente" INTEGER NOT NULL,
    "IdAmico" INTEGER NOT NULL,
    PRIMARY KEY ("IdUtente", "IdAmico"),
    FOREIGN KEY ("IdUtente") REFERENCES "UtenteStandard"("Id"),
    FOREIGN KEY ("IdAmico") REFERENCES "UtenteStandard"("Id")
);

CREATE TABLE "FriendRequest" (
    "IdMittente" INTEGER NOT NULL,
    "IdDestinatario" INTEGER NOT NULL,
    PRIMARY KEY ("IdMittente", "IdDestinatario"),
    FOREIGN KEY ("IdMittente") REFERENCES "UtenteStandard"("Id"),
    FOREIGN KEY ("IdDestinatario") REFERENCES "UtenteStandard"("Id"),
    CONSTRAINT ValidReq CHECK ("IdMittente" != "IdDestinatario")
);
