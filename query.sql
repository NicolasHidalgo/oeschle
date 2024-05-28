IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DEMO')
BEGIN
CREATE DATABASE [DEMO]
END
GO

USE [DEMO]
GO

--drop table TB_EMPLOYEE
CREATE TABLE TB_EMPLOYEE (
id bigint identity primary key,
name varchar(128),
document_number varchar(64),
salary money,
age int,
profile varchar(64),
admission_date date
)
--TRUNCATE TABLE TB_EMPLOYEE
--SELECT * FROM TB_EMPLOYEE
INSERT INTO TB_EMPLOYEE VALUES ('Nico','72843916',5001, 27,'Analista','2020-02-01')
INSERT INTO TB_EMPLOYEE VALUES ('Juan','12345671',5002, 28,'Contador','2020-02-02')
INSERT INTO TB_EMPLOYEE VALUES ('Martin','12345672',5003, 29,'Admin','2020-02-03')
INSERT INTO TB_EMPLOYEE VALUES ('Jose','12345673',5004, 30,'Supervisor','2020-02-04')
INSERT INTO TB_EMPLOYEE VALUES ('Dimas','12345674',5005, 31,'Analista','2020-02-05')

INSERT INTO TB_EMPLOYEE VALUES ('Noelia','12345675',5006, 32,'Contador','2021-04-06')
INSERT INTO TB_EMPLOYEE VALUES ('Maria','12345676',5007, 33,'Admin','2021-04-07')
INSERT INTO TB_EMPLOYEE VALUES ('Giovana','12345677',5008, 34,'Supervisor','2021-04-08')
INSERT INTO TB_EMPLOYEE VALUES ('Camila','12345678',5009, 35,'Analista','2021-04-09')
INSERT INTO TB_EMPLOYEE VALUES ('Silvia','12345679',5010, 36,'Contador','2021-04-10')

INSERT INTO TB_EMPLOYEE VALUES ('Ismael','22843911',5011, 37,'Admin','2022-06-12')
INSERT INTO TB_EMPLOYEE VALUES ('Cristian','22843912',5012, 38,'Supervisor','2022-06-13')
INSERT INTO TB_EMPLOYEE VALUES ('Jorge','22843913',5013, 39,'Analista','2022-06-14')
INSERT INTO TB_EMPLOYEE VALUES ('Javier','22843914',5014, 40,'Contador','2022-06-15')
INSERT INTO TB_EMPLOYEE VALUES ('John','22843915',5015, 30,'Admin','2022-06-16')
INSERT INTO TB_EMPLOYEE VALUES ('Luis','22843916',5016, 31,'Supervisor','2022-06-17')
INSERT INTO TB_EMPLOYEE VALUES ('Piero','22843917',5017, 32,'Analista','2022-06-18')
INSERT INTO TB_EMPLOYEE VALUES ('Fabiana','22843918',5018, 33,'Contador','2022-06-19')
INSERT INTO TB_EMPLOYEE VALUES ('Fabio','22843919',5019, 34,'Admin','2022-06-20')
INSERT INTO TB_EMPLOYEE VALUES ('Antonio','32843911',5020, 35,'Supervisor','2022-06-21')


CREATE OR ALTER PROC [dbo].[SP_EMPLOYEE](
@ACCION				VARCHAR(30)		= 'INSERT',
@ID					BIGINT			= 1,
@NAME				VARCHAR(128)	= 'Pepino',
@DOCUMENT_NUMBER	VARCHAR(64)		= '12345678',
@SALARY				MONEY			= 1200,
@AGE				INT				= 0,
@PROFILE			VARCHAR(64)		= '',
@ADMISSION_DATE		DATE			= '',
@DATE_START			DATE			= '',
@DATE_END			DATE			= ''
)
AS
BEGIN
	IF @ACCION = 'INSERT'
	BEGIN
		IF EXISTS (SELECT * FROM [dbo].TB_EMPLOYEE WHERE document_number = @DOCUMENT_NUMBER)
		BEGIN
			PRINT 'ERROR: Numero documento ya existe'
			RETURN
		END

		INSERT INTO [dbo].TB_EMPLOYEE (name,document_number,salary,age,profile,admission_date)
			VALUES(@NAME,@DOCUMENT_NUMBER,@SALARY,@AGE,@PROFILE,@ADMISSION_DATE)

		RETURN
	END

	IF @ACCION = 'DELETE'
	BEGIN
		DELETE
			FROM TB_EMPLOYEE
			WHERE id = @ID
		RETURN

	END

	IF @ACCION = 'UPDATE'
	BEGIN
		UPDATE [dbo].TB_EMPLOYEE
			SET name = @NAME
				,document_number = @DOCUMENT_NUMBER
				,salary = @SALARY
				,age = @AGE
				,profile = @PROFILE
				,admission_date = @ADMISSION_DATE
			WHERE id = @ID

		RETURN
	END

	IF @ACCION = 'SELECT'
	BEGIN
		SELECT id
			, name
			, document_number
			, salary
			, age
			, profile
			, admission_date
			FROM [dbo].TB_EMPLOYEE
				
		RETURN
	END

	IF @ACCION = 'GETBY_ID'
	BEGIN
		SELECT id
			, name
			, document_number
			, salary
			, age
			, profile
			, admission_date
			FROM [dbo].TB_EMPLOYEE
			WHERE id = @ID
				
		RETURN
	END

	IF @ACCION = 'GETBY_NUMBER'
	BEGIN
		SELECT id
			, name
			, document_number
			, salary
			, age
			, profile
			, admission_date
			FROM [dbo].TB_EMPLOYEE
			WHERE document_number = @DOCUMENT_NUMBER
				
		RETURN
	END

	IF @ACCION = 'REPORTE'
	BEGIN
		SELECT id
			, name
			, document_number
			, salary
			, age
			, profile
			, admission_date
			FROM [dbo].TB_EMPLOYEE
			WHERE admission_date BETWEEN @DATE_START AND @DATE_END
				
		RETURN
	END

END

