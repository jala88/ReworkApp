USE [REWORKAPP_BD]

CREATE PROCEDURE ConsultarTipoReworks
AS
BEGIN
	SELECT           
		id_tipo_rework
		, descripcion
	FROM dbo.Tipo_Rework     
END
GO
