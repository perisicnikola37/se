CREATE TABLE ExpenseGroup (
    id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT
);

CREATE TABLE Expense (
    id INT PRIMARY KEY AUTO_INCREMENT,
    Description TEXT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    ExpenseGroupId INT,
    FOREIGN KEY (ExpenseGroupId) REFERENCES ExpenseGroup(id)
);

CREATE TABLE IncomeGroup (
    id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Description TEXT
);

CREATE TABLE Income (
    id INT PRIMARY KEY AUTO_INCREMENT,
    Description TEXT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    IncomeGroupId INT,
    FOREIGN KEY (IncomeGroupId) REFERENCES IncomeGroup(id)
);

CREATE TABLE Reminder (
    id INT PRIMARY KEY AUTO_INCREMENT,
    ReminderDay DATE NOT NULL,
    Type VARCHAR(255) NOT NULL,
    Active BOOLEAN NOT NULL
);
