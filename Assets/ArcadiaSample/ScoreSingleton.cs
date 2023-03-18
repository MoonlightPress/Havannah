using UnityEngine;
using System.Collections;
   /// <summary>
   /// This singleton is almost 8 years old from back when there were just 3 music games. The music games still use it
   /// And it's used for taking player info between games i a session without calling the server a hundred times.
   /// 
   /// A lot of this was keeping track of high scores
   /// </summary>
public class ScoreSingleton : MonoBehaviour
{

	public string username = "";
	public int playerID = 0;
    public int AvatarID = 0;
    public int health = 3;
    public int xp = 3;
    public int xpNew = 1;
    public int xpToNextLvl;
    public int lvl;
    public int tokens = 100;
    public int cost = 1;

 
    public string lastPlayed = "yesterday";
    public string lastDate = "26042016";
    public bool playedToday = true;

 



    private static ScoreSingleton _instance = null;
	public static ScoreSingleton Instance
	{
		get {return _instance;}
	}
	
	void Awake ()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			_instance = this;
		}
		
		DontDestroyOnLoad(this.gameObject);
		gameObject.name = "$Score";
		
	}


}
