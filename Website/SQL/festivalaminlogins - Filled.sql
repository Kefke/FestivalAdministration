-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Machine: localhost
-- Genereertijd: 24 dec 2013 om 23:26
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

--
-- Gegevens worden uitgevoerd voor tabel `__migrationhistory`
--

INSERT INTO `__migrationhistory` (`MigrationId`, `Model`, `ProductVersion`) VALUES
('201312241554304_InitialCreate', 0x1f8b0800000000000400e55cdd6ee33616be2fb0ef20e8aa2d502bc9748a76e0b4c8e467116c33198cd3de0e18897688529456a2d3e4d9f6621f695fa1a47e29fe4814257beced45025b22bf439ef3f1f0883ac7fffbcf7f97bfbcc4d87b86598e1272ee9f2e4e7c0f92308910d99cfb5bbafeee47ff979ffff1d5f23a8a5fbcdfeb766f783bd693e4e7fe13a5e9bb20c8c32718837c11a3304bf2644d17611207204a82b393939f82d3d30032089f6179def2d3965014c3e20bfb7a999010a6740bf05d12419c57d7d99d5581ea7d0031cc5310c273ffee75f56fbc42718ae11d8c1fd9889e50bab802a1ef5d6004d8805610af7d2ffdfedd6f395cd12c219b550a2802f8e13585ecfe1ae01c56c37f977e6f3b8393333e8300109250069710270df8cddcd8ecae9916e82b1f5631c3739f8d38fb982529cce8abefbd0739e437cb292daa9b6b84a188c270ea2e15ca750c10f6bdaa2bcd982d7def06bdc0e8574836f4a951c11d78a9afb08fbef71b41ccf4ac13cdb6ecf6872dc6e01137df835ea9bf829cf24f7b177c83b2bd485e06adbd6caca831d4bfe06be78230994f702d74bf8d9459077267590f75bf5209b784be391327542980ad8b154d32f84f486006288c3e024a61c616f56d048b990ce99bcb995ddd557b277db77ee098d4fd212183d4becc20eb73c5fe6a39fcf3038a472f12e662d7288b0bc7f590fc01c948eb9d9efd387199dee6d51860a3b4f7498221202e9ee623c8f33f932cba61ae6e9bcda22209325f21b62b7159ab6d18c23c1f32b51dfadef55e0bbe7c026403a33935b502987eb1f9b06004ad51f805196d1cc9f54b8ab2e2ab93b63f8067b429ba6b5c4ebdb1789f202eda70bf574508ad23fcdc697a9325f1a704775ca5d8e2f32ad966211f68d2dbec01641b481dbdf4fdc5963e4d73d5ecca338a60266bacb7b1ab7f6f858da2d59b93e15d6edaa6204f6c1ceb4f763e3ebb8d712ece4bb43211bfa799c2febeb6d39740e9ab1cd85f39b9b13c76f28dbb67c90a86191cbd7bf48e6b42c8ce0def6213decfc5b9d4fd761dab73390eb1fad9db1f76f76894df123eac7cbe607d56d3ec23ae9f6a7ead0c67172a1ac5e43f4d6d14e7696ca8f39c7da32e96e4e070cb568671f2fffd032c5a8cf1e917799e84a81892f2f4f9d9fce07f4d22cf2e026bcf7b8458c9bbdb628a528c4236a473ff64b13855143928a29ea6e1484992a10a604b0b66dced00cc9ee4729a0144a8ba0ed953134a01b61a8bd47bdc920f1a59f29d2b9842c23da495cea70da29125b999216d2d03814afd0ceb0b714c1cb08a775a22c8c1b944866f7bd866132dd9524ed6ee3db9821852e85d84b43878be04790822d5f7b1d51acdc0568bb9ec89b216f63b78de1af71513958637992e8f9a4862045d07f7a763e1ead044f644d4219b1d174b35cf0106ea74638b9979d9094b04f032da3968268a43b7b1bd3e4c77a7a0689769f267e75e1951b23e94f580591d3fea5e685e3df266f0852a64e4102b48d5d09eb1ad0d59750e4ca15e17aa15ae43126383012069ebd2a129e1860d6471906144d39f8ec840d5b25420ca8535d0b9bbb4b5ca6e6f4b580225742aef3e76098dfb4f7665ce5a3f66345393ecaeac02eba70a01b14b4ad9e7743561a1a5de133e5555d6d1f2e8785998a2caf11ecdd944c8bb539ff9e95ed59d5dc4362e669366665a21e382b43da9ab740b037a526306cba861b2663a618280a67741665dd4871dcdd6d4dc5b0665064e7561191852759677204d11d908a93bd5156f55e6ed5c7eb71a9f3013971841986bf2669ad136926892810d94eef277df112c9244ae00058f801fad5d46b1d2cc6d23ae856bf663d58af58e5177e29f7ba2009ed654efe1654e901ad15450376ce6310f898a1343fdf2507b7b3cc30a60d0089002f2cb046f63d21fa49b51aa3c2411a4ba648fd166158930ed557b24214d4884122e6bb198f923543c2efc0ef0b6357195a325425da13ccc508c086036d0c12d03c95a4acca950640c876e73fef97efdf52099381bbe392e32b55947328ec9783bd6b695920f56c7265a9791c15e595ddf6f9da995ab15a3af79ec664a4eb0325b5fe7ddae0c31494c4412af8f405313c53aa0ea6d7bec4e0a9888dab9316e73d02682c97b85b691bd9ce1cc3051e070ebf192f5125c903a69603ad04e83f1f8654e980eb8bc331e5193e8a583d7349b41969cca652559ee74303e53796c9dc771f6a67615b843de7310c168b8264bab631963a2d83092ce29f7e792f5a18e73f15f9216d5f1da8c8cd0643ad993c1d0d9a4678d6318ed04ea942411a4be7630b6d2a4ce385b49777261611f7d379356eb037651aba643f77e1435fc6faf1e8c7dcc194e939e274c90960f14e6eebb8d4dc7197fa726530eb9e4268df4e6b04b3ad45a56074cc3456aca8953d9c4f7ea8da432f782df5f141f2f3162136e5bdc0182d630a785173bf7df2ede4a056e0ec567419e47d8aa02edcbd62e21ae88c1ccc791051872d9124ec8a638d81b0bd429afd3a1b854cbb9e228c56fae40d2637e09469e41163e814c2d9cf8ffa9dc2ac83692026a5d56c43ed339ebb26c54ef5876f5982478e69a2bd7e9dbd65cb91849aeb89a4ba33d055553d5201654cd3d5c63bdd4ce05e9cba1ec74f5b7292fd25a8117138d65bdb63a486fe393f1f0637de711d5c6cca6a36e698b0decd156aeec2452938b56b41a2c4a54fe9e35292e9ba1c66c4eeb7672fd43e73d936b29822e2b694aed8473daa3f935df8e0b190e3fafd6a56ac1891cf2e426a4bf1a73b6a6154b1c0bbd86cff10f8c634e15068741b06ec6977302f9b1506be00cf39079d55b13506608ee3345ff3028647875b0f3fcfee3c8e957330e0def0e7415bafd59fce549327b7c7e4c98cdcbf8cd2dcd7f20cb5f27e84ff898820d5b13eee50036d500bda2dd6b07064a0786a5dad718184b0c7a85e8f376279521f4ca3ba88a05cd1a50f2e1ecea130ce50dc7528a3055118605a7cdb3388af28229cad02d96c18cfb43281b10bd48fbd67d9f531d5115a0be18655baaf01b9f6c4bcfd1a685e0e9a704869dcdb469734bd649bda74b23aa9b48871b77908288edb41719456b1052769bbf3c287ee9a44a6fbd66948f6ec9fd96a65bcaa6cc9600eefc7a0a8f0dfae417a50fdd312fefd3e2a729e698021b26e227e2f7e4fd16e1a819f78de668c600c1838eea148edb92f2d3b8cd6b83a4fe668809a8525f132b3dc038c50c2cbf272bf00ccd631bd6615763cb2b04361988f30aa3edcfbe32fa45f1cbcf7f014dc13ff8a5560000, '5.0.0.net45');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `userprofile`
--

