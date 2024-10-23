using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SpiderBehaviour : MonoBehaviour
{
    public static event Action CaddisflyCountDecreased = delegate { };

    public AudioSource caddisflyTrappedSound;

    void Start()
    {
        //Debug.Log("SpiderBehaviour START");
    }

    void OnTriggerEnter(Collider other)
    {
        // when the spider catches the caddisfly, destroy the fly and make a sound, then make the spider target another caddisfly
        if (other.CompareTag("Caddisfly"))
        {
            Debug.Log("[SPIDERWEB] A fly got caught in a web!");
            GameObject fly = other.gameObject;
            CaddisflyBehaviour flyBehaviour = other.GetComponent<CaddisflyBehaviour>();

            caddisflyTrappedSound.Play();

            // stop the fly from moving
            flyBehaviour.setSpeed(0f);

            // flash red
            StartCoroutine(flyBehaviour.FlashFlyColour(new Color(192, 0, 0), 1));

            // must kill the fly after a delay since we want it to flash red first
            StartCoroutine(KillFlyAfterDelay(fly, 1));

            // trigger this event to decrease the score
            CaddisflyCountDecreased?.Invoke();
        }
    }

    IEnumerator KillFlyAfterDelay(GameObject fly, int delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(fly);
    }

}