
USE master
GO
-- Creacion de una base de datos --
IF EXISTS(
	SELECT * 
	FROM SysDataBases 
	WHERE name='Final2015')
BEGIN
	DROP DATABASE Final2015
END

GO

CREATE DATABASE Final2015
ON(
	NAME=Final2015,
	FILENAME='C:\Final2015.mdf'
)
GO

 --Creacion de tablas
USE Final2015;

GO

CREATE TABLE Bancos(
RUT VARCHAR(12) NOT NULL PRIMARY KEY CHECK(Len(Rut)=12),
Nombre VARCHAR(30) NOT NULL UNIQUE CHECK(Len(Nombre)<=30),
Direccion VARCHAR(50),
Eliminado BIT NOT NULL DEFAULT 0
)

GO
CREATE TABLE Usuarios(
Documento INT NOT NULL PRIMARY KEY CHECK (Len(Documento)=8),
NombreCompleto VARCHAR (50) NOT NULL,
NombreLogueo VARCHAR (20) UNIQUE NOT NULL ,
Contrasenia VARCHAR (7) NOT NULL CHECK (Len(Contrasenia)=7)
);
GO

CREATE TABLE Administradores(
Documento INT PRIMARY KEY FOREIGN KEY REFERENCES Usuarios(Documento),
EjecutaSorteo BIT NOT NULL
);
GO

CREATE TABLE Jugadores(
Documento INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Documento),
NumeroCuentaBancaria INT NOT NULL,
RutBanco VARCHAR(12) NOT NULL FOREIGN KEY REFERENCES Bancos(RUT),
AgregadoPor INT NOT NULL FOREIGN KEY REFERENCES Administradores(Documento),
PRIMARY KEY(Documento)
);
GO

CREATE TABLE Sorteos(
FechaHora DATETIME PRIMARY KEY CHECK(FechaHora > GETDATE())
);
GO

CREATE TABLE NumerosSorteados(
FechaHoraSorteo DATETIME FOREIGN KEY REFERENCES Sorteos(FechaHora),
NumeroSorteado INT CHECK(NumeroSorteado >= 0 AND NumeroSorteado <= 50)
PRIMARY KEY (FechaHoraSorteo, NumeroSorteado)
)
GO

CREATE TABLE Jugadas(
Id INT,
DocumentoJugador INT NOT NULL FOREIGN KEY REFERENCES Jugadores(Documento),
FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
FechaHoraSorteo DATETIME NOT NULL FOREIGN KEY REFERENCES Sorteos(FechaHora)  
PRIMARY KEY (DocumentoJugador, Id)
)
GO

CREATE TABLE NumerosJugada(
IdJugada INT,
DocJugador INT,
FOREIGN KEY(DocJugador, IDJugada) REFERENCES Jugadas(DocumentoJugador, Id),
Numero INT CHECK(Numero >= 0 AND Numero <= 50),
PRIMARY KEY (DocJugador, IdJugada, Numero)
);
GO

---------------------------------------------------SP USUARIOS---------------------------------------------------

--ADMINISTRADORES--

CREATE PROCEDURE ListarAdministradores
AS
BEGIN
	SELECT Usuarios.*, Administradores.EjecutaSorteo
	FROM Usuarios INNER JOIN Administradores
		ON Usuarios.Documento = Administradores.Documento
END
GO

CREATE PROCEDURE BuscarAdministrador
@documento INT
AS
BEGIN
	SELECT Usuarios.*, Administradores.EjecutaSorteo
	FROM Usuarios INNER JOIN Administradores
		ON Usuarios.Documento = Administradores.Documento
	WHERE Usuarios.Documento = @documento
END
GO

CREATE PROCEDURE LogueoAdmin
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7)
AS
BEGIN
	SELECT *
	FROM Usuarios INNER JOIN Administradores
		ON Usuarios.Documento = Administradores.Documento
	WHERE NombreLogueo = @nombreLogueo AND Contrasenia = @contrasenia
END
GO

CREATE PROCEDURE AltaAdministrador
@documento INT,
@nombreCompleto VARCHAR(50),
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7),
@ejecutaSorteo BIT
AS
BEGIN
	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI YA EXISTENTE
		END

	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE NombreLogueo = @nombreLogueo
		)
		BEGIN
			RETURN -2; --USUARIO DE LOGUEO YA EXISTENTE
		END

		BEGIN TRANSACTION;

		INSERT INTO Usuarios
		VALUES(@documento, @nombreCompleto, @nombreLogueo, @contrasenia);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR AGREGAR USUARIO
		END

		INSERT INTO Administradores
		VALUES(@documento, @ejecutaSorteo);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR AGREGAR ADMINISTRADOR
		END
		
		COMMIT TRANSACTION;
