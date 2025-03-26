-- Inserts para la tabla Rol
INSERT INTO Rol (nombre) VALUES 
('Administrador'),
('Profesor'),
('Estudiante');

-- Inserts para la tabla Usuario
INSERT INTO Usuario (nombre, apellidos, gmail, telefono, contraseña, idRol) VALUES 
('Juan', 'García López', 'juan.garcia@gmail.com', '612345678', 'password123', 1),
('María', 'Martínez Pérez', 'maria.martinez@gmail.com', '623456789', 'securepass1', 2),
('Carlos', 'Rodríguez Sánchez', 'carlos.rodriguez@gmail.com', '634567890', 'professor22', 2),
('Laura', 'Fernández Gómez', 'laura.fernandez@gmail.com', '645678901', 'laurapass3', 3),
('Pedro', 'López Martín', 'pedro.lopez@gmail.com', '656789012', 'pedropass4', 3),
('Ana', 'González Ruiz', 'ana.gonzalez@gmail.com', '667890123', 'anapass123', 3),
('Miguel', 'Sánchez Torres', 'miguel.sanchez@gmail.com', '678901234', 'miguelpass', 3),
('Sara', 'Díaz Hernández', 'sara.diaz@gmail.com', '689012345', 'sarapass22', 3),
('Javier', 'Álvarez Moreno', 'javier.alvarez@gmail.com', '690123456', 'javipass33', 2),
('Elena', 'Romero Jiménez', 'elena.romero@gmail.com', '601234567', 'elenapass4', 2);

-- Inserts para la tabla Asignatura (basado en los cursos existentes)
INSERT INTO Asignatura (nombre, imagen, descripcion, fechaCreacion, idCurso) VALUES 
-- 1º ESO (idCurso: 1)
('Matemáticas 1º ESO', 'https://i.pinimg.com/736x/1a/2b/3c/1a2b3c4d5e6f7g8h9i0j.jpg', 'Fundamentos matemáticos para estudiantes de 1º de ESO', '2024-02-16 09:00:00', 1),
('Lengua Castellana 1º ESO', 'https://i.pinimg.com/736x/2b/3c/4d/2b3c4d5e6f7g8h9i0j1k.jpg', 'Gramática y literatura española para 1º de ESO', '2024-02-16 09:15:00', 1),
('Inglés 1º ESO', 'https://i.pinimg.com/736x/3c/4d/5e/3c4d5e6f7g8h9i0j1k2l.jpg', 'Idioma extranjero para principiantes', '2024-02-16 09:30:00', 1),

-- 2º ESO (idCurso: 2)
('Matemáticas 2º ESO', 'https://i.pinimg.com/736x/4d/5e/6f/4d5e6f7g8h9i0j1k2l3m.jpg', 'Matemáticas para estudiantes de 2º de ESO', '2024-02-16 10:00:00', 2),
('Lengua Castellana 2º ESO', 'https://i.pinimg.com/736x/5e/6f/7g/5e6f7g8h9i0j1k2l3m4n.jpg', 'Gramática y literatura española para 2º de ESO', '2024-02-16 10:15:00', 2),
('Inglés 2º ESO', 'https://i.pinimg.com/736x/6f/7g/8h/6f7g8h9i0j1k2l3m4n5o.jpg', 'Idioma extranjero nivel intermedio', '2024-02-16 10:30:00', 2),

-- 3º ESO (idCurso: 3)
('Física y Química 3º ESO', 'https://i.pinimg.com/736x/7g/8h/9i/7g8h9i0j1k2l3m4n5o6p.jpg', 'Introducción a la física y química', '2024-02-16 11:00:00', 3),
('Biología 3º ESO', 'https://i.pinimg.com/736x/8h/9i/0j/8h9i0j1k2l3m4n5o6p7q.jpg', 'Fundamentos de biología para 3º de ESO', '2024-02-16 11:15:00', 3),
('Geografía 3º ESO', 'https://i.pinimg.com/736x/9i/0j/1k/9i0j1k2l3m4n5o6p7q8r.jpg', 'Estudio del mundo y sus características', '2024-02-16 11:30:00', 3),

