-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema BaumankaShop
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema BaumankaShop
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `BaumankaShop` DEFAULT CHARACTER SET utf8 ;
USE `BaumankaShop` ;

-- -----------------------------------------------------
-- Table `BaumankaShop`.`Roles`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`Roles` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Role_Name` VARCHAR(100) NOT NULL,
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE,
  UNIQUE INDEX `Role_Name_UNIQUE` (`Role_Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BaumankaShop`.`Shop_Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`Shop_Users` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `First_Name` VARCHAR(100) NOT NULL,
  `Second_Name` VARCHAR(100) NOT NULL,
  `Role_Id` INT NOT NULL,
  `Email_Adress` VARCHAR(200) NULL,
  `Password` VARCHAR(100) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC) VISIBLE,
  UNIQUE INDEX `Email_Adress_UNIQUE` (`Email_Adress` ASC) VISIBLE,
  UNIQUE INDEX `Password_UNIQUE` (`Password` ASC) VISIBLE,
  INDEX `fk_Shop_Users_Roles1_idx` (`Role_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Shop_Users_Roles`
    FOREIGN KEY (`Role_Id`)
    REFERENCES `BaumankaShop`.`Roles` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BaumankaShop`.`Products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`Products` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Product_Name` VARCHAR(200) NOT NULL,
  `Product_Price` DECIMAL(10,2) ZEROFILL NOT NULL DEFAULT 0.00,
  `Product_About` VARCHAR(400) NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE,
  UNIQUE INDEX `Product_Name_UNIQUE` (`Product_Name` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BaumankaShop`.`Shop_Orders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`Shop_Orders` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `User_Id` INT NOT NULL,
  `Product_Id` INT NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE,
  INDEX `User_Id_idx` (`User_Id` ASC) VISIBLE,
  INDEX `Product_Id_idx` (`Product_Id` ASC) VISIBLE,
  CONSTRAINT `FK_User_Order`
    FOREIGN KEY (`User_Id`)
    REFERENCES `BaumankaShop`.`Shop_Users` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Product_Order`
    FOREIGN KEY (`Product_Id`)
    REFERENCES `BaumankaShop`.`Products` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BaumankaShop`.`Categories`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`Categories` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Category_Name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `Id_UNIQUE` (`Id` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BaumankaShop`.`PrductCategory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `BaumankaShop`.`PrductCategory` (
  `Product_Id` INT NOT NULL,
  `Category_Id` INT NOT NULL,
  INDEX `Category_Id_idx` (`Category_Id` ASC) VISIBLE,
  INDEX `Product_Id_idx` (`Product_Id` ASC) VISIBLE,
  PRIMARY KEY (`Product_Id`, `Category_Id`),
  CONSTRAINT `FK_CategoryId_Category`
    FOREIGN KEY (`Category_Id`)
    REFERENCES `BaumankaShop`.`Categories` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_ProductId_Product`
    FOREIGN KEY (`Product_Id`)
    REFERENCES `BaumankaShop`.`Products` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
