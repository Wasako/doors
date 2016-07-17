using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    static public GameManager singleton;
    public GameManager singletonTest;
    public int maxBodypart;
    int actualParts = 0;
    public List<string> thingsNames = new List<string>(7);
	public List<string> playerThings = new List<string>();
    public GameObject camera;

	public string DoorLeadsTo = "";

	public bool useEnterOrigin = false;
	public Vector3 EnterOrigin = Vector3.zero;
	public string EnterOriginName = "";

	int state = 0;

    void Awake()
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
        print(singleton);
        DontDestroyOnLoad(this.gameObject);
        Load();
    }

	// Use this for initialization
	void Start () {
        maxBodypart = thingsNames.Count;
        playerThings = new List<string>(thingsNames.Count);
        for(int i = 0; i < thingsNames.Count; i++) {
            playerThings.Add("");
        }

	}
	
	// Update is called once per frame
	void Update () {
	    
        if(Input.GetKeyDown(KeyCode.Escape)) {
            BlackScreen();
        }

	}
    
    void OnLevelWasLoaded() {
		Debug.Log("On Level was loaded");
        WearIt();

		if(state == 1) {
			StartCoroutine( EndLoading() );
		} else if( state == 2 ) {
			StartCoroutine( EndDie() );
		}
		state = 0;

    }



	public void OnPlayerDie() {
		StartCoroutine( DieImpl() );
	}


	System.Collections.IEnumerator EndDie() {

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

		yield return 0;	
	}


	System.Collections.IEnumerator DieImpl(){

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
					c.a = Mathf.SmoothStep(0f,1f, t);
					rawImage.color = c;
					yield return 0;
				}
			} else {
				Debug.LogError("cannot fade in...");
			}
		}

		state = 2;
		yield return 0;
		UnityEngine.SceneManagement.SceneManager.LoadScene("crispy_hub");

		//StartCoroutine( EndLoading() );

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
            if (thingsNames[i].ToString() == name.ToString())
            {
                if (playerThings[i].ToString ()!= name.ToString())
                {
                    playerThings[i] = name;
                    actualParts++;
                    WearIt();
                    Save();
                }
            }
        }

        if (actualParts == thingsNames.Count)
        {
            SceneManager.LoadScene(8);
            Destroy(this.gameObject);
        }

    }

    public bool IsStuffOwned(string String)
    {

        for (int i = 0; i < playerThings.Count; i++)
        {
            print(playerThings[i]+" "+String);
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
        for (int i = 0; i < thingsNames.Count; i++ )
        {
            switch(playerThings[i]) {
                case "Head":
                    Move2D.Player.head.sprite = Move2D.Player.headSleepwalker;
                    break;
                case "Chest":
                    Move2D.Player.chest.sprite = Move2D.Player.chestSleepwalker;
                    break;
                case "Heart":
                    Move2D.Player.heart.sprite = Move2D.Player.heartSleepwalker;
                    break;
                case "LeftLeg":
                    Move2D.Player.leftUpperLeg.sprite = Move2D.Player.upperLegSleepwalker;
                    Move2D.Player.leftLowerLeg.sprite = Move2D.Player.lowerLegSleepwalker;
                    break;
                case "RightLeg":
                    Move2D.Player.rightUpperLeg.sprite = Move2D.Player.upperLegSleepwalker;
                    Move2D.Player.rightLowerLeg.sprite = Move2D.Player.lowerLegSleepwalker;
                    break;
                case "LeftArm":
                    Move2D.Player.leftArm.sprite = Move2D.Player.leftArmSleepwalker;
                    break;
                case "RightArm":
                    Move2D.Player.rightArm.sprite = Move2D.Player.rightArmSleepwalker;
                    break;
                default:
                    break;
            }
        }
    }
    
    public void BlackScreen()
    {
		//f(camera!=nu
       // camera.SetActive(!camera.activeSelf);
    }

	public void StartLoading(string doorLeadsTo, Vector3 enterOrigin, string enterOriginName, bool _useEnterOrigin ){
		Debug.LogError ("loading " + doorLeadsTo);
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
		state = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		//Debug.Log("done loading", this.gameObject);
		yield return  0;
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
		Debug.Log( "EndLoading" );

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
