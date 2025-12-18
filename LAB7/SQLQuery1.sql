-- Создание базы данных
USE master;
GO

IF DB_ID('publishing') IS NOT NULL
    DROP DATABASE publishing;
GO

CREATE DATABASE publishing;
GO

USE publishing;
GO

-- Создание таблицы Authors
CREATE TABLE Authors (
    id_Author INT PRIMARY KEY IDENTITY(1,1),
    Surname NVARCHAR(100) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(500)
);

-- Создание таблицы TypesOfProducts
CREATE TABLE TypesOfProducts (
    id_Type INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(100) NOT NULL
);

-- Создание таблицы Customers
CREATE TABLE Customers (
    id_Customer INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Address NVARCHAR(500),
    Phone NVARCHAR(50)
);

-- Создание таблицы Offices
CREATE TABLE Offices (
    id_Office INT PRIMARY KEY IDENTITY(1,1),
    Office NVARCHAR(200) NOT NULL,
    Address NVARCHAR(500),
    Phone NVARCHAR(50)
);

-- Создание таблицы Publications
CREATE TABLE Publications (
    id_Publication INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(300) NOT NULL,
    Author INT NULL,
    ReleaseYear INT,
    VolumeOfSheets INT,
    Circulation INT
);

-- Создание таблицы Orders
CREATE TABLE Orders (
    id_Order INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200),
    Customer INT,
    Type INT,
    Publication INT,
    Office INT,
    DateOfAdmission DATETIME,
    DateOfCompletion DATETIME,
    Price DECIMAL(10,2)
);

-- Вставка данных в таблицу Authors
INSERT INTO Authors (Surname, Name, Address) VALUES
(N'Бегинина', N'Александра', N'ДНР, Донецк, ул. Университетская, д. 125, кв. 10'),
(N'Биренюк', N'Екатерина', N'Россия, Таганрог, ул. Ленина, д. 115, кв. 8'),
(N'Волошин', N'Игорь', N'Россия, Таганрог, ул. Греческая, д. 38, кв. 10'),
(N'Грин', N'Джон', N'United States, New York, Hudson Street, 345'),
(N'Игнатьева', N'Ирина', N'Россия, Белгород, ул. Курская, д. 38, кв. 10'),
(N'Кинг', N'Стивен', N'United States, Bangor, Florida Avenue, 49'),
(N'Коллинз', N'Сьюзан', N'United States, Valley Village, Suite 103'),
(N'Копейкина', N'Валерия', N'Россия, Ростов-на-Дону, ул. Большая Садовая, д. 125, кв. 4'),
(N'Роулинг', N'Джоан', N'London, England, Soho Avenue 38'),
(N'Шипарев', N'Александр', N'Россия, Ростов-на-Дону, ул. Береговая, д. 6, кв. 115'),
(N'Эшер', N'Джей', N'United States, New York, Hudson Street, 375');

-- Вставка данных в таблицу TypesOfProducts
INSERT INTO TypesOfProducts (Type) VALUES
(N'Буклет'),
(N'Журнал'),
(N'Брошюра'),
(N'Листовка'),
(N'Плакат'),
(N'Каталог'),
(N'Книга'),
(N'Календарь'),
(N'Наклейки'),
(N'Папка'),
(N'Карта'),
(N'Визитка');

