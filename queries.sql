USE master;
GO

-- delete brenton_db database if exists
DECLARE @databaseName VARCHAR(50) = 'breton_db'

IF EXISTS (SELECT name FROM sys.databases WHERE name = @databaseName)
BEGIN
	-- Drop all connections to the target database
	DECLARE @SQL varchar(max)
	
	SELECT @SQL = COALESCE(@SQL,'') + 'Kill ' + Convert(varchar, SPId) + ';'
	FROM 
		MASTER..SysProcesses
	WHERE 
		DBId = DB_ID(@databaseName) AND 
		SPId <> @@SPId

	EXEC(@SQL)

	DROP DATABASE breton_db;
END
GO

CREATE DATABASE breton_db
GO

USE breton_db;
GO

-- TABLES
CREATE TABLE tb_user
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	name VARCHAR(250) NOT NULL,
	email VARCHAR(250) NOT NULL,
	password_hash VARCHAR(250) NOT NULL,
	email_token VARCHAR(250) NOT NULL,
	email_token_expires_at DATETIME NOT NULL,
	email_token_verified_at DATETIME,
	created_at DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE tb_customer
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	created_by INT,
	name VARCHAR(250),
	cpf VARCHAR(250),
	birth_date DATE,
	phone BIGINT,
	postal_code VARCHAR(250),
	street VARCHAR(250),
	complement VARCHAR(250),
	neighborhood VARCHAR(250),
	city VARCHAR(250),
	state VARCHAR(250),
	ibge VARCHAR(250),
	gia VARCHAR(250),
	ddd VARCHAR(250),
	siafi VARCHAR(250),
	is_active BIT DEFAULT 1,
	created_at DATETIME DEFAULT GETDATE()

	CONSTRAINT fk_cliente_usuario FOREIGN KEY (created_by) REFERENCES tb_user(id)
)
GO

-- PROCEDURES

-- user
CREATE PROCEDURE pr_user_create
	@name VARCHAR(250),
	@email VARCHAR(250),
	@password_hash VARCHAR(250),
	@email_token VARCHAR(250),
	@email_token_expires_at DATETIME
AS
	INSERT INTO tb_user (name, email, password_hash, email_token, email_token_expires_at)
	VALUES 
		(@name, @email, @password_hash, @email_token, @email_token_expires_at)

	SELECT SCOPE_IDENTITY()
GO

CREATE PROCEDURE pr_user_get_by_email
	@email VARCHAR(250)
AS
	SELECT 
		id,
		name,
		email,
		passwordHash			=		password_hash,
		emailToken				=		email_token,
		emailTokenExpiresAt		=		email_token_expires_at,
		emailTokenVerifiedAt	=		email_token_verified_at,
		createdAt				=		created_at	
	FROM 
		tb_user
	WHERE
		email = @email
GO

CREATE PROCEDURE pr_user_get_by_email_token
	@email_token VARCHAR(250)
AS
	SELECT 
		id,
		name,
		email,
		passwordHash			=		password_hash,
		emailToken				=		email_token,
		emailTokenExpiresAt		=		email_token_expires_at,
		emailTokenVerifiedAt	=		email_token_verified_at,
		createdAt				=		created_at	
	FROM tb_user
	WHERE
		email_token = @email_token
GO

CREATE PROCEDURE pr_user_email_confirmation
	@email_token VARCHAR(250)
AS
	UPDATE U
	SET 
		email_token_verified_at = GETDATE()
	FROM 
		tb_user U
	WHERE
		email_token = @email_token
GO

-- customer
CREATE PROCEDURE pr_customer_create
	@created_by INT,
	@name VARCHAR(250),
	@cpf VARCHAR(250),
	@birth_date DATE,
	@phone BIGINT,
	@postal_code VARCHAR(250),
	@street VARCHAR(250),
	@complement VARCHAR(250),
	@neighborhood VARCHAR(250),
	@city VARCHAR(250),
	@state VARCHAR(250),
	@ibge VARCHAR(250),
	@gia VARCHAR(250),
	@ddd VARCHAR(250),
	@siafi VARCHAR(250)
AS
	INSERT INTO tb_customer (
		created_by, 
		name, 
		cpf, 
		birth_date, 
		phone, 
		postal_code, 
		street, 
		complement, 
		neighborhood,
		city,
		state,
		ibge,
		gia,
		ddd,
		siafi
	)
	VALUES (
		@created_by, 
		@name, 
		@cpf, 
		@birth_date, 
		@phone, 
		@postal_code, 
		@street, 
		@complement, 
		@neighborhood,
		@city,
		@state,
		@ibge,
		@gia,
		@ddd,
		@siafi
	)

	SELECT SCOPE_IDENTITY()
