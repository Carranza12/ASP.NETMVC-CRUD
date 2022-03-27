CREATE DATABASE DBCONTACTO;

USE DBCONTACTO;


CREATE TABLE CONTACTO(
IdContacto int identity,
Nombres varchar(100),
Apellidos varchar(100),
Telefono varchar(100),
Correo varchar(100) 
)

SELECT * FROM CONTACTO;

Insert into CONTACTO (Nombres,Apellidos,Telefono,Correo) values 
('Francisco','carranza','8728485','frank@mail.com'),
('eduardo','camarena','536474','edu@mail.com'),
('Ale','Marquez','8682854','mar@mail.com'),
('Eddy','Treviño','87284854','eddy@mail.com')


create procedure sp_Registrar(
@Nombres varchar(100),
@Apellidos varchar(100),
@Telefono varchar(100),
@Correo varchar(100)
)
as 
begin
	insert into CONTACTO(Nombres,Apellidos,Telefono,Correo) values (@Nombres,@Apellidos,@Telefono,@Correo)
end

create procedure sp_Editar(
@IdContacto int,
@Nombres varchar(100),
@Apellidos varchar (100),
@Telefono varchar (100),
@Correo varchar (100)
)
as 
begin
	update CONTACTO set Nombres=@Nombres, Apellidos=@Apellidos,Telefono=@Telefono,Correo=@Correo where IdContacto=@IdContacto
end


create procedure sp_Eliminar(
@IdContacto int
)
as 
begin
	delete from CONTACTO where IdContacto=@IdContacto
end

