using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

///If you make a new game, make sure it has a unique name because you can Search/Replace every instance of the word "Sample" with your
///game name, then don't have to even think anything beyond game logic and score balancing, this will do the rest.
public class SampleRewardManager : MonoBehaviour
{
  
    //Assigned by Game logic on gameover
    public float EndLevelScore;

    //Each continue costs one more token, resets if you start over ()
    public int costToContinue;


    //Player Info.
    public float PlayerXP;
    public float NewPlayerXP;
    public Text PlayerName;
    public Text ContinuePrice;
    public Text TotalPointsText;
    public Text TokenText;
    public GameObject XPSkillObject;
    public Text XPSkillText;
    public Image XPProgress;
    public Text PlayerLevelText;

    //Skill Info
    public float SampleSP;
    public float SampleFatigue;
    private float newSampleSP;
    public Text SampleSkillText;
    public GameObject SampleSkillObject;
    public Text SampleLevelText;
    public Image SampleProgress;

    //Skill Serialized Objects
    public ThePrimerSkill SampleSkillObj;

    //Challenges
    public GameObject Challenges;

    //Hierarchy References
    public GameObject MenuObj; //The Menu 'scene'
    public GameObject GameplayObj; //The Game ''scene'
    public GameObject RewardsObj; // The Rewards 'Scene'
    public GameObject GameSprites; // This is all the game objects that aren't UI related
    public SampleGameManager Rules; //Gameplay Logic



    //Wait until everything loads before letting player continue or start over
    public float canPressTimer;

    //Buttons
    public Button newGameButton;
    public Button continueButton;


