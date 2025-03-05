using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





public class PlayerController : MonoBehaviour
{
 

    public int maxLineCount = 3;
    public float startingLineXPosition = -50;
    public float lineChangeSpeed = 200f;
    public float jumpForce = 80f;
    public float lineWidth = 50f;
    public float jumpheight = 40.0f;
    public float maxHeight = 60.0f;

    private int currentLine = 0; 

    private int jumpCount = 0;
    public int maxJumpCount = 2;

    public float gravity = 15f;

    public bool isOnGround;
    public bool isJumping ;
    private bool falling = false;

    Vector3 initialPlayerPositions;

    private GameManager gameManager;
    private SpawnManager spawnManager;

    void Start()
    {
        

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        
        SphereCollider collider = GameObject.Find("SpherePlayer").GetComponent<SphereCollider>();
        BoxCollider groundcollider = GameObject.Find("Road (1)").GetComponent<BoxCollider>();
       
        Vector3 newPosition = groundcollider.transform.position;

        collider.transform.position = new Vector3(collider.transform.position.x,newPosition.y + 9,collider.transform.position.z);
        initialPlayerPositions = transform.position;
    }

    void Update()
    {

        MoveSideways();

        if (!isOnGround)
        {
            Fall();
        }

        if (isJumping)
        {
            Jump2();
        }





        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            isJumping = true;

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLine > 0)
        {
            
            currentLine--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLine < maxLineCount-1)
        {
            
            currentLine++;

        }


        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > 9)
        {
            StartCoroutine(GoQuickDown());

        }






    }

    private void MoveSideways()
    {
     
        Vector3 desiredPosition = new Vector3(startingLineXPosition + currentLine * lineWidth
                    , transform.position.y, transform.position.z);

        Vector3 currentPosition = transform.position;
        Vector3 differenceVector = desiredPosition - currentPosition;


        if (differenceVector.magnitude < 0.01)
        {
            transform.position = desiredPosition;
            return;
        }


        Vector3 directionVector = differenceVector.normalized;
        transform.position += directionVector * lineChangeSpeed * Time.deltaTime;

    }

    private void Jump2()
    {
        if (jumpCount > 0 && jumpCount <= maxJumpCount)
        {
            Vector3 desiredPosition = new Vector3(transform.position.x, Math.Min(initialPlayerPositions.y + jumpheight * (jumpCount), maxHeight), transform.position.z);
            Vector3 currentPosition = transform.position;
            Vector3 differenceVector = desiredPosition - currentPosition;
            if (differenceVector.magnitude < 0.01)
            {
                transform.position = desiredPosition;
                
                    isJumping = false;
                
                
                return;
            }


            Vector3 directionVector = differenceVector.normalized;
            transform.position += directionVector * jumpForce * Time.deltaTime;
        }
        
    }

    private void Fall()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, initialPlayerPositions.y, transform.position.z);
        Vector3 currentPosition = transform.position;
        Vector3 differenceVector = desiredPosition - currentPosition;
        if (differenceVector.magnitude < 0.01 && isOnGround)
        {
            transform.position = desiredPosition;            
            jumpCount = 0;

            return;
        }
        Vector3 directionVector = differenceVector.normalized;
        transform.position += directionVector * gravity * Time.deltaTime;

    }

    

    

    IEnumerator GoQuickDown()
    {
        float downIndexY = 9f;
        while (transform.position.y > downIndexY && falling)
        {
            transform.Translate(Vector3.down * jumpForce*2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        falling = false;

    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
            isJumping = false;
            jumpCount = 0;
        }
       
        if (other.CompareTag("Obstacle"))

        {
            Vector3 boundries = other.bounds.center;

            Debug.Log(boundries);
            Vector3 directionVector = (other.transform.position - transform.position).normalized;
            
            if(directionVector.y > 0.7 || directionVector.x > 0.7 || directionVector.x < -0.7 || directionVector.z > 0.7)
            {
                gameManager.GameOver();
                Debug.Log("Game OVER");
                return;
            }

            
            
        }
        if (other.CompareTag("Point") && gameManager.isGameActive)
        {
            gameManager.UpdateScore();
            
            int lineIndex = Mathf.RoundToInt((transform.position.x - startingLineXPosition) / lineWidth);
     
            other.gameObject.SetActive(false);


        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
           
        }
    }


}
