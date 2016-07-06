using UnityEngine;
using System.Collections;

public class QuickSand : MonoBehaviour
{
    public float fallingTime = 2f;
    Vector3 StartingColliderPosition;
    public float fallingSpeed = 0.1f;
    bool startedFalling = false;
    bool collidingWithPlayer = false;
    float lastTimeTouched = 0f;
    public float minDis = 0f;
    Coroutine cor, fallCor;
  //  public float actualDis = 0f;
    void Awake()
    {
        StartingColliderPosition = GetComponent<Collider>().transform.position;
    }
    void Update()
    {

    }



    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "Player" && minDis ==0f   )
            minDis = (coll.gameObject.transform.position - gameObject.transform.position).magnitude;
    }

    void OnCollisionStay(Collision  coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
            if (cor == null)
                cor = StartCoroutine(CheckIfItsStillColliding());

            if (!coll.gameObject.GetComponent<Move2D>().isMoving)
            {
                if (fallCor == null)
                    fallCor = StartCoroutine(ColliderDisappear());
                Invoke("ColliderDisappear", fallingTime);
                startedFalling = true;
                coll.gameObject.GetComponent<Move2D>().QuickSandFalling = true;
                GetComponent<BoxCollider>().center -= new Vector3(0, fallingSpeed, 0);
            }
            else if (coll.gameObject.GetComponent<Move2D>().isMoving)
            {

            }

        }
    }
    IEnumerator ColliderDisappear()
    {
        yield return new WaitForSeconds(fallingTime);
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ColliderAppear", 1.5f);
    }
    void ColliderAppear()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    IEnumerator CheckIfItsStillColliding()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            if((GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position).magnitude > minDis+1)
            {
                GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().QuickSandFalling = false;
                startedFalling = false;
                break;
            }
        }
    }
    
    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Move2D>().isMoving)
        {
            collidingWithPlayer = false;
        }
    }

}

