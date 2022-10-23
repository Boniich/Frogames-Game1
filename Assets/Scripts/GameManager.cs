using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum GameState
{
    menu,
    inTheGame,
    gameOver
}

public class GameManager : MonoBehaviour
{//el GameManager se encarga de gestionar todo el gameplay del juego
 // Use this when game start

    public static GameManager sharedInstance;

    //Cuando el juego inicia, este se encuentra en estado de menu
    public GameState currentGameState = GameState.menu;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public int collectedCoins = 0;
    private int blockCount = 5; // define la cantidad de bloques que se van a generar


    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        //para asegurarnos que al iniciar el juego esta en el menu
        currentGameState = GameState.menu;
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

     void Update()
    {
       /* if ((Input.GetButtonDown("s")) && (currentGameState != GameState.inTheGame ))
        {
            StartGame();
        }*/
    }

    public void StartGame()
    {
        PlayerController.sharedInstance.StartGame();
        LevelGenerator.sharedInstance.GenerateInitialBlocks(blockCount);
        ChangeGameState(GameState.inTheGame);
        ViewInGame.sharedInstance.SetHighScoreLabel();
        
    }

    //called then the player dies
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();
        ChangeGameState(GameState.gameOver);
        ViewGameOver.sharedInstance.UpdateUI();
    }

    // we call this when the player decide to come back to main menu
    public void BackToMainMenu()
    {

        ChangeGameState(GameState.menu);

    }

    void ChangeGameState(GameState newGameState)
    {

        if(newGameState == GameState.menu)
        {

            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            //It should be show up the main menu
        }else if(newGameState == GameState.inTheGame)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            //the scene should be show up the game perse
        }
        else if(newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            //the scene should be show up the over of game
        }

        //guardamos el nuesvo estado pasado por parametros
        currentGameState = newGameState;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.sharedInstance.UpdateCoinsLabel();

    }

}
