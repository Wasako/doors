using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    List<string> thingsNames = new List<string>(7);
    List<string> playerThings = new List<string>(7);

    void Avake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	// Use this for initialization
	void Start () {
        Load();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Save() {
        for (int i = 0; i < playerThings.Count; i++)
        {
            PlayerPrefs.SetString( thingsNames[i], playerThings[i]);
        }
        

    }

    void Load()
    {
        for (int i = 0; i < playerThings.Count; i++)
        {
            playerThings[i] = PlayerPrefs.GetString(thingsNames[i]);
        }
    }

}
