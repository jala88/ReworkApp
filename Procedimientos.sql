CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(100),
	@Contrasenna	varchar(100)
AS
BEGIN

	SELECT	id_usuario,U.Nombre,Correo,U.id_perfil,Estado
	FROM	dbo.Usuario U
	INNER JOIN dbo.Perfil  R ON U.id_perfil = R.id_perfil
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END

Exec IniciarSesion 'admin@gmail.com', 'admin123';