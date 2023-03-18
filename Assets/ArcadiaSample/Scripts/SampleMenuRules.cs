using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleMenuRules : MonoBehaviour
{
    public Button StartButton;
    public GameObject GameSprites;
    public GameObject GamePlay;
    public SampleGameManager manager;
    public ThePrimerSkill SampleSkillObj;
    public ArcadiaChallenges[] challengeList = new ArcadiaChallenges[10];
    public ChallengeManager challengeManager;
  

    void Start()
    {
      

    }
    private void OnEnable()
    {
        StartButton.interactable = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
          
                StartButton.interactable = false;
                StartCoroutine(KeyboardButtons("NewGame"));
                  
        }

    }

    IEnumerator KeyboardButtons(string name)
    {
        yield return new WaitForSeconds(.15f);
        GamePlay.SetActive(true);
        GameSprites.SetActive(true);
        manager.NewGame();
        this.gameObject.SetActive(false);
    }

 

   
}
