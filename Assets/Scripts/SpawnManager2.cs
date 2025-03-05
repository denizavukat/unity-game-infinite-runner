using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2 : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject pointObject;


    public float spawnDistance = 100f;

    public float spawnInterval = 3f;
    public float spawnIntervalPoint = 5f;

    private float nextSpawnTime = 0f;
    private float nextSpawnTimePoint = 0f;


    private List<GameObject> obstacles = new List<GameObject>();
    private List<GameObject> points = new List<GameObject>();
    public float[] positions;
    private float positionY;

    private GameManager gameManager;
    private PlayerController playerController;

    void Start()
    {


        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("SpherePlayer").GetComponent<PlayerController>();
        positionY = playerController.transform.position.y;
        positions = new float[playerController.maxLineCount];

   


        for (int i = 0; i < 10; i++)
        {
            GameObject randomObstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
            randomObstacle.SetActive(false);
            obstacles.Add(randomObstacle);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject point = Instantiate(pointObject);
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



        foreach (GameObject obstacle in obstacles)
        {
            if (!obstacle.activeInHierarchy)
            {
                float additionalDistance = 0;
                int randomLine = Random.Range(0, playerController.maxLineCount);
                additionalDistance += (15 * (positions[randomLine]+1));


                Vector3 spawnPos = new Vector3(playerController.startingLineXPosition + randomLine * playerController.lineWidth, positionY, (playerController.transform.position.z + spawnDistance+ additionalDistance));


                obstacle.SetActive(true);
                obstacle.transform.position = spawnPos;

                positions[randomLine]++;
                Debug.Log($"Line {randomLine} üzerinde Obstacle oluşturuldu");

                break;
            }
        }

    }



    void SpawnPoint()
    {
        Debug.Log("Point spawnlandı.");
        int randomLine = Random.Range(0, playerController.maxLineCount);
        int randomCount = Random.Range(4, 8);
        float additionalDistance = 0;

        additionalDistance += (15 * (positions[randomLine] + 1));
        Vector3 spawnPos = new Vector3(playerController.startingLineXPosition + randomLine * playerController.lineWidth, positionY, (playerController.transform.position.z + spawnDistance+ additionalDistance));

        int k = 0;
        //positions[randomLine] += randomCount;


        Debug.Log($"Line {randomLine} üzerinde {randomCount} Point oluşturuldu");

        foreach (GameObject point in points)
        {


            if (!point.activeInHierarchy && k < randomCount)
            {
                spawnPos.z += 15;
                point.transform.position = spawnPos;
                point.SetActive(true);
                positions[randomLine]++;
                //positions[randomLine] = point.transform.position.z;
                k++;
            }
        }

    }
}
