-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2019. Jún 18. 22:25
-- Kiszolgáló verziója: 10.1.34-MariaDB
-- PHP verzió: 7.2.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `cow_datab`
--
CREATE DATABASE IF NOT EXISTS `cow_datab` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `cow_datab`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow`
--

CREATE TABLE `cow` (
  `cow_primary_key` int(4) NOT NULL,
  `cow_id` varchar(10) COLLATE utf8_hungarian_ci NOT NULL,
  `cow_mother_id` varchar(10) COLLATE utf8_hungarian_ci NOT NULL,
  `cow_cow_type_id` int(4) NOT NULL,
  `cow_cow_sex_id` int(3) NOT NULL,
  `cow_cow_color_id` int(4) NOT NULL,
  `cow_birth` date DEFAULT NULL,
  `cow_death` varchar(10) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `cow_cow_pregnant_id` tinyint(1) NOT NULL,
  `cow_cow_age_type_id` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_age_type`
--

CREATE TABLE `cow_age_type` (
  `cow_age_type_id` int(1) NOT NULL,
  `cow_age_type_name` varchar(50) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `cow_age_type`
--

INSERT INTO `cow_age_type` (`cow_age_type_id`, `cow_age_type_name`) VALUES
(1, 'felnőtt'),
(2, 'gyerek');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_color`
--

CREATE TABLE `cow_color` (
  `cow_color_id` int(4) NOT NULL,
  `cow_color_name` varchar(70) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_insemination`
--

CREATE TABLE `cow_insemination` (
  `cow_insemination_id` int(4) NOT NULL,
  `cow_insemination_date` date NOT NULL,
  `cow_insemination_cow_id` varchar(10) COLLATE utf8_hungarian_ci NOT NULL,
  `cow_insemination_cow_insemination_type_id` int(4) NOT NULL,
  `cow_insemination_cow_insemination_success_id` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_insemination_success`
--

CREATE TABLE `cow_insemination_success` (
  `cow_insemination_success_id` int(1) NOT NULL,
  `cow_insemination_success_name` varchar(4) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `cow_insemination_success`
--

INSERT INTO `cow_insemination_success` (`cow_insemination_success_id`, `cow_insemination_success_name`) VALUES
(0, 'nem'),
(1, 'igen');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_insemination_type`
--

CREATE TABLE `cow_insemination_type` (
  `cow_insemination_type_id` int(4) NOT NULL,
  `cow_insemination_type_method` varchar(50) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_pregnant`
--

CREATE TABLE `cow_pregnant` (
  `cow_pregnant_id` tinyint(1) NOT NULL,
  `cow_pregnant_name` varchar(4) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `cow_pregnant`
--

INSERT INTO `cow_pregnant` (`cow_pregnant_id`, `cow_pregnant_name`) VALUES
(0, 'nem'),
(1, 'igen');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_sex`
--

CREATE TABLE `cow_sex` (
  `cow_sex_id` int(3) NOT NULL,
  `cow_sex_name` varchar(50) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `cow_type`
--

CREATE TABLE `cow_type` (
  `cow_type_id` int(4) NOT NULL,
  `cow_type_name` varchar(70) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `list`
--

CREATE TABLE `list` (
  `list_id` int(5) UNSIGNED NOT NULL,
  `list_cow_id` varchar(10) COLLATE utf8_hungarian_ci NOT NULL,
  `list_medicine_id` int(3) NOT NULL,
  `list_medicine_start` date NOT NULL,
  `list_medicine_expiry` date NOT NULL,
  `list_medicine_dosage_method` text COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `medicine`
--

CREATE TABLE `medicine` (
  `medicine_id` int(3) NOT NULL,
  `medicine_name` varchar(150) COLLATE utf8_hungarian_ci NOT NULL,
  `medicine_medicine_type_id` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `medicine_type`
--

CREATE TABLE `medicine_type` (
  `medicine_type_id` int(3) NOT NULL,
  `medicine_type_name` varchar(50) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `cow`
--
ALTER TABLE `cow`
  ADD PRIMARY KEY (`cow_primary_key`),
  ADD KEY `cow_cow_sex_id` (`cow_cow_sex_id`),
  ADD KEY `cow_cow_pregnant_id` (`cow_cow_pregnant_id`),
  ADD KEY `cow_adult_cow_type_id` (`cow_cow_type_id`),
  ADD KEY `cow_cow_age_type_id` (`cow_cow_age_type_id`),
  ADD KEY `cow_cow_color_id` (`cow_cow_color_id`),
  ADD KEY `cow_id` (`cow_id`);

--
-- A tábla indexei `cow_age_type`
--
ALTER TABLE `cow_age_type`
  ADD PRIMARY KEY (`cow_age_type_id`);

--
-- A tábla indexei `cow_color`
--
ALTER TABLE `cow_color`
  ADD PRIMARY KEY (`cow_color_id`);

--
-- A tábla indexei `cow_insemination`
--
ALTER TABLE `cow_insemination`
  ADD PRIMARY KEY (`cow_insemination_id`),
  ADD KEY `cow_insemination_succesful` (`cow_insemination_cow_insemination_success_id`),
  ADD KEY `cow_insemination_cow_insemination_type_id` (`cow_insemination_cow_insemination_type_id`),
  ADD KEY `cow_insemination_cow_id` (`cow_insemination_cow_id`);

--
-- A tábla indexei `cow_insemination_success`
--
ALTER TABLE `cow_insemination_success`
  ADD PRIMARY KEY (`cow_insemination_success_id`);

--
-- A tábla indexei `cow_insemination_type`
--
ALTER TABLE `cow_insemination_type`
  ADD PRIMARY KEY (`cow_insemination_type_id`);

--
-- A tábla indexei `cow_pregnant`
--
ALTER TABLE `cow_pregnant`
  ADD PRIMARY KEY (`cow_pregnant_id`);

--
-- A tábla indexei `cow_sex`
--
ALTER TABLE `cow_sex`
  ADD PRIMARY KEY (`cow_sex_id`);

--
-- A tábla indexei `cow_type`
--
ALTER TABLE `cow_type`
  ADD PRIMARY KEY (`cow_type_id`);

--
-- A tábla indexei `list`
--
ALTER TABLE `list`
  ADD PRIMARY KEY (`list_id`),
  ADD KEY `list_cow_id` (`list_cow_id`),
  ADD KEY `list_medicine_id` (`list_medicine_id`);

--
-- A tábla indexei `medicine`
--
ALTER TABLE `medicine`
  ADD PRIMARY KEY (`medicine_id`),
  ADD KEY `medicine_medicine_type_id` (`medicine_medicine_type_id`);

--
-- A tábla indexei `medicine_type`
--
ALTER TABLE `medicine_type`
  ADD PRIMARY KEY (`medicine_type_id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `cow`
--
ALTER TABLE `cow`
  MODIFY `cow_primary_key` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT a táblához `cow_age_type`
--
ALTER TABLE `cow_age_type`
  MODIFY `cow_age_type_id` int(1) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `cow_color`
--
ALTER TABLE `cow_color`
  MODIFY `cow_color_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT a táblához `cow_insemination`
--
ALTER TABLE `cow_insemination`
  MODIFY `cow_insemination_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `cow_insemination_success`
--
ALTER TABLE `cow_insemination_success`
  MODIFY `cow_insemination_success_id` int(1) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `cow_insemination_type`
--
ALTER TABLE `cow_insemination_type`
  MODIFY `cow_insemination_type_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `cow_pregnant`
--
ALTER TABLE `cow_pregnant`
  MODIFY `cow_pregnant_id` tinyint(1) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `cow_sex`
--
ALTER TABLE `cow_sex`
  MODIFY `cow_sex_id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `cow_type`
--
ALTER TABLE `cow_type`
  MODIFY `cow_type_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT a táblához `list`
--
ALTER TABLE `list`
  MODIFY `list_id` int(5) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT a táblához `medicine`
--
ALTER TABLE `medicine`
  MODIFY `medicine_id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `medicine_type`
--
ALTER TABLE `medicine_type`
  MODIFY `medicine_type_id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `cow`
--
ALTER TABLE `cow`
  ADD CONSTRAINT `cow_ibfk_1` FOREIGN KEY (`cow_cow_sex_id`) REFERENCES `cow_sex` (`cow_sex_id`),
  ADD CONSTRAINT `cow_ibfk_3` FOREIGN KEY (`cow_cow_type_id`) REFERENCES `cow_type` (`cow_type_id`),
  ADD CONSTRAINT `cow_ibfk_6` FOREIGN KEY (`cow_cow_age_type_id`) REFERENCES `cow_age_type` (`cow_age_type_id`),
  ADD CONSTRAINT `cow_ibfk_7` FOREIGN KEY (`cow_cow_pregnant_id`) REFERENCES `cow_pregnant` (`cow_pregnant_id`),
  ADD CONSTRAINT `cow_ibfk_8` FOREIGN KEY (`cow_cow_color_id`) REFERENCES `cow_color` (`cow_color_id`);

--
-- Megkötések a táblához `cow_insemination`
--
ALTER TABLE `cow_insemination`
  ADD CONSTRAINT `cow_insemination_ibfk_1` FOREIGN KEY (`cow_insemination_cow_insemination_success_id`) REFERENCES `cow_insemination_success` (`cow_insemination_success_id`),
  ADD CONSTRAINT `cow_insemination_ibfk_2` FOREIGN KEY (`cow_insemination_cow_insemination_type_id`) REFERENCES `cow_insemination_type` (`cow_insemination_type_id`),
  ADD CONSTRAINT `cow_insemination_ibfk_3` FOREIGN KEY (`cow_insemination_cow_id`) REFERENCES `cow` (`cow_id`);

--
-- Megkötések a táblához `list`
--
ALTER TABLE `list`
  ADD CONSTRAINT `list_ibfk_1` FOREIGN KEY (`list_medicine_id`) REFERENCES `medicine` (`medicine_id`),
  ADD CONSTRAINT `list_ibfk_2` FOREIGN KEY (`list_cow_id`) REFERENCES `cow` (`cow_id`);

--
-- Megkötések a táblához `medicine`
--
ALTER TABLE `medicine`
  ADD CONSTRAINT `medicine_ibfk_1` FOREIGN KEY (`medicine_medicine_type_id`) REFERENCES `medicine_type` (`medicine_type_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
