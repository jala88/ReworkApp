Alter PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	SELECT	id_usuario,U.Nombre,Correo,U.id_perfil,Estado, EsTemporal, VigenciaTemporal
	FROM	dbo.Usuario U
	INNER JOIN dbo.Perfil  R ON U.id_perfil = R.id_perfil
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END

ALTER PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre			varchar(100),
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	DECLARE @Rol	TINYINT = 2,
			@Estado	BIT		= 1,
			@Temporal Bit = 0

	IF NOT EXISTS(SELECT 1 FROM dbo.Usuario WHERE correo = @Correo)
	BEGIN

		INSERT INTO dbo.Usuario(Nombre,Correo,Contrasenna,id_perfil,Estado,EsTemporal,VigenciaTemporal)
		VALUES (@Nombre,@Correo,@Contrasenna,@Rol,@Estado,@Temporal,GETDATE())

	END

END

CREATE PROCEDURE [dbo].[ActualizarUsuario]
	@Consecutivo	INT,
	@Nombre			VARCHAR(100),
	@Correo			VARCHAR(100),
	@IdRol			TINYINT
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.Usuario WHERE	(Correo = @Correo)
												AND id_usuario != @Consecutivo)
	BEGIN

		UPDATE Usuario
		   SET
			   Nombre = @Nombre,
			   Correo = @Correo,
			   id_perfil = @IdRol
		 WHERE id_usuario = @Consecutivo

	END

END

CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@id_usuario INT
AS
BEGIN

	SELECT	id_usuario,Nombre,Correo,U.id_perfil,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.descripcion
	FROM	dbo.Usuario U
	INNER JOIN dbo.Perfil  R ON U.id_perfil = R.id_perfil
	WHERE id_usuario = @id_usuario

END

CREATE  PROCEDURE [dbo].[ConsultarUsuarios]
	
AS
BEGIN

	SELECT	id_usuario,U.Nombre,Correo,U.id_perfil,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.descripcion
	FROM	dbo.Usuario U
	INNER JOIN dbo.Perfil  R ON U.id_perfil = R.id_perfil
END


CREATE PROCEDURE [dbo].[ConsultarRoles]

AS
BEGIN

	SELECT id_perfil 'value', Descripcion 'text'
	FROM Perfil

END

CREATE PROCEDURE [dbo].[ConsultarUsuarioNombre]
	@Nombre varchar(100)
AS
BEGIN

	SELECT	id_usuario,Nombre,Correo,U.id_perfil,
	CASE WHEN Estado = 1 THEN 'Activo' ELSE 'Inactivo' END 'Estado',R.Descripcion
	FROM	dbo.Usuario U
	INNER JOIN dbo.Perfil  R ON U.id_perfil = R.id_perfil
	WHERE Nombre = @Nombre

END
GO

ALTER PROCEDURE [dbo].[ActualizarContrasenna]
	@id_usuario int, 
	@Contrasenna VARCHAR(100),
	@EsTemporal	 BIT, 
	@VigenciaTemporal DATETIME
AS
BEGIN

	UPDATE Usuario
	   SET Contrasenna = @Contrasenna,
		   EsTemporal = @EsTemporal,
		   VigenciaTemporal = @VigenciaTemporal
	 WHERE id_usuario = @id_usuario

END

select * from usuario

exec ConsultarUsuarioNombre 'jesus'