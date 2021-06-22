![img](.\Assets\Images\Logo.png)



# Introduction

Bonjour,
Si vous lisez ce texte c'est que nôtre projet Gobelin Rush vous intéresse ! Effectivement pour notre 1er année de cycle ingénieur à l'ESIEE Paris nous avons réaliser un jeux avec Unity. Pour cela nous étions une équipe de 4 développeurs :

- Baptiste PERRIN	              Gameplay Developer / Producer
- Colin LE PICHON                 Gameplay Developer / 3D Gfx Artist
- Mathieu TAILLANDIER       Gameplay Developer / 3D Gfx Artist
- Mathieu PELLAN                 Gameplay Developer/  Sound Designer

## Cahier des charges

#### Aspect global

​	Tower Defense en 3D avec thème LowPoly / Médiéval
​	Victoire lorsque toutes les vagues sont terminé et que chaque levels ont été terminés
​	Défaite quand le château n'a plus de point de vies	

#### Aspect technique et fonctionnalités

- Déplacement des ennemis d'un point A à un point B
- Tourelle alliés plaçable à des endroit prédéfini
- Système d'économie
- Système de vague d'apparition des ennemis
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
    - Crédits des dèveloppeurs
  - Pause
    - Reprendre
    - Menu
    - Quitter
  - Victoire / Défaite
    - Niveau suivant si c'est une victoire
    - Menu principal
  - Interface Jeu
    - La monnaie
    - La vie du chateau
    - La vie des ennemies une fois qu'ils ont pris des dégâts
    - HUD de sélection des tours au clique sur les emplacements

## Management

Pour ce projet nous avons mis en place un trello, disponible [ici](https://trello.com/b/fRGsUSr1/goblin-rush). Il nous a permis d'avoir un suivi continu des fonctionnalité à développer et des tâches à réaliser. Chaque tâches étais assigné à une ou plusieurs catégories (Technique, Gameplay, GFX, SFX, UI). Cela permettais au personnes de savoir ce qu'elles avaient a faire selon leur rôles.

Ensuite nous avons travailler avec l'outil git afin de réaliser une branche pour chaque tâches défini au préalable sur le trello. A chaque fois qu'un développeur avais finis sa branche il soumettais une Pull Request qui devais être validé par deux autres développeur pour être mergé. Avoir un protocole si strict nous a permis d'éviter le plus de bug possible. Effectivement nous avons eu que 4 Issues à corriger lors de ce projet. 

# Developper Guide

## Fonctionnement et structure



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

[Freesound - Sword Swipe 2 — CC](https://freesound.org/people/LukeSharples/sounds/209122/) 

[Freesound - Sword Attack — CC](https://freesound.org/people/Saviraz/sounds/547600/)

[Freesound - Shield Bash Impact — CC](https://freesound.org/people/Hybrid_V/sounds/319590/)

[Freesound - Battle Start — CC](https://freesound.org/people/Sound_Genius78/sounds/430772/)

[Freesound - Cannon — CC](https://freesound.org/people/Isaac200000/sounds/184650/)

[Freesound - Bow — CC](https://freesound.org/people/Erdie/sounds/65734/)

[Freesound - Military March — CC](https://freesound.org/people/zagi2/sounds/204196/)

[Freesound - Victory Cheer — CC](https://freesound.org/people/chripei/sounds/165491/)

[Freesound - Pop — CC](https://freesound.org/people/deraj/sounds/202230/)

[Freesound - Hammer — CC](https://freesound.org/people/sgrowe/sounds/342542/)

[YouTube - Villager Sound — Gratuit](https://www.youtube.com/watch?v=561xYvjMbNk)

[YouTube - Aww — Gratuit](https://www.youtube.com/watch?v=ltjT25GyXTM)

### UI

[Freepik - Wooden and golden UI buttons — Gratuit — Freepik License](https://www.freepik.com/free-vector/wooden-gold-buttons-ui-game_12760665.htm#page=1&query=wood%20ui&position=1)

# User Guide

Dans le guide du jeu Gobelin Rush , vous trouverez des instructions détaillées sur les différentes aventures qui vous attend. De plus, chaque description comporte une carte où figurent les éléments clés. Pour cela vous cliquerez sur les caisse en bois pour choisir quelle tourelle positionné.

## Comment jouer

Bonjour jeune aventurier ! 

Tu es ici pour défendre ton royaume et pour cela tu n'aura qu'a suivre une seul et unique règle TUE TOUT LES GOBELINS AVANT QU'ILS NE TE TUENT !!!

Tout d'abord vous commencerez par repousser les envahisseurs dans le 1er niveau : La Vallée

Vous aurez le choix entre 10 emplacement de tourelle différents et 5 vagues différentes vous attaquerons :

![MapLvl1.png](.\Assets\Images\MapLvl1.png)

Ensuite vous devrez défendre votre avant-poste dans 2ème niveau, le Swamp

Vous aurez le choix entre 10 emplacement de tourelle différents et 5 vagues différentes vous attaquerons :

![MapLvl2.png](.\Assets\Images\MapLvl2.png)

# Conclusion

### Limitations

Le fait de n'être que trois et avec peut de notions graphique à été un facteurs très limitant. Nous avons du nous former en même temp que nous développions sur un temp très restreint.

### Voies d'amélioration

Pourquoi pas imaginer des animation plus pousser, plus de tourelles, des ennemis différents de races différentes. Implémenter une gestion du temp pour pouvoir l'accélérer, le mettre en pauses etc... Une interaction avec des sorts aussi pourrais être implémenter avec une gestion du Mana.

### Bugs connus

