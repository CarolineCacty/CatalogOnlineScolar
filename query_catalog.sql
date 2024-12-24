IF OBJECT_ID('Conturi', 'U') IS NOT NULL DROP TABLE Conturi
GO
CREATE TABLE Conturi (
  ContID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Email VARCHAR(30) CHECK (Email LIKE '%@_%.%') UNIQUE NOT NULL,
  Rol INT NOT NULL -- 0->ELEVI, 1->PARINTI, 2->PROFESORI, 3->ADMINISTRATOR
);

IF OBJECT_ID('Utilizatori', 'U') IS NOT NULL DROP TABLE Utilizatori
GO
CREATE TABLE Utilizatori (
  UtilizatorID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Parola NVARCHAR(30) NOT NULL,
  Email VARCHAR(30) CHECK (Email LIKE '%@_%.%') UNIQUE NOT NULL,
  Rol INT NOT NULL, -- 0->ELEVI, 1->PARINTI, 2->PROFESORI, 3->ADMINISTRATOR
  ImagineProfil NVARCHAR(30) NULL
);

IF OBJECT_ID('Profesori', 'U') IS NOT NULL DROP TABLE Profesori
GO
CREATE TABLE Profesori (
  ProfesorID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Nume VARCHAR(50) NOT NULL,
  Prenume VARCHAR(50) NOT NULL,
  Grad VARCHAR(20) NOT NULL,
  Vechime INT NULL,
  UtilizatorID INT NOT NULL FOREIGN KEY REFERENCES Utilizatori(UtilizatorID)
);

IF OBJECT_ID('Parinti', 'U') IS NOT NULL DROP TABLE Parinti
GO
CREATE TABLE Parinti (
  ParinteID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Nume_parinte VARCHAR(50) NOT NULL,
  Prenume_parinte VARCHAR(50) NOT NULL,
  Numar_telefon VARCHAR(15) NOT NULL,
  UtilizatorID INT NOT NULL FOREIGN KEY REFERENCES Utilizatori(UtilizatorID)
);

IF OBJECT_ID('IntervaleOre', 'U') IS NOT NULL DROP TABLE IntervaleOre
GO
CREATE TABLE IntervaleOre (
  IntervalID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Ora_inceput TIME NOT NULL,
  Ora_sfarsit TIME NOT NULL
);
INSERT INTO IntervaleOre (Ora_inceput, Ora_sfarsit)
VALUES ('08:00', '08:50'),
       ('09:00', '09:50'),
       ('10:00', '10:50'),
       ('11:00', '11:50'),
       ('12:00', '12:50'),
       ('13:00', '13:50');

IF OBJECT_ID('Clase', 'U') IS NOT NULL DROP TABLE Clase
GO
CREATE TABLE Clase (
  ClasaID NVARCHAR(10) NOT NULL PRIMARY KEY,
  An_scolar INT NOT NULL,
  Specializare VARCHAR(50) NOT NULL,
  Diriginte INT NOT NULL FOREIGN KEY REFERENCES Profesori(ProfesorID)
);

ALTER TABLE Clase
ADD CONSTRAINT Constraint_Diriginte UNIQUE (Diriginte);


IF OBJECT_ID('Elevi', 'U') IS NOT NULL DROP TABLE Elevi
GO
CREATE TABLE Elevi (
  ElevID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Nume VARCHAR(50) NOT NULL,
  Prenume VARCHAR(50) NOT NULL,
  Data_nasterii DATE NOT NULL,
  Adresa VARCHAR(100) NOT NULL,
  ClasaID NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Clase(ClasaID),
  ParinteID INT NOT NULL FOREIGN KEY REFERENCES Parinti(ParinteID),
  UtilizatorID INT NOT NULL FOREIGN KEY REFERENCES Utilizatori(UtilizatorID)
);

IF OBJECT_ID('Materii', 'U') IS NOT NULL DROP TABLE Materii
GO
CREATE TABLE Materii (
  MaterieID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Nume_materie VARCHAR(50) NOT NULL
);

ALTER TABLE Materii
ADD CONSTRAINT Constraint_Materie UNIQUE (Nume_materie);


IF OBJECT_ID('Predare', 'U') IS NOT NULL DROP TABLE Predare
GO
CREATE TABLE Predare (
  PredareID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  ProfesorID INT NOT NULL FOREIGN KEY REFERENCES Profesori(ProfesorID),
  MaterieID INT NOT NULL FOREIGN KEY REFERENCES Materii(MaterieID),
  ClasaID NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Clase(ClasaID)
);

ALTER TABLE Predare
ADD CONSTRAINT UQ_Predare UNIQUE (ProfesorID,MaterieID,ClasaID);


insert into Predare (ProfesorID,MaterieID,ClasaID) values
(5,1,'9C')

delete from Predare
where ClasaID='9C'

EXEC sp_columns Predare


-- trigger predare PROFESOR-MATERIE
IF EXISTS 
(
    SELECT 1 
    FROM sys.triggers 
    WHERE name = 'trg_VerificareProfesorMaterie'
)
BEGIN
    DROP TRIGGER trg_VerificareProfesorMaterie;
