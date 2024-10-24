using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderwebSpawning : MonoBehaviour
{
    private MinigameOkeoverBridgeUIController uiScript;

    public GameObject spiderwebPrefab;
    private GameObject webSpawningArea;


    void Start()
    {
        uiScript = FindObjectOfType<MinigameOkeoverBridgeUIController>();

        StartCoroutine(SpawnWebs());
    }


    IEnumerator SpawnWebs()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 15)); // can change numbers to make game easier/harder
            if (uiScript.isPlayingGame)
            {
                SpawnWeb();
            }
        }
    }

    void SpawnWeb()
    {
        // for some reason, the gameobjects in the scene can take a few seconds to appear, so
        // when finding the web spawning area, we can't do that in Start() because it might
        // not be loaded yet. So I'm just checking for it every time we want to spawn a web.
        //Debug.Log("WSA size: " + GameObject.FindGameObjectsWithTag("WebSpawningArea").Length);

        try
        {
            webSpawningArea = GameObject.FindGameObjectsWithTag("WebSpawningArea")[0]; // unlike with egg laying areas, there is only 1 web spawning area, since we just have 1 big shape placed over the scene

            // (this code is based on LayEgg(), but is simpler since there's only 1 area)
            // choose a random position within the "possible spiderweb area" - a shape placed in the scene where webs can spawn
            Bounds bounds = webSpawningArea.GetComponent<Renderer>().bounds;
            Vector3 webSpawningPos = new Vector3(
                UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
            );

            // spawn the web
            Instantiate(spiderwebPrefab, webSpawningPos, Quaternion.identity);
            //Debug.Log("[SpiderwebSpawning] spawned a web at: " + webSpawningPos);
        }
        catch (IndexOutOfRangeException e) {
            Debug.LogError("[SpiderwebSpawning] no web spawning area found");
        }
    }





}
