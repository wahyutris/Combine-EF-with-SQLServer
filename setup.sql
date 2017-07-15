DROP TABLE IF EXISTS Passenger
CREATE TABLE Passenger 
(	
	Id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    UserID NVARCHAR(50) NOT NULL UNIQUE,
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(25) NOT NULL,
	PhoneNumber VARCHAR(15) NOT NULL,
	BankName NVARCHAR(25) NOT NULL,
	BankAccountNumber VARCHAR(25) NOT NULL
)