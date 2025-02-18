using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float moveSpeed = 50f;
    public float jumpForce = 80f;
    public float lineChangeSpeed = 50f;
    public float lineWidth = 50f;
    public float jumpheight = 20.0f;

    private int currentLine = 0; 

    private int jumpCount = 0;
    public int maxJumpCount = 2;

    public float gravity = 15f;

    public bool isOnGround;
    private bool falling = false;


    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        /*
        SphereCollider collider = GameObject.Find("SpherePlayer").GetComponent<SphereCollider>();
        BoxCollider groundcollider = GameObject.Find("Road (1)").GetComponent<BoxCollider>();
       
        Vector3 newPosition = groundcollider.transform.position;

        collider.transform.position = new Vector3(collider.transform.position.x,newPosition.y + 9,collider.transform.position.z);*/

    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(Time.deltaTime * 30 * Vector3.left);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
                jumpCount++;
                Debug.Log(jumpCount);
                isOnGround = false;
                StartCoroutine(Jump());

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLine > -1)
        {
                StartCoroutine(GoLeft());
;                
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLine < 1)
        {
                StartCoroutine(GoRight());
                
        }

        
        else if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y >9)
        {
            StartCoroutine(GoQuickDown());

        }
        





    }

    IEnumerator Jump()
    {
        float topIndex = Mathf.Max(transform.position.y + jumpheight, 50);
        while (transform.position.y < topIndex)
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        falling = true;
        while (transform.position.y > 8)
        {
            transform.Translate(Vector3.down * gravity * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        falling = false;

    }

    IEnumerator GoLeft()
    {
        float leftIndex;
        if (currentLine == 0)
        {
            leftIndex = -50;
            while (transform.position.x > leftIndex)
            {
                transform.Translate(Vector3.left * lineChangeSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            currentLine = -1;
        }
        else
        {
            leftIndex = 0;
            while (transform.position.x > leftIndex)
            {
                transform.Translate(Vector3.left * lineChangeSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            currentLine = 0;
        }

        

    }

    IEnumerator GoRight()
    {
        float rightIndex;
        if (currentLine == 0)
        {
            rightIndex = 50;
            while (transform.position.x < rightIndex)
            {
                transform.Translate(Vector3.right * lineChangeSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            currentLine = 1;
        }
        else
        {
            rightIndex = 0;
            while (transform.position.x < rightIndex)
            {
                transform.Translate(Vector3.right * lineChangeSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            currentLine = 0;
        }

        

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
            //Debug.Log("Is on ground");
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
            //Destroy(other.gameObject);

        }


    }


}
