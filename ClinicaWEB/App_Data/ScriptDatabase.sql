
CREATE TABLE Usuarios(
IdUsuario int IDENTITY(1,1),
Usuario varchar(30),
CONtrASeña varchar(20),
Estado varchar(15),
PRIMARY KEY(IdUsuario)               
)

CREATE TABLE Roles(
IdRol int IDENTITY(1,1),
Nombre varchar(20),
Estado varchar(15),
PRIMARY KEY(IdRol),
)

CREATE TABLE UsuarioRol(
IdUsuario int,              
IdRol int,
PRIMARY KEY(idUsuario,IdRol),
FOREIGN KEY(IdRol) REFERENCES Roles(IdRol),
FOREIGN KEY(IdUsuario) REFERENCES Usuarios(IdUsuario)
)

CREATE TABLE FunciONalidades(
IdFunciONalidad int IDENTITY(1,1),
DescripciON varchar(20),
PRIMARY KEY(IdFunciONalidad),
)

CREATE TABLE RolFunciON(
IdRol int,
IdFunciONalidad int
PRIMARY KEY(IdFunciONalidad,IdRol)
FOREIGN KEY(IdRol) REFERENCES Roles(IdRol),
FOREIGN KEY(IdFunciONalidad) REFERENCES FunciONalidades(IdFunciONalidad)
)

CREATE TABLE Planes(
IdPlan int IDENTITY(1,1),
DescripciON varchar(255),
PrecBCONsulta numeric(18, 0) ,
PrecBFarmacia numeric(18, 0),
Codigo numeric(18,0),
PRIMARY KEY(IdPlan)
)

CREATE TABLE Afiliados(
IdAfiliado int IDENTITY(1,1),
Nombre varchar(255), 
Apellido varchar(255),
TipoDoc varchar(3), 
NumDoc numeric(18, 0) , 
DirecciON varchar(255),
TelefONo numeric(18, 0), 
Mail varchar(255), 
FechaNac dateTime, 
Sexo char, 
EstadoCivil varchar(10), 
CantFliaCargo int, 
IdPlan int,
NumAfiliado int, 
IdUsuario int,
NumCONsultAS int,
Estado varchar(15),
PRIMARY KEY(IdAfiliado),
FOREIGN KEY(IdPlan) REFERENCES Planes(IdPlan),
FOREIGN KEY(IdUsuario) REFERENCES Usuarios(IdUsuario),
)

CREATE TABLE CambioDePlan(
IdCambioDePlan int IDENTITY(1,1),
IdAfiliado int,
IdPlanAnterior int,
IdPlanActual int,
Fecha dateTime,
Motivo varchar(50),
PRIMARY KEY(IdCambioDePlan),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
FOREIGN KEY(IdPlanAnterior) REFERENCES Planes(IdPlan),
FOREIGN KEY(IdPlanActual) REFERENCES Planes(IdPlan),
)

CREATE TABLE AfiliadosBaja(
IdAfiliadosBaja int IDENTITY(1,1), 
IdAfiliado int, 
Fecha dateTime,
PRIMARY KEY(IdAfiliadosBaja),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado)
)


CREATE TABLE ProfesiONales(
IdProfesiONal int IDENTITY(1,1),
Nombre varchar(255), 
Apellido varchar(255),
TipoDoc varchar(3), 
NumDoc numeric(18, 0) , 
DirecciON varchar(255),
TelefONo numeric(18, 0), 
Mail varchar(255), 
FechaNac dateTime, 
Sexo char, 
Matricula varchar(9),
IdUsuario int,
Motivo varchar(15),
PRIMARY KEY(IdProfesiONal),
FOREIGN KEY(IdUsuario) REFERENCES Usuarios(IdUsuario)
)

CREATE TABLE TipoEspecialidad(
IdTipo int IDENTITY(1,1), 
DescripciON varchar(255), 
Codigo numeric(18,0),
PRIMARY KEY(IdTipo)
)

CREATE TABLE Especialidades(
IdEspec int IDENTITY(1,1), 
DescripciON varchar(255), 
IdTipo int ,
Codigo numeric(18,0),
Estado varchar(15)
PRIMARY KEY(IdEspec),
FOREIGN KEY(IdTipo) REFERENCES TipoEspecialidad(IdTipo)
)

