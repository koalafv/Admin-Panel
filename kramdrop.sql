-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 08 Sty 2023, 22:24
-- Wersja serwera: 10.4.24-MariaDB
-- Wersja PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `kramdrop`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `uzytkownicy`
--

CREATE TABLE `uzytkownicy` (
  `ID` int(11) NOT NULL,
  `Nick` text NOT NULL,
  `Haslo` text NOT NULL,
  `Mail` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `uzytkownicy`
--

INSERT INTO `uzytkownicy` (`ID`, `Nick`, `Haslo`, `Mail`) VALUES
(2, 'arek', '123', 'koalafv@onet.pl'),
(2, 'szkola', '123', 'sdsdasad@wp.pl'),
(3, 'szkola1', '123', 'szkola@wp.pl'),
(4, 'Szkola', '123', 'Szkoala@wp.pl'),
(5, 'Szkola1', '123', 'assadsd@wp.pl'),
(6, 'Szkola12', '123', 'dszsd@wp.pl');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `zwierzeta`
--

CREATE TABLE `zwierzeta` (
  `ID` int(11) NOT NULL,
  `Nazwa_zwierzecia` text NOT NULL,
  `Gdzie_wystepuje` text NOT NULL,
  `co_je` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `zwierzeta`
--

INSERT INTO `zwierzeta` (`ID`, `Nazwa_zwierzecia`, `Gdzie_wystepuje`, `co_je`) VALUES
(1, 'Hiena', 'Afryka', ' Mięsożerny'),
(2, 'Renifer', 'Norwegia', ' Roslinozerny');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
