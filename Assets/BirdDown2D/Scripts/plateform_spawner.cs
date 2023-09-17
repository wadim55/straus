using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateform_spawner : MonoBehaviour
{

    public static plateform_spawner Instance;

    public GameObject platformPrefab;
    public GameObject spikePlatformPrefab;
    public GameObject[] moving_Platforms;
    public GameObject breakablePlatfform;

    public float platform_spawn_Timer;
    private float current_platform_spawn_timer;
    private int platform_spawn_count;
    public float min_X = -2f, max_X = 2f;

    int speedincreser;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //Set PlatForm Spawn Timer
        current_platform_spawn_timer = platform_spawn_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        //calling SpawnPaaltForm Function
        spawnplatform();   
    }
    //spawnPlatform
    void spawnplatform()
    {
        //add time in curren platform spawn timer
        current_platform_spawn_timer += Time.deltaTime;
       //check
        if(current_platform_spawn_timer >= platform_spawn_Timer)
        {
            //time addition
            platform_spawn_count++;
            //assign value to temp variable
            Vector3 temp = transform.position;
            //set value of minX and maxX to temp.x
            temp.x = Random.Range(min_X, max_X);
            GameObject newPlatform = null;
            //if Condition on platform spawn count
            if (platform_spawn_count < 2)
            {
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
            }
            else if (platform_spawn_count == 2)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);

                }
                else
                {
                    newPlatform = Instantiate(moving_Platforms[Random.Range(0, moving_Platforms.Length)], temp, Quaternion.identity);
                }
            }
            else if(platform_spawn_count == 3)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);

                }
                else
                {
                    newPlatform = Instantiate(spikePlatformPrefab, temp, Quaternion.identity);
                }
            }
            else if (platform_spawn_count == 4)
            {
                if (Random.Range(0, 2) > 0)
                {
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);

                }
                else
                {
                    newPlatform = Instantiate(breakablePlatfform, temp, Quaternion.identity);
                }
                platform_spawn_count = 0;
            }
            if(newPlatform)
            newPlatform.transform.parent = transform;
            current_platform_spawn_timer = 0f;
        }
        }//spawn platform
    }

