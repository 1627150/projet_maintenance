ALTER TABLE utilisateur
ADD type_user int
GO

update utilisateur
set type_user = 2
where LEN(id_utilisateur) = 5;
GO

update utilisateur
set type_user = 1
where LEN(id_utilisateur) = 7;
GO

ALTER TABLE utilisateur ALTER COLUMN type_user int NOT NULL
GO

ALTER TABLE concentration
ADD type_user int
GO


update concentration
set type_user = 1;
GO

update concentration
set type_user = 2
where type_concentration like '%service%' or type_concentration like '%département%';
GO

update concentration
set type_user = 4
where type_concentration like '%externe%';
GO

ALTER TABLE seance_entrainement ALTER COLUMN intensite int NULL;
GO

update concentration set type_concentration = 'Partenaire' where nom_concentration like 'partenaire%';
update concentration set type_concentration = 'Communaute' where nom_concentration like 'Communaut%';
update concentration set type_user = 5 where nom_concentration like 'Communaut%';


create table capsule_sante
(
	id nvarchar(36) not null PRIMARY KEY,
	titre nvarchar(100) not null,
	date_publication datetime not null,
	contenu nvarchar(3000) not null 
);
GO

ALTER TABLE utilisateur
ADD Email nvarchar(100);
GO

ALTER TABLE utilisateur
ADD Nom nvarchar(100);
GO

create table sous_concentration
(
	id_concentration nvarchar(36) not null PRIMARY KEY,
	nom_concentration nvarchar(100) not null,
	type_concentration nvarchar(50) not null,
	objectif_credits int not null,
	type_user int not null,
	id_parent nvarchar(36) not null
);
GO

ALTER TABLE parametre
add value nvarchar(1000);
GO

ALTER TABLE utilisateur
ADD email_telephone nvarchar(100);
GO

insert into Parametre (nom, valeur, value) 
values('FournisseurServiceCell', 0  ,'Rogers:@pcs.rogers.com|Fido:@fido.ca|Telus:@msg.telus.com|Bell:@txt.bell.ca|Kudo:@msg.koodomobile.com|MTS:@text.mtsmobility.com|Sasktel:@sms.sasktel.com|Solo:@txt.bell.ca|Virgin:@vmobile.ca');
GO

ALTER TABLE utilisateur
ADD last_login datetime
GO

update utilisateur
set last_login = GETDATE();
GO

insert into Parametre (nom, valeur, value) 
values('NotifiedAdminEmail', 0  ,'normand.faucher@cstjean.qc.ca');
GO

INSERT INTO Parametre VALUES ('UtilisationInviteAmi', 0, null, null);
GO