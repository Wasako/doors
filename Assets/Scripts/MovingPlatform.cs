using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 DeltaTargetPosition = Vector3.zero;
    Vector3 StartingPosition;
    float JourneyLength;
    public float speed = 1.0F;
    private float startTime;
    int timesDid=0;
    bool PlayerAttached = false;
    Vector3 PlayerLocationOnPlatform;
    void Start()
    {
        startTime = 0f;
        StartingPosition = transform.position;
        JourneyLength = Vector2.Distance(StartingPosition, StartingPosition + DeltaTargetPosition);
        StartCoroutine(Movement());
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerAttached = true;
        }
    }
    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerAttached = true;
            PlayerLocationOnPlatform = this.transform.position - coll.gameObject.transform.position;
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            PlayerAttached = false;
            PlayerLocationOnPlatform = Vector3.zero;
        }
    }




    public float fracJourney;
    public float distCovered;
    IEnumerator Movement()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);
            distCovered = (Time.time - startTime ) * speed;
            fracJourney = (distCovered / JourneyLength) - timesDid;

            if (transform.position != DeltaTargetPosition + StartingPosition)
            {
                transform.position = Vector3.Lerp(StartingPosition, DeltaTargetPosition + StartingPosition, fracJourney);
                if (PlayerAttached)
                    GameObject.FindGameObjectWithTag("Player").transform.position = transform.position - PlayerLocationOnPlatform;
            }
            else
            {
                timesDid += 1;
                StartingPosition = transform.position;
                DeltaTargetPosition *= -1;
            }

        }
    }



}
