using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class pin : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public Vector3 centerOfMass;

    Vector3 basePosition;
    Vector3 liftedPosition;

    Quaternion baseRotation;
    AudioSource audio;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        basePosition = transform.position;
        liftedPosition = basePosition + (Vector3.up * 1.5f);
        baseRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.WakeUp();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool shouldMove(){
        return rb.isKinematic;
    }
    public void startLift(){
        rb.isKinematic = true;
    }
    public void preRack(){
        rb.isKinematic = true;
        transform.position = liftedPosition;
        transform.rotation = baseRotation;
    }
    public void endLift(){
        transform.position = basePosition;
        transform.rotation = baseRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = false;
    }

    public bool isDown(){
       
        if(Mathf.Abs(Vector3.Dot(transform.up,Vector3.up)) < .8f )
        {
            rb.WakeUp();
            return true;
        }
        
        return false;
    }
    void OnCollisionEnter(Collision c)
    {
        if(audio.isPlaying)
            return;
        float collisionForce = c.impulse.magnitude / Time.fixedDeltaTime;
        audio.volume = collisionForce * .06f ;
        audio.pitch = Mathf.Clamp(collisionForce * .005f,.1f,1.2f);
        audio.Play();
    }
}
