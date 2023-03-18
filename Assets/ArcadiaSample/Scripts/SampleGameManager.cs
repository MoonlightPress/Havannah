using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleGameManager : MonoBehaviour
{

    public static SampleGameManager instance;

  public   int level = 1;//current level
    public  int score;

    public int hearts = 3;

    public Image Heart01;
    public Image Heart02;
    public Image Heart03;
    public Text ScoreText;
    bool hasLost = false;


    int scoreToBonusLife = 10000;
    static int bonusScore;

    public GameObject Menu;
    public GameObject Gameplay;
    public GameObject ScoreScreen;
    public GameObject GampeplaySprites;
    public SampleHearts Healthbar;
    public SamplePlayerMovement player;

    public SampleRewardManager Rewards;
 
     

 
    public void GameOver()
    {
        Rewards.EndLevelScore = score;
        score = 0;

       
        Rewards.gameObject.SetActive(true);
        Gameplay.SetActive(false);
        GampeplaySprites.SetActive(false);
        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);

   
    }

    public void Continue()
    {
       // player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
       // player.gameObject.SetActive(true);
        score = 0;
        hearts = 3;
        player.health = 3;
        bonusScore = 0;
        ScoreText.text = "";
        Healthbar.SetHearts(hearts);
    }




    public void NewGame()
    {
       // player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);  
             
        ScoreText.text = "";
        level = 1;
        score = 0;
        hearts = 3;
        bonusScore = 0;
        Healthbar.SetHearts(hearts);

    }
    public void Win()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    public void Lose()
    {
        hearts--;
        Healthbar.SetHearts(hearts);

        if (hearts <= 0)
        {
            //die
            GameOver();
        }
    }

      

    
    public void resetHearts()
    {
        if (player.health >= 3)
        { Heart01.enabled = true;
            Heart02.enabled = true;
            Heart03.enabled = true;
        }
        else if (player.health == 2)
        {
            Heart01.enabled = true;
            Heart02.enabled = true;
            Heart03.enabled = false;
        }
        else if (player.health == 1)
        {
            Heart01.enabled = true;
            Heart02.enabled = false;
            Heart03.enabled = false;
        }
        else if (player.health <= 0)
        {
            Heart01.enabled = false;
            Heart02.enabled = false;
            Heart03.enabled = false;
        }
    }

    private void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Escape))
        {        
            score = 0;      
            Menu.SetActive(true);
            Gameplay.SetActive(false);
            GampeplaySprites.SetActive(false);
        }
    }

   


}
