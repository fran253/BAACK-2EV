CREATE DATABASE reto2_api;

SHOW DATABASES;

USE reto2_api;

CREATE TABLE PlatoPrincipal (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes TEXT NOT NULL
);

INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');

SELECT * FROM PlatoPrincipal;

SELECT * 
FROM PlatoPrincipal
WHERE Ingredientes LIKE '%Tofu%';

SELECT * 
FROM PlatoPrincipal
ORDER BY Precio ASC;

SELECT DISTINCT Nombre, Precio 
FROM PlatoPrincipal;

