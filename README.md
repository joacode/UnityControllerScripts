# UnityControllerScripts

For the player 3rd person controller, a Game Object called "Focal Point" must be created. The Focal Point is going to have attached the RotateCamera.cs script (which allow you to rotate the camera by moving the mouse horizontally).
Also, the script has a public Game Object called "Player". Just drag your player object there.

To control the player movement, attach the script PlayerController.cs to the Player. Then, in the Player object you have to drag the Focal Point you've just created to the "Focal Point" public Game Object.
