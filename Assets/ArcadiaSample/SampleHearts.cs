using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleHearts : MonoBehaviour
{
    public Text scoreText;
    public Image[] Hearts = new Image[12];
    public Sprite HeartFull;
    public Sprite HeartEmpty;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetScore(int score)
    {
        if (score > 0)
            scoreText.text = score.ToString();
        else
            scoreText.text = "";
    }
    public void SetHearts(int health)
    {
        if (health >= 3)
        {
            Hearts[0].sprite = HeartFull;
            Hearts[1].sprite = HeartFull;
            Hearts[2].sprite = HeartFull;
        }
        else if (health == 2)
        {
            Hearts[0].sprite = HeartFull;
            Hearts[1].sprite = HeartFull;
            Hearts[2].sprite = HeartEmpty;
        }
        else if (health == 1)
        {
            Hearts[0].sprite = HeartFull;
            Hearts[1].sprite = HeartEmpty;
            Hearts[2].sprite = HeartEmpty;
        }
        else if (health <= 0)
        {
            Hearts[0].sprite = HeartEmpty;
            Hearts[1].sprite = HeartEmpty;
            Hearts[2].sprite = HeartEmpty;
        }
    }

}
