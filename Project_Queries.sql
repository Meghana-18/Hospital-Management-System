--drop database HealthcareSystem_DB;

CREATE DATABASE HealthcareSystem_DB;

use HealthcareSystem_DB;

CREATE TABLE Patient(
  PID int not null identity(1,1)  primary key,
  PFirstName varchar(40) NOT NULL,
  PMiddleName varchar(40) NOT NULL,
  PLastName varchar(40) NOT NULL,
  PMobile varchar(10) not null,
  PEmailID varchar(100) not null,
  PHouseNo varchar(20) not null,
  PSociety varchar(20) not null,
  PArea varchar(20) not null,
  PCity varchar(20) not null,
  PState varchar(20) not null,
  PDOB datetime not null,
  PGender varchar(6),
  PAgeType varchar(5),
  PAdmit bit not null) ;

select * from Patient;

CREATE TABLE Doctor(
  DID int not null identity(1,1)  primary key,
  DFirstName varchar(40) NOT NULL,
  DMiddleName varchar(40) NOT NULL,
  DLastName varchar(40) NOT NULL,
  DMobile varchar(10) not null,
  DEmailID varchar(100) not null,
  DHouseNo varchar(20) not null,
  DSociety varchar(20) not null,
  DArea varchar(20) not null,
  DCity varchar(20) not null,
  DState varchar(20) not null,
  DDOB datetime not null,
  DGender varchar(6),
  DSpecialization varchar(20) not null,
  DSalary decimal not null,
  DVisiting bit not null,
  DFees decimal not null,
  DCharges decimal not null);

CREATE TABLE OPD_Patient(
  PID int not null primary key,
  DID int,
  OPDFees decimal,
  ODiagnoseFees decimal,
  ODressing bit,
  OBloodTest bit,
  OECG bit,
  OMedicineCharges decimal,
  OBillAmount decimal,
  OBillPaid bit,
  FOREIGN KEY(PID) REFERENCES Patient(PID),
  FOREIGN KEY(DID) REFERENCES Doctor(DID)) ;

CREATE TABLE Nurse(
  NID int not null identity(1,1)  primary key,
  NFirstName varchar(40) NOT NULL,
  NMiddleName varchar(40) NOT NULL,
  NLastName varchar(40) NOT NULL,
  NMobile varchar(10) not null,
  NEmailID varchar(100) not null,
  NHouseNo varchar(20) not null,
  NSociety varchar(20) not null,
  NArea varchar(20) not null,
  NCity varchar(20) not null,
  NState varchar(20) not null,
  NDOB datetime not null,
  NGender varchar(6),
  NSalary decimal not null,
  NFees decimal not null);

CREATE TABLE WardBoy(
  WID int not null identity(1,1)  primary key,
  WFirstName varchar(40) NOT NULL,
  WMiddleName varchar(40) NOT NULL,
  WLastName varchar(40) NOT NULL,
  WMobile varchar(10) not null,
  WEmailID varchar(100) not null,
  WHouseNo varchar(20) not null,
  WSociety varchar(20) not null,
  WArea varchar(20) not null,
  WCity varchar(20) not null,
  WState varchar(20) not null,
  WDOB datetime not null,
  WGender varchar(6),
  WSalary decimal not null,
  WFees decimal not null);

CREATE TABLE Room(
  RID int not null identity(1,1)  primary key,
  RType varchar(20) NOT NULL,
  RCharge decimal not null,
  RCapacity int not null,
  ROccupied bit);

CREATE TABLE IPD_Patient(
  PID int not null primary key,
  DID int,
  NID int,
  RID int,
  IWard varchar(20) not null,
  IRoomNo int,
  IAdvance bit ,
  IAdmitDate datetime not null,
  IDischargeDate datetime not null,
  IDocVisitCount int,
  IMedicineCharges decimal,
  IBloodCheck bit,
  IRadiology bit,
  IInjection bit,
  ILaundry decimal,
  IFoodCharges decimal,
  IBillAmount decimal,
  IBillPaid bit,
  FOREIGN KEY(PID) REFERENCES Patient(PID),
  FOREIGN KEY(DID) REFERENCES Doctor(DID),
  FOREIGN KEY(NID) REFERENCES Nurse(NID),
  FOREIGN KEY(RID) REFERENCES Room(RID)) ;

 Create Table Constants(
  CID int not null identity(1,1)  primary key,
  CName varchar(50) not NULL,
  CPrice decimal not null);

Create Table Medicines(
  MID int not null identity(1,1)  primary key,
  MName varchar(50) not null,
  MPrice decimal not null);

Create Table Canteen(
  CID int not null identity(1,1)  primary key,
  CName varchar(50) not null,
  CPrice decimal not null);