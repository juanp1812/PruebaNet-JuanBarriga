USE master
go
CREATE DATABASE PruebaNet
GO
USE PruebaNet
GO

CREATE TABLE Bancos(
	iBanco			INT	IDENTITY,
	xNombreBanco	VARCHAR(250),
	xDireccion		VARCHAR(250),
	iEstado			INT,
	dFechaRegistro	DATETIME
)
INSERT INTO Bancos VALUES('BCP', 'Direccion central', 1, GETDATE())



CREATE TABLE Sucursales(
	iSucursal		INT IDENTITY,
	iBanco			INT,
	xNombre			varchar(250),
	xDireccion		VARCHAR(250),
	iEstado			INT,
	dFechaRegistro	DATETIME
)
INSERT INTO Sucursales VALUES( 1, 'Surco', 'proceres 2121', 1, GETDATE() )
go

CREATE TABLE Estados(
	iEstado				INT,
	xDescripcionEstado	VARCHAR(150)
)
INSERT INTO Estados VALUES(1, 'Pagada')
INSERT INTO Estados VALUES(2, 'Declinada')
INSERT INTO Estados VALUES(3, 'Fallida')
INSERT INTO Estados VALUES(4, 'Anulada')

CREATE TABLE OrdenesPago(
	iOrden			INT IDENTITY,
	iSucursal		INT,
	fMonto			FLOAT,
	iTipoMoneda		INT,
	iEstado			INT,
	dFechaPago		DATETIME
)

go
INSERT INTO dbo.OrdenesPago
        ( iSucursal ,
          fMonto ,
          iTipoMoneda ,
          iEstado ,
          dFechaPago
        )
VALUES  ( 1 , -- iSurcusal - int
          25.50 , -- fMonto - float
          1 , -- iTipoMoneda - int
          1 , -- iEstado - int
          '2017-01-24 04:44:27'  -- dFechaPago - datetime
        )
go

CREATE TABLE Monedas(
	iMoneda	INT,
	xMoneda VARCHAR(150)
)
INSERT INTO dbo.Monedas(iMoneda, xMoneda)VALUES ( 1, 'Soles')
INSERT INTO dbo.Monedas(iMoneda, xMoneda)VALUES ( 2, 'Dólares')


go
-------------------------------------------------------------------
CREATE PROCEDURE sp_listarBancos
AS
	SELECT iBanco, xNombreBanco, xDireccion, CONVERT(VARCHAR, dFechaRegistro, 103)dFechaRegistro
	FROM dbo.Bancos
	WHERE iEstado = 1
	ORDER BY ibanco desc
go

CREATE PROCEDURE sp_ObtenerBanco
	@iBanco	INT
AS
	SELECT iBanco, xNombreBanco, xDireccion, CONVERT(VARCHAR, dFechaRegistro, 103)dFechaRegistro
	FROM dbo.Bancos
	WHERE iBanco = @iBanco


go
CREATE PROCEDURE sp_insertBanco(
	@xNombreBanco	VARCHAR(250),
	@xDireccion		VARCHAR(250)
)
AS
	INSERT INTO dbo.Bancos(xNombreBanco, xDireccion, iEstado, dFechaRegistro)
	VALUES (@xNombreBanco, @xDireccion, 1, GETDATE())
	SELECT @@IDENTITY
GO

CREATE PROCEDURE sp_updateBanco(
	@iBanco			INT,
	@xNombreBanco	VARCHAR(250),
	@xDireccion		VARCHAR(250)
)
AS
	UPDATE dbo.Bancos
		SET xNombreBanco = @xNombreBanco,
			xDireccion = @xDireccion
	WHERE iBanco = @iBanco
	SELECT @iBanco
GO

CREATE PROCEDURE sp_deleteBanco(
	@iBanco			INT
)
AS
	UPDATE dbo.Bancos
			SET iEstado = 0
		WHERE iBanco = @iBanco
		SELECT @iBanco
GO
-----------------------------------------------------------

CREATE PROCEDURE sp_ObtenerSucursalesPorBanco(
	@iBanco	INT
)as
	SELECT suc.iSucursal, ba.iBanco, ba.xNombreBanco, xNombre, suc.xDireccion, CONVERT(VARCHAR, suc.dFechaRegistro, 103)dFechaRegistro
	FROM dbo.Sucursales suc
	INNER JOIN dbo.Bancos ba ON ba.iBanco = suc.iBanco
	WHERE suc.iBanco = @iBanco
	AND suc.iEstado = 1
	ORDER BY 1 desc
GO

CREATE PROCEDURE sp_insertSucursal(
	@iBanco				INT,
	@xNombreSucursal	VARCHAR(250),
	@xDireccion			VARCHAR(250)
)
AS
	INSERT INTO dbo.Sucursales(iBanco,xNombre, xDireccion, dFechaRegistro, iEstado)
	VALUES (@iBanco, @xNombreSucursal, @xDireccion, GETDATE(), 1)
	SELECT @@IDENTITY
