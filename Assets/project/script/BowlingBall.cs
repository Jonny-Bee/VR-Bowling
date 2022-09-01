using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform respawnPoint;
    public AudioSource audio;
    public AudioSource audio2;
    public Rigidbody rb;
    bool returning = false;
    bool home = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.GetComponent<bowling>() != null)
        {
            c.gameObject.GetComponent<bowling>().thrown();
        }
        if(c.tag =="bin")
        {
            if(!returning)
            {
                returning = true;
                StartCoroutine(returnMe());
            }
            
        }
        if(c.tag =="ballhop")
        {
            home = true;
            
        }
    }
    void OnTriggerExit(Collider c){
        if(c.tag =="ballhop")
        {
            home = false;
            
        }
    }
    public void allReturn(){
        if(!returning && !home)
            {
                returning = true;
                StartCoroutine(returnMe());
            }
    }
    void returnBall(){
        transform.position = respawnPoint.position;
        rb.velocity = respawnPoint.transform.forward;
        returning = false;
    }
    void OnCollisionEnter(Collision c)
    {
       
            if(c.collider.tag == "ground")
            {
                float collisionForce = c.impulse.magnitude / Time.fixedDeltaTime;
                audio.volume = collisionForce * .1f ;
                audio.pitch = Mathf.Clamp(collisionForce * .001f,.01f,.2f);
                audio.Play();
                if(audio2.isPlaying)
                return;
                audio2.volume = rb.velocity.magnitude *4f;
                audio2.pitch = Mathf.Clamp(rb.velocity.magnitude,.5f,1.2f);
                audio2.Play();
            }
            else
            {
                
                
                audio.volume = .1f;
                audio.pitch = Mathf.Clamp(Random.Range(1.9f,2.5f),.1f,2.5f);
                audio.Play();
            }
    }
    IEnumerator returnMe(){
        yield return new WaitForSeconds(2f);
        returnBall();

    }
    void OnCollisionExit(Collision c)
    {
       
           
            if(c.collider.tag == "ground")
            {
                audio2.Stop();
            }
    }

    void OnCollisionStay(Collision c)
    {
        if(c.collider.tag == "ground")
        {
             audio2.volume = rb.velocity.magnitude *8f;
            audio2.pitch = Mathf.Clamp(rb.velocity.magnitude,.5f,1.2f);
        }
    }
}