-- Вставка данных в таблицу Customers
INSERT INTO Customers (Name, Type, Address, Phone) VALUES
(N'Книжный магазин "Читай Город"', N'Организация', N'Россия, Ростов-на-Дону, ул. Большая Садовая, д. 215', N'+7 (919) 457-15-55'),
(N'Книжный магазин "Буклет"', N'Организация', N'ДНР, Макеевка, просп. 250-летия Донбасса, 4А', N'+7 (949) 322-01-01'),
(N'Книжный магазин "Букинист"', N'Организация', N'ДНР, Макеевка, ул. Свердлова, 129', N'+7 (949) 091-18-90'),
(N'Книжный магазин "Любимая книга"', N'Организация', N'Россия, Белгород, ул. Ленина, д. 45', N'+7 (928) 016-15-08'),
(N'Никополь И. П.', N'Частное лицо', N'Россия, Москва, пер. Газетный, д. 218', N'+7 (985) 475-02-11'),
(N'Анаев А. Д.', N'Частное лицо', N'ДНР, Макеевка, ул. Ленина, д. 215', N'+7 (949) 315-18-05'),
(N'Гелий С. А.', N'Частное лицо', N'Россия, Ростов-на-Дону, ул. Береговая, д. 7', N'+7 (919) 230-98-95'),
(N'Лавин А. С.', N'Частное лицо', N'ДНР, Донецк, ул. Ленина, д. 288', N'+7 (949) 231-22-33'),
(N'Кураев И. В.', N'Частное лицо', N'Россия, Таганрог, ул. Горького, д. 105', N'+7 (918) 455-21-11');

-- Вставка данных в таблицу Offices
INSERT INTO Offices (Office, Address, Phone) VALUES
(N'ООО "ВОЛЬФ ТИПОГРАФИЯ"', N'ДНР, Донецк, ул. Университетская, д. 125', N'+7 (919) 457-15-55'),
(N'"МОСКВА"', N'Россия, Москва, ул. Ленина, стр. 1, корп. 2', N'+7 (928) 021-45-48'),
(N'"Рекламный квартал"', N'Россия, Таганрог, ул. Греческая, д. 205', N'+7 (918) 455-18-22'),
(N'ООО "РусПринтСтудия"', N'Россия, Москва, ул. Габаритная, д. 212Б', N'+7 (928) 016-15-08'),
(N'"РОСТОВСКАЯ ГОРОДСКАЯ ТИПОГРАФИЯ"', N'Россия, Белгород, ул. Курская, д. 58', N'+7 (995) 475-02-11'),
(N'"КАТРАН КПК"', N'ДНР, Макеевка, ул. Московская, 121', N'+7 (949) 315-18-05'),
(N'"ПРИНТ ПЛЮС"', N'Россия, Ростов-на-Дону, ул. Михаила Нагибина, д. 308А', N'+7 (919) 230-98-95'),
(N'"ЦИФРОВАЯ ТИПОГРАФИЯ"', N'Россия, Ростов-на-Дону, ул. Большая Садовая, д. 218', N'+7 (919) 198-08-05'),
(N'"Ardy"', N'Россия, Ростов-на-Дону, пр. Ворошиловский, д. 59', N'+7 (919) 123-12-13'),
(N'"Ice Print"', N'Россия, Ростов-на-Дону, ул. Береговая, д. 5', N'+7 (919) 189-28-25'),
(N'"Веритакс"', N'Россия, Таганрог, ул. Петровская, д. 22', N'+7 (918) 231-08-12'),
(N'Типография "ТЭС"', N'Россия, Таганрог, пер. Гарибальди, д. 9', N'+7 (918) 222-01-02'),
(N'"Триада-М"', N'Россия, Таганрог, ул. Октябрьская, д. 35', N'+7 (918) 331-38-22'),
(N'"Universal Print"', N'ДНР, Донецк, ул. Артёма, д. 225', N'+7 (949) 444-12-12'),
(N'"ТВОРИ"', N'ДНР, Донецк, ул. Щорса, д. 17', N'+7 (949) 648-15-05'),
(N'"ТИПОГРАФИЯ №1"', N'ДНР, Харцызск, ул. Ленина, д. 12', N'+7 (949) 228-88-55'),
(N'Городская типография "Санкт-Петербург"', N'Россия, Санкт-Петербург, ул. Восстания, 1', N'+7 (985) 478-48-54');

