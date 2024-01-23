CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `ExpenseGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_ExpenseGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IncomeGroups` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_IncomeGroups` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Reminders` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ReminderDayEnum` int NOT NULL,
    `ReminderDay` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Active` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Reminders` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `AccountTypeEnum` int NOT NULL,
    `AccountType` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Username` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Blogs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Author` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Text` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Blogs` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Blogs_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Expenses` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Amount` float NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ExpenseGroupId` int NOT NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Expenses` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Expenses_ExpenseGroups_ExpenseGroupId` FOREIGN KEY (`ExpenseGroupId`) REFERENCES `ExpenseGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Expenses_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Incomes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Amount` float NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `IncomeGroupId` int NOT NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_Incomes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Incomes_IncomeGroups_IncomeGroupId` FOREIGN KEY (`IncomeGroupId`) REFERENCES `IncomeGroups` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Incomes_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `Users` (`Id`, `AccountType`, `AccountTypeEnum`, `CreatedAt`, `Email`, `Password`, `Username`)
VALUES (1, 'Administrator', 1, TIMESTAMP '2024-01-22 11:47:29', 'admin@gmail.com', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 'Administrator');

CREATE INDEX `IX_Blogs_UserId` ON `Blogs` (`UserId`);

CREATE INDEX `IX_Expenses_ExpenseGroupId` ON `Expenses` (`ExpenseGroupId`);

CREATE INDEX `IX_Expenses_UserId` ON `Expenses` (`UserId`);

CREATE INDEX `IX_Incomes_IncomeGroupId` ON `Incomes` (`IncomeGroupId`);

CREATE INDEX `IX_Incomes_UserId` ON `Incomes` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240122104730_i', '8.0.1');