CREATE TABLE ProfEsp(
IdProfesiONal int ,
IdEspec int ,
Estado varchar(15),
PRIMARY KEY(IdProfesiONal, IdEspec),
FOREIGN KEY(IdProfesiONal) REFERENCES ProfesiONales(IdProfesiONal),
FOREIGN KEY(IdEspec) REFERENCES Especialidades(IdEspec),
)


CREATE TABLE Agenda(
IdAgenda int IDENTITY(1,1),
Dia date ,
HoraI time ,  
HoraF time ,  
IdProfesiONal int , 
IdEspec int , 
PRIMARY KEY(IdAgenda),
FOREIGN KEY(IdProfesiONal) REFERENCES ProfesiONales(IdProfesiONal),
FOREIGN KEY(IdEspec) REFERENCES Especialidades(IdEspec),
)

CREATE TABLE Turnos(
IdTurno int IDENTITY(1,1),
IdAfiliado int ,
IdProfesiONal int , 
IdEspec int , 
Fecha datetime , 
NumTurno numeric(18, 0),
Estado varchar(15),
PRIMARY KEY (IdTurno),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
FOREIGN KEY(IdProfesiONal) REFERENCES ProfesiONales(IdProfesiONal),
FOREIGN KEY(IdEspec) REFERENCES Especialidades(IdEspec),
)

CREATE TABLE BONoCONsulta(
IdBONoCON int IDENTITY(1,1),    
IdAfiliado int,              
IdPlan int,                  
Precio numeric(18, 0),               
FechaCompra dateTime,
NumCONsul int,
FechaImpresiON dateTime,
FechaUso dateTime,
NumBONoCON numeric(18, 0),
PRIMARY KEY(IdBONoCON),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
FOREIGN KEY(IdPlan) REFERENCES Planes(IdPlan),
)

CREATE TABLE CONsultAS(
IdCONsul int IDENTITY(1,1),
IdAfiliado int,
IdProfesiONal int,
IdBONoCON int,
Sintoma varchar(255),
Enfermedad varchar(255),
Fecha date,
Hora time,
PRIMARY KEY(IdCONsul),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
FOREIGN KEY(IdProfesiONal) REFERENCES ProfesiONales(IdProfesiONal),
FOREIGN KEY(IdBONoCON) REFERENCES BONoCONsulta(IdBONoCON)
)
  
CREATE TABLE BONoFarmacia(
IdBONoFarm int IDENTITY(1,1),    
IdAfiliado int,              
IdPlan int,                  
Precio numeric(18, 0),               
FechaCompra dateTime, 
fechaVnto dateTime,
Medicamentos varchar(255),
fechaImpresiON dateTime,
NumBONoFarm numeric(18, 0),
PRIMARY KEY(IdBONoFarm),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
FOREIGN KEY(IdPlan) REFERENCES Planes(IdPlan),
)
                           
CREATE TABLE RecetAS(
IdReceta int IDENTITY(1,1),
CantBNec int ,
CantMedic int,
IdCONsul int,
PRIMARY KEY(IdReceta),
FOREIGN KEY(IdCONsul) REFERENCES CONsultAS(IdCONsul)
)


CREATE TABLE BONoFarmReceta(
IdBONoFarm int,
IdReceta int,
PRIMARY KEY(IdBONoFarm, IdReceta),
FOREIGN KEY(IdBONoFarm) REFERENCES BONoFarmacia(IdBONoFarm),
FOREIGN KEY(IdReceta) REFERENCES RecetAS(IdReceta),
)

CREATE TABLE CompraBONos(
IdCompra int IDENTITY(1,1),
FechaCompra dateTime , 
IdAfiliado int ,
CantBCONsulta int ,
CantBFarmacia int ,
PrecioTot float ,
PRIMARY KEY(IdCompra),
FOREIGN KEY(IdAfiliado) REFERENCES Afiliados(IdAfiliado),
)

CREATE TABLE CompraBONosCONDetalle(
IdCompra int,
IdBONoCON int ,
PRIMARY KEY(IdCompra,IdBONoCON),
FOREIGN KEY(IdCompra) REFERENCES CompraBONos(IdCompra),
FOREIGN KEY(IdBONoCON) REFERENCES BONoCONsulta(IdBONoCON),
)

