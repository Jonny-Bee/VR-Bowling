using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreboard : MonoBehaviour
{
    // Start is called before the first frame update
    public List<playerPanel> scorecards;
    public Transform panelPrefab;
    public bowling game;
    void Start()
    {
        
    }
    public void createScoreCards(int players){
        if(scorecards != null)
        {
            foreach(playerPanel g in scorecards)
            {
                Destroy(g.gameObject);
            }
        }
        scorecards = new List<playerPanel>();
        Vector3 p = new Vector3(0f,0f,16f);
        for(int i = 0; i < players; i++)
        {
            Transform pn = Instantiate(panelPrefab,p,Quaternion.identity,transform);
            pn.GetComponent<playerPanel>().setPlayerId(i);
            scorecards.Add(pn.GetComponent<playerPanel>());
            pn.GetComponent<RectTransform>().localPosition = p;
            pn.GetComponent<RectTransform>().localRotation = Quaternion.identity;
            p.y -= 240;
        }
       showRound(0,0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void showRound(int fr, int pl){
        foreach(playerPanel c in scorecards)
            {
                
                c.showRound(fr,pl);
                

            }
    }
    public void updateScores(List<bowlingplayer> players){
        foreach(bowlingplayer p in players){
            foreach(playerPanel c in scorecards)
            {
                if(c.playerId == p.id)
                {
                    c.updateScores(p,game.currentFrame,game.currentPlayer);
                }

            }
        }
        
    }
}
