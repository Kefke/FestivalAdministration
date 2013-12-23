-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Machine: localhost
-- Genereertijd: 23 dec 2013 om 21:17
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
('201312232115169_InitialCreate', 0x1f8b0800000000000400e55ccd6edc3610be17e83b083ab505bab29da66883750bc76b1746e338f03ab906b4c45d13912855e23af6b3f5d047ea2b94d42fc51f89a2b49bdde662c812f90d39f37138a266f6dfbfff99fffe1485ce234c3314e353f77876e43a10fb7180f0fad4dd90d58fbfb8bffff6ed37f38b207a723e54ed5eb076b427ce4edd074292579e97f90f3002d92c427e1a67f18accfc38f240107b274747bf7ac7c71ea4102ec5729cf9ed061314c1fc1ffaef798c7d98900d08afe3008659799f3e59e6a8ce5b10c12c013e3c75af9f977f854b142521bc86d13d1dd1034a660be0bbce5988001dd012862bd7497e7af53e834b92c678bd4c004120bc7b4e207dbe026106cbe1bf4a7e329dc1d1099b8107308e09858bb19506dc7a6e747617540be4990d2b9fe1a94b479cbe4be304a6e4d9755e830cb287c59466e5c3150a218f4271aa2e25ca450450e83a655792525bbace257a82c11b88d7e4a156c13578aaeed04bd7798f11353ded44d20d7dfc761386e03eacfff73aa55e52f3dcc7f1a79d0b3e5bc34ae615262f4e86f6bf05a40658c41bda6f28c21b901176b57b9da3742792e75e435513022b38fa277c6edde026730b575cf7ab409ab5277616f550f5d3b0a0540075094b12a7f00f88614a8d1ebc0384c094fab3ab00e633e9d3379333b9bacbf656fa6e5ce021a9fb6d8c7ba97d9e42da67c1af4d7a7d87a2c18b84ee2e2b9446b9cfbe8b3f413cd07ac727bf8c5ca657593906582bed751c8710601b4ff30e64d9e7380d2ea997dfa493a84880cc96886ec84cd672e3fb30cbfa4c6d86be73bd5782cf1f005ec3604a4d2d4148bed87c681c8656c8ff828cd68ee4e2294169feaf95b6df8247b4cebb2b5c4eb5b138b730ccdb30bf5706478d23fcd86a7a99c6d16d1cb65c25dfe2e332dea43e1b68dcd9ec0ea46b482cbdf4cdd9863c8c73d5f4ce230a602a6aacb3b1ad7f6f840da2d58ba3fe5d6edca6204e6c18eb8fb63e3eb38d712ace0bb4d211bfa399c4feaeb6e39740e1ab2cd85f3ab9a13cb6f28ddb67c912fa291cbc7b748e6b44c8ce0c6f6313d6cfc6b954fdb61dab333916b1fac9cb9fb7f76a945d6136ac6cba607d52d3ec22ae1f6b7ea50c6b17ca1b45e73f756d24e7a96da8f29c5da3ce9764ef708b569a71b2bfdd03cc5b0cf1e9675916fb281f92f4f6f951ffe27f8103c72c026b8ebab858c9b9de84042521f2e9904edda3d9ec585264af886a9a9ad33441862c802e2d9832b70342fa26979114204ce47548df9a500242a3b108bd872d79af96253e59c00462e6218d743e6e10b52cc1cdf4696bee7154ea66585788a3e38051bcd310410cce0532fcd0c1369368c99472a2766ff002869040e7cc27f999fb39c87c10c8be8faed66002b61acc65479435b0dfdef356bbafe8a8d4bfc9b47954471203e8dabb3f1d0a57fb26b223a2f6d9ecb058aa780fd050a71d5b4ccccb5658c28117d1ce5e33911fba89edd561ba3d0579bb8c933f39f78a8892f621b4074cabf851f52d7771cf9ac127229191412c2191437bcab62664553930897a6da846b80a898f0d7a8084ad4b8526851b2690f94186164d7d3a220295cb52822816564fe7f6d2562abb792c60719450a9bcfddac535ee3ed915396bfc9a514f4db0bbb40a8cdf2a38c43629459fd3d68481963a4ff864551947cb83e3656e8a32c73b346712216f4f7dfab77b59776611dbb0984d98996e850c0bd276a4aec22df4e8498e190ca386d19a6985091c9ada05e975511d76d45b53fd6cee15c947e58db9a7c9529a5f83244178cd652d95779c6591b274fee37278ae505460787ea64819aa475b4b22710ad65078cabe7d07304f12590002ee013b5a3b0f22a999dd465c0957ecc7b215ab1da3eac4ae3ba20096d155ede1453a941cd194509774e6110b89f21343f5f2907b3b2cb90c84a0162004e4e771b889707790ae472953b07890f29639469350c5c33477cd91f20c291e24bf61debfc890e2018a3be6084d86148fd2dc1da09526e5a9a596e6b6128b523940f9abcf07106e1aba96a9763cd402657e8a228401e5930a6eee09cc93e26789ee43d6c355c6ae6f56dff52e0cc6ecef0f6b61341954228ece785bd6b69192f756c73a5a1751ce4e595d3d6f3606a36d838f24a7b19b2ed1c2c86c5d9db7bb32f884371e89bf3f004d4e7a6b81ca8fcdb15be96c3c6aebc1b0cd4199d426ee15ca46e672fab3dc7881fdad874b564bb0416aa5b4a9405b0d86e317f96d2ae0e2c9704445d29a0a5ed16c0259625a9a9164b1d3def84ce9157c1ac7d999a696e3f679cf5e04ade1ea8cb39665b4496ffd482aa7dc9d17d7853accc57f495a94478513324291b5654e064d679d9e158e61b013a8d2ab7890eadeded84a9106646d25d5298c817dd4ddb4ef80e5c782d65ba0e60342378a1cfe3777f7c63efa6cad51ef133a48c3170a7df7edc6a6c38cbf559349077662935a7a7d70271cd0cdcbc3b2fe5a43e9f4ac68e23ad546529a7bc69ecff2cbf310d109372dae01462b9891dc8b9dba2f672f853a458b1a422fcb82d0a890f0cbd66121a688de2cce81c52462095618e3757e483914a85525a942b1297ab4c5e16a1873add95730069354305aeb432c48b405128e2b0a30fc0852ff01a47231cbffa79a4e30bf1195e55ab9805e93296be54c546f590a47d74f38711d9cedf44debe06c8c2456c14da5d18e22b7b16ae08bdca61eaeb6866deb82d4256a66bafa6a4abe945660055e4359afacd852dbf86838fc50df7940f54a93e9a85d6e64027bb0d5445b8938c54222a506f3b2a1afb34ec866335498cd6add8eae49697d2fb32d0f51658a8da967b14e45d57faedc4a0274e759f19ee53adb54925891439cdc8894646d1eddb8029643a157fff7883de39855d5c77e10ac9d85679dd47f28d4ea398bdd675e75d66914599bbb2c9bd80f0a693e816cbde6e230ea2ce42c50d186dad28abeca8ae2449cbe3edfc7d4e645fc66577ad15379a112f419de27604dd7847d8986498546a768fb7a8e9e728e7ea9e6751fdab28f4e21ea5cea51a5219df2f6aa8a44b106a4bc3eb39a114dc9c9a194878c558466c129f3450ea2e4638c32548ba5b70a621f4a39782fd2640fec72aa032a35e40fbc744be57e72966ee9195a37102c8d1643bfb599d66daef02aaef67461445513e170e31a1210d09df62c2568057c421fb38f07f9afcf9469ba1794f2c115bed9906443e894e912085bbf68c362832ef979394a7bccf39b24ffb99029a6408789d889f80d7ebd4161508ffb527134a381604147790ac76c49d869dcfab946927fc7450754aaaf8e95ee609484142cbbc14bf008f563ebd7615b63f30502eb14445989d1f4a7ff52fa05d1d36fff0175c3cf0134590000, '5.0.0.net45');

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

--
-- Gegevens worden uitgevoerd voor tabel `userprofile`
--

INSERT INTO `userprofile` (`UserId`, `UserName`, `Email`, `Facebook`, `Age`, `Rate`, `LastName`, `FirstName`, `Discriminator`) VALUES
(1, 'admin', 'xyz3710@gmail.com', 'http://facebook.com/xyz37', 40, 100, 'Kim', 'Ki Won', 'UserProperty'),
(2, 'Kefke', NULL, NULL, NULL, NULL, NULL, NULL, 'UserProfile'),
(3, 'Kefke', NULL, NULL, NULL, NULL, NULL, NULL, 'UserProperty');

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
(2, '2013-12-23 22:16:04', '', 1, NULL, 0, 'AIIu+v5Pt/nx5e7keCU8JdFUHz2HgDR7cCvQH4ZwHOopJ9OSRaERJ5cRuo7fEDTfEQ==', NULL, '', NULL, NULL);

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
-- Gegevens worden uitgevoerd voor tabel `webpages_usersinroles`
--

INSERT INTO `webpages_usersinroles` (`UserId`, `RoleId`) VALUES
(1, 1);

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
