using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin_Grabber : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pins;
    public Transform grabPoint;
    bool grabbing = false;
    bool sweeping = false;
    bool presweeping = false;
    bool triggered = false;
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbing)
        {
            foreach(GameObject g in pins)
            {
                if(g.GetComponent<pin>().shouldMove())
                {
                     Vector3 p = g.transform.position;
                     p.y = grabPoint.position.y;
                     g.transform.position = p;
                }
            }

        }
        if(sweeping)
        {
            foreach(GameObject g in pins)
            {
                g.GetComponent<Rigidbody>().WakeUp();
            }
        }
        if(presweeping)
        {
            foreach(GameObject g in pins)
            {
                g.GetComponent<Rigidbody>().WakeUp();
            }
        }
        
    }
    public void setPins(){
        foreach(GameObject g in pins)
        {
            
            g.GetComponent<pin>().preRack();

            
        }
        sweeping = false;
        presweeping = false;
       grabbing = true;
    }
    public void grabPins(){
        foreach(GameObject g in pins)
        {
            if(!g.GetComponent<pin>().isDown())
            {
                g.GetComponent<pin>().startLift();

            }
        }
        
       grabbing = true;
    }
    public int checkpins(){
        int c = 0;
        foreach(GameObject g in pins)
        {
            if(g.GetComponent<pin>().isDown())
            {
                c++;
            }
        }
        Debug.Log(c);
        return c;
    }
    public void dropPins(){
        grabbing = false;
        sweeping = false;
        presweeping = false;
        triggered = false;
        foreach(GameObject g in pins)
        {
            if(!g.GetComponent<pin>().isDown())
            {
                g.GetComponent<pin>().endLift();
            }
        }
    }

    
    public void sweep(){
        triggered = true;
        anim.SetTrigger("Go");
        sweeping = true;

    }
    public void rack(){
        triggered = true;
        anim.SetTrigger("Rack");
        presweeping = true;
    }
}
