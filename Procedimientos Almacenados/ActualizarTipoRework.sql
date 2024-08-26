CREATE PROCEDURE [dbo].[ActualizarTipoRework]
	@id_tipo_rework INT,
	@descripcion VARCHAR(100)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM dbo.Tipo_Rework WHERE	(id_tipo_rework = @id_tipo_rework))
	BEGIN
		UPDATE Tipo_Rework
		   SET descripcion = @descripcion
		 WHERE id_tipo_rework = @id_tipo_rework
	END
END
GO
