# Demo_CharacterControl

Charactor Control is very common and general system handling in every Role Play Game.
So today, I wanna to pick up my unity development skill to play it around with 2020.1.8f1 library. 

In this demo, I have tried 2 kind of Charactor Control by using Unity build-in library.
1. Charactor Control with keyboard and mouse key press.
2. Unity AI path finding
And then, I also try to make a town in pixel style model.
The voxel modeling tool named "MagicaVoxel".



## Charactor Control
![SceneShoot_CharactorControl](https://github.com/cyrus1127/Demo_CharacterControl/blob/main/SceneShoot_CharactorControl.png)

Key Map :
- W,S - move charactor forward & backward
- A,D - rotate charactor in counterclockwise & clockwise
- Space - charactor jump up
- Mouse left - fire something


## AI path finding
![SceneShoot_UnityPathFound](https://github.com/cyrus1127/Demo_CharacterControl/blob/main/SceneShoot_UnityPathFound.png)

Setup a maze and target object and CPU
Prepared : 
- maze ( with Navgiation Baked )
- target object ball
- character controller (with Nav Mesh Agent & some custom scripts)
