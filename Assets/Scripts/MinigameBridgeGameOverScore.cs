using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameBridgeGameOverScore : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text eggScoreText;
    [SerializeField] TMP_Text flyScoreText;


    public void Start()
    {
        scoreText.text = (eggScoreText.text.ToIntArray()[0] + flyScoreText.text.ToIntArray()[0]).ToString();
    }
}
