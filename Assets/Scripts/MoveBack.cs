using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    private float speed = 40.0f;
    private float bound = -500.0f;

    private GameManager gameManager;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);

        }
        if (transform.position.z < bound && (gameObject.CompareTag("Obstacle") || gameObject.CompareTag("Point")))
        {
            //Debug.Log("Deleted obstacle");
            //Destroy(gameObject);
            //Debug.Log($"Deactivating {gameObject.name} at {transform.position.z}");
            gameObject.SetActive(false);
            //spawnManager.prevPositions.Remove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z +200));
            //Debug.Log(spawnManager.prevPositions.Count);
        }
    }
}