END
GO 

CREATE PROCEDURE BajaAdministrador
@documento INT
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Administradores
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI NO EXISTENTE
		END

	BEGIN TRANSACTION;

		DELETE Administradores
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -2; --ERROR AL INTENTAR ELIMINAR ADMINISTRADOR
		END

		DELETE Usuarios
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR ELIMINAR USUARIO
		END

	COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE ModificarAdministrador
@documento INT,
@nombreCompleto VARCHAR(50),
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7),
@ejecutaSorteo BIT
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Administradores
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI  NO EXISTENTE
		END

	IF EXISTS (
	SELECT *
	FROM Usuarios
	WHERE NombreLogueo = @nombreLogueo AND Documento <> @documento
	)

	BEGIN
		RETURN -2; --USUARIO YA EXISTENTE
	END

	BEGIN TRANSACTION;

		UPDATE Administradores
		SET EjecutaSorteo = @ejecutaSorteo
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3; --ERROR AL INTENTAR MODIFICAR ADMINISTRADOR
		END

		UPDATE Usuarios
		SET NombreCompleto = @nombreCompleto, Contrasenia = @contrasenia, NombreLogueo = @nombreLogueo
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR MODIFICAR USUARIO
		END
		
	COMMIT TRANSACTION;
END
GO 


--JUGADORES--
CREATE PROCEDURE BuscarJugador
@documento INT
AS
BEGIN
	SELECT Usuarios.*, Jugadores.*
	FROM Usuarios INNER JOIN Jugadores
		ON Usuarios.Documento = Jugadores.Documento
	WHERE Usuarios.Documento = @documento
END
GO

CREATE PROCEDURE LogueoJugador
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7)
AS
BEGIN
	SELECT *
	FROM Usuarios INNER JOIN Jugadores
		ON Usuarios.Documento = Jugadores.Documento
	WHERE NombreLogueo = @nombreLogueo AND Contrasenia = @contrasenia
END
GO

CREATE PROCEDURE AltaJugador
@documento INT,
@nombreCompleto VARCHAR(50),
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7),
@numeroCuentaBancaria INT,
@rutBanco VARCHAR(12),
@aceptadoPor INT
AS
BEGIN
	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI YA EXISTENTE
		END

	IF EXISTS (
		SELECT *
		FROM Usuarios
		WHERE NombreLogueo = @nombreLogueo
		)
		BEGIN
			RETURN -2; --USUARIO DE LOGUEO YA EXISTENTE
		END

	IF NOT EXISTS (
		SELECT *
		FROM Administradores
		WHERE Documento = @aceptadoPor
		)
		BEGIN
			RETURN -4; --CI DE ADMINISTRADOR NO EXISTENTE
		END

	BEGIN TRANSACTION;
		INSERT INTO Usuarios
		VALUES(@documento, @nombreCompleto, @nombreLogueo, @contrasenia);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR AGREGAR USUARIO
		END	
				
		INSERT INTO Jugadores
		VALUES(@documento, @numeroCuentaBancaria, @rutBanco, @aceptadoPor);

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -5; --ERROR AL INTENTAR AGREGAR JUGADOR
		END

	COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE BajaJugador
@documento INT
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Jugadores
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI NO EXISTENTE
		END

		DECLARE @jugadasPremiadas INT

		SELECT @jugadasPremiadas = COUNT(*)
		FROM Jugadas J
			WHERE (
			SELECT COUNT(*)
			FROM NumerosJugada NJ
			WHERE NJ.IdJugada = J.Id AND J.DocumentoJugador = NJ.DocJugador AND NJ.Numero IN(
				SELECT NS.NumeroSorteado
				FROM NumerosSorteados NS
				WHERE NS.FechaHoraSorteo = J.FechaHoraSorteo
					)
				) = 10 and J.DocumentoJugador = @documento

		IF @jugadasPremiadas > 0
		RETURN -4; --ERROR USUARIO CON JUGADAS PREMIADAS


	BEGIN TRANSACTION;

		IF EXISTS (
			SELECT *
			FROM Jugadas
			WHERE Jugadas.DocumentoJugador = @documento
		)
		BEGIN
			DELETE Jugadas
			WHERE DocumentoJugador = @documento;

			IF @@ERROR <> 0 
			BEGIN
				ROLLBACK TRANSACTION;

				RETURN -5; --ERROR AL INTENTAR ELIMINAR JUGADAS DEL JUGADOR
			END
		END

		DELETE Jugadores
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -2; --ERROR AL INTENTAR ELIMINAR ADMINISTRADOR
		END

		DELETE Usuarios
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -3; --ERROR AL INTENTAR ELIMINAR USUARIO
		END

	COMMIT TRANSACTION;
