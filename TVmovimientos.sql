DECLARE @XmlData XML

-- Cargar el contenido del archivo XML en la variable @XmlData
SELECT @XmlData = BulkColumn
FROM OPENROWSET(BULK 'C:\Users\fabri\OneDrive\Escritorio\BaseDatos1\datos.xml', SINGLE_BLOB) AS x

-- Crear una tabla de variable para almacenar los movimientos
DECLARE @Movimientos TABLE (
    ValorDocId INT,
    IdTipoMovimiento VARCHAR(50),
    Fecha DATE,
    Monto DECIMAL(10, 2),
    PostByUser VARCHAR(50),
    PostInIP VARCHAR(50),
    PostTime DATETIME
)

-- Insertar los datos de la sección de movimientos en la tabla de variable
INSERT INTO @Movimientos (ValorDocId, IdTipoMovimiento, Fecha, Monto, PostByUser, PostInIP, PostTime)
SELECT 
    Movimiento.value('@ValorDocId', 'INT') AS ValorDocId,
    Movimiento.value('@IdTipoMovimiento', 'VARCHAR(50)') AS IdTipoMovimiento,
    Movimiento.value('@Fecha', 'DATE') AS Fecha,
    Movimiento.value('@Monto', 'DECIMAL(10, 2)') AS Monto,
    Movimiento.value('@PostByUser', 'VARCHAR(50)') AS PostByUser,
    Movimiento.value('@PostInIP', 'VARCHAR(50)') AS PostInIP,
    Movimiento.value('@PostTime', 'DATETIME') AS PostTime
FROM @XmlData.nodes('/Datos/Movimientos/movimiento') AS Movimientos(Movimiento)

-- Verificar los datos insertados en la tabla de variable
SELECT * FROM @Movimientos
