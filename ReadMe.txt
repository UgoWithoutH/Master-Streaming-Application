
					    READ ME
===============================================================================================

Veuillez, s'il vous plaît, lire ce fichier avant votre première utilisation de Master_Streaming.

Pour lancer l'application, un double-click sur l'exécutable dans votre Bureau suffit.



Master_Streaming est une application développée par Ugo VIGNON et Maël CHAUMONT lors du 

projet tuteuré de 1ère année de DUT de développement d'une application WPF en C# et XAML.



Master_Streaming a été créé dans l'optique d'être un listing de films et séries détaillé

et doit donc être utilisé de cette manière.

Éléments important dans le code : 
- utilisation de EventTrigger et Storyboard dans UC_Master.xaml
- le thème clair/sombre : sur certains composants n'était pas appliqué le changement de couleur via MaterialDesign, nous avons donc fait des méthodes dans UC_header.xaml.cs
- le texte du bouton d'ajout et de suppression à la watchlist du detail d'une oeuvre est dynamique dans UC_Detail.xaml.cs

Ajout personnels : 
- système de profils utilisateurs permettant de stocker plusieurs données de profils
- animation d'ouverture et de fermeture du menu des genres via l'utilisation de Storyboard 
- fonction de recherche d'oeuvres par rapport à une chaine de caractère renseignée (compare la chaine de caractère avec le début du titre de chaque oeuvres)
- système de Watchlist permettant à l'utilisateur de savoir les oeuvres qu'il a déjà visionnées

Comment lancer l'application :
1) lancer Master_Streaming.Setup.msi se trouvant dans le dossier trunk\source\Master_Streaming.Setup\Debug (un ordinateur protégé bloque l'installation, il faut confirmer pour installer)
2) un racourcis de l'application apparait sur le bureau. Il est aussi possible de trouver Master_Streaming.exe dans le dossier Program Files (x86)\Master_Streaming_Company\Master_Streaming.Setup\Master_Streaming.
Il suffit ensuite de lancer l'executable



Bonne utilisation !

===============================================================================================