CREATE TABLE CompraBONosFarmDetalle(
IdCompra int,
IdBONoFarm int ,
PRIMARY KEY(IdCompra,IdBONoFarm),
FOREIGN KEY(IdCompra) REFERENCES CompraBONos(IdCompra),
FOREIGN KEY(IdBONoFarm) REFERENCES BONoFarmacia(IdBONoFarm),
)

CREATE TABLE TurnosCancelados(
IdCancelaciON int IDENTITY(1,1),
IdTurno  int,
Motivo varchar(15),
DescripciON varchar(30),
Fecha dateTime,
PRIMARY KEY(IdCancelaciON),
FOREIGN KEY(IdTurno) REFERENCES Turnos(IdTurno),
)

CREATE TABLE Administrativo(
IdAdministrativo int IDENTITY(1,1),
IdUsuario int,
PRIMARY KEY(IdAdministrativo),
FOREIGN KEY(IdUsuario) REFERENCES Usuarios(IdUsuario)
)

--migraciON

INSERT INTO TipoEspecialidad 
SELECT DISTINCT Tipo_Especialidad_DescripciON,Tipo_Especialidad_Codigo  
FROM Maestra

INSERT INTO Roles
VALUES('Afiliado','Habilitado')
INSERT INTO Roles
VALUES('ProfesiONal','Habilitado')
INSERT INTO Roles
VALUES('Administrativo','Habilitado')

--INSERT INTO Usuarios
--VALUES('admin',)

--ALTER TABLE Usuarios MODIFY CONtrASeña varchar(255)

INSERT INTO Afiliados
(Nombre,Apellido,NumDoc,DirecciON,TelefONo,Mail,FechaNac,TipoDoc ,CantFliaCargo,NumCONsultAS,Estado   )
SELECT DISTINCT Paciente_Nombre,Paciente_Apellido,Paciente_Dni,Paciente_DirecciON,Paciente_TelefONo, Paciente_Mail ,Paciente_Fecha_Nac,'DNI',0,0,'Habilitado'
FROM Maestra 
ORDER BY Paciente_Nombre,Paciente_Apellido  

INSERT INTO Planes 
SELECT DISTINCT Plan_Med_DescripciON,Plan_Med_Precio_BONo_CONsulta,Plan_Med_Precio_BONo_Farmacia,Plan_Med_Codigo 
FROM Maestra
ORDER BY Plan_Med_DescripciON 

INSERT INTO TipoEspecialidad
SELECT DISTINCT Tipo_Especialidad_DescripciON,Tipo_Especialidad_Codigo 
FROM Maestra
ORDER BY Tipo_Especialidad_Codigo 

INSERT INTO Especialidades
SELECT DISTINCT Especialidad_DescripciON,2,Especialidad_Codigo,'Habilitado' 
FROM Maestra 
WHERE Tipo_Especialidad_Codigo = 1002
ORDER BY Especialidad_DescripciON  

INSERT INTO Especialidades
SELECT DISTINCT Especialidad_DescripciON,3,Especialidad_Codigo,'Habilitado' 
FROM Maestra 
WHERE Tipo_Especialidad_Codigo = 1000
ORDER BY Especialidad_DescripciON 
 
INSERT INTO Especialidades
SELECT DISTINCT Especialidad_DescripciON,4,Especialidad_Codigo,'Habilitado' 
FROM Maestra 
WHERE Tipo_Especialidad_Codigo = 1001
ORDER BY Especialidad_DescripciON  

INSERT INTO Especialidades
SELECT DISTINCT Especialidad_DescripciON,5,Especialidad_Codigo,'Habilitado' 
FROM Maestra 
WHERE Tipo_Especialidad_Codigo = 1003
ORDER BY Especialidad_DescripciON  

INSERT INTO ProfesiONales(Nombre,Apellido,TipoDoc,NumDoc,DirecciON,TelefONo,Mail,FechaNac)
SELECT DISTINCT Medico_Nombre,Medico_Apellido,'DNI',Medico_Dni,Medico_DirecciON,Medico_TelefONo,Medico_Mail,Medico_Fecha_Nac 
FROM Maestra 
ORDER BY Medico_Nombre,Medico_Apellido 

