// Based on Ryan's SillyCube exercise.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaddisflyBehaviour : MonoBehaviour
{
    public static event Action EggCountIncreased = delegate { };

    public GameObject eggPrefab;

    private Vector3 hatchPosition;
    private const float maxDist = 5;
    private const float speed = 0.8f;
    private const float interval = 4;
    private Vector3 targetPosition;
    private GameObject[] eggLayingAreas;

    // Start is called before the first frame update
    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(ChangeTargetPosition());

        eggLayingAreas = GameObject.FindGameObjectsWithTag("EggLayingArea");
        StartCoroutine(LayEggs());
    }

    // Update is called once per frame
    void Update()
    {
        moveAround();
        Debug.DrawLine(transform.position, Vector3.up, Color.black);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

    // Move the caddisfly by randomly moving directly forward at set speed.
    // We don't need to rotate it since at the moment it's just a 2D sprite with billboarding (it'll need to be rotated once it's a 3D model tho)
    void moveAround()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Once the caddisfly reaches the target position, set a new target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTargetPosition();
        }
    }

    // Set a new random target position, centered on the hatch position
    void SetNewTargetPosition()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * maxDist; // Random point in a sphere
        targetPosition = hatchPosition + randomDirection; // Target position around the hatch position
    }

    //IEnumerators are coroutines that run outside of Update(). You can control your own timing and events in them.
    IEnumerator ChangeTargetPosition()
    {
        // While true means this loop will run for as long as the object exists.
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, interval)); // Wait for a random time before changing the target
            SetNewTargetPosition();
        }
    }



    IEnumerator LayEggs()
    {
        // Wait an initial period of 20-30s before starting to lay eggs
        yield return new WaitForSeconds(UnityEngine.Random.Range(20, 30));

        // Continously lay eggs every 30-40s
        while (true)
        {
            LayEgg();

            yield return new WaitForSeconds(UnityEngine.Random.Range(30, 40));
        }
    }


    void LayEgg()
    {
        Debug.Log("Starting egg-laying process...");

        // pick an egg-laying area to go to
        GameObject eggLayingArea = eggLayingAreas[UnityEngine.Random.Range(0, eggLayingAreas.Length)];

        //TODO: need to pick a point across all of the planes, NOT randomly pick a plane and then randomly pick a point, since that results in many eggs on small planes & few eggs on large planes.

        // pick a point on that area to lay te eggs at
        Bounds bounds = eggLayingArea.GetComponent<Renderer>().bounds;
        Vector3 eggLayingPos = new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            eggLayingArea.transform.position.y,
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );

        // lay the egg sac
        transform.position = Vector3.MoveTowards(transform.position, eggLayingPos, speed * Time.deltaTime);
        Debug.Log("Laying an egg at " + eggLayingPos + "...");
        Instantiate(eggPrefab, eggLayingPos, Quaternion.identity);


        EggCountIncreased?.Invoke();
    }



    public void SetHatchPosition(Vector3 pos)
    {
        hatchPosition = pos;
        Debug.Log("hatch position: " + hatchPosition);
    }

}
