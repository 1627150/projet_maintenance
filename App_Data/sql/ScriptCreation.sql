IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='activite') DROP TABLE activite
CREATE TABLE Activite (
	Id_Activite NVARCHAR(36) NOT NULL,
	Nom_Activite NVARCHAR(50) NOT NULL,
	Groupe_Activite NVARCHAR(50),
	CONSTRAINT PK_activite PRIMARY KEY (Id_Activite)
	);

IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Concentration') DROP TABLE Concentration
CREATE TABLE Concentration (
	Id_Concentration NVARCHAR(36) NOT NULL,
	Nom_Concentration NVARCHAR(100),
	Type_Concentration NVARCHAR(50) NOT NULL,
	Objectif_Credits INT NOT NULL DEFAULT 0,
	CONSTRAINT PK_concentration PRIMARY KEY (Id_Concentration)
	);

IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Parametre') DROP TABLE Parametre
CREATE TABLE Parametre (
	nom NVARCHAR(50) NOT NULL,
	valeur INT,
	date datetime,
	CONSTRAINT PK_parametre PRIMARY KEY (nom)
	);

IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Seance_Entrainement') DROP TABLE Seance_Entrainememt
CREATE TABLE Seance_Entrainement (
	Id_Seance NVARCHAR(36) NOT NULL,
	Intensite INT,
	Temps_Activite INT,
	Nb_Credits NUMERIC(18,2) NOT NULL,
	Id_Utilisateur NVARCHAR(50) NOT NULL,
	Id_Activite NVARCHAR(36),
	Date DATETIME NOT NULL,
	Est_Valide BIT NOT NULL,
	CONSTRAINT PK_seance_entrainement PRIMARY KEY (Id_Seance)
	);

IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='Utilisateur') DROP TABLE Utilisateur
CREATE TABLE Utilisateur (
	Id_Utilisateur NVARCHAR(50) NOT NULL,
	Mot_De_Passe NVARCHAR(50) NOT NULL,
	Id_Concentration NVARCHAR(36) NOT NULL,
	Type_Role NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_utilisateur PRIMARY KEY (Id_Utilisateur)
	);


INSERT INTO Parametre
VALUES ('DateDebutBougeotte', 0, '2012-09-11 00:00:00');
INSERT INTO Parametre
VALUES ('DateFinBougeotte', 0, '2012-09-22 00:00:00');
INSERT INTO Parametre(nom, valeur)
VALUES ('MaxCreditsSeance', 15);
INSERT INTO Parametre(nom, valeur)
VALUES ('MaxCreditsSemaine', 100);
INSERT INTO Parametre(nom, valeur)
VALUES ('MaxSeancesJour', 10);
INSERT INTO Parametre(nom, valeur)
VALUES ('MaxTempsEntrainement', 600);
INSERT INTO Parametre(nom, valeur)
VALUES ('MaxTempsNotification', 100);
INSERT INTO Parametre(nom, valeur)
VALUES ('UtilisationActivite', 0);

INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Marche');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Marche');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Marche en montagne');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Course');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Balai-ballon sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ballon au poing');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Baseball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Basket-ball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Beach Volley');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Crosse');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Curling');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Football (ou soccer)');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Football am�ricain');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Handball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey en salle');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey subaquatique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey sur gazon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kinball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Korfball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Longue paume');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Moto-ball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Netball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ringuette');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Rugby ');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Softball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tchoukball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ultimate');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Volley-ball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Water polo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'BMX');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Cyclo-cross');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Cyclotourisme');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'V�lo tout terrain');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'A�kido');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bando et Banshay');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ju-jitsu (jujutsu)');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Judo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Karat�');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kendo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kobudo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Sumo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Wushu (Kung Fu)');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Boxe am�ricaine ');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Boxe anglaise');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kick-boxing am�ricain');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Escrime');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Lutte gr�cromaine');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Curling');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Luge');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Patinage artistique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Patinage de vitesse');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ringuette');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bodybuilding');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Fitness');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Halt�rophilie');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Motocross');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Moto sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Vitesse moto');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Badminton');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Jeu de paume');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Racquetball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Squash');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tennis');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tennis de table');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), '�quitation classique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), '�quitation Western');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'A�robic');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Gymnastique artistique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Gymnastique rythmique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Trampoline');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tumbling');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Alpinisme');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Canyonisme');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Course d''orientation');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Escalade');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Grimpe d''arbres');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Raid nature');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Sp�l�ologie');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Apn�e');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Aviron');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bateau-dragon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Cano�-kayak');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Canyonisme');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey subaquatique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Natation');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Natation synchronis�e');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Planche � voile');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Plong�e sous-marine');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Plongeon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Rafting');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski nautique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Surf');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Voile');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Water polo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Wakeboard');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Biathlon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Saut � ski');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Snowboard');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski alpin');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski acrobatique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski de fond');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski t�l�mark');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Danse sportive');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Marche nordique');

INSERT INTO Concentration VALUES (NEWID(), 'Administration', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Arts', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Biologie', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Chimie', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Design d''int�rieur', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), '�ducation physique', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Fran�ais', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion et exploitation d''entreprise agricole', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Informatique', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Langues modernes', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Math�matiques', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Philosophie', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Physique', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Sciences humaines', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Soins infirmiers', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de g�nie m�canique', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de travail social', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Technologie de l''�lectronique', 'D�partement', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Technologie d''analyses biom�dicales', 'D�partement', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Direction g�n�rale', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de l''informatique et du multim�dia', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Relations internationales', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Coll�ge militaire Royal', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des communications et des affaires corporatives', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de la formation continue et du d�veloppement institutionnel', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Secteur Brossard (Toutes cat�gories de personnel)', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources humaines', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de la direction des �tudes', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de consultation et cheminement scolaire', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des programmes et r�ussite scolaire', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de l�organisation scolaire et des moyens d''enseignement', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources financi�res', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des approvisionnements et de la reprographie', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources mat�rielles', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service � la vie �tudiante et � la communaut�', 'Service', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Sciences de la nature', 'ProgrammePreUniversitaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Sciences humaines', 'ProgrammePreUniversitaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Arts et lettres', 'ProgrammePreUniversitaire', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Technologie d�analyses biom�dicales', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion et exploitation d�entreprise agricole', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Soins infirmiers', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de g�nie m�canique', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Technologie de l��lectronique', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de travail social', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de comptabilit� et de gestion', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion de commerces', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de l�informatique', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de design d''int�rieur', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de la logistique du transport', 'ProgrammeTechnique', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Accueil et int�gration et mise � niveau', 'ProgrammeAccueilIntegration', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Partenaires du C�gep', 'Partenaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Communaut� ext�rieur au c�gep', 'Communaute', 0);