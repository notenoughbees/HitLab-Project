using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private GameObject[] caddisflies;
    private GameObject target;
    public float spiderSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        caddisflies = GameObject.FindGameObjectsWithTag("Caddisfly");
        FindTarget();
        
        if(target != null) // spider doesn't move until it has a target. TODO: update this so that the spider doesn't appear until there are caddisflies, then it moves immeaditely towards one
        {
            // https://docs.unity3d.com/ScriptReference/Vector3.MoveTowards.html
            var step = spiderSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
    }

    void FindTarget()
    {
        if(target = null)
        {
            target = caddisflies[Random.Range(0, caddisflies.Length)];
            Debug.Log("[SPIDER] target found");
        }
    }
}
