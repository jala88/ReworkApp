
CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre			varchar(100),
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	DECLARE @Rol	TINYINT = 2,
			@Estado	BIT		= 1

	IF NOT EXISTS(SELECT 1 FROM dbo.Usuario WHERE correo = @Correo)
	BEGIN

		INSERT INTO dbo.Usuario(Nombre,Correo,Contrasenna,id_perfil,Estado)
		VALUES (@Nombre,@Correo,@Contrasenna,@Rol,@Estado)

	END

END

CREATE PROCEDURE [dbo].[ConsultarRoles]

AS
BEGIN

	SELECT id_perfil 'value', Nombre 'text'
	FROM dbo.perfil
END
GO