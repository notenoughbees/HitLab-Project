using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// From https://www.youtube.com/watch?v=oq_oXrFuUus
public class ScoreManager : MonoBehaviour
{
    TMP_Text TextScoreUI; 
    private int _scr;
    public int Score
    {
        get { return _scr; }
        set
        {
            _scr = value; // Update the UI text
            TextScoreUI.text = Score.ToString();
            PlayerPrefs.SetInt("Score", Score);
        }
    }
}