INSERT INTO ProfEsp 
SELECT DISTINCT p.IdProfesiONal,e.IdEspec,'Habilitado'
FROM ProfesiONales p,Especialidades e,Maestra m
WHERE m.Especialidad_Codigo = e.Codigo AND m.Medico_Dni=p.NumDoc 
ORDER BY 1

INSERT INTO Turnos 
SELECT DISTINCT a.IdAfiliado ,p.IdProfesiONal ,e.IdEspec ,m.Turno_Fecha,m.Turno_Numero,'Habilitado'
FROM Maestra m,Especialidades e,ProfesiONales p,Afiliados a
WHERE m.Paciente_Dni = a.NumDoc AND m.Medico_Dni = p.NumDoc AND m.Especialidad_Codigo = e.Codigo
ORDER BY Turno_Numero  

INSERT INTO BONoCONsulta(IdAfiliado,IdPlan,Precio,FechaCompra,NumCONsul,FechaImpresiON,NumBONoCON)
SELECT DISTINCT a.IdAfiliado ,p.IdPlan ,p.PrecBCONsulta ,m.Compra_BONo_Fecha,0,m.BONo_CONsulta_Fecha_ImpresiON,m.BONo_CONsulta_Numero 
FROM Maestra m,Afiliados a,Planes p
WHERE m.Paciente_Dni = a.NumDoc AND m.Plan_Med_Codigo = p.Codigo 
ORDER BY m.BONo_CONsulta_Numero 

DELETE BONoCONsulta
WHERE FechaCompra IS NULL AND FechaImpresiON  IS NULL

INSERT INTO BONoFarmacia 
SELECT DISTINCT a.IdAfiliado ,p.IdPlan ,p.PrecBFarmacia ,m.Compra_BONo_Fecha,m.BONo_Farmacia_Fecha_Vencimiento,m.BONo_Farmacia_Medicamento,m.BONo_Farmacia_Fecha_ImpresiON ,m.BONo_Farmacia_Numero  
FROM Maestra m,Afiliados a,Planes p
WHERE m.Paciente_Dni = a.NumDoc AND m.Plan_Med_Codigo = p.Codigo

DELETE BONoFarmacia 
WHERE FechaCompra IS NULL AND FechaImpresiON  IS NULL

INSERT INTO CONsultAS(IdAfiliado,IdProfesiONal,IdBONoCON,Sintoma,Enfermedad)
SELECT a.IdAfiliado,p.IdProfesiONal,b.IdBONoCON,m.CONsulta_SintomAS,m.CONsulta_Enfermedades
FROM Afiliados a,ProfesiONales p,BONoCONsulta b,Maestra m
WHERE m.Paciente_Dni = a.NumDoc AND m.Medico_Dni = p.NumDoc AND m.BONo_CONsulta_Numero = b.NumBONoCON  

INSERT INTO FunciONalidades
VALUES('ABM Usuario')
INSERT INTO FunciONalidades
VALUES('ABM Afiliado')
INSERT INTO FunciONalidades
VALUES('ABM ProfesiONal')
INSERT INTO FunciONalidades
VALUES('ABM Especialidad')
INSERT INTO FunciONalidades
VALUES('ABM Plan')
INSERT INTO FunciONalidades
VALUES('ABM Rol')
INSERT INTO FunciONalidades
VALUES('Compra de BONos')
INSERT INTO FunciONalidades
VALUES('Pedir Turno')
INSERT INTO FunciONalidades
VALUES('Login')

INSERT INTO FunciONalidades
VALUES('SelecciON de Rol')
INSERT INTO FunciONalidades
VALUES('Home del Usuario')
INSERT INTO FunciONalidades
VALUES('Registrar Llegada')
INSERT INTO FunciONalidades
VALUES('Registrar Agenda')
INSERT INTO FunciONalidades
VALUES('Listado Estadistica')
INSERT INTO FunciONalidades
VALUES('Cargar receta')
INSERT INTO FunciONalidades
VALUES('Registrar Diagnost')
INSERT INTO FunciONalidades
VALUES('CancelaciON Turnos')