    //Re-activates the continue/New Game buttons once a timer has elapsed.
    private void Update()
    {
        if (canPressTimer > 0) canPressTimer -= Time.deltaTime;
        else
        {
            continueButton.interactable = true;
            newGameButton.interactable = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
             
                    canPressTimer = 3;
                    newGameButton.interactable = false;
                    StartCoroutine(KeyboardButtons("NewGame"));
             
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
               
                    canPressTimer = 3;
                    continueButton.interactable = false;
                    StartCoroutine(KeyboardButtons("Continue"));
             
            }
        }
    }


    //When you press a Enter or Escape, it does this. I have no idea why it does this...
    IEnumerator KeyboardButtons(string name)
    {
        yield return new WaitForSeconds(.15f);
        if(name == "Continue")
        {
            Continue();
   
        }
        else if (name == "NewGame")
        {
            NewGame();
            MenuObj.SetActive(true);
            RewardsObj.SetActive(false);
        }
    }
    
    //Makes sure the continue price starts at 1 when the game is loaded. 
    private void Awake()
    {
        costToContinue = 1;
    }
    

    //Assigns local variables based on saved data, disables continue/New Game buttons until calculations are complete
    void OnEnable()
    {
        continueButton.interactable = false;
        newGameButton.interactable = false;

        canPressTimer = .3f;

        SampleSP = SampleSkillObj.skillPoints;
        SampleFatigue = SampleSkillObj.skillFatigue;
        
        SampleProgress.fillAmount = 0;

        PlayerLevelText.text = ScoreSingleton.Instance.lvl.ToString();

        TokenText.text = ScoreSingleton.Instance.tokens.ToString();

        PlayerName.text = ScoreSingleton.Instance.username;


        CalculateRewards();
    }

    //Calculates final scores, turns on the Skill Level UI, sets skills, checks challenges, assigns continue price
    public void CalculateRewards()
    {
        //Assign the score and make sure that the end level is at least 1. 
        EndLevelScore = Mathf.Round( EndLevelScore /2);
        if (EndLevelScore < 0)
            EndLevelScore = 0;


        //HideSkills and set them to zero
        ResetSkillBar();

        //Display the number of points earned total, then disappear
        TotalPointsText.gameObject.SetActive(true);
        if (EndLevelScore == 0)
            TotalPointsText.text = "Try Again";
        else if (EndLevelScore == 1)
            TotalPointsText.text = EndLevelScore.ToString() + " pt";
        else TotalPointsText.text = EndLevelScore.ToString() + " pts";

        //Assign scores to the player profile
        AssignScores(EndLevelScore);

        //Activate all the skill objects
        SetSkillBar();


        //Change the Cost 
        if (costToContinue == 1 || costToContinue == 0) ContinuePrice.text = "";
        else
            ContinuePrice.text = "x" + costToContinue.ToString();

      
    

    }

    //Resets the skill game objects for next time
    void ResetSkillBar()
    {
        newSampleSP = 0;
        XPSkillText.text = "";
        SampleSkillObject.SetActive(false);
        XPSkillObject.SetActive(false);

    }


    //Adjusts skill points based on player fatigue in that skill and assigns XP earned
    void AssignScores(float score)
    {
        NewPlayerXP = 0;

        if (score <= 1) score = 1;

        //Make sure no fatigues are less than 1;
        CheckFatigues();

        //Create the new scores to be added to the player SP/XP scores.
        newSampleSP = Mathf.Floor(score / SampleSkillObj.skillFatigue);


        //Assign SP to the player profile
        SampleSkillObj.skillPoints += Mathf.RoundToInt(newSampleSP);

    
        //Assign XP based on all the SP earned this round
        NewPlayerXP = newSampleSP;

        //Animate XP bars
        if (NewPlayerXP < 1)
            NewPlayerXP = 1;
     
       
    }
    
    //Making sure there wasn't an accident and the fatigue is less than one.
    //It's bad if that happens
    void CheckFatigues()
    {

        if (SampleFatigue < 1) SampleFatigue = 1;
    }

    //Animate SP bars
    void SetSkillBar()
    {
        if (newSampleSP > 0)
        {
            SampleSkillObj.skillFatigue += 0.01f;
            SampleSkillText.text = "+" + newSampleSP.ToString();
            SampleSkillObject.SetActive(true);
            SampleLevelText.text = SampleSkillObj.skillLevel.ToString();          
            StartCoroutine(AnimateFillBar(SampleSkillObj.skillPoints, SampleSkillObj.skillLevel, SampleLevelText, SampleProgress));
            StartCoroutine(AnimateNewSP(SampleSkillText));
        
        }


        float a = SampleSkillObj.skillPoints;
        float b = pointsToNextLevel(SampleSkillObj.skillLevel);

        if (SampleSkillObj.skillLevel < 10)
        {
            while (a >= b)
            {
                SampleSkillObj.skillPoints -= (int)b;       
                SampleSkillObj.skillLevel++;
                    if (SampleSkillObj.skillLevel > 10) SampleSkillObj.skillLevel = 10;
                    a = SampleSkillObj.skillPoints;
                 b = pointsToNextLevel(SampleSkillObj.skillLevel);
            }
        }
        else
        {
            if (a >= b)
            {
                a = b;
                SampleSkillObj.skillPoints = (int)b;
            }
        }

        XPSkillObject.SetActive(true);
        XPSkillText.text = "+" + NewPlayerXP.ToString();

        ScoreSingleton.Instance.xp += Mathf.RoundToInt(NewPlayerXP);

        StartCoroutine(AnimateFillBar(ScoreSingleton.Instance.xp, ScoreSingleton.Instance.lvl, PlayerLevelText, XPProgress));

        StartCoroutine(AnimateNewSP(XPSkillText));

        float e = ScoreSingleton.Instance.xp;
        float f = pointsToNextLevel(ScoreSingleton.Instance.lvl);

        while (e >= f)
        {
            ScoreSingleton.Instance.xp -= (int)f;
            ScoreSingleton.Instance.lvl++;
            if (ScoreSingleton.Instance.lvl > 10) ScoreSingleton.Instance.lvl = 10;
          
            //Leveled up, tell the website
            e = ScoreSingleton.Instance.xp;
            f = pointsToNextLevel(ScoreSingleton.Instance.lvl);
        }

     
    
    }


    //Called by the New Game Button
    public void NewGame()
    {
        costToContinue = 1;
        ResetSkillBar();
    }

    //Called by Continue Button
    public void Continue()
    {
        if (ScoreSingleton.Instance.tokens >= costToContinue)
        {
            ScoreSingleton.Instance.tokens -= costToContinue;
            costToContinue++;

            ContinuePrice.text = costToContinue.ToString();
            TokenText.text = costToContinue.ToString();
            GameplayObj.SetActive(true);
            GameSprites.SetActive(true);
            RewardsObj.SetActive(false);
            Rules.Continue();


            
            ResetSkillBar();
        }
        else
        {
            Debug.Log("Out of tokens!");
        }
    }

    //Animate the radial progress bar based on % completed
    IEnumerator AnimateFillBar(float currentAmount, int skillLevel, Text skillLevelText, Image fillBar)
    {

        float finalAmount = pointsToNextLevel(skillLevel);
        float tempAmount = 0;
        if (currentAmount > finalAmount && skillLevel >= 10)
            currentAmount = finalAmount;

        while (tempAmount < currentAmount)
        {

            yield return new WaitForSeconds(.05f);
            fillBar.fillAmount = tempAmount / finalAmount;
            if (fillBar.fillAmount == 1) break;
            tempAmount++;
            if (finalAmount > 75 && tempAmount < finalAmount) tempAmount++;
            if (finalAmount > 100 && tempAmount < finalAmount) tempAmount++;
            if (finalAmount > 200 && tempAmount < finalAmount) tempAmount++;
            if (finalAmount > 300 && tempAmount < finalAmount) tempAmount++;
            if (finalAmount > 400 && tempAmount < finalAmount) tempAmount++;
        }

        if (currentAmount >= finalAmount)
        {
            //run this again, but with the new information;
          
            //Needs a "LEVEL UP!" alert
         
            skillLevel++;
            if (skillLevel > 10) skillLevel = 10;
            skillLevelText.text = skillLevel.ToString();
            float newSP = currentAmount - finalAmount;
            StartCoroutine(AnimateFillBar(newSP, skillLevel, skillLevelText, fillBar));

        }

        if (fillBar.fillAmount == 1 && skillLevel < 10) 
        { 
            fillBar.fillAmount = 0;
        }
        else if (fillBar.fillAmount == 1 && skillLevel >= 10) 
        { 
            fillBar.fillAmount = 1;
        }
      
        if (fillBar.fillAmount > 0.99f) 
        { 
            fillBar.fillAmount = 1;
        }
    }
    
    //Pops up a little "points earned" text as the bar fills, then goes away.     
    IEnumerator AnimateNewSP(Text spText)
    {

        Color curColor = spText.color;

        curColor = new Color(curColor.r, curColor.g, curColor.b, 1);
        spText.color = curColor;

        yield return new WaitForSeconds(2.5f);
        while (curColor.a > 0)
        {
            float alphaBase = curColor.a -= 0.07f;
            curColor = new Color(curColor.r, curColor.g, curColor.b, alphaBase);
            spText.color = curColor;
            yield return new WaitForSeconds(.1f);
        }
    }

     

    int pointsToNextLevel(int level)
    {

        if (level <= 1) return 25;
        else if (level == 2) return 50;
        else if (level == 3) return 75;
        else if (level == 4) return 100;
        else if (level == 5) return 150;
        else if (level == 6) return 200;
        else if (level == 7) return 300;
        else if (level == 8) return 400;
        else if (level == 9) return 500;
        else if (level >= 10) return 1000;

        else return 100;
    }
}
