using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score = 0;

    public int highScore = 0;

    public int currentLevel = 1;

    public int highestLevel = 3;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int amount){
        score += amount; 
        print("New Score: " + score.ToString());

        if(score > highScore){
            highScore = score;
            print("New high score: " + highScore);
        }
    }

    public void Reset(){
        score = 0;
        currentLevel = 1;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void CloseGame(){
        SceneManager.LoadScene("Home");
    }

    public void IncreaseLevel(){
        if(currentLevel <= highestLevel){
            currentLevel += 1;
        }
        else{
            currentLevel = 1;
        }
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
