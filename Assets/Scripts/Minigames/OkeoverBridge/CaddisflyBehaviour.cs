// Based on Ryan's SillyCube exercise.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaddisflyBehaviour : MonoBehaviour
{
    public static event Action EggCountIncreased = delegate { };

    public AudioSource caddisflyTrappedSound;

    private Vector3 hatchPosition;
    private const float maxDist = 5;
    private float speed = 0.8f;
    private const float interval = 4;
    private Vector3 targetPosition;

    // for laying eggs
    public GameObject eggPrefab;
    private bool isLayingEgg;
    private GameObject[] eggLayingAreas;

    // for changing the fly colour
    private SpriteRenderer flyRenderer;
    private Color flyOriginalColour;

    public bool isCaught { get; private set; } = false;


    void Start()
    {
        SetNewTargetPosition();
        StartCoroutine(ChangeTargetPosition());

        eggLayingAreas = GameObject.FindGameObjectsWithTag("EggLayingArea");
        StartCoroutine(LayEggs());

        // for changing the fly colour
        flyRenderer = GetComponent<SpriteRenderer>();
        flyOriginalColour = flyRenderer.color;
    }

    void Update()
    {
        flyAroundRandomly();
        //Debug.DrawLine(transform.position, Vector3.up, Color.black);
        //Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

    // Move the caddisfly by randomly moving directly forward at set speed.
    // We don't need to rotate it since at the moment it's just a 2D sprite with billboarding (it'll need to be rotated once it's a 3D model tho)
    void flyAroundRandomly()
    {
        if(!isLayingEgg)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Once the caddisfly reaches the target position, set a new target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                SetNewTargetPosition();
            }
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
        // Wait an initial period before starting to lay eggs
        yield return new WaitForSeconds(UnityEngine.Random.Range(20, 30));

        // Continously lay eggs semi-periodically
        while (true)
        {
            StartCoroutine(LayEgg());

            yield return new WaitForSeconds(UnityEngine.Random.Range(30, 45));
        }
    }


    IEnumerator LayEgg() // is coroutine due to MoveTowards()
    {
        isLayingEgg = true;

        // pick an egg-laying area to go to
        GameObject eggLayingArea = eggLayingAreas[UnityEngine.Random.Range(0, eggLayingAreas.Length)];

        //TODO: need to pick a point across all of the planes, NOT randomly pick a plane and then randomly pick a point, since that results in many eggs on small planes & few eggs on large planes.
        // pick a point on that area to lay the eggs at
        Bounds bounds = eggLayingArea.GetComponent<Renderer>().bounds;
        Vector3 eggLayingPos = new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            eggLayingArea.transform.position.y, //TODO: make it possible to lay eggs anywhere on an angled plane w/o treating it like a cube
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );

        // fly over to the point
        Debug.Log("Moving to " + eggLayingPos + " to lay an egg...");
        while(Vector3.Distance(transform.position, eggLayingPos) > 0.001f) {
            transform.position = Vector3.MoveTowards(transform.position, eggLayingPos, speed * Time.deltaTime);
            // must yield each frame so the fly actually flies over to the point across multiple frames, instead of just teleporting there over one frame
            yield return null;
        }
        // lay the egg sac
        Debug.Log("Laid an egg at " + eggLayingPos);
        Instantiate(eggPrefab, eggLayingPos, Quaternion.identity);


        // flash green to provide visual feedback
        StartCoroutine(FlashFlyColour(new Color(0, 255, 0), 1));

        // trigger this event to increase the score
        EggCountIncreased?.Invoke();

        isLayingEgg = false;
    }

    public IEnumerator FlashFlyColour(Color colour, float delay)
    {
        flyRenderer.color = colour;
        yield return new WaitForSeconds(delay);
        flyRenderer.color = flyOriginalColour;
    }

    public void FlyCaught()
    {
        // only call this method once, when the fly collides with a web
        if (isCaught) { return; }
        isCaught = true;

        caddisflyTrappedSound.Play();

        // stop the fly from moving
        setSpeed(0f);

        // flash red
        StartCoroutine(FlashFlyColour(new Color(192, 0, 0), 1));

        // must kill the fly after a delay since we want it to flash red first
        StartCoroutine(KillFlyAfterDelay(1));
    }

    IEnumerator KillFlyAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }




    public void SetHatchPosition(Vector3 pos)
    {
        hatchPosition = pos;
        //Debug.Log("hatch position: " + hatchPosition);
    }

    // This method is used when flies get stuck in webs, as their speed becomes 0
    public void setSpeed(float s)
    {
        speed = s;
    }

}
