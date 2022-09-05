using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public GameObject objToSpawn;
    public float timer;
    public float spawnPeriod;
    public int numberSpawnedEachPeriod;
    public Vector3 originInScreenCoords;
    public int score;

    // Use this for initialization
    void Start()
    {
        score = 0;
        timer = 0;
        spawnPeriod = 5.0f;
        numberSpawnedEachPeriod = 3;
        originInScreenCoords = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
    }
        
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnPeriod)
        {
            Debug.Log("spawning!");
            timer = 0;
            float width = Screen.width;
            float height = Screen.height;
            for (int i = 0; i < numberSpawnedEachPeriod; i++)
            {
                float horizontalPos = Random.Range(0.0f, width);
                float verticalPos = Random.Range(0.0f, height);
                Instantiate(
                    objToSpawn,
                    Camera.main.ScreenToWorldPoint(
                        new Vector3(horizontalPos, verticalPos, originInScreenCoords.z)
                    ),
                    Quaternion.identity
                );
            }
            /* if you want to verify that this method works, uncomment
            this code. What will happen when it runs is that one object will be spawned
            at each corner of the screen, regardless of the size of the screen. If you
            pause the Scene and inspect each object, you will see that each has a Ycoordinate
            value of 0.
            */
            // Vector3 botLeft = new Vector3(0,0,originInScreenCoords.z);
            // Vector3 botRight = new Vector3(width, 0, originInScreenCoords.z);
            // Vector3 topLeft = new Vector3(0, height, originInScreenCoords.z);
            // Vector3 topRight = new Vector3(width, height, originInScreenCoords.z);
            // Instantiate(objToSpawn, Camera.main.ScreenToWorldPoint(topLeft), Quaternion.identity );
            // Instantiate(objToSpawn, Camera.main.ScreenToWorldPoint(topRight), Quaternion.identity );
            // Instantiate(objToSpawn, Camera.main.ScreenToWorldPoint(botLeft), Quaternion.identity );
            // Instantiate(objToSpawn, Camera.main.ScreenToWorldPoint(botRight), Quaternion.identity );
        }
    }
}
