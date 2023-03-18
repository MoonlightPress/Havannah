using UnityEngine;
using System.Collections;

using System;
using SimpleJSON;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;


//[CreateAssetMenu]
public class ThePrimerSkillLibrary : MonoBehaviour {
    [Header("Skills")]
    public ThePrimerSkill[] MathSkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] BiologySkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] AstronomySkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] EngineeringSkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] ChemistrySkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] PhysicsSkills = new ThePrimerSkill[16];
    public ThePrimerSkill[] GameSkills = new ThePrimerSkill[0];
    public ThePrimerSkill[] AllSkills = new ThePrimerSkill[0];
    [Header("Attributes")]
    public ThePrimerAttribute Wisdom;
    public ThePrimerAttribute Intellect;
    public ThePrimerAttribute Charisma;
    public ThePrimerAttribute Perception;
    public ThePrimerAttribute Dexterity;
    public ThePrimerAttribute Endurance;
    [Header("Color Library")]
    public Color[] Colors = new Color[6];
    [Header("Something")]
    public Sprite BlankSkill;
    public Sprite WarmupSkill;
    public Sprite[] LevelButtons = new Sprite[7];
    public ThePrimerSkill[] PrimerSkillObjects = new ThePrimerSkill[90];
    public Sprite currentImage;
    [Header("DateInfo")]
    public DateTime rightNow;
    public string rightNowString;
    public DateTime receivedDate;
    public string receivedDateString;

    [Header("Challenges")]
    public bool challengeActive = false;
    public List<ArcadiaChallenges> AllChallenges = new List <ArcadiaChallenges>();
    public ArcadiaChallenges[] skillChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] MorseEncoderChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] MorseCoderChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] TapEnCoderChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] TapCoderChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] LineupChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] JumperChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] MemoryMatchChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] StarGrinderChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] TonalRecallChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] RepeaterChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] PitchBackChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] TyperWriterChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] ColorizeChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] SudokuChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] RhythmRunnerChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] ConsumptionChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] ThePrimerChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] FrontierChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] CisterciaChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] ColorRecallChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] SpatialRecallChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] SymbolRecallChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] DriftwardChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] ChoppaChallenges = new ArcadiaChallenges[16];
    public ArcadiaChallenges[] PlatformsChallenges = new ArcadiaChallenges[3];
    public ArcadiaChallenges[] StacksChallenges = new ArcadiaChallenges[3];
  

   
   
        //Make a comprehensive list out of all the challenges
    void Start()
    {
        for (int i = 0; i < skillChallenges.Length; i++)
        {
            AllChallenges.Add(skillChallenges[i]);
        }
        //for (int i = 0; i < GameNameChallenges.Length; i++)
        //{
        //    AllChallenges.Add(GameNameChallenges[i]);
        //}

    }




    //Wipe all the skill objects, then call the server to find out their values
    public void LoadSkillStats(int userID)
    {
        ScoreSingleton.Instance.playerID = userID; 
        //Debug.Log("Loading skills for  " + userID +".  erasing everything and reloading SP()");
        for (int i = 0; i < AllSkills.Length; i++)
        {
            AllSkills[i].skillFatigue = 1;
            AllSkills[i].skillPoints = 0;
            AllSkills[i].skillLevel = 1;
            AllSkills[i].LastUpdated = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }    


    }

//Wipe all the challenge objects clean
   public void WipeEverything()
    {
      //  Debug.Log("Wipe challenges");
      //  Debug.Log(AllChallenges.Count);
        for (int i = 0; i < AllChallenges.Count; i++)
        {

            AllChallenges[i].hasCollected = false;
            AllChallenges[i].hasCompleted = false;
            AllChallenges[i].dateCompleted = "";
            AllChallenges[i].progress = 0;
        }

    }

    //Wipe all the skill objects, call the server to find out their values, not sure why there's two of these scripts
    public void WipeSkillStats()
    {
 
        for (int i = 0; i < AllSkills.Length; i++)
        {
            AllSkills[i].skillFatigue = 1;
            AllSkills[i].skillPoints = 0;
            AllSkills[i].skillLevel = 1;
            AllSkills[i].LastUpdated = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
          
    }


  

    public Dictionary<string, ThePrimerSkill> SkillArray = new Dictionary<string, ThePrimerSkill>();


  






 
}
