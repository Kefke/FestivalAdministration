-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Machine: localhost
-- Genereertijd: 05 jan 2014 om 15:51
-- Serverversie: 5.6.13
-- PHP-versie: 5.4.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Databank: `festivaladmin`
--
CREATE DATABASE IF NOT EXISTS `festivaladmin` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `festivaladmin`;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `band`
--

CREATE TABLE IF NOT EXISTS `band` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Twitter` varchar(50) NOT NULL,
  `Facebook` varchar(50) NOT NULL,
  `Picture` mediumblob NOT NULL,
  `Description` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=15 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `bandgenre`
--

CREATE TABLE IF NOT EXISTS `bandgenre` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BandID` int(11) NOT NULL,
  `GenreID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Band_idx` (`BandID`),
  KEY `Genre_idx` (`GenreID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=49 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `contact`
--

CREATE TABLE IF NOT EXISTS `contact` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Company` varchar(50) NOT NULL,
  `Function` int(11) NOT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Tel` varchar(15) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Extra` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Function_idx` (`Function`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `contacttype`
--

CREATE TABLE IF NOT EXISTS `contacttype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Name` (`Name`),
  UNIQUE KEY `ID` (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=34 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `festival`
--

CREATE TABLE IF NOT EXISTS `festival` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `genre`
--

CREATE TABLE IF NOT EXISTS `genre` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=17 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `stage`
--

CREATE TABLE IF NOT EXISTS `stage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `ticket`
--

CREATE TABLE IF NOT EXISTS `ticket` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TicketHolder` varchar(50) NOT NULL,
  `TicketHolderEmail` varchar(50) NOT NULL,
  `TicketType` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `TicketType_idx` (`TicketType`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=20 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tickettype`
--

CREATE TABLE IF NOT EXISTS `tickettype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Price` float NOT NULL,
  `AvailableTickets` int(11) NOT NULL,
  `TicketsLeft` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `timeslot`
--

CREATE TABLE IF NOT EXISTS `timeslot` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BandID` int(11) NOT NULL,
  `StageID` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `BandID_idx` (`BandID`),
  KEY `StageID_idx` (`StageID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=9 ;

--
-- Beperkingen voor gedumpte tabellen
--

--
-- Beperkingen voor tabel `bandgenre`
--
ALTER TABLE `bandgenre`
  ADD CONSTRAINT `Band` FOREIGN KEY (`BandID`) REFERENCES `band` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `Genre` FOREIGN KEY (`GenreID`) REFERENCES `genre` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Beperkingen voor tabel `contact`
--
ALTER TABLE `contact`
  ADD CONSTRAINT `Function` FOREIGN KEY (`Function`) REFERENCES `contacttype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Beperkingen voor tabel `ticket`
--
ALTER TABLE `ticket`
  ADD CONSTRAINT `TicketType` FOREIGN KEY (`TicketType`) REFERENCES `tickettype` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Beperkingen voor tabel `timeslot`
--
ALTER TABLE `timeslot`
  ADD CONSTRAINT `BandID` FOREIGN KEY (`BandID`) REFERENCES `band` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `StageID` FOREIGN KEY (`StageID`) REFERENCES `stage` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
