using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameBridgeScoreHUD : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            UpdateHUD();
        }
    }

    private void Start()
    {
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        scoreText.text = score.ToString();
    }
}
