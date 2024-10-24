using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SpiderBehaviour : MonoBehaviour
{
    public static event Action CaddisflyCountDecreased = delegate { };

    void OnTriggerEnter(Collider other)
    {
        // when a fly collides with a spiderweb, stop it moving, colour it red, then destroy it after a delay
        if (other.CompareTag("Caddisfly"))
        {
            Debug.Log("[SPIDERWEB] A fly got caught in a web!");
            GameObject fly = other.gameObject;
            CaddisflyBehaviour flyBehaviour = other.GetComponent<CaddisflyBehaviour>();

            // must destroy the fly outside of this script so that the fly
            // still gets destroyed even if we swipe the web (destroying it) 
            // after a fly gets caught in it but before the fly can be destroyed
            flyBehaviour.FlyCaught();

            // trigger this event to decrease the score
            CaddisflyCountDecreased?.Invoke();
        }
    }

}