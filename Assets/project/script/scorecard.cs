using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorecard : MonoBehaviour
{
    // Start is called before the first frame update
    public int playerid = 0;
    public int frameid = 0;

    public Text totalText;
    public Text ballone;
    public Text balltwo;
    public Text ballthree;
    public Image strike;
    public Image strike2;
    public Image strike3;
    public Image spare;
    public Image spare3;
    public Image bg;
    public Color color;
   
    void Start()
    {
        //color = bg.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScores(frame fr , int t,int currentframe,int currentplayer,List<frame> additions){
        if(currentframe  < frameid )
            return;
        if(currentframe == frameid && currentplayer < playerid)
            return;
        strike.gameObject.SetActive(false);
        spare.gameObject.SetActive(false);
        totalText.text = formatTotal(t,fr,additions,currentframe,currentplayer);
        int s = fr.getScore(0);
        
        int s2 = fr.getScore(1);
        int s3 = fr.getScore(2);
        if(s == 10)
        {
            strike.gameObject.SetActive(true);
            ballone.text = "";
            balltwo.text = "";
        }
        else if(s + s2 == 10)
        {
            spare.gameObject.SetActive(true);
            ballone.text = formatScore(s);
            balltwo.text = "";
        }
        else{
             ballone.text = formatScore(s);
            balltwo.text = formatScore(s2);
        }
        if(ballthree != null) // we're in final round
        {
            ballthree.text = formatScore(s3);
            if(s2 == 10 && s3 == 10)
            {
                strike3.gameObject.SetActive(true);
                ballthree.text = "";
            }
            else if(s2 + s3 == 10)
            {
                spare3.gameObject.SetActive(true);
                ballthree.text = "";
            }
            if(s == 10 && s2 == 10)
            {
                strike2.gameObject.SetActive(true);
                ballone.text = "";
                balltwo.text = "";
            }
        }
    }

    public void highlight(int currentframe , int currentplayer){
        if(currentframe == frameid && currentplayer == playerid)
            bg.color = Color.yellow;
        else
            bg.color = color;
        

    }

    string formatScore(int s){

        string r = "";
        r += s == 10 ? "" : s.ToString();
        return r;

    }

    string formatTotal(int s , frame fr,List<frame> additions,int currentframe, int currentplayer){
        if(frameid == currentframe && playerid >= currentplayer)
        {
            return "--";
        }
        if(fr.additions > 0)
            return "--";

        string r = "";
        r += s == 10 ? "" : s.ToString();
        return r;

    }
}