END
GO
CREATE TRIGGER trg_VerificareProfesorMaterie
ON Predare
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS 
	(
        SELECT 1
        FROM inserted i
        JOIN Predare p ON i.ProfesorID = p.ProfesorID
        WHERE i.MaterieID <> p.MaterieID
    )
    BEGIN
        RAISERROR ('Un profesor nu poate preda mai multe materii!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;




IF OBJECT_ID('Note', 'U') IS NOT NULL DROP TABLE Note
GO


CREATE TABLE Note (
  NotaID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Nota DECIMAL(4,2) NOT NULL,
  DataNota DATE NOT NULL,
  ElevID INT NOT NULL FOREIGN KEY REFERENCES Elevi(ElevID),
  PredareID INT NOT NULL FOREIGN KEY REFERENCES Predare(PredareID)
);


IF OBJECT_ID('Absente', 'U') IS NOT NULL DROP TABLE Absente
GO
CREATE TABLE Absente (
  AbsentaID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Data_absenta DATE NOT NULL,
  Motivata BIT NOT NULL,
  ElevID INT NOT NULL FOREIGN KEY REFERENCES Elevi(ElevID),
  PredareID INT NOT NULL FOREIGN KEY REFERENCES Predare(PredareID)
);

IF OBJECT_ID('Notificari', 'U') IS NOT NULL DROP TABLE Notificari
GO
CREATE TABLE Notificari (
  NotificareID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Data_Notificare DATE NOT NULL,
  Mesaj NVARCHAR(200) NOT NULL,
  ProfesorID INT NOT NULL FOREIGN KEY REFERENCES Profesori(ProfesorID),
  ParinteID INT NOT NULL FOREIGN KEY REFERENCES Parinti(ParinteID)
);



IF OBJECT_ID('OrarClase', 'U') IS NOT NULL DROP TABLE OrarClase
GO
CREATE TABLE OrarClase (
  ClasaID NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Clase(ClasaID),
  Zi_saptamana NVARCHAR(10) NOT NULL CHECK (Zi_saptamana IN ('Luni', 'Marti', 'Miercuri', 'Joi', 'Vineri')),
  IntervalID INT NOT NULL FOREIGN KEY REFERENCES IntervaleOre(IntervalID),
  PredareID INT NOT NULL FOREIGN KEY REFERENCES Predare(PredareID),
  CONSTRAINT PK_OrarClase PRIMARY KEY (ClasaID, Zi_saptamana, IntervalID)
);


-- Trigger pentru evitarea suprapunerii unui profesor la doua clase diferite in acelasi timp
IF EXISTS 
(
    SELECT 1 
    FROM sys.triggers 
    WHERE name = 'trg_EvitareSuprapunereProfesor'
)
BEGIN
    DROP TRIGGER trg_EvitareSuprapunereProfesor;
END
GO
CREATE TRIGGER trg_EvitareSuprapunereProfesor
ON OrarClase
AFTER INSERT, UPDATE
AS
BEGIN
  IF EXISTS (
    SELECT 1
    FROM OrarClase o1 
    JOIN inserted i ON o1.PredareID = i.PredareID
                    AND o1.IntervalID = i.IntervalID
                    AND o1.Zi_saptamana = i.Zi_saptamana
                    AND o1.ClasaID <> i.ClasaID
  )
  BEGIN
    RAISERROR ('Profesorul este deja alocat unei alte clase în acest interval orar.', 16, 1);
    ROLLBACK TRANSACTION;
  END
END;
GO

INSERT INTO OrarClase (ClasaID, Zi_saptamana, IntervalID, PredareID) VALUES
('10B', 'Luni', 2, 20)


INSERT INTO Clase (ClasaID, An_scolar, Specializare, Diriginte) VALUES
('9D', 2024, 'Geografie-Istorie', 5)


INSERT INTO Materii (Nume_materie) VALUES
('Matematica'),
('Informatica'),
('Biologie');



INSERT INTO OrarClase (ClasaID, Zi_saptamana, IntervalID, PredareID) VALUES
('10B', 'Luni', 1, 2),
('10B', 'Marti', 1, 3),
('10B', 'Miercuri', 1, 3),
('10B', 'Joi', 1, 2),
('10B', 'Vineri', 1, 4);

INSERT INTO OrarClase (ClasaID, Zi_saptamana, IntervalID, PredareID) VALUES
('10B', 'Luni', 3, 2),
('10B', 'Marti', 3, 3),
('10B', 'Miercuri', 3, 3),
('10B', 'Joi', 3, 2),
('10B', 'Vineri', 3, 4);

INSERT INTO Note (Nota, DataNota, ElevID, PredareID) VALUES
(9.50, '2024-11-01', 18, 3),
(8.75, '2024-11-15', 18, 2);

INSERT INTO Absente (Data_absenta, Motivata, ElevID, PredareID) VALUES
('2023-11-10', 0, 18, 2);


-- Trigger pt tabela Predare
IF EXISTS 
(
    SELECT 1 
    FROM sys.triggers 
    WHERE name = 'trg_Predare'
)
BEGIN
    DROP TRIGGER trg_Predare;
END
GO
CREATE TRIGGER trg_Predare
ON Predare
INSTEAD OF INSERT, UPDATE
AS
BEGIN
  IF EXISTS (
    SELECT 1
    FROM Predare p 
	join inserted i on p.ProfesorID = i.ProfesorID
	where p.ProfesorID = i.ProfesorID and p.MaterieID = i.MaterieID and p.ClasaID = i.ClasaID
  )
  BEGIN
    RAISERROR ('Exista deja o astfel de intrare in tabela Predare!', 16, 1);
    ROLLBACK TRANSACTION;
  END

  INSERT INTO Predare(ProfesorID, MaterieID, ClasaID) 
  SELECT i.ProfesorID,i.MaterieID,i.ClasaID
  FROM inserted i

END;
GO


INSERT INTO Predare (ProfesorID, MaterieID, ClasaID) VALUES
(3, 1, '10B')



INSERT INTO Clase (ClasaID, An_scolar, Specializare, Diriginte) VALUES
('9C', 2024, 'Geografie-Istorie', 5)
