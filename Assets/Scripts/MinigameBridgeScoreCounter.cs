using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBridgeScoreCounter : MonoBehaviour
{
    [SerializeField] MinigameBridgeScoreHUD scoreHUD;

    private void Start()
    {
        StartCoroutine(CountScore());
    }

    private IEnumerator CountScore ()
    {
        while(true)
        {
            scoreHUD.Score += 1;

            yield return new WaitForSeconds(1);
        }
    }
}
