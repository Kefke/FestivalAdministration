-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Machine: localhost
-- Genereertijd: 23 dec 2013 om 21:19
-- Serverversie: 5.6.13
-- PHP-versie: 5.4.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Databank: `festivaladminlogins`
--
CREATE DATABASE IF NOT EXISTS `festivaladminlogins` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `festivaladminlogins`;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `__migrationhistory`
--

CREATE TABLE IF NOT EXISTS `__migrationhistory` (
  `MigrationId` varchar(255) CHARACTER SET utf8 NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `userprofile`
--

CREATE TABLE IF NOT EXISTS `userprofile` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` longtext NOT NULL,
  `Email` longtext,
  `Facebook` longtext,
  `Age` int(11) DEFAULT NULL,
  `Rate` double DEFAULT NULL,
  `LastName` longtext,
  `FirstName` longtext,
  `Discriminator` varchar(128) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `webpages_membership`
--

CREATE TABLE IF NOT EXISTS `webpages_membership` (
  `UserId` int(11) NOT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `ConfirmationToken` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `IsConfirmed` tinyint(1) DEFAULT NULL,
  `LastPasswordFailureDate` datetime DEFAULT NULL,
  `PasswordFailuresSinceLastSuccess` int(11) NOT NULL,
  `Password` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `PasswordChangedDate` datetime DEFAULT NULL,
  `PasswordSalt` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `PasswordVerificationToken` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `PasswordVerificationTokenExpirationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `webpages_oauthmembership`
--

CREATE TABLE IF NOT EXISTS `webpages_oauthmembership` (
  `Provider` varchar(30) CHARACTER SET utf8 NOT NULL,
  `ProviderUserId` varchar(100) CHARACTER SET utf8 NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`Provider`,`ProviderUserId`),
  KEY `UserId` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `webpages_oauthtoken`
--

CREATE TABLE IF NOT EXISTS `webpages_oauthtoken` (
  `Token` varchar(100) CHARACTER SET utf8 NOT NULL,
  `Secret` varchar(100) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`Token`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `webpages_roles`
--

CREATE TABLE IF NOT EXISTS `webpages_roles` (
  `RoleId` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(256) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Gegevens worden uitgevoerd voor tabel `webpages_roles`
--

INSERT INTO `webpages_roles` (`RoleId`, `RoleName`) VALUES
(1, 'Administrators');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `webpages_usersinroles`
--

CREATE TABLE IF NOT EXISTS `webpages_usersinroles` (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `UsersInRoles_Role` (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Beperkingen voor gedumpte tabellen
--

--
-- Beperkingen voor tabel `webpages_membership`
--
ALTER TABLE `webpages_membership`
  ADD CONSTRAINT `Membership_UserProfile` FOREIGN KEY (`UserId`) REFERENCES `userprofile` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Beperkingen voor tabel `webpages_oauthmembership`
--
ALTER TABLE `webpages_oauthmembership`
  ADD CONSTRAINT `OAuthMembership_UserProfile` FOREIGN KEY (`UserId`) REFERENCES `userprofile` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Beperkingen voor tabel `webpages_usersinroles`
--
ALTER TABLE `webpages_usersinroles`
  ADD CONSTRAINT `UsersInRoles_Role` FOREIGN KEY (`RoleId`) REFERENCES `webpages_roles` (`RoleId`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `UsersInRoles_UserProfile` FOREIGN KEY (`UserId`) REFERENCES `userprofile` (`UserId`) ON DELETE CASCADE ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
