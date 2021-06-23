

![img](.\Assets\Images\Logo.png)



- Baptiste PERRIN	              Gameplay Developer / Producer
- Colin LE PICHON                 Gameplay Developer / 3D Gfx Artist
- Mathieu TAILLANDIER       Gameplay Developer / 3D Gfx Artist
- Mathieu PELLAN                 Gameplay Developer/  Sound Designer

# Introduction

Dans le cadre du module d'Introduction à Unity3D, nous avions pour projet de créer un jeu-vidéo avec Unity. Nous sommes partis dans l'idée de faire un jeu-vidéo Tower Defense en 3D. Cela nous permettrait d'en apprendre plus sur la conception et le développement d'applications, et plus spécifiquement de jeux-vidéos.

Notre jeu se base dans un univers médiéval fantastique, où l'on doit à partir de tourelles, venir à bout des gobelins envahisseurs voulant envahir notre château.

## Cahier des charges

#### Aspect global

Tower Defense en 3D avec thème LowPoly / Médiéval
Victoire lorsque toutes les vagues et niveaux sont terminés
Défaite quand le château n'a plus de point de vie	

#### Aspect technique et fonctionnalités

- Déplacement des ennemis d'un point A à un point B
- Tourelles alliées plaçables à des endroit prédéfinis
- Système d'économie
- Système de vagues d'apparition des ennemis
- Système de vie des bâtiments et des ennemis  
- Système de caméra mobile

#### Aspect graphique

- Style fantasy / médiéval
- HUD :
  - Menu :
    - Jouer
    - Charger une sauvegarde
    - Meilleurs scores
    - Quitter
    - Logo ESIEE
    - Crédits des développeurs
  - Pause
    - Reprendre
    - Menu
    - Quitter
  - Victoire / Défaite
    - Niveau suivant (victoire)
    - Menu principal
  - Interface Jeu
    - La monnaie
    - HUD de vie pour les entités concernées
    - HUD de sélection des tours au clique sur les emplacements

## Management