-- 1º Bachillerato Ciencias (idCurso: 7)
('Matemáticas I', 'https://i.pinimg.com/736x/0j/1k/2l/0j1k2l3m4n5o6p7q8r9s.jpg', 'Matemáticas avanzadas para bachillerato científico', '2024-02-16 14:00:00', 7),
('Física y Química', 'https://i.pinimg.com/736x/1k/2l/3m/1k2l3m4n5o6p7q8r9s0t.jpg', 'Física y química para bachillerato', '2024-02-16 14:15:00', 7),
('Biología', 'https://i.pinimg.com/736x/2l/3m/4n/2l3m4n5o6p7q8r9s0t1u.jpg', 'Estudio avanzado de los seres vivos', '2024-02-16 14:30:00', 7),

-- Grado Superior Informática (idCurso: 11)
('Programación', 'https://i.pinimg.com/736x/3m/4n/5o/3m4n5o6p7q8r9s0t1u2v.jpg', 'Fundamentos de programación', '2024-02-16 16:00:00', 11),
('Bases de Datos', 'https://i.pinimg.com/736x/4n/5o/6p/4n5o6p7q8r9s0t1u2v3w.jpg', 'Diseño y gestión de bases de datos', '2024-02-16 16:15:00', 11),
('Desarrollo Web', 'https://i.pinimg.com/736x/5o/6p/7q/5o6p7q8r9s0t1u2v3w4x.jpg', 'Creación de aplicaciones web', '2024-02-16 16:30:00', 11);

-- Inserts para la tabla Temario
INSERT INTO Temario (titulo, descripcion, idAsignatura) VALUES 
-- Para Matemáticas 1º ESO
('Números enteros', 'Operaciones con números enteros y propiedades', 1),
('Fracciones', 'Concepto de fracción y operaciones con fracciones', 1),
('Geometría básica', 'Introducción a las figuras geométricas', 1),
('Decimales', 'Operaciones con números decimales', 1),
('Álgebra elemental', 'Introducción a las expresiones algebraicas', 1),
('Proporcionalidad', 'Razones y proporciones, regla de tres', 1),

-- Para Lengua Castellana 1º ESO
('Gramática elemental', 'Estructura de la oración y categorías gramaticales', 2),
('Literatura medieval', 'Introducción a la literatura de la Edad Media', 2),
('Ortografía básica', 'Reglas de acentuación y puntuación', 2),
('Tipología textual', 'Textos narrativos, descriptivos, dialogados, etc.', 2),
('Comunicación oral', 'Técnicas de expresión y comprensión oral', 2),

-- Para Inglés 1º ESO
('Present Simple', 'Usos y formación del presente simple', 3),
('Present Continuous', 'Usos y formación del presente continuo', 3),
('Vocabulary: Daily Routines', 'Vocabulario sobre rutinas diarias', 3),
('Basic Questions', 'Formación de preguntas simples en inglés', 3),
('Reading Comprehension', 'Técnicas de comprensión lectora en inglés', 3),

-- Para Matemáticas 2º ESO
('Números racionales', 'Operaciones con fracciones y decimales', 4),
('Potencias y raíces', 'Propiedades y operaciones con potencias', 4),
('Ecuaciones de primer grado', 'Resolución de ecuaciones lineales', 4),
('Sistemas de ecuaciones', 'Métodos de resolución de sistemas lineales', 4),
('Proporcionalidad y porcentajes', 'Aplicaciones de la proporcionalidad', 4),

-- Para Física y Química 3º ESO
('La materia y sus propiedades', 'Características y clasificación de la materia', 7),
('Estados de agregación', 'Sólido, líquido y gaseoso, cambios de estado', 7),
('Átomo y tabla periódica', 'Estructura atómica y sistema periódico', 7),
('Formulación inorgánica básica', 'Nomenclatura de compuestos inorgánicos', 7),
('Leyes de los gases', 'Ley de Boyle-Mariotte, Gay-Lussac y Charles', 7),

-- Para Biología 3º ESO
('La célula', 'Estructura celular y tipos de células', 8),
('Nutrición humana', 'Aparatos digestivo, respiratorio, circulatorio y excretor', 8),
('Sistema nervioso', 'Estructura y funcionamiento del sistema nervioso', 8),
('Sistema endocrino', 'Hormonas y glándulas endocrinas', 8),
('Reproducción humana', 'Aparato reproductor y fecundación', 8),

-- Para Matemáticas I (1º Bachillerato Ciencias)
('Números reales', 'Conjuntos numéricos y propiedades', 10),
('Álgebra', 'Polinomios, fracciones algebraicas y ecuaciones', 10),
('Trigonometría', 'Razones trigonométricas y resolución de triángulos', 10),
('Geometría analítica', 'Vectores y ecuaciones de la recta', 10),
('Análisis', 'Funciones, límites y continuidad', 10),

