using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    static public GameManager singleton;
    public int maxBodypart;
    int actualParts = 0;
    public List<string> thingsNames = new List<string>(7);
	public List<string> playerThings = new List<string>();
    public GameObject camera;

	public string DoorLeadsTo = "";

	public bool useEnterOrigin = false;
	public Vector3 EnterOrigin = Vector3.zero;
	public string EnterOriginName = "";

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
	    
        if(Input.GetKeyDown(KeyCode.Escape)) {
            BlackScreen();
        }

	}
    
	public static GameManager GetSingleton() {
		GameManager ret = singleton;
		if( ret==null ) {
			var go = new GameObject("GameManager");
			ret = singleton = go.AddComponent<GameManager>();
		}
		return ret;
	}

    public void AddStuff(string name)
    {
        for (int i = 0; i < thingsNames.Count; i++) {

            print("jebaniutcy");
            if (thingsNames[i] == name)
            {
                print(thingsNames.Count + " " + playerThings.Count);
                if (playerThings[i] != name)
                {
                    actualParts++;
                    WearIt();
                    Save();
                }
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
    
    public void BlackScreen()
    {
        camera.SetActive(!camera.activeSelf);
    }

	public void StartLoading(string doorLeadsTo, Vector3 enterOrigin, string enterOriginName, bool _useEnterOrigin ){
		DoorLeadsTo = doorLeadsTo;
		useEnterOrigin = _useEnterOrigin;
		this.EnterOrigin = enterOrigin;
		this.EnterOriginName = enterOriginName;
		DontDestroyOnLoad(this.gameObject);
		StartCoroutine( SetNextLevel(DoorLeadsTo) );
	}

	System.Collections.IEnumerator SetNextLevel( string name ) {
		Move2D.Player.enabled=false;
		var canvas = UINode.GetCanvasGO();
		if(canvas!=null ) {
			var rawImage = canvas.GetComponentInChildren<UnityEngine.UI.RawImage>(true);
			if( rawImage!=null ) {
				rawImage.gameObject.SetActive(true);

				var c = Color.black;
				c.a = 0f;
				rawImage.color = c;

				float t = 0f;
				while( t < 1f ) {
					t += Time.deltaTime*2f;
					if( t > 1f ) t = 1f;
					c.a = Mathf.SmoothStep(0,1f, t);
					rawImage.color = c;
					yield return 0;
				}

			} else {

			}

		}

		//Debug.Log( "Loading level" );

		yield return new WaitForSeconds(0.25f);

		yield return  0;
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		//Debug.Log("done loading", this.gameObject);
		yield return  0;
	}


	void OnLevelWasLoaded( int level ) {
		//Debug.Log("On Level was loaded");
		StartCoroutine( EndLoading() );
	}

	public Vector3 EnterOriginPos {
		get {
			if( useEnterOrigin ) {
				if( !string.IsNullOrEmpty(EnterOriginName) ) {
					var go = GameObject.Find(""+EnterOriginName);
					if(go==null){
						Debug.Log( "Error cannot find object :" + EnterOriginName + " falling back on vector : " + EnterOrigin);
					} else {
						return go.transform.position;
					}
				}
				return EnterOrigin;
			}
			return Vector3.zero;
		}
	}

	public bool EnterAtPosBasedOnGO {
		get {
			bool maybe = string.IsNullOrEmpty(EnterOriginName) == false;
			if( maybe ) {
			}
			return maybe;
		}
	}

	System.Collections.IEnumerator EndLoading() {
		//Debug.Log( "EndLoading" );

		yield return 0;

		if( useEnterOrigin ) {
			//Debug.Log("Set position to GO pos.");
			Move2D.Player.transform.position = EnterOriginPos;
			if( EnterAtPosBasedOnGO ) {
				var go_door =  GameObject.Find(EnterOriginName);
				if(go_door==null) {
					Debug.LogError("Error finding door : " + EnterOriginName );
				}
                if (go_door != null && go_door.GetComponent<Door>() != null)
                {
					go_door.GetComponent<Door>().OnPlayerEnteredThroughThisDoorJustNow();
					Move2D.Player.enabled=true;
				}
			}
		}
		yield return  0;

		var canvas = UINode.GetCanvasGO();
		if( canvas!=null ) {
			//Debug.Log("fading in now!");
			var rawImage = canvas.GetComponentInChildren<UnityEngine.UI.RawImage>(true);
			if( rawImage!=null ) {
				//Debug.Log("disabling object...");
				rawImage.gameObject.SetActive(true);

				var c = Color.black;
				c.a = 0f;
				rawImage.color = c;

				//Debug.Log("fading in..");
				float t = 0f;
				while( t < 1f ) {
					//Debug.Log("counting time...");
					t += Time.deltaTime*2f;
					if( t > 1f ) t = 1f;
					c.a = Mathf.SmoothStep(1f,0f, t);
					rawImage.color = c;
					yield return 0;
				}

				rawImage.gameObject.SetActive(false);
			} else {
				Debug.LogError("cannot fade in...");
			}
		}

		//GameObject.DestroyObject(this.gameObject);
	}

}
