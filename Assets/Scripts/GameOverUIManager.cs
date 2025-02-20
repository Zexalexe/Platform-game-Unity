using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour {
    public Text score;
    public Text highScore;

    void Start()
    {
        score.text = GameManager.instance.score.ToString();
        highScore.text = GameManager.instance.highScore.ToString();
    }
    public void RestartGame()
    {
        GameManager.instance.Reset();
    }

    public void CloseGame(){
        GameManager.instance.CloseGame();
    }
}