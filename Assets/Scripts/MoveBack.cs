using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float speed = 30.0f;
    private float bound = -500.0f;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        }
    }
}
