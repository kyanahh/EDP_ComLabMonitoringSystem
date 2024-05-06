-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 05, 2024 at 07:55 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `labmonitoring`
--

-- --------------------------------------------------------

--
-- Table structure for table `comlab`
--

CREATE TABLE `comlab` (
  `labid` int(11) NOT NULL,
  `comlab` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comlab`
--

INSERT INTO `comlab` (`labid`, `comlab`) VALUES
(1, 'Computer Laboratory 1'),
(2, 'Computer Laboratory 2'),
(3, 'Computer Laboratory 3'),
(4, 'Computer Laboratory 4'),
(5, 'Computer Laboratory 5'),
(6, 'Computer Laboratory 6');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `itemid` int(11) NOT NULL,
  `itemname` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`itemid`, `itemname`) VALUES
(1, 'Aircon'),
(2, 'Projector'),
(3, 'Carpet\r\n'),
(4, 'Windows\r\n'),
(5, 'Light Switch\r\n'),
(6, 'Power Socket\r\n'),
(7, 'Light'),
(8, 'Table\r\n'),
(9, 'Cabinets\r\n'),
(10, 'White Board\r\n'),
(11, 'Chairs'),
(12, 'Door');

-- --------------------------------------------------------

--
-- Table structure for table `reports`
--

CREATE TABLE `reports` (
  `reportid` int(11) NOT NULL,
  `labid` int(11) NOT NULL,
  `itemid` int(11) NOT NULL,
  `usernumber` int(11) NOT NULL,
  `timein` varchar(255) NOT NULL,
  `timeout` varchar(255) NOT NULL,
  `section` varchar(255) NOT NULL,
  `qty` int(11) NOT NULL,
  `remarks` varchar(500) NOT NULL,
  `todate` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reports`
--

INSERT INTO `reports` (`reportid`, `labid`, `itemid`, `usernumber`, `timein`, `timeout`, `section`, `qty`, `remarks`, `todate`) VALUES
(1, 1, 1, 1234, '05:24:27 AM', '05:26:27 AM', 'IT2S', 2, 'Changed filter', '2024-05-03 05:25:24'),
(2, 1, 3, 1, '06:37:49 AM', '06:38:49 AM', 'IT2S', 6, 'Cleaneddddddddddddddddddddddddddddddddddddddddd', '2024-05-03 06:38:06'),
(4, 2, 4, 1, '06:41:41 AM', '06:43:41 AM', 'IT2S', 1, 'Cleaned', '2024-05-03 06:41:54'),
(5, 2, 5, 1, '06:46:06 AM', '08:46:06 AM', 'IT2S', 2, 'Changed', '2024-05-03 06:46:26'),
(6, 2, 6, 1234, '06:51:06 AM', '06:53:06 AM', 'IT2Z', 5, 'Changed socket', '2024-05-03 06:49:44'),
(7, 6, 2, 1, '06:51:10 AM', '06:53:10 PM', 'IT2S', 1, 'Borrowed', '2024-05-03 06:51:27'),
(8, 1, 1, 1, '02:11:45 PM', '03:11:45 PM', 'IT3S', 2, 'SDDASDSAD', '2024-05-03 14:12:13');

-- --------------------------------------------------------

--
-- Table structure for table `userlogs`
--

CREATE TABLE `userlogs` (
  `logid` int(11) NOT NULL,
  `log_time` datetime NOT NULL,
  `usernumber` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userlogs`
--

INSERT INTO `userlogs` (`logid`, `log_time`, `usernumber`) VALUES
(1, '2024-05-02 14:46:04', 1234),
(2, '2024-05-02 14:46:53', 1234),
(3, '2024-05-02 14:47:32', 1234),
(4, '2024-05-02 15:02:20', 1),
(5, '2024-05-02 15:02:47', 1),
(6, '2024-05-02 15:03:47', 1),
(7, '2024-05-02 15:04:23', 1),
(8, '2024-05-02 15:06:09', 1234),
(9, '2024-05-02 15:08:22', 1234),
(10, '2024-05-02 15:09:35', 1234),
(11, '2024-05-02 15:10:17', 1),
(12, '2024-05-02 15:13:27', 1234),
(13, '2024-05-02 15:13:41', 1),
(14, '2024-05-02 15:15:13', 1234),
(15, '2024-05-02 15:15:28', 1),
(16, '2024-05-02 15:17:15', 1),
(17, '2024-05-02 15:18:10', 1),
(18, '2024-05-02 15:18:30', 1),
(19, '2024-05-02 15:20:22', 1),
(20, '2024-05-02 15:20:52', 1),
(21, '2024-05-02 15:21:19', 1234),
(22, '2024-05-02 15:21:43', 1),
(23, '2024-05-02 20:54:21', 1),
(24, '2024-05-02 20:54:57', 1),
(25, '2024-05-02 20:55:47', 1),
(26, '2024-05-02 21:00:20', 1),
(27, '2024-05-02 21:05:43', 1),
(28, '2024-05-02 21:07:17', 1),
(29, '2024-05-02 21:07:53', 1),
(30, '2024-05-02 21:52:35', 1),
(31, '2024-05-02 21:57:03', 1),
(32, '2024-05-03 05:14:53', 1),
(33, '2024-05-03 05:17:00', 1234),
(34, '2024-05-03 05:17:52', 1234),
(35, '2024-05-03 05:18:40', 1234),
(36, '2024-05-03 05:20:30', 1234),
(37, '2024-05-03 05:24:26', 1234),
(38, '2024-05-03 06:22:17', 1),
(39, '2024-05-03 06:23:41', 1),
(40, '2024-05-03 06:36:57', 1),
(41, '2024-05-03 06:37:34', 1),
(42, '2024-05-03 06:39:14', 1),
(43, '2024-05-03 06:41:40', 1),
(44, '2024-05-03 06:43:53', 1),
(45, '2024-05-03 06:44:27', 1),
(46, '2024-05-03 06:46:02', 1),
(47, '2024-05-03 06:49:04', 1234),
(48, '2024-05-03 06:49:51', 1),
(49, '2024-05-03 06:51:09', 1234),
(50, '2024-05-03 06:51:33', 1),
(51, '2024-05-03 06:54:27', 1),
(52, '2024-05-03 07:05:10', 1),
(53, '2024-05-03 07:07:13', 1),
(54, '2024-05-03 07:10:48', 1),
(55, '2024-05-03 07:11:54', 1),
(56, '2024-05-03 07:12:39', 1),
(57, '2024-05-03 07:12:55', 1),
(58, '2024-05-03 07:15:53', 1),
(59, '2024-05-03 07:20:32', 1),
(60, '2024-05-03 07:21:40', 1),
(61, '2024-05-03 07:30:00', 1),
(62, '2024-05-03 07:49:18', 1),
(63, '2024-05-03 08:02:03', 1),
(64, '2024-05-03 08:02:19', 1),
(65, '2024-05-03 08:21:51', 1234),
(66, '2024-05-03 08:33:26', 1234),
(67, '2024-05-03 08:44:52', 1234),
(68, '2024-05-03 08:49:21', 1234),
(69, '2024-05-03 14:02:33', 1),
(70, '2024-05-03 14:04:17', 1234),
(71, '2024-05-03 14:04:23', 1),
(72, '2024-05-03 14:10:08', 1),
(73, '2024-05-05 13:38:37', 1);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userid` int(11) NOT NULL,
  `usernumber` int(20) NOT NULL,
  `firstname` varchar(255) NOT NULL,
  `lastname` varchar(255) NOT NULL,
  `pin` int(20) NOT NULL,
  `typeid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userid`, `usernumber`, `firstname`, `lastname`, `pin`, `typeid`) VALUES
(1, 51563, 'Keisha', 'Alvarez', 1234, 2),
(2, 1234, 'Keisha', 'Alvarez', 1234, 2),
(3, 1112154, 'Keisha', 'Alvarez', 1234, 2),
(4, 11111, 'Keisha', 'Alvarez', 1234, 2),
(5, 1, 'admin', 'admin', 1234, 1),
(6, 3, 'test', 'test', 1234, 2);

-- --------------------------------------------------------

--
-- Table structure for table `usertype`
--

CREATE TABLE `usertype` (
  `typeid` int(11) NOT NULL,
  `userType` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usertype`
--

INSERT INTO `usertype` (`typeid`, `userType`) VALUES
(1, 'Admin'),
(2, 'Student');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `comlab`
--
ALTER TABLE `comlab`
  ADD PRIMARY KEY (`labid`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`itemid`);

--
-- Indexes for table `reports`
--
ALTER TABLE `reports`
  ADD PRIMARY KEY (`reportid`);

--
-- Indexes for table `userlogs`
--
ALTER TABLE `userlogs`
  ADD PRIMARY KEY (`logid`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userid`);

--
-- Indexes for table `usertype`
--
ALTER TABLE `usertype`
  ADD PRIMARY KEY (`typeid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `comlab`
--
ALTER TABLE `comlab`
  MODIFY `labid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `itemid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `reports`
--
ALTER TABLE `reports`
  MODIFY `reportid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `userlogs`
--
ALTER TABLE `userlogs`
  MODIFY `logid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=74;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `userid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `usertype`
--
ALTER TABLE `usertype`
  MODIFY `typeid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
