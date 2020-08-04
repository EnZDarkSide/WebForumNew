/*
 Navicat Premium Data Transfer

 Source Server         : DeliveryFreedom
 Source Server Type    : MySQL
 Source Server Version : 100411
 Source Host           : localhost:3306
 Source Schema         : webforum

 Target Server Type    : MySQL
 Target Server Version : 100411
 File Encoding         : 65001

 Date: 29/07/2020 15:57:52
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for discussions
-- ----------------------------
DROP TABLE IF EXISTS `discussions`;
CREATE TABLE `discussions`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `OwnerID` int(11) NOT NULL,
  `OwnerType` enum('discussion','section') CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL DEFAULT 'section',
  `Title` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `Description` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NULL DEFAULT NULL,
  `ImgSource` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of discussions
-- ----------------------------

-- ----------------------------
-- Table structure for passwords
-- ----------------------------
DROP TABLE IF EXISTS `passwords`;
CREATE TABLE `passwords`  (
  `UserId` int(11) NOT NULL,
  `Password` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  PRIMARY KEY (`UserId`) USING BTREE,
  CONSTRAINT `UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of passwords
-- ----------------------------
INSERT INTO `passwords` VALUES (1, '1234567');
INSERT INTO `passwords` VALUES (2, '1234567');

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles`  (
  `Id` varchar(128) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of roles
-- ----------------------------

-- ----------------------------
-- Table structure for sections
-- ----------------------------
DROP TABLE IF EXISTS `sections`;
CREATE TABLE `sections`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sections
-- ----------------------------
INSERT INTO `sections` VALUES (1, 'Android');

-- ----------------------------
-- Table structure for topicmessages
-- ----------------------------
DROP TABLE IF EXISTS `topicmessages`;
CREATE TABLE `topicmessages`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TopicID` int(11) NOT NULL,
  `AuthorID` int(11) NOT NULL,
  `Message` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `DateTime` datetime(0) NOT NULL DEFAULT '1970-01-01 00:00:01' ON UPDATE CURRENT_TIMESTAMP(0),
  `isPinned` binary(1) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of topicmessages
-- ----------------------------
INSERT INTO `topicmessages` VALUES (1, 0, 1, 'фцэпжзрфцщзш', '2020-07-26 19:36:16', NULL);

-- ----------------------------
-- Table structure for topics
-- ----------------------------
DROP TABLE IF EXISTS `topics`;
CREATE TABLE `topics`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `Description` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of topics
-- ----------------------------

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `Email` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  `Role` varchar(255) CHARACTER SET cp1251 COLLATE cp1251_general_ci NOT NULL,
  PRIMARY KEY (`Id`, `Username`) USING BTREE,
  INDEX `Id`(`Id`) USING BTREE,
  INDEX `Username`(`Username`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = cp1251 COLLATE = cp1251_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 'EnZDS', 'vlad@ya.ru', 'Admin');
INSERT INTO `users` VALUES (2, 'ADAD', 'paifh', 'User');

SET FOREIGN_KEY_CHECKS = 1;
