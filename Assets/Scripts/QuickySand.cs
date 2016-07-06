using UnityEngine;
using System.Collections;

public class QuickySand : MonoBehaviour {

    public float fallingTime = 2f;
    public float fallingSpeed = 0.1f;
    public bool startedFalling = false;
    float minDis = 1f;
    Coroutine FallingCoroutine;
    public float extentX, centerX;
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<Move2D>().jumped = false;

        Debug.Log("dsadsadsa");
    }



    IEnumerator falling()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.02f);
            GetComponent<BoxCollider>().center -= new Vector3(0, fallingSpeed, 0);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Move2D>().QuickSandFalling = true;
            if (-GetComponent<BoxCollider>().center.y > GetComponent<Renderer>().bounds.size.y)
            {
                GetComponent<BoxCollider>().enabled = false;
                Invoke("TurnOnCollider", 1f);
            }
           if (player.transform.position.x - this.gameObject.transform.position.x > GetComponent<Renderer>().bounds.size.x || player.transform.position.x - this.gameObject.transform.position.x < -GetComponent<Renderer>().bounds.size.x)
            {
                player.GetComponent<Move2D>().QuickSandFalling = false;
                GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                FallingCoroutine = null;
                break;
            }

        }
    }
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().QuickSandFalling)
        {
            if (Input.GetButtonDown("Jump")) // && jumpTime > endJumpTime)
            {
                if (GetComponent<BoxCollider>().center.y < 0)
                {
                    GetComponent<BoxCollider>().center += new Vector3(0, fallingSpeed * 5, 0);
                   // GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().QuickSandFalling = true;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().QuickSandFalling = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().jumped = false;
                }
            }
        }
    }

    void TurnOnCollider()
    {
        GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        GetComponent<BoxCollider>().enabled = true; 
        FallingCoroutine = null;
        StopCoroutine(falling());

    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!coll.gameObject.GetComponent<Move2D>().isMoving)
            {
                if (FallingCoroutine == null)
                    FallingCoroutine = StartCoroutine("falling");
            }
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Move2D>().isMoving)
        {

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
        GetComponent<BoxCollider>().center = Vector3.zero;
    }
    IEnumerator CheckIfItsStillColliding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if ((GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position).magnitude > minDis)
            {
                GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().QuickSandFalling = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Move2D>().canMove = false;
                startedFalling = false;

                break;
            }
        }
    }



}
