// Based on Ryan's SillyCube exercise.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaddisflyMovement : MonoBehaviour
{
    private Vector3 hatchPosition;

    const float maxDist = 5;
    const float speed = 0.8f;
    const float interval = 4;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(ChangeTargetPosition());
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
        Vector3 randomDirection = Random.insideUnitSphere * maxDist; // Random point in a sphere
        targetPosition = hatchPosition + randomDirection; // Target position around the hatch position
    }

    //IEnumerators are coroutines that run outside of Update(). You can control your own timing and events in them.
    IEnumerator ChangeTargetPosition()
    {
        // While true means this loop will run for as long as the object exists.
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, interval)); // Wait for a random time before changing the target
            SetNewTargetPosition();
        }
    }


    public void SetHatchPosition(Vector3 pos)
    {
        hatchPosition = pos;
        Debug.Log("hatch position: " + hatchPosition);
    }

}
