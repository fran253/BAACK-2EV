CREATE TABLE Rol (
    idRol INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(255) NOT NULL,
    apellidos VARCHAR(255) NOT NULL,
    gmail VARCHAR(255) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    contraseña VARCHAR(255) NOT NULL,
    idRol INT NOT NULL,
    FOREIGN KEY (idRol) REFERENCES Rol(idRol) ON DELETE CASCADE
);

CREATE TABLE Curso (
    idCurso INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(255) NOT NULL,
    imagen TEXT,
    descripcion TEXT,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP
);  

CREATE TABLE Usuario_Curso (
    idUsuario INT NOT NULL,
    idCurso INT NOT NULL,
    PRIMARY KEY (idUsuario, idCurso),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idCurso) REFERENCES Curso(idCurso) ON DELETE CASCADE
);

CREATE TABLE Asignatura (
    idAsignatura INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(255) NOT NULL,
    imagen TEXT,
    descripcion TEXT,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    idCurso INT NOT NULL,
    FOREIGN KEY (idCurso) REFERENCES Curso(idCurso) ON DELETE CASCADE
);

CREATE TABLE Temario (
    idTemario INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(255) NOT NULL,
    descripcion TEXT,
    idAsignatura INT NOT NULL,
    FOREIGN KEY (idAsignatura) REFERENCES Asignatura(idAsignatura) ON DELETE CASCADE
);

CREATE TABLE Test (
    idTest INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(255) NOT NULL,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    idTemario INT NOT NULL,
    FOREIGN KEY (idTemario) REFERENCES Temario(idTemario) ON DELETE CASCADE
);

CREATE TABLE Pregunta (
    idPregunta INT PRIMARY KEY AUTO_INCREMENT,
    enunciado TEXT NOT NULL,
    idTest INT NOT NULL,
    FOREIGN KEY (idTest) REFERENCES Test(idTest) ON DELETE CASCADE
);

CREATE TABLE Opcion (
    idOpcion INT PRIMARY KEY AUTO_INCREMENT,
    texto TEXT NOT NULL,
    esCorrecta BOOLEAN DEFAULT 0,
    idPregunta INT NOT NULL,
    FOREIGN KEY (idPregunta) REFERENCES Pregunta(idPregunta) ON DELETE CASCADE
);

CREATE TABLE Resultado (
    idResultado INT PRIMARY KEY AUTO_INCREMENT,
    puntuacion BOOLEAN NOT NULL,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    idUsuario INT NOT NULL,
    idPregunta INT NOT NULL,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idPregunta) REFERENCES Pregunta(idPregunta) ON DELETE CASCADE
);

CREATE TABLE Archivo (
    idArchivo INT PRIMARY KEY AUTO_INCREMENT,
    titulo VARCHAR(255) NOT NULL,
    url TEXT NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    idUsuario INT NOT NULL,
    idTemario INT NOT NULL,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idTemario) REFERENCES Temario(idTemario) ON DELETE CASCADE
);

CREATE TABLE Comentario (
    idComentario INT PRIMARY KEY AUTO_INCREMENT,
    contenido TEXT NOT NULL,
    fechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    idUsuario INT NOT NULL,
    idArchivo INT NOT NULL,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idArchivo) REFERENCES Archivo(idArchivo) ON DELETE CASCADE
);

CREATE TABLE ArchivoUsuario (  
    idArchivo INT NOT NULL,
    idUsuario INT NOT NULL,
    PRIMARY KEY (idArchivo, idUsuario),
    FOREIGN KEY (idArchivo) REFERENCES Archivo(idArchivo) ON DELETE CASCADE,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario) ON DELETE CASCADE
);