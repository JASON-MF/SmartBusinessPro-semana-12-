-- =====================================================
--  Script de base de datos: SmartBusinessPro
--  Ejecutar en SQL Server Management Studio (SSMS)
-- =====================================================
IF DB_ID('SmartBusinessPro') IS NULL
    CREATE DATABASE SmartBusinessPro;
GO

USE SmartBusinessPro;
GO

IF OBJECT_ID('dbo.Productos') IS NOT NULL DROP TABLE dbo.Productos;
IF OBJECT_ID('dbo.Categorias') IS NOT NULL DROP TABLE dbo.Categorias;
GO

CREATE TABLE dbo.Categorias (
    CategoriaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.Productos (
    ProductoId  INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(150) NOT NULL,
    Precio      DECIMAL(18,2) NOT NULL,
    Existencia  INT NOT NULL,
    CategoriaId INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias
        FOREIGN KEY (CategoriaId) REFERENCES dbo.Categorias(CategoriaId)
);
GO

INSERT INTO dbo.Categorias (Nombre) VALUES
(N'Electrónica'), (N'Papelería'), (N'Hogar'), (N'Alimentos'), (N'Herramientas');
GO

INSERT INTO dbo.Productos (Nombre, Precio, Existencia, CategoriaId) VALUES
(N'Laptop 15"',            649.99,  12, 1),
(N'Mouse inalámbrico',      12.50,  80, 1),
(N'Teclado mecánico',       45.00,  35, 1),
(N'Cuaderno universitario',  2.25, 200, 2),
(N'Caja de bolígrafos',      4.80, 150, 2),
(N'Resma de papel carta',    5.90,  90, 2),
(N'Lámpara de escritorio',  18.75,  25, 3),
(N'Juego de sábanas',       29.99,  18, 3),
(N'Café molido 500 g',       6.40,  60, 4),
(N'Arroz 5 lb',              3.10, 120, 4),
(N'Taladro eléctrico',      75.00,  10, 5),
(N'Juego de destornilladores', 14.20, 40, 5);
GO

SELECT p.Nombre, p.Precio, p.Existencia, c.Nombre AS Categoria
FROM dbo.Productos p
INNER JOIN dbo.Categorias c ON c.CategoriaId = p.CategoriaId;
GO
