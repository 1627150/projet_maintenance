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
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Football américain');
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
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Vélo tout terrain');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Aïkido');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bando et Banshay');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ju-jitsu (jujutsu)');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Judo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Karaté');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kendo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kobudo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Sumo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Wushu (Kung Fu)');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Boxe américaine ');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Boxe anglaise');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Kick-boxing américain');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Escrime');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Lutte grécromaine');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Curling');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Luge');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Patinage artistique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Patinage de vitesse');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ringuette');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bodybuilding');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Fitness');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Haltérophilie');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Motocross');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Moto sur glace');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Vitesse moto');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Badminton');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Jeu de paume');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Racquetball');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Squash');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tennis');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Tennis de table');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Équitation classique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Équitation Western');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Aérobic');
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
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Spéléologie');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Apnée');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Aviron');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Bateau-dragon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Canoë-kayak');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Canyonisme');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Hockey subaquatique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Natation');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Natation synchronisée');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Planche à voile');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Plongée sous-marine');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Plongeon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Rafting');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski nautique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Surf');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Voile');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Water polo');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Wakeboard');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Biathlon');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Saut à ski');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Snowboard');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski alpin');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski acrobatique');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski de fond');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Ski télémark');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Danse sportive');
INSERT INTO Activite (Id_Activite, Nom_Activite) VALUES (NEWID(), 'Marche nordique');

INSERT INTO Concentration VALUES (NEWID(), 'Administration', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Arts', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Biologie', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Chimie', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Design d''intérieur', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Éducation physique', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Français', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion et exploitation d''entreprise agricole', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Informatique', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Langues modernes', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Mathématiques', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Philosophie', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Physique', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Sciences humaines', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Soins infirmiers', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de génie mécanique', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de travail social', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Technologie de l''électronique', 'Département', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Technologie d''analyses biomédicales', 'Département', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Direction générale', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de l''informatique et du multimédia', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Relations internationales', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Collège militaire Royal', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des communications et des affaires corporatives', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de la formation continue et du développement institutionnel', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Secteur Brossard (Toutes catégories de personnel)', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources humaines', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de la direction des études', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de consultation et cheminement scolaire', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des programmes et réussite scolaire', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service de l’organisation scolaire et des moyens d''enseignement', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources financières', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des approvisionnements et de la reprographie', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service des ressources matérielles', 'Service', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Service à la vie étudiante et à la communauté', 'Service', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Sciences de la nature', 'ProgrammePreUniversitaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Sciences humaines', 'ProgrammePreUniversitaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Arts et lettres', 'ProgrammePreUniversitaire', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Technologie d’analyses biomédicales', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion et exploitation d’entreprise agricole', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Soins infirmiers', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de génie mécanique', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Technologie de l’électronique', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de travail social', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de comptabilité et de gestion', 'ProgrammeTechnique', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Gestion de commerces', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de l’informatique', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de design d''intérieur', 'ProgrammeTechnique', 0); 
INSERT INTO Concentration VALUES (NEWID(), 'Techniques de la logistique du transport', 'ProgrammeTechnique', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Accueil et intégration et mise à niveau', 'ProgrammeAccueilIntegration', 0);

INSERT INTO Concentration VALUES (NEWID(), 'Partenaires du Cégep', 'Partenaire', 0);
INSERT INTO Concentration VALUES (NEWID(), 'Communauté extérieur au cégep', 'Communaute', 0);