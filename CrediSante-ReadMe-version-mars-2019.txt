Modifier la Base de donnée de test

Étape 1
	Pour ce faire veuillez-vous rendre dans le dossier "CreditSante\Prod\Base de données"
	Utilisé le fichier sql suivant "ModificationsProjetEtudiant_Mars_2019.sql" pour les prochaines étapes

Étape 2
	ouvrir la base de donnée CreditSante dans l'environnement de test
	Créer une nouvelle requête avec le fichier sql de l'étape 1
		
	Vous aurez donc toutes les modifications en base de donnée nécessaire pour le bon fonctionnements des nouvelles fonctionnalité.

Étape 3
	Ouvrir la solution avec Visual Studio

Étape 4
	Générer la Solution en cliquant sur Générer puis Générer la Solution dans la barre en haut de la page

Étape 5
	Dans Visual Studio Exécuter un clic droit sur la solution dans l'explorateur de solutions et choisir "Restaurer les packages NuGet"

Étape 6
	Publier le site ASP.NET avec Visual Studio

Étape 7
	Remplacler les fichiers du site présentements sur le serveur IIS avec les nouveau fichiers
	OU
	Faire pointer le site IIS vers le dossier contenant le nouveau site publié

Étape 8
	Envoyer un courriel à Gabriel Simard avec l'addresse du site de test pour que Madame Ménard puisse tester de son coté.
	1459644@cstjean.qc.ca