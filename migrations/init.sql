-- Создание таблицы Users (Пользователи)
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

-- Создание таблицы Hotels (Отели)
CREATE TABLE Hotels (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL,
    Stars INT NOT NULL,
    PhoneNumber NVARCHAR(20),
    UserId INT NOT NULL,
    CONSTRAINT FK_Hotels_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Создание таблицы Rooms (Номера)
CREATE TABLE Rooms (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Number INT NOT NULL,
    HotelId INT NOT NULL,
    CONSTRAINT FK_Rooms_Hotels FOREIGN KEY (HotelId) REFERENCES Hotels(Id)
);
