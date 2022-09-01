using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class frame
{
    public int number;
    public int[] scores;
    public int extraScore = 0;

    public bool strike = false;
    public bool spare = false;

    public int additions  = 0;
    public frame(int i)
    {
        number = i;
        scores = new int[3];
    }

    public int getTotal(){
        int c = 0;
        foreach( int n in scores)
        {
            c += n;
        }
        c += extraScore;
        return c;
    }

    public int getScore(int ball){
        return scores[ball];
    }

}
[System.Serializable]
public class bowlingplayer{
    public int id;
    public List<frame> frames;
    public List<frame> additions;

    
    
    public bowlingplayer(int idn){
        id = idn;
        frames = new List<frame>();
        additions = new List<frame>();
        for(int i = 0; i < 10 ; i++)
        {
            frames.Add(new frame(i));
        }
    }
    public void setScores(int frame,int ball,int score)
    {
        frames[frame].scores[ball] = score;
        foreach(frame fra in additions)
        {
            if(fra.additions > 0)
            {
                fra.additions --;
                fra.extraScore += score;
            }
            
        }
       
        if(ball == 0 && score == 10)
        {
            frames[frame].additions = 2;
            additions.Add(frames[frame]);
            
        }
        if(ball > 0 && frames[frame].scores[ball] + frames[frame].scores[ball-1] == 10 )
        {
             frames[frame].additions = 1;
            additions.Add(frames[frame]);
        }
       
    }
    public int totalToFrame(int fr){
        int c = 0;
        for(int i = 0; i <= fr ; i++)
        {
            frame f = frames[i];
            c += f.getTotal();
        }
        return c;
    }
    public frame getFrame(int fid){
            return frames[fid];
                }
    
}
public class bowling : MonoBehaviour
{
    public List<bowlingplayer> players;
    public int currentFrame;
    public int currentBall = 0;
    public int currentPlayer;
    public int totalPlayers = 0;

    public bool inPlay = false;
    public pin_Grabber machine;
    bool triggered = false;
    int pinsDown = 0;

    public scoreboard scoreb;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void beginGame(int n){
        players = new List<bowlingplayer>();
        for(int i=0;i<n;i++ )
        {
            bowlingplayer p = new bowlingplayer(i);
            players.Add(p);
        }
        totalPlayers = n;
        machine.rack();
        inPlay = true;
        currentFrame = 0;
        currentBall = 0;
        currentPlayer = 0;
        scoreb.createScoreCards(n);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void nextPlayer(){
        currentPlayer ++;
        if(currentPlayer >= players.Count)
        {
            currentPlayer = 0;
            currentFrame ++;
        }
        currentBall = 0;
        scoreb.showRound(currentFrame,currentPlayer);
    }
    public void readyup(){
        triggered = false;
    }
    public void thrown(){
        if(totalPlayers > 0)
            StartCoroutine(wait());
    }

    public void returnBalls(){
        StartCoroutine(returnBallLoop());
    }
    IEnumerator returnBallLoop(){
        BowlingBall[] balls = FindObjectsOfType<BowlingBall>();
        foreach(BowlingBall b in balls)
        {
             yield return new WaitForSeconds(2f);
            b.allReturn();
        }
    }
    IEnumerator wait(){
        
        yield return new WaitForSeconds(3f);
        postthrown();
    }
    public void postthrown(){
        if(triggered)
            return;
        triggered = true;
        int score = machine.checkpins();
        if(currentFrame < 9)
        {
            
            if(score == 10 || currentBall > 0)
            {
                players[currentPlayer].setScores(currentFrame,currentBall,score - pinsDown);
                nextPlayer();
                machine.rack();
                pinsDown = 0;
            }
            else
            {
                players[currentPlayer].setScores(currentFrame,currentBall,score - pinsDown);
                currentBall ++;
                machine.sweep();
                pinsDown = score;
            }
            

        }
        else
        {
            if(currentBall < 2)
            {
                if(score == 10 )
                {
                    players[currentPlayer].setScores(currentFrame,currentBall,score - pinsDown);
                    currentBall ++;
                    machine.rack();
                    pinsDown = 0;
                }
                else
                {
                    players[currentPlayer].setScores(currentFrame,currentBall,score - pinsDown);
                    if(currentBall == 1)
                    {
                        if(players[currentPlayer].getFrame(currentFrame).scores[0] == 10)
                        {
                            currentBall ++;
                            machine.sweep();
                            pinsDown = score;
                        }
                        else
                        {
                            nextPlayer();
                            machine.rack();
                            pinsDown = 0;

                        }
                        
                    }
                    else
                    {
                        currentBall ++;
                         machine.sweep();
                         pinsDown = score;
                    }
                }

            }
            else
            {
                players[currentPlayer].setScores(currentFrame,currentBall,score);
                nextPlayer();
                machine.rack();
            }
            
        }
        scoreb.updateScores(players);
    }
}
