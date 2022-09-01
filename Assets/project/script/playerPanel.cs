using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public List<scorecard> scorecards;
    public Text playerName;

    public int playerId;

    void Start()
    {
       
        
    }

    public void setPlayerId(int playerid){
        playerId = playerid;
        playerName.text = "Player " + (playerId + 1);
         foreach(scorecard c in scorecards)
        {
                
                c.playerid = playerId;
                
        }
    }
    public void showRound(int currentFrame, int currentPlayer){
        foreach(scorecard c in scorecards)
        {
            
            c.highlight(currentFrame,currentPlayer);
            

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateScores(bowlingplayer p, int currentFrame,int currentPlayer){
        
            foreach(scorecard c in scorecards)
            {
                
                c.updateScores(p.getFrame(c.frameid),p.totalToFrame(c.frameid),currentFrame,currentPlayer,p.additions);
                

            
        }
        
    }
}
