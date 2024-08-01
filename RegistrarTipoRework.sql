CREATE PROCEDURE RegistrarTipoRework
    @descripcion NVARCHAR(100)
AS
BEGIN
    INSERT INTO Tipo_Rework (descripcion)
    VALUES (@descripcion);
END;
GO

