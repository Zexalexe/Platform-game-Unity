using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text scoreLabel;

    void Start(){
        Refresh();
    }
    public void Refresh(){
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }
    
}