go	

CREATE PROCEDURE sp_updateSucursal(
	@iSucursal			INT,
	@xNombreSucursal	VARCHAR(250),
	@xDireccion			VARCHAR(250)
)
AS
	UPDATE dbo.Sucursales
	SET xNombre = @xNombreSucursal,
		xDireccion = @xDireccion
	WHERE iSucursal = @iSucursal
	SELECT @iSucursal

go

CREATE PROCEDURE sp_ObtenerSucursalesPorId(
	@iSucursal	INT
)as
	SELECT suc.iSucursal, ba.iBanco, ba.xNombreBanco, xNombre, suc.xDireccion, CONVERT(VARCHAR, suc.dFechaRegistro, 103)dFechaRegistro
	FROM dbo.Sucursales suc
	INNER JOIN dbo.Bancos ba ON ba.iBanco = suc.iBanco
	WHERE suc.iSucursal = @iSucursal
GO


CREATE PROCEDURE sp_deleteSucursal(
	@isucursal			INT
)
AS
	UPDATE dbo.Sucursales
			SET iEstado = 0
		WHERE iSucursal = @isucursal
		SELECT @isucursal
GO


---------------------------------


CREATE PROCEDURE sp_obtenerOrdenesPago(
	@iSucursal	int
)as
	SELECT ord.iOrden, ord.iSucursal, ord.fMonto, ord.iTipoMoneda, (CASE ord.iTipoMoneda
																		WHEN 1 THEN 'Soles'
																		ELSE 'Dólares' end)xMoneda,
	ord.iEstado, CONVERT(VARCHAR, ord.dFechaPago, 103)dFechaPago, es.xDescripcionEstado
    FROM dbo.OrdenesPago ord
	INNER JOIN dbo.Estados es ON ord.iestado = es.iestado
	WHERE iSucursal = @iSucursal
	ORDER BY 1 DESC
go

CREATE PROCEDURE sp_obtenerOrdenPagoPorId(
	@iOrdenPago	int
)as
	SELECT ord.iOrden, ord.iSucursal, ord.fMonto, ord.iTipoMoneda, (CASE ord.iTipoMoneda
																		WHEN 1 THEN 'Soles'
																		ELSE 'Dólares' end)xMoneda,
	ord.iEstado, CONVERT(VARCHAR, ord.dFechaPago, 103)dFechaPago, es.xDescripcionEstado
    FROM dbo.OrdenesPago ord
	INNER JOIN dbo.Estados es ON ord.iestado = es.iestado
	WHERE iOrden = @iOrdenPago
go



create PROCEDURE sp_insertOrdenPago(
	@iSucursal		INT,
	@fMonto			FLOAT,
	@iTipoMoneda	INT
)AS
	INSERT INTO dbo.OrdenesPago(iSucursal, fMonto, iTipoMoneda, iEstado, dFechaPago)
	VALUES (@iSucursal, @fMonto, @iTipoMoneda, 1, GETDATE())
	SELECT @@IDENTITY
GO

CREATE PROCEDURE sp_listarEstados
AS
SELECT*FROM dbo.Estados
GO

create PROCEDURE sp_modificarOrdenPago(
	@iOrdenPago		INT,
	@fMonto			FLOAT,
	@iTipoMoneda	INT,
	@iEstado		INT
)AS

	UPDATE dbo.OrdenesPago
		SET fMonto = @fMonto,
			iTipoMoneda = @iTipoMoneda,
			iEstado = @iEstado
	WHERE iOrden = @iOrdenPago
	SELECT @iOrdenPago

go 

CREATE PROCEDURE sp_obtenerOrdenesPagoPorSucursalTipoMoneda(
	@iSucursal		INT,
	@iTipoMoneda	int
)as
	SELECT ord.iOrden, ord.iSucursal, sr.xNombre,ord.fMonto, ord.iTipoMoneda, (CASE ord.iTipoMoneda
																		WHEN 1 THEN 'Soles'
																		ELSE 'Dólares' end)xMoneda,
	ord.iEstado, CONVERT(VARCHAR, ord.dFechaPago, 103)dFechaPago, es.xDescripcionEstado
    FROM dbo.OrdenesPago ord
		INNER JOIN dbo.Estados es ON ord.iestado = es.iestado	
		INNER JOIN dbo.Sucursales sr ON sr.iSucursal = ord.iSucursal
	WHERE iTipoMoneda = @iTipoMoneda
	AND ord.iSucursal = @iSucursal
	ORDER BY 1 DESC
go




CREATE PROCEDURE sp_listarMonedas
AS
SELECT*FROM dbo.Monedas


