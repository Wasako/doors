using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class Door : MonoBehaviour {
	public bool Over = false;

	public string DoorLeadsTo = "";

	public bool useEnterOrigin = false;
	public Vector3 EnterOrigin = Vector3.zero;
	public string EnterOriginName = "";

	public SpriteRenderer graphics;


	public bool ExitOnly {
		get {
			return DoorLeadsTo == "" || DoorLeadsTo == "ExitOnly";
		}
	}



	public Vector3 EnterOriginPos {
		get {
			if( useEnterOrigin ) {
				if( !string.IsNullOrEmpty(EnterOriginName) ) {
					var go = GameObject.Find(""+EnterOriginName);
					if(go==null){
						Debug.LogError( "Error cannot find object :" + EnterOriginName + " falling back on vector : " + EnterOrigin);
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


	public void OnPlayerEnteredThroughThisDoorJustNow() {
		Debug.LogError("WAAAY! OnPlayerEnteredThroughThisDoorJustNow");
		graphics.enabled=true;
		graphics.color = Color.white;
		if( ExitOnly ) {
			StartCoroutine( FadeOutDoor() );
		}
	}


	IEnumerator FadeOutDoor(){
		var c = graphics.color;
		float t = 0;
		yield return 0;
		while( t< 1f ) {
			t+=Time.deltaTime;
			if(t>1f)t=1f;
			var al = Mathf.SmoothStep( 1f, 0f, t );
			c.a = al;
			graphics.color = c;
		}
	}


	void Awake() {
		if( !Application.isPlaying ) {
			return;
		}
		Over=false;
	}


	void Update() {
		if( !Application.isPlaying ) {
			var comps = this.name.Split( new char[]{':'} );
			if( comps.Length == 3 ) {
				if( comps[0] == "Door" && comps[1] == "To" ) {
					DoorLeadsTo = comps[2];
				} else if( comps[0] == "Door" && comps[1] == "From" ) {
					DoorLeadsTo = "ExitOnly";
				}
			}
			return;
		}
		if( ExitOnly ) return;
		if( Input.GetKeyDown(KeyCode.UpArrow) && Over ) {
			Debug.Log("next level");
			StartCoroutine( SetNextLevel(DoorLeadsTo) );
		}
	}


	IEnumerator SetNextLevel( string name ) {
		Move2D.Player.enabled=false;
		var canvas = GameObject.Find("Canvas");
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

		Debug.LogError("Loading level");
		GameObject.DontDestroyOnLoad(this.gameObject);
		yield return  0;
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
		Debug.LogError("done loading", this.gameObject);
		yield return  0;
		Debug.LogError("next frame.");

		if( useEnterOrigin ) {
			Debug.LogError("set position to GO pos.");
			Move2D.Player.transform.position = EnterOriginPos;
			if( EnterAtPosBasedOnGO ) {
				var go_door =  GameObject.Find(EnterOriginName);
				if( go_door.GetComponent<Door>() != null ) {
					go_door.GetComponent<Door>().OnPlayerEnteredThroughThisDoorJustNow();
					Move2D.Player.enabled=true;
				}
			}
		}
		yield return  0;
	}

	void OnLevelWasLoaded(int level) {
		Debug.Log("On Level was loaded");
		StartCoroutine( EndLoading() );

	}


	IEnumerator EndLoading() {
		var canvas = GameObject.Find("Canvas");
		if( canvas!=null ) {
			Debug.LogError("fading in now!");
			var rawImage = canvas.GetComponentInChildren<UnityEngine.UI.RawImage>(true);
			if( rawImage!=null ) {
				Debug.Log("disabling object...");
				rawImage.gameObject.SetActive(true);

				var c = Color.black;
				c.a = 0f;
				rawImage.color = c;

				Debug.Log("fading in..");
				float t = 0f;
				while( t < 1f ) {
					Debug.Log("counting time...");
					t += Time.deltaTime*2f;
					if( t > 1f ) t = 1f;
					c.a = Mathf.SmoothStep(1f,0f, t);
					rawImage.color = c;
					yield return 0;
				}

				rawImage.gameObject.SetActive(false);
			} else {
				Debug.Log("cannot fade in...");
			}
		}

		GameObject.DestroyObject(this.gameObject);	}



	void OnTriggerEnter(Collider coll)
	{
		if( !Application.isPlaying ) {
			return;
		}
		Debug.Log("moved over");
		if( coll.gameObject.tag == "Player"    ) {
			Debug.Log("moved over");
			Over=true;
			//minDis = (coll.gameObject.transform.position - gameObject.transform.position).magnitude;
		}
	}


	void OnTriggerExit(Collider  coll)
	{
		if( !Application.isPlaying ) {
			return;
		}
		if (coll.gameObject.tag == "Player")
		{
			Over=false;
		}
	}




}

