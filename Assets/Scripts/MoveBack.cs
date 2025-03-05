using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private float speed = 40.0f;
    private float bound = -500.0f;

    private GameManager gameManager;
    private SpawnManager spawnManager;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("SpherePlayer").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            
            //spawnManager.positions[0]-= Time.deltaTime * speed;
            //spawnManager.positions[1] -= Time.deltaTime * speed;
            //spawnManager.positions[2] -= Time.deltaTime * speed;
            

        }
        if (transform.position.z < bound && (gameObject.CompareTag("Obstacle") || gameObject.CompareTag("Point")))
        {
            
            
            //int lineIndex = Mathf.RoundToInt((transform.position.x - playerController.startingLineXPosition) / playerController.lineWidth);
            Debug.Log($"Z Positions: [ {spawnManager.positions[0]} ,{spawnManager.positions[1]} ,{spawnManager.positions[2]} ]");
            //spawnManager.positions[lineIndex]--;
            /*
            if (spawnManager.positions.ContainsKey(lineIndex))
            {
                spawnManager.positions[lineIndex]--;
                Debug.Log($"Line {lineIndex} üzerinde {spawnManager.positions[lineIndex]} obje kaldı");
                //Debug.Log(spawnManager.positions[lineIndex]);
                //= Mathf.Max(0, spawnManager.positions[lineIndex] - 1);
            }*/
            gameObject.SetActive(false);
        }
    }
}