END
GO

CREATE PROCEDURE ModificarJugador
@documento INT,
@nombreCompleto VARCHAR(50),
@nombreLogueo VARCHAR (20),
@contrasenia VARCHAR (7),
@numeroCuenta INT,
@rutBanco VARCHAR(12)
AS
BEGIN
	IF NOT EXISTS (
		SELECT *
		FROM Jugadores
		WHERE Documento = @documento
		)
		BEGIN
			RETURN -1; --CI  NO EXISTENTE
		END

	IF EXISTS (
	SELECT *
	FROM Usuarios
	WHERE NombreLogueo = @nombreLogueo AND Documento <> @documento
	)

	BEGIN
		RETURN -2; --USUARIO YA EXISTENTE
	END

	BEGIN TRANSACTION;

		UPDATE Jugadores
		SET NumeroCuentaBancaria = @numeroCuenta, RutBanco = @rutBanco
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;
			RETURN -3; --ERROR AL INTENTAR MODIFICAR JUGADOR
		END

		UPDATE Usuarios
		SET NombreCompleto = @nombreCompleto, Contrasenia = @contrasenia, NombreLogueo = @nombreLogueo
		WHERE Documento = @documento;

		IF @@ERROR <> 0 
		BEGIN
			ROLLBACK TRANSACTION;

			RETURN -4; --ERROR AL INTENTAR MODIFICAR USUARIO
		END
		
	COMMIT TRANSACTION;
END
GO 

---------------------------------------------------SP BANCO---------------------------------------------------

CREATE VIEW BuscarBanco
AS
	SELECT * FROM Bancos;
GO

CREATE PROCEDURE BuscarBancoActivo
@rut VARCHAR(12)
AS
BEGIN
	SELECT * FROM BuscarBanco WHERE RUT = @rut AND Eliminado = 0;
END 
GO

CREATE PROCEDURE BuscarBancoSinFiltro
@rut VARCHAR(12)
AS
BEGIN
	SELECT * FROM BuscarBanco WHERE RUT = @rut; 
END 
GO

CREATE PROCEDURE ListarBancos 
AS
BEGIN
	SELECT * FROM Bancos WHERE Eliminado = 0;
END
GO

CREATE PROCEDURE AltaBanco
@rut VARCHAR(12),
@nombre VARCHAR(30),
@Direccion VARCHAR(50)
AS
BEGIN
	IF EXISTS(SELECT * FROM BuscarBanco WHERE RUT = @rut AND Eliminado = 0)
		RETURN -1; --YA EXISTE EL BANCO EN LA TABLA

	IF EXISTS(SELECT * FROM BuscarBanco WHERE Nombre = @nombre)
		RETURN -2; --YA EXISTE ESE NOMBRE EN LA TABLA BANCOS

	IF EXISTS(SELECT * FROM BuscarBanco WHERE RUT = @rut AND Eliminado = 1)
		UPDATE Bancos SET Eliminado = 0 WHERE RUT = @rut; --SI EL BANCO ESTA ELIMINADO LOGICAMENTE SE LO VUELVE A UTILIZAR
	ELSE
		INSERT INTO Bancos(RUT, Nombre, Direccion) VALUES (@rut, @nombre, @Direccion);
END
GO

CREATE PROCEDURE BajaBanco
@rut VARCHAR(12)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM BuscarBanco WHERE RUT = @rut)
		RETURN -1; --NO EXISTE EL BANCO

	IF EXISTS(SELECT * FROM BuscarBanco WHERE  RUT = @rut AND Eliminado = 1)
		RETURN -2;--EL BANCO ESTA ELIMINADO LOGICAMENTE 

	IF EXISTS(SELECT * FROM Jugadores WHERE RutBanco = @rut)
		UPDATE Bancos SET Eliminado = 1 WHERE RUT = @rut; --SI HAY JUGADORES QUE USAN EL BANCO SE DA DE BAJA LOGICA
	ELSE
		DELETE Bancos WHERE RUT = @rut; --BAJA FISICA DEL BANCO
END
GO

CREATE PROCEDURE ModificarBanco
@rut VARCHAR(12),
@nombre VARCHAR(30),
@Direccion VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM BuscarBanco WHERE RUT = @rut AND Eliminado = 0)
		RETURN -1; --NO EXISTE EL BANCO

	IF EXISTS(SELECT * FROM BuscarBanco WHERE RUT <> @rut AND Nombre = @nombre)
		RETURN -2; --YA EXISTE EL NOMBRE EN LA TABLA PARA OTRO BANCO

	UPDATE BANCOS SET Nombre = @nombre, Direccion = @Direccion WHERE RUT = @rut;
