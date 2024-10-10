using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    public float spiderSpeed = 0.1f;
    public AudioSource caddisflyDieSound;

    private GameObject[] caddisflies;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        caddisflies = GameObject.FindGameObjectsWithTag("Caddisfly");
    }

    // Update is called once per frame
    void Update()
    {
        caddisflies = GameObject.FindGameObjectsWithTag("Caddisfly");
        FindTarget();
        
        if(target != null) // spider doesn't move until it has a target. TODO: update this so that the spider doesn't appear until there are caddisflies, then it moves immeaditely towards one
        {
            // https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            //Debug.Log("[SPIDER] moving towards " + target.ToString());
            var step = spiderSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            // when the spider catches the caddisfly, destroy the fly and make a sound, then make the spider target another caddisfly
            if(transform.position == target.transform.position)
            {
                //WaitForSeconds(2);
                Destroy(target);
                caddisflyDieSound.Play();
                //WaitForSeconds(2);
            }
        }
    }

    void FindTarget()
    {
        // if the spider has no target and there is at least 1 fly to target...
        if(target == null && caddisflies.Length != 0)
        {
            Debug.Log("caddisflies string; length: " + caddisflies.ToString() + "; " + caddisflies.Length);
            target = caddisflies[Random.Range(0, caddisflies.Length)];
            Debug.Log("[SPIDER] target found: " + target);
        }
    }
}