-- Para Física y Química (1º Bachillerato)
('Cinemática', 'Estudio del movimiento', 11),
('Dinámica', 'Fuerzas y leyes de Newton', 11),
('Energía y trabajo', 'Conceptos y principios de conservación', 11),
('Química del carbono', 'Compuestos orgánicos', 11),
('Termoquímica', 'Intercambios de energía en reacciones químicas', 11),

-- Para Biología (1º Bachillerato)
('Bioelementos y biomoléculas', 'Componentes químicos de los seres vivos', 12),
('La célula eucariota', 'Estructura y orgánulos celulares', 12),
('Metabolismo celular', 'Anabolismo y catabolismo', 12),
('Genética molecular', 'ADN, replicación y transcripción', 12),
('Microbiología', 'Virus, bacterias y sus aplicaciones', 12),

-- Para Programación (Grado Superior)
('Introducción a la programación', 'Fundamentos y conceptos básicos', 13),
('Estructuras de control', 'Condicionales y bucles en programación', 13),
('Programación orientada a objetos', 'Conceptos de clases y objetos', 13),
('Estructuras de datos', 'Arrays, listas, pilas y colas', 13),
('Programación funcional', 'Conceptos y aplicaciones', 13),
('Interfaces gráficas', 'Diseño de UI/UX en aplicaciones', 13),
('Patrones de diseño', 'Soluciones a problemas comunes', 13),

-- Para Bases de Datos (Grado Superior)
('Modelo entidad-relación', 'Diseño conceptual de bases de datos', 14),
('SQL básico', 'Consultas, inserciones y actualizaciones en SQL', 14),
('Normalización', 'Formas normales y optimización de bases de datos', 14),
('Lenguaje de definición de datos', 'Creación y modificación de esquemas', 14),
('Lenguaje de manipulación de datos', 'Consultas avanzadas y subconsultas', 14),
('Bases de datos NoSQL', 'Alternativas a las bases de datos relacionales', 14),
('Optimización y rendimiento', 'Índices y técnicas de mejora', 14),

-- Para Desarrollo Web (Grado Superior)
('HTML5 y CSS3', 'Fundamentos de marcado y estilos', 15),
('JavaScript', 'Programación en el lado del cliente', 15),
('Frameworks frontend', 'React, Angular y Vue', 15),
('Desarrollo backend', 'Node.js, PHP, y otros lenguajes', 15),
('Bases de datos web', 'Integración con MySQL y MongoDB', 15),
('Diseño responsivo', 'Adaptación a diferentes dispositivos', 15),
('Seguridad web', 'Protección contra vulnerabilidades comunes', 15);

-- Inserts para la tabla ArchivoUsuario (relaciona usuarios con archivos a los que tienen acceso)
INSERT INTO ArchivoUsuario (idArchivo, idUsuario) VALUES 
(1, 4), -- Laura tiene acceso a los apuntes de números enteros
(1, 5), -- Pedro tiene acceso a los apuntes de números enteros
(2, 4), -- Laura tiene acceso a los ejercicios de fracciones
(3, 6), -- Ana tiene acceso a la presentación de gramática
(4, 8), -- Sara tiene acceso al video tutorial de programación
(5, 8), -- Sara tiene acceso a los ejemplos de consultas SQL
(6, 4); -- Laura tiene acceso a la resolución de ejercicios de enteros

-- Inserts para la tabla Resultado (registros de respuestas a tests)
INSERT INTO Resultado (puntuacion, fecha, idUsuario, idPregunta) VALUES 
(1, '2024-02-20 09:30:00', 4, 1), -- Laura acertó la pregunta 1
(0, '2024-02-20 09:32:00', 4, 2), -- Laura falló la pregunta 2
(1, '2024-02-20 09:34:00', 4, 3), -- Laura acertó la pregunta 3
(1, '2024-02-20 10:00:00', 5, 1), -- Pedro acertó la pregunta 1
(1, '2024-02-20 10:02:00', 5, 2), -- Pedro acertó la pregunta 2
(1, '2024-02-21 11:00:00', 8, 8), -- Sara acertó la pregunta 8
(0, '2024-02-21 11:02:00', 8, 9); -- Sara falló la pregunta 9