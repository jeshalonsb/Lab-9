ISaveable is the interface used that can allow any object that has the interface attached to it be Save() and Load()

TransformSaver works to keep the transforms and isMoving bool for the targets for the moving ones and the stationary ones. Player position is also saved here but since the player is still on the game then it doesn't really show. I probably should've replaced it with it to save the rotation but it is there. 

BinaryWriter/Reader saves everything to binary 

Application.persistentDataPath stores everything in files 