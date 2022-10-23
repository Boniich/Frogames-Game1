using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ViewInGame : MonoBehaviour
{


    public static ViewInGame sharedInstance;

    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI highScoreLebel;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {

        if(GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
        
    }

    public void SetHighScoreLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            highScoreLebel.text = PlayerPrefs.GetFloat("highscore", 0).ToString("f0");
        }
    }

    public void UpdateCoinsLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
        {
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
        }
    }
}