CREATE TABLE IF NOT EXISTS `userprofile` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` longtext NOT NULL,
  `Email` longtext,
  `LastName` longtext,
  `FirstName` longtext,
  `Discriminator` varchar(128) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Gegevens worden uitgevoerd voor tabel `userprofile`
--

INSERT INTO `userprofile` (`UserId`, `UserName`, `Email`, `LastName`, `FirstName`, `Discriminator`) VALUES
(1, 'admin', NULL, NULL, NULL, 'UserProfile'),
(2, 'Kefke', NULL, NULL, NULL, 'UserProfile'),
(3, 'Kefke', 'kevin.aelterman@student.howest.be', 'Aelterman', 'Kevin', 'UserProperty');

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

--
-- Gegevens worden uitgevoerd voor tabel `webpages_membership`
--

INSERT INTO `webpages_membership` (`UserId`, `CreateDate`, `ConfirmationToken`, `IsConfirmed`, `LastPasswordFailureDate`, `PasswordFailuresSinceLastSuccess`, `Password`, `PasswordChangedDate`, `PasswordSalt`, `PasswordVerificationToken`, `PasswordVerificationTokenExpirationDate`) VALUES
(1, '2013-12-24 16:55:38', '', 1, NULL, 0, 'APnP0/EyS6MN6I12t7o5QQjrQwqKJearHBXZuq2uE3PUZRd0R2ap11W5naXU4/bepQ==', NULL, '', NULL, NULL),
(2, '2013-12-24 16:57:51', '', 1, NULL, 0, 'APnP0/EyS6MN6I12t7o5QQjrQwqKJearHBXZuq2uE3PUZRd0R2ap11W5naXU4/bepQ==', NULL, '', NULL, NULL);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Gegevens worden uitgevoerd voor tabel `webpages_roles`
--

INSERT INTO `webpages_roles` (`RoleId`, `RoleName`) VALUES
(1, 'Administrators'),
(2, 'Users');

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
-- Gegevens worden uitgevoerd voor tabel `webpages_usersinroles`
--

INSERT INTO `webpages_usersinroles` (`UserId`, `RoleId`) VALUES
(1, 1),
(3, 2);

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
