using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public int maxLineCount = 3;
    public float startingLineXPosition = -50;
    public float moveSpeed = 50f;
    public float jumpForce = 80f;
    public float lineChangeSpeed = 50f;
    public float lineWidth = 50f;
    public float jumpheight = 20.0f;

    private int currentLine = 0; 

    private int jumpCount = 0;
    public int maxJumpCount = 1;

    public float gravity = 15f;

    public bool isOnGround;
    public bool isJumping ;
    private bool falling = false;

    Vector3 initialPlayerPositions;
    private GameManager gameManager;
    private SpawnManager spawnManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

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
        if (isJumping)
        {
            Jump2();
        }
        else if(!isOnGround)
        {
            Fall();
        }
       
           
        


        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            isJumping = true;
            //isOnGround = false;
            Debug.Log(jumpCount);


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
        transform.position += directionVector * moveSpeed * Time.deltaTime;

    }

    private void Jump2()
    {
        
        Vector3 desiredPosition = new Vector3(transform.position.x, initialPlayerPositions.y + jumpheight, transform.position.z);
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

    private void Fall()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, initialPlayerPositions.y, transform.position.z);
        Vector3 currentPosition = transform.position;
        Vector3 differenceVector = desiredPosition - currentPosition;
        Debug.Log(differenceVector.magnitude);
        if (differenceVector.magnitude < 0.001 && isOnGround)
        {
            transform.position = desiredPosition;
            Debug.Log("aşağı indi");
            
            jumpCount = 0;
            return;
        }
        Vector3 directionVector = differenceVector.normalized;
        transform.position += directionVector * 300 * Time.deltaTime;

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
            //gameManager.isGameActive = false;
            gameManager.GameOver();
            Debug.Log("Game OVER");
            
        }
        if (other.CompareTag("Point") && gameManager.isGameActive)
        {
            gameManager.UpdateScore();
            other.gameObject.SetActive(false);
            //spawnManager.prevPositions.Remove(other.transform.position);
            //spawnManager.prevPositions.Remove(new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 200));

            //Debug.Log(spawnManager.prevPositions.Count);
            //Destroy(other.gameObject);

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
            //Debug.Log("Is on ground");
        }
    }


}
