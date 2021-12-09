CREATE DATABASE Library;

USE Library;

CREATE TABLE Areas(
  are_codigo INT PRIMARY KEY NOT NULL,
  are_nombre NVARCHAR(100) NOT NULL,
  are_tiempo INT NOT NULL,
);

CREATE TABLE Usuarios(
  usu_documento NVARCHAR(50) NOT NULL, --c#: [DatabaseGenerated(DatabaseGeneratedOption.None)]
  usu_nombre NVARCHAR(100) NOT NULL,
  usu_direccion TEXT NOT NULL,
  usu_telefono NVARCHAR(50) NOT NULL,
  usu_correo NVARCHAR(100) NULL,
  usu_estado NVARCHAR(50) NOT NULL,  
  CONSTRAINT PK_documento PRIMARY KEY CLUSTERED (usu_documento),
  CONSTRAINT UQ_correo UNIQUE (usu_correo)
);

CREATE TABLE Libros(
  lib_codigo INT PRIMARY KEY NOT NULL,
  lib_nombre NVARCHAR(100) NOT NULL,
  lib_num_pag INT NULL,
  lib_autor NVARCHAR(100) NOT NULL,
  lib_editorial NVARCHAR(100) NOT NULL,
  lib_area INT NOT NULL,
  CONSTRAINT FK_lib_area FOREIGN KEY (lib_area) REFERENCES areas(are_codigo)
);

CREATE TABLE Prestamos(
  pre_codigo UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  pre_fecha DATETIME NOT NULL,
  pre_usuario NVARCHAR(50) NOT NULL,
  pre_vigente BIT NOT NULL,
  CONSTRAINT FK_pre_usuario FOREIGN KEY (pre_usuario) REFERENCES usuarios(usu_documento) 
);

CREATE TABLE DetallePrestamos(
  dtp_prestamo UNIQUEIDENTIFIER NOT NULL,
  dtp_libro INT NOT NULL,
  dtp_cantidad INT NOT NULL,
  dtp_fecha_fin DATETIME NOT NULL,
  dtp_fecha_dev DATETIME NULL,
  CONSTRAINT PK_id_dtp PRIMARY KEY CLUSTERED (dtp_prestamo, dtp_libro),
  CONSTRAINT FK_dtp_prestamo FOREIGN KEY (dtp_prestamo) REFERENCES prestamos(pre_codigo),
  CONSTRAINT FK_dtp_libro FOREIGN KEY (dtp_libro) REFERENCES libros(lib_codigo) 
);

CREATE TABLE Sanciones(
  san_codigo UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
  san_prestamo UNIQUEIDENTIFIER NOT NULL,
  san_dias_sancion INT NOT NULL,
  san_fecha_inicio DATETIME NOT NULL,
  san_fecha_fin DATETIME NOT NULL,
  CONSTRAINT FK_san_prestamo FOREIGN KEY (san_prestamo) REFERENCES prestamos(pre_codigo) 
);

 ----TRIGGER CUANDO INGRESA FECHADEV (UPDATE), INSERTA NUEVO REGISTRO EN SANCIONES
 CREATE TRIGGER Tr_FechaDev_Sancion
 ON DetallePrestamos 
 AFTER UPDATE 
 AS BEGIN
	SET NOCOUNT ON --agregado para evitar conjuntos de resultados adicionales
	--declarar variables
	DECLARE 
	@FechaDev DATETIME,
	@Prestamo uniqueidentifier,
	@FechaFin DATETIME
	--asignar valor a las variables de las tablas temporales Inserted-deleted
	SELECT @FechaDev = dtp_fecha_dev FROM INSERTED
	SELECT @Prestamo = dtp_prestamo FROM DELETED
	SELECT @FechaFin = dtp_fecha_fin FROM DELETED
	--validar condicion y ejecutar
	IF (@FechaDev > @FechaFin)
		INSERT INTO Sanciones VALUES(NEWID(), @Prestamo, 5, @FechaDev, DATEADD(DAY,5,@FechaDev));
 END


/* --Ayudas
ALTER TABLE Prestamos
DROP constraint FK_pre_usuario;

ALTER TABLE Prestamos
DROP COLUMN pre_usuario;

ALTER TABLE Usuarios ADD
CONSTRAINT UQ_correo UNIQUE (usu_correo);

ALTER TABLE Prestamos
ADD pre_usuario NVARCHAR(50) NOT NULL,
CONSTRAINT FK_pre_usuario FOREIGN KEY (pre_usuario) REFERENCES usuarios(usu_documento);
*/
