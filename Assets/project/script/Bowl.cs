using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public Vector3 centerOfMass;
    Rigidbody rb;
    bool bThrown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void align(){
        
        bThrown = true;
       
    }
    public void OnCollisionEnter(Collision c){
        if(c.collider.tag == "ground" && bThrown){
            Debug.Log("align");
            bThrown = false;
            Vector3 v = Vector3.up;
            v.x = rb.velocity.x;
            v.z = rb.velocity.z;
            Quaternion q = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
            transform.rotation = q;
            transform.LookAt(transform.position - rb.velocity);
        }
    }
}
