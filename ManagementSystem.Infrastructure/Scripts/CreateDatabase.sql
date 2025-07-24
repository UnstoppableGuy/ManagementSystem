-- Создание таблицы Employees
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Employees]') AND type in (N'U'))
BEGIN
    CREATE TABLE Employees (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Position NVARCHAR(100) NOT NULL,
        BirthYear INT NOT NULL CHECK (BirthYear >= YEAR(GETDATE())-100 AND BirthYear <= YEAR(GETDATE())-14),
        Salary DECIMAL(10, 2) NOT NULL CHECK (Salary >= 0),
        -- Защита от дубликатов
        CONSTRAINT UQ_Employee UNIQUE (FirstName, LastName, Position, BirthYear)
    );

    -- Индекс для ускорения фильтрации по должности
    CREATE NONCLUSTERED INDEX IX_Employees_Position ON Employees (Position);

    INSERT INTO Employees (FirstName, LastName, Position, BirthYear, Salary)
    VALUES 
    (N'Cаша', N'Иванов', N'Разработчик', 2000, 2000.00),
    (N'Паша', N'Петров', N'Разработчик', 2001, 3000.00),
    (N'Маша', N'Сидоров', N'Разработчик', 2002, 4000.00),
    (N'Даша', N'Кузнецова', N'Разработчик', 2003, 5000.00),
    (N'Дима', N'Смирнов', N'HR', 2004, 2345.00);
END
GO

-- Процедура GetEmployees
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetEmployees' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE GetEmployees AS SELECT Id, FirstName, LastName, Position, BirthYear, Salary FROM Employees ORDER BY LastName, FirstName')
END
GO

-- Процедура GetEmployeesByPosition  
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetEmployeesByPosition' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE GetEmployeesByPosition @Position NVARCHAR(100) AS SELECT Id, FirstName, LastName, Position, BirthYear, Salary FROM Employees WHERE Position = @Position ORDER BY LastName, FirstName')
END
GO

-- Процедура GetPositions
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetPositions' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE GetPositions AS SELECT DISTINCT Position FROM Employees ORDER BY Position')
END
GO

-- Процедура GetAverageSalaryByPosition
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'GetAverageSalaryByPosition' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE GetAverageSalaryByPosition AS SELECT Position, ROUND(AVG(Salary), 2) AS AverageSalary FROM Employees GROUP BY Position ORDER BY Position')
END
GO

-- Процедура AddEmployee
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'AddEmployee' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE AddEmployee @FirstName NVARCHAR(50), @LastName NVARCHAR(50), @Position NVARCHAR(100), @BirthYear INT, @Salary DECIMAL(10,2) AS INSERT INTO Employees (FirstName, LastName, Position, BirthYear, Salary) VALUES (@FirstName, @LastName, @Position, @BirthYear, @Salary)')
END
GO

-- Процедура DeleteEmployee
IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'DeleteEmployee' AND type = 'P')
BEGIN
    EXEC('CREATE PROCEDURE DeleteEmployee @Id INT AS DELETE FROM Employees WHERE Id = @Id')
END