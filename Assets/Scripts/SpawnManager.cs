using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject pointObject;
   
    private float lineWidth = 40f;

    public Transform player;
    public float spawnDistance = 100f;

    public float spawnInterval = 3f;
    public float spawnIntervalPoint = 15f;

    private float nextSpawnTime = 0f;
    private float nextSpawnTimePoint = 0f;

    private int[] lines = { -1, 0, 1 };
    private List<GameObject> obstacles = new List<GameObject>();
    private List<GameObject> points = new List<GameObject>();
    public List<Vector3> prevPositions = new List<Vector3>();

    private float positionY;

    //private PlayerController playerControllerScript;
    private GameManager gameManager;

    void Start()
    {
        positionY = player.position.y;
        //Debug.Log(player.position.z + spawnDistance);
        //playerControllerScript = GameObject.Find("SpherePlayer").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < 10; i++)
        {
            //int randomLine = lines[Random.Range(0, lines.Length)];
            GameObject randomObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
            //Vector3 spawnPos = new Vector3(randomLine * lineWidth, positionY, (player.position.z + spawnDistance));
            randomObstacle.SetActive(false);
            obstacles.Add(randomObstacle);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject point = Instantiate(pointObject);
            //Vector3 spawnPos = new Vector3(randomLine * lineWidth, positionY, (player.position.z + spawnDistance));
            point.SetActive(false);
            points.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime && gameManager.isGameActive)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnInterval;
            
          
            
        }
        if (Time.time > nextSpawnTimePoint && gameManager.isGameActive)
        {

            SpawnPoint();
            nextSpawnTimePoint = Time.time + spawnIntervalPoint;

        }
    }

    void SpawnObstacle()
    {
        
        //GameObject randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        
        
        
        foreach (GameObject obstacle in obstacles)
        {
            if (!obstacle.activeInHierarchy)
            {
                int randomLine = lines[Random.Range(0, lines.Length)];
                Vector3 spawnPos = new Vector3(randomLine * lineWidth, positionY, (player.position.z + spawnDistance));
                //Debug.Log(spawnPos);
                /*
                if (prevPositions.Contains(spawnPos))
                {
                    return;
                } */
                obstacle.SetActive(true);
                obstacle.transform.position = spawnPos;
                //prevPositions.Add(spawnPos);
                
                break;
            }
        }
        
    }



    void SpawnPoint()
    {
        int randomLine = lines[Random.Range(0, lines.Length)];
        int randomCount = Random.Range(4, 8);
        Vector3 spawnPos = new Vector3(randomLine * lineWidth, positionY, (player.position.z + spawnDistance));
        int k = 0;
        /*
         
        if (prevPositions.Contains(spawnPos) )
        {
            return;
        }
        */
                foreach (GameObject point in points)
        {

            if (!point.activeInHierarchy && k < randomCount )
            {
                spawnPos.z += 15;
                point.transform.position = spawnPos;
                point.SetActive(true);
                //prevPositions.Add(spawnPos);
                k++;
            }
        }
            
    }
}