END
GO

-----------------------------------------------------------------------------------------------------------
------------------------creacion de usuarios IIS para poder acceder a la bd---------------------------------
-----------------------------------------------------------------------------------------------------------

--usuario por defecto
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS WITH DEFAULT_DATABASE = master
GO

USE Final2015
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

GRANT EXECUTE TO [IIS APPPOOL\DefaultAppPool];
GO

-----------------------------------------------------------

use master
go

CREATE LOGIN [UsuarioLogueo] WITH  PASSWORD = '123';
GO

USE Final2015
GO

CREATE USER [UsuarioBD] FOR LOGIN [UsuarioLogueo]
GO

GRANT EXECUTE TO [UsuarioBD];
GO

--usuario de logueo creado desde app
CREATE PROCEDURE NuevoUsuarioLogueoYBD
@nombreUsuario VARCHAR(20),
@rol VARCHAR(30),
@permiso VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM Administradores INNER JOIN Usuarios
			ON Administradores.Documento = Usuarios.Documento
			WHERE Usuarios.NombreLogueo = @nombreUsuario) 
	BEGIN
		RETURN -1; --EL NOMBRE DE USUARIO NO PERTENECE A UN ADMINISTRADOR
	END

	IF EXISTS(SELECT * FROM sys.server_principals WHERE name = @nombreUsuario)
		RETURN -2 --YA EXISTE EL USUARIO DE BD
	ELSE
	BEGIN
		BEGIN TRANSACTION;

			DECLARE @pass VARCHAR (7);
			SET @pass = (SELECT Contrasenia FROM Usuarios WHERE @nombreUsuario = NombreLogueo);

			--CREACION USUARIO DE LOGUEO--
			DECLARE @SentenciaLogueo VARCHAR(200)
			SET @SentenciaLogueo = 'CREATE LOGIN [' + @nombreUsuario + '] WITH  PASSWORD = ' + QUOTENAME(@pass, '''');
			
			EXECUTE (@SentenciaLogueo)

			IF @@ERROR <> 0
			BEGIN 
				ROLLBACK TRANSACTION;

				RETURN -3; --ERROR AL CREAR USUARIO DE LOGUEO
			END

			--CREACION USUARIO BD--
			DECLARE @SentenciaBD VARCHAR(200);
			SET @SentenciaBD = 'CREATE USER ['+ @nombreUsuario + '] FROM LOGIN [' + @nombreUsuario + ']';
			EXECUTE (@SentenciaBD);

			IF @@ERROR <> 0
			BEGIN
				ROLLBACK TRANSACTION;

				RETURN -5; --ERROR AL CREAR USUARIO DE BD
			END
			
			--SE OTORGA EL PERMISO SELCCIONADO
			DECLARE @SentenciaPermiso VARCHAR(200);
			SET @SentenciaPermiso = 'GRANT ' + @permiso + ' TO [' + @nombreUsuario + ']';
			EXECUTE (@SentenciaPermiso);


		COMMIT TRANSACTION;

		
		--ASIGNACION DE ROL PARA EL USUARIO DE SERVIDOR
		IF (@rol <> 'Public')
			EXEC sp_addsrvrolemember @loginame=@nombreUsuario, @rolename=@rol

		IF @@ERROR <> 0
			RETURN -4; --ERROR AL ASIGNAR EL ROL DE SERVIDOR AL USUARIO

	END
END
GO
--use 
--master
--DROP DATABASE Final2015
--go

--################--CREACION USUARIO DE LOGUEO Y BASE DE DATOS--#################--

--AYUDA PARA BORRAR USUARIOS--
--Declare @VarSentencia varchar(200)
--set @VarSentencia = 'Drop Login [Chuck]'	
--Exec (@VarSentencia)

--SELECT * FROM sys.server_principals WHERE name = 'Chuck'

--DECLARE @resultado INT;

--EXECUTE @resultado = NuevoUsuarioLogueoYBD 'Chuck', 'securityadmin', 'Execute';

--PRINT @resultado;
--GO
---------------------------------------------------SP JUGADAS---------------------------------------------------

CREATE PROCEDURE AltaJugada
@documento INT,
@fechaHoraSorteo DATETIME
AS
BEGIN
	IF NOT EXISTS(
		SELECT *
		FROM Jugadores
		WHERE Documento = @documento
	)
	BEGIN
		RETURN 1; --No existe el jugador
	END

	IF NOT EXISTS(
		SELECT *
		FROM Sorteos
		WHERE FechaHora = @fechaHoraSorteo
	)
	BEGIN
		RETURN 2; --No existe un sorteo con esa fecha y hora
	END

	IF EXISTS (
		SELECT *
		FROM Jugadas
		WHERE DocumentoJugador = @documento AND FechaHoraSorteo = @fechaHoraSorteo
	)
	BEGIN
		RETURN 3; --El jugador ya realizó una jugada para ese sorteo
	END

	DECLARE @id INT = (select count(*) from Jugadas where DocumentoJugador = @documento) + 1


	INSERT INTO Jugadas(Id, DocumentoJugador, FechaHoraSorteo)
	VALUES(@id, @documento, @fechaHoraSorteo);

	IF @@ERROR <> 0
		BEGIN
			RETURN 4; --No se pudo agregar la jugada
		END
	
	ELSE
	
		BEGIN
			return @id;
		END
END

go

CREATE PROCEDURE AltaNumerosJugada
@idJugada INT,
@documentoJugador INT,
@numero INT
AS
BEGIN
	IF NOT EXISTS(
		SELECT *
		FROM Jugadas
		WHERE Id = @idJugada AND DocumentoJugador = @documentoJugador
	)
	BEGIN
		RETURN 1; --No existe la jugada
	END

	IF EXISTS (
		SELECT * 
		FROM NumerosJugada
		WHERE IdJugada = @idJugada AND DocJugador = @documentoJugador AND Numero = @numero
	)
	BEGIN
		RETURN 2; --Número repetido en la jugada
	END

	IF(SELECT COUNT(*) FROM NumerosJugada WHERE IdJugada = @idJugada AND DocJugador = @documentoJugador) = 10
	BEGIN
		RETURN 3; --Se superan los 10 números por jugada
	END

	INSERT INTO NumerosJugada (IdJugada, DocJugador, Numero)
	VALUES(@idJugada, @documentoJugador, @numero);

	IF @@ERROR <> 0
	BEGIN
		RETURN 4; --No se pudo agregar el número a la jugada
	END
END

go

CREATE PROCEDURE BuscarJugada
@id INT,
@documento INT
AS
BEGIN
	SELECT *
	FROM Jugadas
	WHERE Id = @id AND DocumentoJugador = @documento;
END

go

CREATE PROCEDURE NumerosDeJugada
@id INT,
@documento INT
AS
BEGIN
	SELECT * 
	FROM NumerosJugada 
	WHERE IdJugada = @id AND DocJugador = @documento
END

go
CREATE PROCEDURE ListarJugadasDeJugador
@documento INT
AS
BEGIN
	SELECT * FROM Jugadas WHERE DocumentoJugador = @documento
END
go

CREATE PROCEDURE ListarJugadasPremiadasPorSorteo
@fechaHoraSorteo DATETIME
AS
BEGIN
	SELECT * 
	FROM Jugadas J
	WHERE (
		SELECT COUNT(*)
		FROM NumerosJugada NJ
		WHERE NJ.IdJugada = J.Id AND J.DocumentoJugador = NJ.DocJugador AND NJ.Numero IN(
			SELECT NS.NumeroSorteado
			FROM NumerosSorteados NS
			WHERE NS.FechaHoraSorteo = J.FechaHoraSorteo
				)
			) = 10 and J.FechaHoraSorteo = @fechaHoraSorteo
END

go

CREATE PROCEDURE ListarJugadasPremiadas
@documento INT
AS
BEGIN
	SELECT * 
	FROM Jugadas J
	WHERE (
		SELECT COUNT(*)
		FROM NumerosJugada NJ
		WHERE NJ.IdJugada = J.Id AND J.DocumentoJugador = NJ.DocJugador AND NJ.Numero IN(
			SELECT NS.NumeroSorteado
			FROM NumerosSorteados NS
			WHERE NS.FechaHoraSorteo = J.FechaHoraSorteo
				)
			) = 10 and J.DocumentoJugador = @documento
END

go
CREATE PROCEDURE GenerarSorteo --ALTA DEL SORTEO EN BD
@fechaYHora DATETIME
AS
BEGIN
	IF EXISTS (
		SELECT *
		FROM Sorteos
		WHERE FechaHora = @fechaYHora
	)
	BEGIN
		RETURN 1; --Ya existe un sorteo en esa fecha y hora
	END

	IF @fechaYHora < GETDATE()
	BEGIN
		RETURN 2; --Fecha y hora incorrectas
	END

	INSERT INTO Sorteos
	VALUES(@fechaYHora);

	IF @@ERROR <>0
	BEGIN
		RETURN 3; -- No se pudo agregar el sorteo
	END
END

GO

CREATE PROCEDURE AltaNumerosSorteados --Realización del sorteo (carga de los números sorteados)	
@fechaYHoraSorteo DATETIME,
@numero INT
AS
BEGIN
	IF EXISTS(
		SELECT *
		FROM NumerosSorteados
		WHERE FechaHoraSorteo = @fechaYHoraSorteo AND NumeroSorteado = @numero
	)
	BEGIN
		RETURN 1; --Ya se agregó ese número al sorteo
	END

	IF (SELECT COUNT(*) FROM NumerosSorteados WHERE FechaHoraSorteo = @fechaYHoraSorteo) = 15
	BEGIN
		RETURN 3; --Se intenta agregar más de 15 número al sorteo
	END

	INSERT INTO NumerosSorteados
	VALUES (@fechaYHoraSorteo, @numero);

	IF @@ERROR <>0
	BEGIN
		RETURN 4; --No se pudo agregar el sorteo
	END
END

GO

CREATE PROCEDURE BuscarSorteo
@fecha DATETIME
AS
BEGIN
	SELECT * 
	FROM Sorteos 
	WHERE FechaHora = @fecha
END

GO

CREATE PROCEDURE ListaNumerosSorteados
@fechaHoraSorteo DATETIME
AS
BEGIN
	SELECT *
	FROM NumerosSorteados
	WHERE FechaHoraSorteo = @fechaHoraSorteo;
END

GO


CREATE PROCEDURE ListaSorteosDisponibles --Aún no sorteados
AS
BEGIN
	SELECT * FROM Sorteos
	WHERE FechaHora NOT IN (
		SELECT FechaHoraSorteo
		FROM NumerosSorteados
	) AND FechaHora > GETDATE() 
END
GO


CREATE PROCEDURE ListaSorteosDisponiblesJugador --Aún no sorteados y sin jugada del jugador indicado
@Documento int
AS
BEGIN
	SELECT * FROM Sorteos
	WHERE FechaHora NOT IN (
		SELECT FechaHoraSorteo
		FROM NumerosSorteados
	) AND FechaHora > GETDATE() and FechaHora NOT IN(SELECT FechaHoraSorteo FROM Jugadas WHERE DocumentoJugador = @Documento) 
END
GO

-----------------------------------------------------------------------------------------------------------
--------------------------------------------DATOS DE PRUEBA -----------------------------------------------
-----------------------------------------------------------------------------------------------------------

--################--BANCOS--#################--

EXECUTE AltaBanco '111111111111', 'Santander', 'Avda Brasil 2212';
EXECUTE AltaBanco '222222222222', 'BROU', '18 de Julio 4452';
EXECUTE AltaBanco '333333333333', 'Galicia', 'Avda Agraciada 709';
EXECUTE AltaBanco '444444444444', 'Boston', 'Boulevard Artigas 8755';
GO

----------- SP AltaBanco -------------

--DECLARE @resultado INT;

----EXECUTE @resultado = AltaBanco '111111111111', 'Santander', 'Avda Brasil 2212'; --ERROR -1 (YA EXISTE EL BANCO)
----EXECUTE @resultado = AltaBanco '555555555555', 'Galicia', 'Avda Brasil 2212'; --ERROR -2 (YA EXISTE ESE NOMBRE EN LA TABLA BANCOS)
----EXECUTE @resultado = AltaBanco '555555555555', 'Itaú', 'Paraguay 2214'; --Alta correcta

--PRINT @resultado;
--GO

----------- SP BajaBanco -------------

--DECLARE @resultado INT;

----EXECUTE @resultado = BajaBanco '999999999999'; --ERROR -1 (NO EXISTE EL BANCO)
----EXECUTE @resultado = BajaBanco '222222222222'; -- SI HAY JUGADORES QUE USAN EL BANCO SE DA DE BAJA LOGICA
----EXECUTE @resultado = BajaBanco '444444444444'; --Baja correcta

--PRINT @resultado;
--GO

----------- SP ModificarBanco -------------

--DECLARE @resultado INT;

----EXECUTE @resultado = ModificarBanco'999999999999','Santander', 'Avda Brasil 2212'; --ERROR -1 (NO EXISTE EL BANCO)
----EXECUTE @resultado = ModificarBanco '222222222222', 'Santander', '18 de Julio 4452'; --ERROR -1 (BANCO BORRADO LOGICAMENTE)
----EXECUTE @resultado = ModificarBanco '333333333333', 'Santander', '18 de Julio 4452'; --ERROR -2 (YA EXISTE EL NOMBRE EN LA TABLA PARA OTRO BANCO)
----EXECUTE @resultado = ModificarBanco '111111111111', 'BBVA', 'Calle de Tierra 2255'; --Modificacion correcta

--PRINT @resultado;
--GO

----------- SP ListarBancos -------------

--EXECUTE ListarBancos;

----------- SP BuscarBancoActivo -------------

--EXECUTE BuscarBancoActivo '222222222222'; --(NO SE ECUENTRA BANCO BORRADO LOGICAMENTE)
--EXECUTE BuscarBancoActivo '333333333333'; -- BUSQUEDA CORRECTA

----------- SP BuscarBancoSinFiltro -------------

--EXECUTE BuscarBancoSinFiltro '222222222222'; --(BANCO BORRADO LOGICAMENTE ENCONTRADO)
--EXECUTE BuscarBancoActivo '333333333333'; -- (BANCO SIN BORRADO LOGICO ENCONTRADO)
--go



--################--USUARIOS--#################--

EXECUTE AltaAdministrador 33333333, 'Chuck Norris', 'Chuck', '1234567', 1;
go

EXECUTE AltaAdministrador 22222222, 'Stephen Curry', 'Curry', '7654321', 0;
go

EXECUTE AltaJugador 12345678, 'Juan Salaberry', 'Juanito', '1234567', 12345, '222222222222','22222222';
go

EXECUTE AltaJugador 11111111, 'Maria Rodriguez', 'Maru', '1111111', 11111, '222222222222', '33333333';
go



--################--SORTEOS--#################--
EXECUTE GenerarSorteo '14/4/2017 23:00'
EXECUTE GenerarSorteo '7/12/2017 22:00'
EXECUTE GenerarSorteo '22/5/2017 21:30'
EXECUTE GenerarSorteo '16/8/2017 21:00'
EXECUTE GenerarSorteo '14/4/2017 19:00'
EXECUTE GenerarSorteo '30/3/2016 19:00'
go

EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 00;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 20;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 35;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 24;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 33;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 50;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 38;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 41;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 21;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 09;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 01;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 03;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 02;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 05;
EXECUTE AltaNumerosSorteados '30/3/2016 19:00', 29;
go

EXEC AltaJugada 11111111, '30/3/2016 19:00';
go

Exec AltaNumerosJugada 3, '11111111', 00;
Exec AltaNumerosJugada 3, '11111111', 20;
Exec AltaNumerosJugada 3, '11111111', 35;
Exec AltaNumerosJugada 3, '11111111', 24;
Exec AltaNumerosJugada 3, '11111111', 33;
Exec AltaNumerosJugada 3, '11111111', 50;
Exec AltaNumerosJugada 3, '11111111', 38;
Exec AltaNumerosJugada 3, '11111111', 41;
Exec AltaNumerosJugada 3, '11111111', 21;
Exec AltaNumerosJugada 3, '11111111', 09;
go


EXEC AltaJugada 12345678, '30/3/2016 19:00';
go

Exec AltaNumerosJugada 2, '12345678', 35;
Exec AltaNumerosJugada 2, '12345678', 24;
Exec AltaNumerosJugada 2, '12345678', 33;
Exec AltaNumerosJugada 2, '12345678', 50;
Exec AltaNumerosJugada 2, '12345678', 38;
Exec AltaNumerosJugada 2, '12345678', 41;
Exec AltaNumerosJugada 2, '12345678', 21;
Exec AltaNumerosJugada 2, '12345678', 09;
Exec AltaNumerosJugada 2, '12345678', 01;
Exec AltaNumerosJugada 2, '12345678', 03;
go

EXEC AltaJugada 12345678, '30/3/2016 19:00';
go

Exec AltaNumerosJugada 2, '12345678', 35;
Exec AltaNumerosJugada 2, '12345678', 24;
Exec AltaNumerosJugada 2, '12345678', 33;
Exec AltaNumerosJugada 2, '12345678', 50;
Exec AltaNumerosJugada 2, '12345678', 38;
Exec AltaNumerosJugada 2, '12345678', 41;
Exec AltaNumerosJugada 2, '12345678', 21;
Exec AltaNumerosJugada 2, '12345678', 09;
Exec AltaNumerosJugada 2, '12345678', 01;
Exec AltaNumerosJugada 2, '12345678', 28;
go

--################--JUGADAS--#################--

EXECUTE AltaJugada 11111111, '14/4/2017 19:00';
EXECUTE AltaJugada 11111111, '7/12/2017 22:00';
EXECUTE AltaJugada 12345678, '14/4/2017 22:00';
EXECUTE AltaJugada 12345678, '16/8/2017 21:00';
EXECUTE AltaJugada 22222222, '16/8/2017 21:00';
go
--Números de Jugada
EXECUTE AltaNumerosJugada 1, 11111111, 0;
EXECUTE AltaNumerosJugada 1, 11111111, 1;
EXECUTE AltaNumerosJugada 1, 11111111, 2;
EXECUTE AltaNumerosJugada 1, 11111111, 3;
EXECUTE AltaNumerosJugada 1, 11111111, 5;
EXECUTE AltaNumerosJugada 1, 11111111, 9;
EXECUTE AltaNumerosJugada 1, 11111111, 20;
EXECUTE AltaNumerosJugada 1, 11111111, 21;
EXECUTE AltaNumerosJugada 1, 11111111, 24;
EXECUTE AltaNumerosJugada 1, 11111111, 29;

go 

EXECUTE AltaNumerosJugada 2, 11111111, 20;
EXECUTE AltaNumerosJugada 2, 11111111, 21;
EXECUTE AltaNumerosJugada 2, 11111111, 22;
EXECUTE AltaNumerosJugada 2, 11111111, 23;
EXECUTE AltaNumerosJugada 2, 11111111, 24;
EXECUTE AltaNumerosJugada 2, 11111111, 25;
EXECUTE AltaNumerosJugada 2, 11111111, 26;
EXECUTE AltaNumerosJugada 2, 11111111, 27;
EXECUTE AltaNumerosJugada 2, 11111111, 28;
EXECUTE AltaNumerosJugada 2, 11111111, 29;

go

EXECUTE AltaNumerosJugada 3, 11111111, 01;
EXECUTE AltaNumerosJugada 3, 11111111, 00;
EXECUTE AltaNumerosJugada 3, 11111111, 24;
EXECUTE AltaNumerosJugada 3, 11111111, 14;
EXECUTE AltaNumerosJugada 3, 11111111, 18;
EXECUTE AltaNumerosJugada 3, 11111111, 25;
EXECUTE AltaNumerosJugada 3, 11111111, 10;
EXECUTE AltaNumerosJugada 3, 11111111, 09;
EXECUTE AltaNumerosJugada 3, 11111111, 36;
EXECUTE AltaNumerosJugada 3, 11111111, 50;

go

EXECUTE AltaNumerosJugada 1, 12345678, 01;
EXECUTE AltaNumerosJugada 1, 12345678, 00;
EXECUTE AltaNumerosJugada 1, 12345678, 24;
EXECUTE AltaNumerosJugada 1, 12345678, 14;
EXECUTE AltaNumerosJugada 1, 12345678, 18;
EXECUTE AltaNumerosJugada 1, 12345678, 25;
EXECUTE AltaNumerosJugada 1, 12345678, 10;
EXECUTE AltaNumerosJugada 1, 12345678, 09;
EXECUTE AltaNumerosJugada 1, 12345678, 36;
EXECUTE AltaNumerosJugada 1, 12345678, 50;

go

EXECUTE AltaNumerosJugada 2, 12345678, 01;
EXECUTE AltaNumerosJugada 2, 12345678, 00;
EXECUTE AltaNumerosJugada 2, 12345678, 24;
EXECUTE AltaNumerosJugada 2, 12345678, 14;
EXECUTE AltaNumerosJugada 2, 12345678, 18;
EXECUTE AltaNumerosJugada 2, 12345678, 25;
EXECUTE AltaNumerosJugada 2, 12345678, 10;
EXECUTE AltaNumerosJugada 2, 12345678, 09;
EXECUTE AltaNumerosJugada 2, 12345678, 36;
EXECUTE AltaNumerosJugada 2, 12345678, 50;

go

EXECUTE AltaNumerosJugada 5, 22222222, 00;
EXECUTE AltaNumerosJugada 5, 22222222, 50;
EXECUTE AltaNumerosJugada 5, 22222222, 49;
EXECUTE AltaNumerosJugada 5, 22222222, 25;
EXECUTE AltaNumerosJugada 5, 22222222, 33;
EXECUTE AltaNumerosJugada 5, 22222222, 22;
EXECUTE AltaNumerosJugada 5, 22222222, 05;
EXECUTE AltaNumerosJugada 5, 22222222, 06;
EXECUTE AltaNumerosJugada 5, 22222222, 24;
EXECUTE AltaNumerosJugada 5, 22222222, 01;

go