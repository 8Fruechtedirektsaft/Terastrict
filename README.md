# Terastrict
In this game each player places tiles that influence each others value. At the end the player with the highest combined values wins.
(further instructions are given at the start of the game)

Assets from the Unity Asset Store:
https://assetstore.unity.com/packages/2d/fonts/free-pixel-font-thaleah-140059


code summary:

Assets/Code/Game
-controls the turn order
-activates/deactivates the UI objects
-takes in UI input from the players
-places tiles 
-calculates tile values

Assets/Code/Player
-contains information about the player and how to display his name
-determines font size based on the length of the name

Assets/Code/PlayerTextUI
-controls the player name display
-sets the text of the UI win message object

Assets/Code/ScoreUI
-sets the text of the score UI object

Assets/Code/SpriteHolder
-contains the sprites for the tiles
-returns the fitting sprite for a tile

Assets/Code/TileChoicesUI
-sets the buttons that represent tiles which can be selected to not interactable

Assets/Code/Tiledata
-contains the information about a tile

Assets/Code/ValueTextMapUI
-displays the values of the placed tiles
