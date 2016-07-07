using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MovingPlatform : MonoBehaviour {

    public Vector3 DeltaTargetPosition = Vector3.zero;
    public float floatingTime = 5f;
    Vector3 StartingPosition;
    float JourneyLength;
    public float speed = 1.0F;
    private float startTime;
    int timesDid=0;
    bool PlayerAttached = false;
    Vector3 PlayerLocationOnPlatform;
    float dis = 0f;
    Vector3 PlayerScale = Vector3.zero;
	public Transform offset;
	public bool useTarget = true;
	public Vector3 EndPosition;

    void Awake()
    {
		if( !Application.isPlaying ) {
			return;
		}
        startTime = 0f;
        StartingPosition = transform.position;
		if( offset != null && useTarget ) {
			EndPosition = this.offset.position;
		} else {
			EndPosition = StartingPosition + DeltaTargetPosition;
		}
		JourneyLength = Vector2.Distance(StartingPosition, EndPosition);
        StartCoroutine(Movement());
    }

	void Update() {
		if( !Application.isPlaying && offset!=null && useTarget ) {
			DeltaTargetPosition = this.offset.position - this.transform.position;
		}
	}


    void OnCollisionEnter(Collision coll)
    {
		if( !Application.isPlaying ) {
			return;
		}
        if (coll.gameObject.tag == "Player") // && coll.gameObject.transform.position.y > gameObject.transform.position.y)
        {
			if( coll.gameObject.GetComponent<GetSideHit>().ifTop(this.gameObject) ) {
            	coll.gameObject.GetComponent<Move2D>().jumped = false;
			}
        }
    }
    

	void OnCollisionStay(Collision coll) {
		if( !Application.isPlaying ) {
			return;
		}
        if (coll.gameObject.tag == "Player") {

        }
    }


    void OnCollisionExit( Collision coll ) {
		if( !Application.isPlaying ) {
			return;
		}
        if (coll.gameObject.tag == "Player")
        {
          //  coll.transform.SetParent(null);

            // PlayerAttached = false;
            //  PlayerLocationOnPlatform = Vector3.zero;
        }
    }




    public float fracJourney;
    public float distCovered;

	public float traverse= 0f;
	public float t = 0f;
	public float oldTime = 0f;

    IEnumerator Movement()
    {
        while(true)
        {

			float spd = JourneyLength / speed;
			traverse += Time.deltaTime / spd;
			t = ( traverse % 2f );
			t = t > 1f ?(2f-t) : t;
			transform.position = Vector3.Lerp( StartingPosition, EndPosition, t );

			if( PlayerAttached ) {
				Debug.Log("moving platform with player attached");
				if( this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y == dis) {
					GameObject.FindGameObjectWithTag("Player").transform.position = transform.position + PlayerLocationOnPlatform;
				}
			} else {
			}
			oldTime = t;

			yield return 0;
        }
    }



}
