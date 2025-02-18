using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRoad : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z /2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < startPos.z - 100)
        {
            transform.position = startPos;
        }
    }
}


/*
 * Objeye çarpılan yere göre game over
 * 
 * 
 * 
 * 
 * 
 * 
 */