CREATE PROCEDURE EliminarTipoRework
    @id_tipo_rework INT
AS
BEGIN
    IF EXISTS(SELECT 1 FROM dbo.Tipo_Rework WHERE id_tipo_rework = @id_tipo_rework)
    BEGIN
        DELETE FROM Tipo_Rework
        WHERE id_tipo_rework = @id_tipo_rework
    END
END
GO