Pour ce projet nous avons mis en place un [Trello](https://trello.com/b/fRGsUSr1/goblin-rush). Il nous a permis d'avoir un suivi continu des fonctionnalité à développer et des tâches à réaliser. Chaque tâche était assigné à une ou plusieurs catégories (Technique, Gameplay, GFX, SFX, UI). Afin de permettre aux développeurs de savoir sur quels sujets travailler. L'organisation permet une meilleure cohésion d'équipe et donc une meilleure productivité.

Nous avons collaboré à l'aide de Git pour versionner nos différents travaux de manière continue. Nous nous sommes mis d'accord pour travailler de sorte à ce que chaque tâche Trello corresponde à une branche Git. Dès lors qu'un collaborateur en avait fini avec sa tâche, il pouvait soumettre sa branche actuelle à Pull Request, outil disponible sur GitHub. Chaque Pull Request devait être validé par au moins deux personnes.

Cette continuité de travail nous a permis d'avoir une qualité de rendu rapide et efficace, nous évitions les risques au plus possible.



# Developper Guide

## Fonctionnement et structure

### Shared object between scenes

### Singleton

### Event

### Manager

## ASSET utilisés

### GFX

[Unity Asset Store - Castle Pack — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/environments/castle-pack-by-progru-185976)

[Unity Asset Store - Fantasy Medieval Houses/Props — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/environments/fantasy/free-fantasy-medieval-houses-and-props-pack-167010)

[Sketchfab - Low Poly Goblin — Gratuit — CC](https://sketchfab.com/3d-models/low-poly-goblin-rigged-7dc07eff136d4725ac6d1d5001656182)

[Sketchfab - Low Poly Rock Cave — Gratuit — CC](https://sketchfab.com/3d-models/low-poly-rock-cave-7923065c6e5449dc8cdc5dd2ae653fbe)

[Unity Asset Store - Mid Poly Stylized Swamp — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/environments/landscapes/free-mid-poly-stylized-swamp-remastered-123765)

[Unity Asset Store - Low Poly Vegetation — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/environments/low-poly-free-vegetation-kit-176906)

[Unity Asset Store - Low Poly Trees/Rocks — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/vegetation/lowpoly-trees-and-rocks-88376)

[Unity Asset Store - Terrain Tools Sample — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/2d/textures-materials/nature/terrain-tools-sample-asset-pack-145808)

[Unity Asset Store - Low Poly Crates — Gratuit — Unity AS EULA](https://assetstore.unity.com/packages/3d/props/low-poly-crates-80037)

### SFX

[Freesound - Sword Swipe 2 — Gratuit — CC](https://freesound.org/people/LukeSharples/sounds/209122/) 

[Freesound - Sword Attack — Gratuit — CC](https://freesound.org/people/Saviraz/sounds/547600/)

[Freesound - Shield Bash Impact — Gratuit — CC](https://freesound.org/people/Hybrid_V/sounds/319590/)

[Freesound - Battle Start — Gratuit — CC](https://freesound.org/people/Sound_Genius78/sounds/430772/)

[Freesound - Cannon — Gratuit — CC](https://freesound.org/people/Isaac200000/sounds/184650/)

[Freesound - Bow — Gratuit — CC](https://freesound.org/people/Erdie/sounds/65734/)

[Freesound - Military March — Gratuit — CC](https://freesound.org/people/zagi2/sounds/204196/)

[Freesound - Victory Cheer — Gratuit — CC](https://freesound.org/people/chripei/sounds/165491/)

[Freesound - Pop — Gratuit — CC](https://freesound.org/people/deraj/sounds/202230/)

[Freesound - Hammer — Gratuit — CC](https://freesound.org/people/sgrowe/sounds/342542/)

[YouTube - Villager Sound — Gratuit](https://www.youtube.com/watch?v=561xYvjMbNk)

[YouTube - Aww — Gratuit](https://www.youtube.com/watch?v=ltjT25GyXTM)

### UI

[Freepik - Wooden and golden UI buttons — Gratuit — Freepik License](https://www.freepik.com/free-vector/wooden-gold-buttons-ui-game_12760665.htm#page=1&query=wood%20ui&position=1)



# User Guide

Dans le guide du jeu Gobelin Rush , vous trouverez des instructions détaillées sur les différentes aventures qui vous attend. De plus, chaque description comporte une carte où figurent les éléments clés. Pour cela vous cliquerez sur les caisse en bois pour choisir quelle tourelle positionné.

## Comment jouer

#### Touches

![commands](.\Assets\Images\commands.png)

Goblin Rush se joue principalement à la souris, où se font les interactions comme voir la portée des tourelles ou interagir avec les HUD. 

La rotation de la caméra se fait avec les touches Q et D ou flèche gauche/droite. Le zoom/dézoom se font soit avec les touches Z et S ou les flèches haut/bas.

#### Déroulement

Vous pouvez poser des tourelles sur des emplacements prédéfinis (boîtes). Vous avez alors le choix de poser un canon ou une baliste (arbalète).

Vous devez survivre aux vagues d'ennemis avant que celles-ci ne détruisent votre château. Vous ferez face à des ennemis divers et variés, les gobelins sont farouches.

Il y a deux niveaux à dispositions actuellement : le premier niveau est une vallée avec un seul chemin distinct. Le deuxième niveau est un marécage, beaucoup plus sombre, avec plusieurs possibles chemins empruntables par les ennemis.







Le cas de victoire arrive quand toutes les vagues d'ennemis sont vaincus et les deux niveaux ont été complétés. Le cas de défaite se produit lorsque le château a perdu tous ses points de vie.

#### Tourelles

Nous avons donc 

#### Gobelins

#### Niveaux

- Vallée

![MapLvl1.png](.\Assets\Images\MapLvl1.png)

- Marécage

![MapLvl2.png](.\Assets\Images\MapLvl2.png)

# Conclusion

### Limitations

Ce projet était une première pour la majorité d'entre-nous. Les procédés sont toujours plus lents lorsque l'on doit passer par une phase d'auto-formation afin d'assimiler de nouveaux concepts, méthodes, principes ou technologies plus généralement.

### Voies d'amélioration

#### GFX

- Plus de particules
- Plus d'animations
- Afficher le coût des tourelles

#### SFX

- Éventuellement des sons faits-maison pour avoir une base SFX originale

#### Gameplay

- Différents types d'ennemis (autre que des gobelins)
- Plus de tourelles
- Système de magie pour des pouvoirs tierces
- Gestion du temps
- Paramètres
- Niveau de difficulté
- Amélioration du système de waypoints
- Meilleure exploitation des events

### Bugs connus

Tous les bugs techniques bloquants ont été corrigés lors du développement. Nous avons eu quelques soucis notamment au niveau de la suppression des tourelles, des bugs d'affichage de la barre de vie des diverses entités, problème de gestion de projectile ou de déplacement d'ennemis, etc.

Il nous reste actuellement un seul bug à corriger au niveau de l'affichage du ciel sur le niveau de la vallée.