# snakesAndLaddersUnity

The game is a basic interpretaion of the classic snakes and laddders. 

It gives you a way to create a custom board with number of snakes/ladders and the grid size. Then it will create a square box with the given size and add snakes and ladders randomly. Although as it is a basic representation the sprites used are circular for the tiles and colours are used for differentiating. Green is the base colour.
Yellow is the colour of a portal (it can be either snake or ladder), this uncertainity brings some more randomness to the game. Players are also differentiated by coloours. Red is Player-1, Pink is player-2, Purple is player-3 and Cyan is player-4. 

The Roll the Die button will play the turn for the respective player and move the player forward by corresponding steps, taking into account any portal they land on.

Most of the game is present in BoardManager script, and only Tile prefab is used for representing 1 cell on the grid.

Known issues and areas of improvement:
- Need to print tile number on the sprite
- Need to re-arrange the tiles so that the new row starts just above the end of the previous row
- Need to clear the canvas (or delete the pre-existing prefabs) every time new board is created
- Need to hide extra players in case less than 4 players are playing
