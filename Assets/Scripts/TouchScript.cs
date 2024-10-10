using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    public GameObject caddisflyPrefab; // a prefab, not a sprite

    public AudioSource eggHatchSound;
    public AudioSource spiderSquishSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 200, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("name: " + hit.collider.name);

                GameObject sprite;
                switch (hit.collider.tag)
                {
                    // if we touched an egg sprite, delete it, play a sound, & replace it with a caddisfly sprite
                    case "Egg":
                        Debug.Log("touched an egg");
                        sprite = hit.transform.gameObject;
                        Destroy(sprite);

                        eggHatchSound.Play();

                        //Touch myTouch = Input.GetTouch(0);
                        Vector3 eggPos = hit.collider.transform.position;
                        Debug.Log("eggPos: " + eggPos.ToString());
                        SpawnCaddisfly(eggPos);
                        break;

                    // if we touched a spider, delete it & play a sound
                    case "Spider":
                        Debug.Log("touched a spider");
                        sprite = hit.transform.gameObject;
                        Destroy(sprite);

                        spiderSquishSound.Play();
                        break;

                    default:
                        Debug.Log("touched something else");
                        break;
                }
            }
            else
            {
                Debug.Log("didn't hit anything");
            }
        }
    }

    private void SpawnCaddisfly(Vector3 objPos)
    {
        Debug.Log("spawning a caddisfly at " + objPos + "...");
        //Vector3 objPos = Camera.main.ScreenToWorldPoint(touch.position);
        GameObject newCaddisfly = Instantiate(caddisflyPrefab, objPos, Quaternion.identity);

        // set the fly's hatching position because the fly will centre around here when it flies around
        CaddisflyMovement moveScript = newCaddisfly.GetComponent<CaddisflyMovement>();
        moveScript.SetHatchPosition(objPos);
    }
}