GO

CREATE PROCEDURE pr_customer_update
	@id INT,
	@name VARCHAR(250),
	@cpf VARCHAR(250),
	@birth_date DATE,
	@phone BIGINT,
	@postal_code VARCHAR(250),
	@street VARCHAR(250),
	@complement VARCHAR(250),
	@neighborhood VARCHAR(250),
	@city VARCHAR(250),
	@state VARCHAR(250),
	@ibge VARCHAR(250),
	@gia VARCHAR(250),
	@ddd VARCHAR(250),
	@siafi VARCHAR(250)
AS

	UPDATE tb_customer
	SET 
		name				=		@name,
		cpf					=		@cpf, 
		birth_date			=		@birth_date, 
		phone				=		@phone, 
		postal_code			=		@postal_code, 
		street				=		@street, 
		complement			=		@complement, 
		neighborhood		=		@neighborhood,
		city				=		@city,
		state				=		@state,
		ibge				=		@ibge,
		gia					=		@gia,
		ddd					=		@ddd,
		siafi				=		@siafi
	WHERE
		id = @id
GO

CREATE PROCEDURE pr_customer_delete
	@id INT
AS
	UPDATE tb_customer
	SET 
		is_active = 0
	WHERE 
		id = @id
GO

CREATE PROCEDURE pr_customer_get_by_id
	@id INT
AS
	SELECT 
		id,
		createdBy		=		created_by,
		name,
		cpf,
		birthDate		=		birth_date,
		phone,
		postalCode		=		postal_code,
		street,
		complement,
		neighborhood,
		city,
		state,
		ibge,
		gia,
		ddd,
		siafi,
		isActive		=		is_active,
		createdAt		=		created_at	
	FROM 
		tb_customer
	WHERE id = @id
GO

CREATE PROCEDURE pr_customer_get_by_cpf
	@cpf VARCHAR(250)
AS
	SELECT 
		id,
		createdBy		=		created_by,
		name,
		cpf,
		birthDate		=		birth_date,
		phone,
		postalCode		=		postal_code,
		street,
		complement,
		neighborhood,
		city,
		state,
		ibge,
		gia,
		ddd,
		siafi,
		isActive		=		is_active,
		createdAt		=		created_at	
	FROM 
		tb_customer
	WHERE cpf = @cpf
GO

CREATE PROCEDURE pr_customer_get
	@page_size INT,
	@page_number INT,
	@name VARCHAR(250) = NULL,
	@cpf VARCHAR(250) = NULL
AS
	SELECT 
		id,
		createdBy		=		created_by,
		name,
		cpf,
		birthDate		=		birth_date,
		phone,
		postalCode		=		postal_code,
		street,
		complement,
		neighborhood,
		city,
		state,
		ibge,
		gia,
		ddd,
		siafi,
		isActive		=		is_active,
		createdAt		=		created_at	
	FROM 
		tb_customer
	WHERE 
		(name LIKE '%' + @name + '%' OR @name IS NULL) AND
		(cpf LIKE '%' + @cpf + '%' OR @cpf IS NULL) AND
		is_active = 1
	ORDER BY created_at
	OFFSET (@page_number - 1) * @page_size ROWS FETCH NEXT @page_size ROWS ONLY

	SELECT COUNT(*) FROM tb_customer
GO

EXEC pr_user_create
	@name = 'Maria Silva',
	@email = 'maria@example.com',
	@password_hash = 'HASH_DA_SENHA',
	@email_token = 'TOKEN_DE_EMAIL',
	@email_token_expires_at = '2024-06-13T12:00:00'
GO

EXEC pr_user_create
    @name = 'João Oliveira',
    @email = 'joao@example.com',
    @password_hash = 'HASH_DA_SENHA',
    @email_token = 'TOKEN_DE_EMAIL',
    @email_token_expires_at = '2024-06-13T12:00:00'

GO

EXEC pr_customer_create
	@created_by = 1,
	@name = 'João Silva',
	@cpf = 12345678900,
	@birth_date = '1990-05-15',
	@phone = 1123456789,
	@postal_code = 12345678,
	@street = 'Rua das Flores',
	@complement = 'Apto 101',
	@neighborhood = 'Centro',
	@city = 'São Paulo',
	@state = 'SP',
	@ibge = '1234567',
	@gia = '1234',
	@ddd = '11',
	@siafi = '1234'
GO