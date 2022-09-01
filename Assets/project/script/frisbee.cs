using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frisbee : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(-transform.forward * rb.angularVelocity.z *.1f);
    }
}
