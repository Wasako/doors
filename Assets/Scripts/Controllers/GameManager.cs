using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    static public GameManager singleton;
    public int maxBodypart;
    int actualParts = 0;
    public List<string> thingsNames = new List<string>(7);
    public List<string> playerThings;
    public GameObject camera;

    void Avake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton == this)
        {

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        Load();

    }

	// Use this for initialization
	void Start () {
        if(singleton == null) {
            singleton = this;
        }else if(singleton == this) {

        } else {
            Destroy(gameObject);
        }
        maxBodypart = thingsNames.Count;
        playerThings = new List<string>(thingsNames.Count);
        for(int i = 0; i < thingsNames.Count; i++) {
            playerThings.Add("");
        }
        Load();

	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AddStuff(string name)
    {
        for (int i = 0; i < thingsNames.Count; i++)
        {
            if (thingsNames[i] == name)
            {
                print(thingsNames.Count + " " + playerThings.Count);
                playerThings[i] = name;
                actualParts++;
                WearIt();
                Save();
            }
        }

        if (actualParts == thingsNames.Count)
        {
            print("Cool mother fucker! You win!");
        }

    }

    public bool IsStuffOwned(string String)
    {

        for (int i = 0; i < playerThings.Count; i++)
        {
            if (playerThings[i] == String)
            {
                return true;
            }
        }
        return false;
    }

    void Save() {
        for (int i = 0; i < thingsNames.Count; i++)
        {
            PlayerPrefs.SetString( thingsNames[i], playerThings[i]);
        }
        
    }

    void Load()
    {
        for (int i = 0; i < playerThings.Count; i++)
        {
            playerThings[i] = PlayerPrefs.GetString(thingsNames[i]);
            if (playerThings[i] != "")
            {
                actualParts++;
                WearIt();
            }
        }
    }

    public void WearIt()
    {
        //Trigger or something?
    }

}
