using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class screens : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] UIscreens;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchScreen(int a){
        foreach(GameObject g in UIscreens)
        {
            g.SetActive(false);
        }
        if(a < UIscreens.Length)
            UIscreens[a].SetActive(true);
    }
}
