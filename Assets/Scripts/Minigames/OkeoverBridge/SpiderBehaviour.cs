using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiderBehaviour : MonoBehaviour
{
    public static event Action CaddisflyCountDecreased = delegate { };

    public float spiderSpeed = 0.5f;
    public AudioSource caddisflyDieSound;

    private GameObject[] caddisflies;
    private GameObject target;

    void Start()
    {
        Debug.Log("SpiderBehaviour START");
        caddisflies = GameObject.FindGameObjectsWithTag("Caddisfly");
    }

    void Update()
    {
        caddisflies = GameObject.FindGameObjectsWithTag("Caddisfly");
        FindTarget();
        
        if(target != null) // spider doesn't move until it has a target. TODO: update this so that the spider doesn't appear until there are caddisflies, then it moves immeaditely towards one
        {
            // https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            //Debug.Log("[SPIDER] moving towards target located at " + target.transform.position);
            var step = spiderSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            // when the spider catches the caddisfly, destroy the fly and make a sound, then make the spider target another caddisfly
            if(Vector3.Distance(transform.position, target.transform.position) < 0.01f)
            {
                Debug.Log("[SPIDER] Spider caught the fly!");
                //WaitForSeconds(2);
                Destroy(target);
                caddisflyDieSound.Play();
                //WaitForSeconds(2);

                CaddisflyCountDecreased?.Invoke(); // trigger the event
            }
        }
    }

    void FindTarget()
    {
        // if the spider has no target and there is at least 1 fly to target...
        if(target == null && caddisflies.Length != 0)
        {
            Debug.Log("caddisflies string; length: " + caddisflies.ToString() + "; " + caddisflies.Length);
            target = caddisflies[UnityEngine.Random.Range(0, caddisflies.Length)]; //TODO: make spider choose the closest fly instead
            Debug.Log("[SPIDER] target found, at " + target.transform.position);
        }
    }
}