INSERT INTO  RolFunciON
VALUES(3,1)
INSERT INTO  RolFunciON
VALUES(3,2)
INSERT INTO  RolFunciON
VALUES(3,3)
INSERT INTO  RolFunciON
VALUES(3,4)
INSERT INTO  RolFunciON
VALUES(3,5)
INSERT INTO  RolFunciON
VALUES(3,6)
INSERT INTO  RolFunciON
VALUES(3,7)
INSERT INTO  RolFunciON
VALUES(3,8)
INSERT INTO  RolFunciON
VALUES(3,9)
INSERT INTO  RolFunciON
VALUES(3,10)
INSERT INTO  RolFunciON
VALUES(3,11)
INSERT INTO  RolFunciON
VALUES(3,12)
INSERT INTO  RolFunciON
VALUES(3,13)
INSERT INTO  RolFunciON
VALUES(3,14)
INSERT INTO  RolFunciON
VALUES(3,15)
INSERT INTO  RolFunciON
VALUES(3,17)
INSERT INTO  RolFunciON
VALUES(3,16)
INSERT INTO  RolFunciON
VALUES(1,7)
INSERT INTO  RolFunciON
VALUES(1,8)
INSERT INTO  RolFunciON
VALUES(1,9)
INSERT INTO  RolFunciON
VALUES(1,10)
INSERT INTO  RolFunciON
VALUES(1,11)
INSERT INTO  RolFunciON
VALUES(1,17)
INSERT INTO  RolFunciON
VALUES(2,9)
INSERT INTO  RolFunciON
VALUES(2,10)
INSERT INTO  RolFunciON
VALUES(2,11)
INSERT INTO  RolFunciON
VALUES(2,15)
INSERT INTO  RolFunciON
VALUES(2,17)
INSERT INTO  RolFunciON
VALUES(2,16)

--TRIGGERS
GO

CREATE TRIGGER AfiliadosBajAS
ON Afiliados
FOR UPDATE
AS

DECLARE @now datetime

BEGIN TRY

SET NOCOUNT ON

SET @now = GETDATE()

DECLARE @OLDValuE AS VARCHAr(15)
 
DECLARE @NEWValue AS VARCHAr(15)

SELECT @OLDValuE = ESTADO FROM DELETED
SELECT @NEWValue = estado FROM INSERTED
IF
@OLDValuE = 'Habilitado' AND @NEWValue ='Deshabilitado'
BEGIN
INSERT INTO AfiliadosBaja
(IdAfiliado,Fecha)
SELECT DELETED.IdAfiliado,@now 
FROM DELETED

END

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
END CATCH

GO

CREATE TRIGGER CambioDePlanA
ON Afiliados
FOR UPDATE
AS

DECLARE @now datetime

BEGIN TRY

SET NOCOUNT ON

SET @now = GETDATE()

DECLARE @OLDValuE AS int
 
DECLARE @NEWValue AS int

SELECT @OLDValuE = IdPlan FROM DELETED
SELECT @NEWValue = IdPlan FROM INSERTED
IF
@OLDValuE !=  @NEWValue
BEGIN
INSERT INTO CambioDePlan
(IdAfiliado,IdPlanAnterior,IdPlanActual,Fecha)
SELECT DELETED.IdAfiliado, @OLDValuE,@NEWValue,@now
FROM DELETED

END

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
END CATCH

GO

CREATE TRIGGER TurnoCancelado
ON Turnos
FOR UPDATE
AS

DECLARE @now datetime

BEGIN TRY

SET NOCOUNT ON

SET @now = GETDATE()

DECLARE @OLDValuE AS varchar(15)
 
DECLARE @NEWValue AS varchar(15)

SELECT @OLDValuE = Estado FROM DELETED
SELECT @NEWValue = Estado FROM INSERTED
IF
@OLDValuE !=  @NEWValue AND (@NEWValue = 'CanceladoXProf' or  @NEWValue = 'CanceladoxAfi')
BEGIN
INSERT INTO TurnosCancelados
(IdTurno,Fecha)
SELECT DELETED.IdTurno,@now
FROM DELETED

END

END TRY

BEGIN CATCH
ROLLBACK TRANSACTION
END CATCH
