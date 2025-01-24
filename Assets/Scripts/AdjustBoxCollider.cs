using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBoxCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("No BoxCollider found on the GameObject!");
            return;
        }

        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("No Renderer found on the GameObject or its children!");
            return;
        }

        Bounds bounds = renderer.bounds;

        // Convert bounds to local space
        Vector3 localSize = transform.InverseTransformVector(bounds.size);
        Vector3 localCenter = transform.InverseTransformPoint(bounds.center) - transform.localPosition;


        Vector3 size = bounds.size; // Size of the bounding box
        Vector3 center = transform.InverseTransformPoint(bounds.center);


        //localCenter -= transform.localPosition;
        //boxCollider.size = localSize;
        //boxCollider.center = localCenter;

        Debug.Log("BoxCollider adjusted to fit the mesh bounds!");

        Debug.Log("Sizes" + localSize);
        Debug.Log("Center" + localCenter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

