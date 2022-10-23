using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewGameOver : MonoBehaviour
{

    public static ViewGameOver sharedInstance;
    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI scoreLabel;
    // Start is called before the first frame update

    private void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }
}
