-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Machine: localhost
-- Genereertijd: 15 dec 2013 om 12:23
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
  `Genre1` int(11) NOT NULL,
  `Genre2` int(11) DEFAULT NULL,
  `Genre3` int(11) DEFAULT NULL,
  `Twitter` varchar(50) NOT NULL,
  `Facebook` varchar(50) NOT NULL,
  `Picture` mediumblob NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Gegevens worden uitgevoerd voor tabel `contact`
--

INSERT INTO `contact` (`ID`, `Name`, `Company`, `Function`, `Street`, `City`, `Tel`, `Email`, `Extra`) VALUES
(1, 'Kefke', 'Kefkes Company', 25, 'Boerderijstraat 404', '8500 Kortrijk', '0478000000', 'kevin.aelterman@student.howest.be', 'Dummy data'),
(2, 'Eva D', 'Evas Company', 27, 'Stationstraat 12', 'Gent', '020000000', 'evake@gmail.be', 'More dummy data'),
(3, 'Nina', 'Electrabel', 17, 'Schippersstraat 2', 'Brugge', '024562130', 'nina@electrabel.be', 'Werkt alleen op maandag, dinsdag en woensdag');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=28 ;

--
-- Gegevens worden uitgevoerd voor tabel `contacttype`
--

INSERT INTO `contacttype` (`ID`, `Name`) VALUES
(27, 'Band manager'),
(26, 'Band member'),
(24, 'Bartender'),
(25, 'Security'),
(17, 'Ticket seller');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `daterange`
--

CREATE TABLE IF NOT EXISTS `daterange` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LineupID` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Gegevens worden uitgevoerd voor tabel `festival`
--

INSERT INTO `festival` (`ID`, `Name`, `Street`, `City`) VALUES
(1, 'Awesome Festival', 'Awesomestreet 54', '12345 Surfcity');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `genre`
--

CREATE TABLE IF NOT EXISTS `genre` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `lineup`
--

CREATE TABLE IF NOT EXISTS `lineup` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `DateRange` int(11) NOT NULL,
  `StageID` int(11) NOT NULL,
  `BandID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `stage`
--

CREATE TABLE IF NOT EXISTS `stage` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LineupID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Gegevens worden uitgevoerd voor tabel `tickettype`
--

INSERT INTO `tickettype` (`ID`, `Name`, `Price`, `AvailableTickets`, `TicketsLeft`) VALUES
(1, 'Normal', 50, 10000, 10000),
(2, 'VIP', 150, 100, 100),
(5, 'Exclusive', 249.99, 20, 20);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
