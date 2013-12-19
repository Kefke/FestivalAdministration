SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `festivaladmin` ;
CREATE SCHEMA IF NOT EXISTS `festivaladmin` DEFAULT CHARACTER SET latin1 ;
USE `festivaladmin` ;

-- -----------------------------------------------------
-- Table `festivaladmin`.`band`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`band` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`band` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Twitter` VARCHAR(50) NOT NULL,
  `Facebook` VARCHAR(50) NOT NULL,
  `Picture` MEDIUMBLOB NOT NULL,
  `Description` VARCHAR(200) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`contacttype`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`contacttype` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`contacttype` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `Name` (`Name` ASC),
  UNIQUE INDEX `ID` (`ID` ASC))
ENGINE = InnoDB
AUTO_INCREMENT = 28
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`contact`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`contact` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`contact` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Company` VARCHAR(50) NOT NULL,
  `Function` INT(11) NOT NULL,
  `Street` VARCHAR(50) NULL DEFAULT NULL,
  `City` VARCHAR(50) NULL DEFAULT NULL,
  `Tel` VARCHAR(15) NOT NULL,
  `Email` VARCHAR(50) NOT NULL,
  `Extra` VARCHAR(250) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`),
  INDEX `Function_idx` (`Function` ASC),
  CONSTRAINT `Function`
    FOREIGN KEY (`Function`)
    REFERENCES `festivaladmin`.`contacttype` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 4;


-- -----------------------------------------------------
-- Table `festivaladmin`.`festival`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`festival` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`festival` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Street` VARCHAR(50) NULL DEFAULT NULL,
  `City` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`genre`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`genre` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`genre` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
AUTO_INCREMENT = 1
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`timeslot`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`timeslot` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`timeslot` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `BandID` INT NOT NULL,
  `StartDate` DATETIME NOT NULL,
  `EndDate` DATETIME NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `BandID_idx` (`BandID` ASC),
  CONSTRAINT `BandID`
    FOREIGN KEY (`BandID`)
    REFERENCES `festivaladmin`.`band` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `festivaladmin`.`stage`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`stage` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`stage` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `TimeSlotID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `TimeSlotID_idx` (`TimeSlotID` ASC),
  CONSTRAINT `TimeSlotID`
    FOREIGN KEY (`TimeSlotID`)
    REFERENCES `festivaladmin`.`timeslot` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`tickettype`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`tickettype` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`tickettype` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  `Price` FLOAT NOT NULL,
  `AvailableTickets` INT(11) NOT NULL,
  `TicketsLeft` INT(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE INDEX `Name` (`Name` ASC))
ENGINE = InnoDB
AUTO_INCREMENT = 6
DEFAULT CHARACTER SET = latin1;


-- -----------------------------------------------------
-- Table `festivaladmin`.`ticket`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`ticket` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`ticket` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `TicketHolder` VARCHAR(50) NOT NULL,
  `TicketHolderEmail` VARCHAR(50) NOT NULL,
  `TicketType` INT(11) NOT NULL,
  `Amount` INT(11) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `TicketType_idx` (`TicketType` ASC),
  CONSTRAINT `TicketType`
    FOREIGN KEY (`TicketType`)
    REFERENCES `festivaladmin`.`tickettype` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 7;


-- -----------------------------------------------------
-- Table `festivaladmin`.`bandgenre`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `festivaladmin`.`bandgenre` ;

CREATE TABLE IF NOT EXISTS `festivaladmin`.`bandgenre` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `BandID` INT NOT NULL,
  `GenreID` INT NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `Band_idx` (`BandID` ASC),
  INDEX `Genre_idx` (`GenreID` ASC),
  CONSTRAINT `Band`
    FOREIGN KEY (`BandID`)
    REFERENCES `festivaladmin`.`band` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Genre`
    FOREIGN KEY (`GenreID`)
    REFERENCES `festivaladmin`.`genre` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
