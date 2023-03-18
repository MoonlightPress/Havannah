using UnityEngine;
using System.Collections;

using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class ChallengeManager : MonoBehaviour
{
 

    public List<ArcadiaChallenges> activeChallenges = new List<ArcadiaChallenges>();
    public int selectedChallenge = 0;
    public bool active = false;
    public Text challengeText;

    public GameObject RewardPanel;

    public GameObject PlayerInfoObj;
    public Text playerLevelText;
    public Text playerNewXPText;
    public Image playerXPBar;

    public GameObject SkillInfoObj;
    public GameObject TokenInfoObj;
    public Text newTokenText;

    public List<GameObject> activeSkillObj = new List<GameObject>();
    public List<ThePrimerSkill> activeSkills = new List<ThePrimerSkill>();
    public List<Text> activeSkillsNewSPText = new List<Text>();
    public List<Image> activeSkillsImage = new List<Image>();
    public List<Image> activeSkillsBGImage = new List<Image>();
    public ThePrimerSkillLibrary library;
    public Button SaveButton;

    private void OnEnable()
    {
        SaveButton.interactable = true;
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (this.transform.gameObject.active)
            {
               
       
            }

        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
        {
            //if (this.transform.gameObject.active)
            //{

                SaveButton.interactable = true;
            //}

        }

    }

    void SetUpScreen()
    {
     
    }

    public void AddChallenge(ArcadiaChallenges challenge)
    {
       // Debug.Log("AddChallenge");
        activeChallenges.Add(challenge);
        if (!active) ActivateChallengeScreen();
    }

    void ActivateChallengeScreen()
    {
       // SaveButton.interactable = true;
        this.gameObject.SetActive(true);
        for (int i = 0; i < activeChallenges.Count; i++)
        {
            if (activeChallenges[i].hasCollected == true)
                activeChallenges.Remove(activeChallenges[i]);
        }

        if (activeChallenges.Count == 0)
        {
            this.gameObject.SetActive(false); }

        else {

            for (int i = 0; i < activeChallenges.Count; i++)
            {

                if (activeChallenges[i].hasCollected == false)
                {

                    selectedChallenge = i;
                    challengeText.text = activeChallenges[selectedChallenge].challengeDescription;
                    activeSkills.Clear();
                    for (int j = 0; j < activeChallenges[selectedChallenge].Skills.Length; j++)
                        {
                            activeSkills.Add(activeChallenges[selectedChallenge].Skills[j]);
                        }
                
                    for (int j = 0; j < activeSkills.Count; j++)
                        {
                        activeSkillsImage[j].sprite = activeSkills[j].skillIcon;
                        activeSkillsBGImage[j].color = activeSkills[j].skillColor;
                        activeSkillsNewSPText[j].text = "+"+activeChallenges[selectedChallenge].spRewardAmount.ToString();
                        }
                    for (int j = 0; j < 4; j++)
                    {
                        if (activeSkills.Count > j)
                            activeSkillObj[j].SetActive(true);
                        else
                            activeSkillObj[j].SetActive(false);
                    }

                    if (activeChallenges[selectedChallenge].tokenRewardAmount > 0) newTokenText.text = "+" + activeChallenges[selectedChallenge].tokenRewardAmount;
                    if (activeChallenges[selectedChallenge].xpRewardAmount > 0) playerNewXPText.text = "+" + activeChallenges[selectedChallenge].xpRewardAmount;


                    break;
                        }
            }

        }

       
         
      

    }

  


   
    

    void AddPoints()
    {
        for (int i = 0; i < activeChallenges[selectedChallenge].Skills.Length; i++)
        {
            if (activeChallenges[selectedChallenge].spRewardAmount > 0)
            {
                activeChallenges[selectedChallenge].Skills[i].skillPoints += activeChallenges[selectedChallenge].spRewardAmount;
               
                //AnimateSkillBar();
            }
        }
            if (activeChallenges[selectedChallenge].xpRewardAmount > 0)
            {
                ScoreSingleton.Instance.xp += activeChallenges[selectedChallenge].xpRewardAmount;
                //AnimateSkillBar();
            }
            if (activeChallenges[selectedChallenge].tokenRewardAmount > 0)
            {
                ScoreSingleton.Instance.tokens += activeChallenges[selectedChallenge].tokenRewardAmount;
                //AnimateSkillBar();
            }

         
        
        if (activeChallenges.Count > selectedChallenge)
        {
            selectedChallenge++;
            ActivateChallengeScreen();
        }
        else this.gameObject.SetActive(false);
    }

    void AnimateSkillBar()
    {

    }

   

}
