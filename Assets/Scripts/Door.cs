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
	public bool isExitOnly = false;


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


	public void OnPlayerEnteredThroughThisDoorJustNow() {
		Debug.Log("WAAAY! OnPlayerEnteredThroughThisDoorJustNow");
		if( graphics!=null ) {
			graphics.enabled=true;
			graphics.color = Color.white;
			if( ExitOnly ) {
				StartCoroutine( FadeOutDoor() );
			}
		}
	}


	IEnumerator FadeOutDoor(){
		Debug.Log("Fade out door - its an exit only");
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
		isTransitionalDoor = false;
		Over=false;
		if( graphics==null )
			graphics = this.gameObject.GetComponentInChildren<SpriteRenderer>( true );
	}


	void Update() {
		if( !Application.isPlaying ) {
			if( DoorLeadsTo == "ExitOnly" ) {
				isExitOnly = true;
			}
			if( isExitOnly ) {
				DoorLeadsTo = "ExitOnly";
			} else {
				var comps = this.name.Split( new char[]{':'} );
				if( comps.Length == 3 ) {
					if( comps[0] == "Door" && comps[1] == "To" ) {
						DoorLeadsTo = comps[2];
					} else if( comps[0] == "Door" && comps[1] == "From" ) {
						DoorLeadsTo = comps[2];
						//DoorLeadsTo = "ExitOnly";
					}
				}
			}
			return;
		}
		if( ExitOnly ) return;
		if( (Input.GetKeyDown(KeyCode.UpArrow)||  Input.GetKeyDown(KeyCode.W)) && Over && this.gameObject.name!="TRANSITIONAL DOOR" ) {
			Debug.Log("Door; next level");
			openDoor();
		}
	}


	void openDoor() {
		enabled=false;
		GameManager.GetSingleton().StartLoading( this.DoorLeadsTo, this.EnterOrigin, this.EnterOriginName, this.useEnterOrigin );
	}


	void OnDestroy() {
		if( isTransitionalDoor ) {
			//Debug.Log("Destroyed transitional door : " + this.name );
		}
	}


	[HideInInspector]
	public bool isTransitionalDoor = false;


	void OnTriggerEnter(Collider coll) {
		if( !Application.isPlaying ) {
			return;
		}
		Debug.Log("Door: moved over");
		if( coll.gameObject.tag == "Player"    ) {
			Debug.Log("Door: moved over");
			Over=true;
			//minDis = (coll.gameObject.transform.position - gameObject.transform.position).magnitude;
		}
	}


	void OnTriggerExit( Collider  coll ) {
		if( !Application.isPlaying ) {
			return;
		}
		if (coll.gameObject.tag == "Player") {
			Over=false;
		}
	}




}

