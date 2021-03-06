-- MySQL Script generated by MySQL Workbench
-- 08/04/15 08:26:52
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema proyecto_final
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `proyecto_final` ;

-- -----------------------------------------------------
-- Schema proyecto_final
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `proyecto_final` DEFAULT CHARACTER SET utf8 ;
USE `proyecto_final` ;

-- -----------------------------------------------------
-- Table `proyecto_final`.`grado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`grado` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`grado` (
  `idgrado` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `grado` INT(11) NULL DEFAULT NULL COMMENT '',
  `año_ingreso` DATETIME NULL DEFAULT NULL COMMENT '',
  `Activo` INT(1) NOT NULL COMMENT '',
  PRIMARY KEY (`idgrado`)  COMMENT '')
ENGINE = InnoDB
AUTO_INCREMENT = 7
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `proyecto_final`.`grupo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`grupo` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`grupo` (
  `idgrupo` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `nombre_grupo` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `especialidad` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `grado_idgrado` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`idgrupo`)  COMMENT '',
  CONSTRAINT `fk_grupo_grado1`
    FOREIGN KEY (`grado_idgrado`)
    REFERENCES `proyecto_final`.`grado` (`idgrado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_grupo_grado1_idx` ON `proyecto_final`.`grupo` (`grado_idgrado` ASC)  COMMENT '';


-- -----------------------------------------------------
-- Table `proyecto_final`.`alumnos`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`alumnos` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`alumnos` (
  `matricula` INT(11) NOT NULL COMMENT '',
  `ape_pa` VARCHAR(40) NULL DEFAULT NULL COMMENT '',
  `ape_ma` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `nombres` VARCHAR(30) NULL DEFAULT NULL COMMENT '',
  `contra_alum` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `genero` VARCHAR(10) NULL DEFAULT NULL COMMENT '',
  `fecha_nac` DATETIME NULL DEFAULT NULL COMMENT '',
  `tipo_sang` VARCHAR(10) NULL DEFAULT NULL COMMENT '',
  `calle_num` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `colon_comu` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `cod_pos` INT(11) NULL DEFAULT NULL COMMENT '',
  `ciudad` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `muni` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `estado` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `alergias` VARCHAR(100) NULL DEFAULT NULL COMMENT '',
  `ape_pa_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `ape_ma_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `nombres_tutor` VARCHAR(30) NULL DEFAULT NULL COMMENT '',
  `genero_tutor` VARCHAR(10) NULL DEFAULT NULL COMMENT '',
  `nivel_max_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `num_tel_tutor` DOUBLE NULL DEFAULT NULL COMMENT '',
  `correo_tutor` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `num_hijos_tutor` INT(11) NULL DEFAULT NULL COMMENT '',
  `calle_num_tutor` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `colon_comu_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `cod_pos_tutor` INT(11) NULL DEFAULT NULL COMMENT '',
  `ciudad_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `muni_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `estado_tutor` VARCHAR(20) NULL DEFAULT NULL COMMENT '',
  `prof` VARCHAR(40) NULL DEFAULT NULL COMMENT '',
  `estado_escolar` VARCHAR(10) NULL DEFAULT NULL COMMENT '',
  `imagen` MEDIUMBLOB NULL DEFAULT NULL COMMENT '',
  `grupo_idgrupo` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`matricula`, `grupo_idgrupo`)  COMMENT '',
  CONSTRAINT `fk_alumnos_grupo1`
    FOREIGN KEY (`grupo_idgrupo`)
    REFERENCES `proyecto_final`.`grupo` (`idgrupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_alumnos_grupo1_idx` ON `proyecto_final`.`alumnos` (`grupo_idgrupo` ASC)  COMMENT '';


-- -----------------------------------------------------
-- Table `proyecto_final`.`director`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`director` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`director` (
  `iddirector` INT(11) NOT NULL COMMENT '',
  `contra` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`iddirector`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `proyecto_final`.`materia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`materia` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`materia` (
  `idmateria` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `nombre_materia` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `descripcion` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `cantidad` INT(11) NULL DEFAULT NULL COMMENT '',
  `grupo_idgrupo` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`idmateria`)  COMMENT '',
  CONSTRAINT `fk_materia_grupo1`
    FOREIGN KEY (`grupo_idgrupo`)
    REFERENCES `proyecto_final`.`grupo` (`idgrupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 16
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_materia_grupo1_idx` ON `proyecto_final`.`materia` (`grupo_idgrupo` ASC)  COMMENT '';


-- -----------------------------------------------------
-- Table `proyecto_final`.`horario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`horario` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`horario` (
  `idhorario` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `lunes` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `martes` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `miercoles` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `jueves` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `viernes` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `materia_idmateria` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`idhorario`, `materia_idmateria`)  COMMENT '',
  CONSTRAINT `fk_horario_materia1`
    FOREIGN KEY (`materia_idmateria`)
    REFERENCES `proyecto_final`.`materia` (`idmateria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_horario_materia1_idx` ON `proyecto_final`.`horario` (`materia_idmateria` ASC)  COMMENT '';


-- -----------------------------------------------------
-- Table `proyecto_final`.`parcial`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`parcial` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`parcial` (
  `idparcial` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `alumnos_matricula` INT(11) NOT NULL COMMENT '',
  `materia_idmateria` INT(11) NOT NULL COMMENT '',
  `numero` INT(11) NULL DEFAULT NULL COMMENT '',
  `calificacion` DOUBLE NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`idparcial`, `alumnos_matricula`, `materia_idmateria`)  COMMENT '',
  CONSTRAINT `fk_alumnos_has_materia_alumnos1`
    FOREIGN KEY (`alumnos_matricula`)
    REFERENCES `proyecto_final`.`alumnos` (`matricula`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_alumnos_has_materia_materia1`
    FOREIGN KEY (`materia_idmateria`)
    REFERENCES `proyecto_final`.`materia` (`idmateria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 108
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_alumnos_has_materia_materia1_idx` ON `proyecto_final`.`parcial` (`materia_idmateria` ASC)  COMMENT '';

CREATE INDEX `fk_alumnos_has_materia_alumnos1_idx` ON `proyecto_final`.`parcial` (`alumnos_matricula` ASC)  COMMENT '';


-- -----------------------------------------------------
-- Table `proyecto_final`.`profesor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `proyecto_final`.`profesor` ;

CREATE TABLE IF NOT EXISTS `proyecto_final`.`profesor` (
  `idprofesor` INT(11) NOT NULL COMMENT '',
  `nombre_profesor` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `ape_pa` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `ape_ma` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `genero` VARCHAR(10) NULL DEFAULT NULL COMMENT '',
  `contra_prof` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `grupo_idgrupo` INT(11) NULL DEFAULT NULL COMMENT '',
  `tip_san` VARCHAR(5) NOT NULL COMMENT '',
  `fecha_nac` DATETIME NOT NULL COMMENT '',
  `materia_idmateria` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`idprofesor`)  COMMENT '',
  CONSTRAINT `fk_profesor_grupo1`
    FOREIGN KEY (`grupo_idgrupo`)
    REFERENCES `proyecto_final`.`grupo` (`idgrupo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_profesor_materia1`
    FOREIGN KEY (`materia_idmateria`)
    REFERENCES `proyecto_final`.`materia` (`idmateria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE INDEX `fk_profesor_grupo1_idx` ON `proyecto_final`.`profesor` (`grupo_idgrupo` ASC)  COMMENT '';

CREATE INDEX `fk_profesor_materia1_idx` ON `proyecto_final`.`profesor` (`materia_idmateria` ASC)  COMMENT '';


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