-- Вставка данных в таблицу Publications (сначала без авторов)
INSERT INTO Publications (Name, ReleaseYear, VolumeOfSheets, Circulation) VALUES
(N'"Зелёная миля"', 1999, 498, 145000),
(N'"Оно"', 1986, 1138, 150000),
(N'"Кладбище домашних животных"', 1983, 373, 28000),
(N'"Ловец снов"', 2002, 736, 158000),
(N'"Возрождение"', 2014, 489, 250000),
(N'"Фантастическае твари и где они обитают"', 2001, 127, 600000),
(N'"На службе зла"', 2015, 512, 950000),
(N'"Гарри Поттер и философский камень"', 1997, 456, 1000000),
(N'"Гарри Поттер и Кубок Огня"', 2002, 636, 900000),
(N'"Гарри Поттер и Орден Феникса"', 2004, 766, 125000),
(N'"Гарри Поттер и Дары Смерти"', 2008, 607, 130000),
(N'"Бумажные города"', 2008, 305, 150000),
(N'"13 причин почему"', 2011, 144, 1200),
(N'"Голодные игры"', 2008, 384, 15000),
(N'"Casual"', 2024, 28, 15000),
(N'"М О Д А +"', 2024, 16, 25000),
(N'"Music Plus"', 2024, 24, 18000),
(N'"A V O N"', 2024, 20, 2500),
(N'Реклама магазина "iStore"', 2024, 5, 15000),
(N'Туристическое агенство "PRADA"', 2024, 2, 1500);

-- Обновляем авторов для публикаций
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Зелёная миля"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Оно"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Кладбище домашних животных"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Ловец снов"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Возрождение"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Кинг') WHERE Name = N'"Фантастическае твари и где они обитают"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Роулинг') WHERE Name = N'"На службе зла"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Роулинг') WHERE Name = N'"Гарри Поттер и философский камень"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Роулинг') WHERE Name = N'"Гарри Поттер и Кубок Огня"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Роулинг') WHERE Name = N'"Гарри Поттер и Орден Феникса"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Роулинг') WHERE Name = N'"Гарри Поттер и Дары Смерти"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Грин') WHERE Name = N'"Бумажные города"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Эшер') WHERE Name = N'"13 причин почему"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Коллинз') WHERE Name = N'"Голодные игры"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Биренюк') WHERE Name = N'"Casual"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Бегинина') WHERE Name = N'"М О Д А +"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Шипарев') WHERE Name = N'"Music Plus"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Копейкина') WHERE Name = N'"A V O N"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Игнатьева') WHERE Name = N'Реклама магазина "iStore"';
UPDATE Publications SET Author = (SELECT id_Author FROM Authors WHERE Surname = N'Волошин') WHERE Name = N'Туристическое агенство "PRADA"';

