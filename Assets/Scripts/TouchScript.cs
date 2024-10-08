using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour
{
    //public Sprite caddisfly;
    public GameObject caddisfly; // a prefab, not a sprite

    public AudioSource egg_hatch_sound;
    public AudioSource spider_squish_sound;

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

            //Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //RaycastHit2D hit2D = Physics2D.Raycast(touchPos, Vector2.zero);

            if (Physics.Raycast(ray, out hit))
            //if(hit2D.collider != null)
            {
                Debug.Log("name: " + hit.collider.name);

                // if we touched an egg sprite, delete it, play a sound, & replace it with a caddisfly sprite
                if (hit.collider != null && hit.collider.tag == "Egg")
                {
                    Debug.Log("touched an egg");
                    GameObject sprite = hit.transform.gameObject;
                    Destroy(sprite);
                    //Destroy(hit2D.collider.gameObject);

                    Debug.Log("*egg hatching sound*");
                    //AudioSource audio = GetComponent<AudioSource>();
                    //audio.Play(); // egg hatch sound
                    egg_hatch_sound.Play();

                    //Touch myTouch = Input.GetTouch(0);
                    Vector3 eggPos = hit.collider.transform.position;
                    SpawnCaddisfly(eggPos);
                }
                // if we touched a spider, delete it & play a sound
                else if (hit.collider != null && hit.collider.tag == "Spider")
                {
                    Debug.Log("touched a spider");
                    GameObject sprite = hit.transform.gameObject;
                    Destroy(sprite);
                    Debug.Log("*spider die sound*");
                    spider_squish_sound.Play();
                }
                else
                {
                    Debug.Log("touched something else");
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
        Debug.Log("spawning a caddisfly...");
        //Vector3 objPos = Camera.main.ScreenToWorldPoint(touch.position);
        Debug.Log("objPos: " + objPos);
        objPos.z = 1; // make the obj appear in front of everything else
        Debug.Log("caddisfly: " + caddisfly.ToString());
        Instantiate(caddisfly, objPos, Quaternion.identity);
    }
}
