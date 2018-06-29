/*
Generated Date: 04-Sep-2015 11:15:17
Generated User: SCHRODERSAD\hoangd
*/

PRINT N'Merging static data to [Suppliers]';
GO

BEGIN TRY
SET IDENTITY_INSERT [Suppliers] ON
END TRY
BEGIN CATCH
END CATCH
GO

MERGE INTO [Suppliers] AS Target
USING( VALUES
(1,N'Exotic Liquids',N'Charlotte Cooper',N'Purchasing Manager',N'49 Gilbert St.',N'London',NULL,N'EC1 4SD',N'UK',N'(171) 555-2222',NULL,NULL),
(2,N'New Orleans Cajun Delights',N'Shelley Burke',N'Order Administrator',N'P.O. Box 78934',N'New Orleans',N'LA',N'70117',N'USA',N'(100) 555-4822',NULL,N'#CAJUN.HTM#'),
(3,N'Grandma Kelly''s Homestead',N'Regina Murphy',N'Sales Representative',N'707 Oxford Rd.',N'Ann Arbor',N'MI',N'48104',N'USA',N'(313) 555-5735',N'(313) 555-3349',NULL),
(4,N'Tokyo Traders',N'Yoshi Nagase',N'Marketing Manager',N'9-8 Sekimai Musashino-shi',N'Tokyo',NULL,N'100',N'Japan',N'(03) 3555-5011',NULL,NULL),
(5,N'Cooperativa de Quesos ''Las Cabras''',N'Antonio del Valle Saavedra',N'Export Administrator',N'Calle del Rosal 4',N'Oviedo',N'Asturias',N'33007',N'Spain',N'(98) 598 76 54',NULL,NULL),
(6,N'Mayumi''s',N'Mayumi Ohno',N'Marketing Representative',N'92 Setsuko Chuo-ku',N'Osaka',NULL,N'545',N'Japan',N'(06) 431-7877',NULL,N'Mayumi''s (on the World Wide Web)#http://www.microsoft.com/accessdev/sampleapps/mayumi.htm#'),
(7,N'Pavlova, Ltd.',N'Ian Devling',N'Marketing Manager',N'74 Rose St. Moonie Ponds',N'Melbourne',N'Victoria',N'3058',N'Australia',N'(03) 444-2343',N'(03) 444-6588',NULL),
(8,N'Specialty Biscuits, Ltd.',N'Peter Wilson',N'Sales Representative',N'29 King''s Way',N'Manchester',NULL,N'M14 GSD',N'UK',N'(161) 555-4448',NULL,NULL),
(9,N'PB Knäckebröd AB',N'Lars Peterson',N'Sales Agent',N'Kaloadagatan 13',N'Göteborg',NULL,N'S-345 67',N'Sweden',N'031-987 65 43',N'031-987 65 91',NULL),
(10,N'Refrescos Americanas LTDA',N'Carlos Diaz',N'Marketing Manager',N'Av. das Americanas 12.890',N'Sao Paulo',NULL,N'5442',N'Brazil',N'(11) 555 4640',NULL,NULL),
(11,N'Heli Süßwaren GmbH & Co. KG',N'Petra Winkler',N'Sales Manager',N'Tiergartenstraße 5',N'Berlin',NULL,N'10785',N'Germany',N'(010) 9984510',NULL,NULL),
(12,N'Plutzer Lebensmittelgroßmärkte AG',N'Martin Bein',N'International Marketing Mgr.',N'Bogenallee 51',N'Frankfurt',NULL,N'60439',N'Germany',N'(069) 992755',NULL,N'Plutzer (on the World Wide Web)#http://www.microsoft.com/accessdev/sampleapps/plutzer.htm#'),
(13,N'Nord-Ost-Fisch Handelsgesellschaft mbH',N'Sven Petersen',N'Coordinator Foreign Markets',N'Frahmredder 112a',N'Cuxhaven',NULL,N'27478',N'Germany',N'(04721) 8713',N'(04721) 8714',NULL),
(14,N'Formaggi Fortini s.r.l.',N'Elio Rossi',N'Sales Representative',N'Viale Dante, 75',N'Ravenna',NULL,N'48100',N'Italy',N'(0544) 60323',N'(0544) 60603',N'#FORMAGGI.HTM#'),
(15,N'Norske Meierier',N'Beate Vileid',N'Marketing Manager',N'Hatlevegen 5',N'Sandvika',NULL,N'1320',N'Norway',N'(0)2-953010',NULL,NULL),
(16,N'Bigfoot Breweries',N'Cheryl Saylor',N'Regional Account Rep.',N'3400 - 8th Avenue Suite 210',N'Bend',N'OR',N'97101',N'USA',N'(503) 555-9931',NULL,NULL),
(17,N'Svensk Sjöföda AB',N'Michael Björn',N'Sales Representative',N'Brovallavägen 231',N'Stockholm',NULL,N'S-123 45',N'Sweden',N'08-123 45 67',NULL,NULL),
(18,N'Aux joyeux ecclésiastiques',N'Guylène Nodier',N'Sales Manager',N'203, Rue des Francs-Bourgeois',N'Paris',NULL,N'75004',N'France',N'(1) 03.83.00.68',N'(1) 03.83.00.62',NULL),
(19,N'New England Seafood Cannery',N'Robb Merchant',N'Wholesale Account Agent',N'Order Processing Dept. 2100 Paul Revere Blvd.',N'Boston',N'MA',N'02134',N'USA',N'(617) 555-3267',N'(617) 555-3389',NULL),
(20,N'Leka Trading',N'Chandra Leka',N'Owner',N'471 Serangoon Loop, Suite #402',N'Singapore',NULL,N'0512',N'Singapore',N'555-8787',NULL,NULL),
(21,N'Lyngbysild',N'Niels Petersen',N'Sales Manager',N'Lyngbysild Fiskebakken 10',N'Lyngby',NULL,N'2800',N'Denmark',N'43844108',N'43844115',NULL),
(22,N'Zaanse Snoepfabriek',N'Dirk Luchte',N'Accounting Manager',N'Verkoop Rijnweg 22',N'Zaandam',NULL,N'9999 ZZ',N'Netherlands',N'(12345) 1212',N'(12345) 1210',NULL),
(23,N'Karkki Oy',N'Anne Heikkonen',N'Product Manager',N'Valtakatu 12',N'Lappeenranta',NULL,N'53120',N'Finland',N'(953) 10956',NULL,NULL),
(24,N'G''day, Mate',N'Wendy Mackenzie',N'Sales Representative',N'170 Prince Edward Parade Hunter''s Hill',N'Sydney',N'NSW',N'2042',N'Australia',N'(02) 555-5914',N'(02) 555-4873',N'G''day Mate (on the World Wide Web)#http://www.microsoft.com/accessdev/sampleapps/gdaymate.htm#'),
(25,N'Ma Maison',N'Jean-Guy Lauzon',N'Marketing Manager',N'2960 Rue St. Laurent',N'Montréal',N'Québec',N'H1J 1C3',N'Canada',N'(514) 555-9022',NULL,NULL),
(26,N'Pasta Buttini s.r.l.',N'Giovanni Giudici',N'Order Administrator',N'Via dei Gelsomini, 153',N'Salerno',NULL,N'84100',N'Italy',N'(089) 6547665',N'(089) 6547667',NULL),
(27,N'Escargots Nouveaux',N'Marie Delamare',N'Sales Manager',N'22, rue H. Voiron',N'Montceau',NULL,N'71300',N'France',N'85.57.00.07',NULL,NULL),
(28,N'Gai pâturage',N'Eliane Noz',N'Sales Representative',N'Bat. B 3, rue des Alpes',N'Annecy',NULL,N'74000',N'France',N'38.76.98.06',N'38.76.98.58',NULL),
(29,N'Forêts d''érables',N'Chantal Goulet',N'Accounting Manager',N'148 rue Chasseur',N'Ste-Hyacinthe',N'Québec',N'J2S 7S8',N'Canada',N'(514) 555-2955',N'(514) 555-2921',NULL)
)AS Source([SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax],[HomePage])
ON Target.[SupplierID] = Source.[SupplierID]
WHEN MATCHED THEN
UPDATE SET [CompanyName] = Source.[CompanyName],
[ContactName] = Source.[ContactName],
[ContactTitle] = Source.[ContactTitle],
[Address] = Source.[Address],
[City] = Source.[City],
[Region] = Source.[Region],
[PostalCode] = Source.[PostalCode],
[Country] = Source.[Country],
[Phone] = Source.[Phone],
[Fax] = Source.[Fax],
[HomePage] = Source.[HomePage]
WHEN NOT MATCHED BY TARGET THEN
INSERT([SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax],[HomePage])
VALUES([SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax],[HomePage])
;
GO

BEGIN TRY
SET IDENTITY_INSERT [Suppliers] OFF
END TRY
BEGIN CATCH
END CATCH
GO

PRINT N'Completed merge static data to Suppliers';