-- Вставка данных в таблицу Orders с использованием CONVERT для безопасного преобразования дат
INSERT INTO Orders (Name, Customer, Type, Publication, Office, DateOfAdmission, DateOfCompletion, Price) VALUES
(N'Заказ 1', 1, 7, 1, 1, CONVERT(DATETIME, '2024-06-01', 120), CONVERT(DATETIME, '2024-07-01', 120), 375.00),
(N'Заказ 2', 1, 7, 2, 2, CONVERT(DATETIME, '2024-06-06', 120), CONVERT(DATETIME, '2024-07-06', 120), 1345.00),
(N'Заказ 3', 1, 7, 3, 3, CONVERT(DATETIME, '2024-06-15', 120), CONVERT(DATETIME, '2024-07-15', 120), 450.00),
(N'Заказ 4', 1, 7, 4, 4, CONVERT(DATETIME, '2024-06-21', 120), CONVERT(DATETIME, '2024-07-21', 120), 388.00),
(N'Заказ 5', 1, 7, 5, 5, CONVERT(DATETIME, '2024-06-30', 120), CONVERT(DATETIME, '2024-07-30', 120), 468.00),
(N'Заказ 6', 2, 7, 6, 6, CONVERT(DATETIME, '2024-07-03', 120), CONVERT(DATETIME, '2024-08-03', 120), 856.00),
(N'Заказ 7', 2, 7, 7, 7, CONVERT(DATETIME, '2024-07-09', 120), CONVERT(DATETIME, '2024-08-09', 120), 748.00),
(N'Заказ 8', 2, 7, 8, 8, CONVERT(DATETIME, '2024-07-15', 120), CONVERT(DATETIME, '2024-08-15', 120), 935.00),
(N'Заказ 9', 3, 7, 9, 9, CONVERT(DATETIME, '2024-07-21', 120), CONVERT(DATETIME, '2024-08-21', 120), 1200.00),
(N'Заказ 10', 3, 7, 10, 10, CONVERT(DATETIME, '2024-07-29', 120), CONVERT(DATETIME, '2024-08-29', 120), 845.00),
(N'Заказ 11', 3, 7, 11, 11, CONVERT(DATETIME, '2024-07-30', 120), CONVERT(DATETIME, '2024-08-30', 120), 825.00),
(N'Заказ 12', 4, 7, 12, 4, CONVERT(DATETIME, '2024-08-01', 120), CONVERT(DATETIME, '2024-08-04', 120), 954.00),
(N'Заказ 13', 4, 7, 13, 12, CONVERT(DATETIME, '2024-08-02', 120), CONVERT(DATETIME, '2024-08-06', 120), 1150.00),
(N'Заказ 14', 4, 7, 14, 6, CONVERT(DATETIME, '2024-08-07', 120), CONVERT(DATETIME, '2024-08-11', 120), 745.00),
(N'Заказ 15', 5, 2, 15, 13, CONVERT(DATETIME, '2024-08-10', 120), CONVERT(DATETIME, '2024-08-12', 120), 320.00),
(N'Заказ 16', 6, 2, 16, 14, CONVERT(DATETIME, '2024-08-11', 120), CONVERT(DATETIME, '2024-08-12', 120), 245.00),
(N'Заказ 17', 7, 2, 17, 15, CONVERT(DATETIME, '2024-08-15', 120), CONVERT(DATETIME, '2024-08-16', 120), 198.00),
(N'Заказ 18', 8, 6, 18, 16, CONVERT(DATETIME, '2024-08-25', 120), CONVERT(DATETIME, '2024-09-25', 120), 224.00),
(N'Заказ 19', 8, 4, 19, 17, CONVERT(DATETIME, '2024-08-28', 120), CONVERT(DATETIME, '2024-09-28', 120), 158.00),
(N'Заказ 20', 9, 12, 20, 3, CONVERT(DATETIME, '2024-08-30', 120), CONVERT(DATETIME, '2024-09-30', 120), 125.00);

-- Добавление внешних ключей
ALTER TABLE Publications
ADD CONSTRAINT FK_Publications_Authors
FOREIGN KEY (Author) REFERENCES Authors(id_Author);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers
FOREIGN KEY (Customer) REFERENCES Customers(id_Customer);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Types
FOREIGN KEY (Type) REFERENCES TypesOfProducts(id_Type);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Publications
FOREIGN KEY (Publication) REFERENCES Publications(id_Publication);

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Offices
FOREIGN KEY (Office) REFERENCES Offices(id_Office);

-- Проверка создания базы данных
PRINT 'База данных publishing успешно создана!';

-- Показать статистику
SELECT 
    (SELECT COUNT(*) FROM Authors) AS 'Количество авторов',
    (SELECT COUNT(*) FROM Publications) AS 'Количество публикаций',
    (SELECT COUNT(*) FROM Orders) AS 'Количество заказов',
    (SELECT COUNT(*) FROM Customers) AS 'Количество клиентов',
    (SELECT COUNT(*) FROM Offices) AS 'Количество офисов',
    (SELECT COUNT(*) FROM TypesOfProducts) AS 'Количество типов продуктов';

-- Вывести первые 5 записей из каждой таблицы для проверки
PRINT '--- Первые 5 авторов ---';
SELECT TOP 5 * FROM Authors;

PRINT '--- Первые 5 публикаций ---';
SELECT TOP 5 * FROM Publications;

PRINT '--- Первые 5 заказов ---';
SELECT TOP 5 * FROM Orders;