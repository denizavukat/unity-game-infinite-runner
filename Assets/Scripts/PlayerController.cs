using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float moveSpeed = 50f;
    public float jumpForce = 200f;
    public float lineChangeSpeed = 70f;

    private int jumpCount = 0;
    public int maxJumpCount = 2;

    public float gravityModifier;

    //private bool isOnGround = true;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        BoxCollider collider = GameObject.Find("Player").GetComponent<BoxCollider>();
        BoxCollider groundcollider = GameObject.Find("Ground").GetComponent<BoxCollider>();

        Vector3 newPosition = groundcollider.transform.position;
        collider.transform.position = new Vector3(collider.transform.position.x,
            newPosition.y, collider.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount < maxJumpCount)
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            jumpCount++;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !gameOver)
        {
            transform.Translate(Vector3.left * lineChangeSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !gameOver)
        {
            transform.Translate(Vector3.right * lineChangeSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isOnGround = true;
            jumpCount = 0;
        }
        /*
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game OVER");
            
        }*/


    }


}
