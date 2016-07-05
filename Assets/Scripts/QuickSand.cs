using UnityEngine;
using System.Collections;

public class QuickSand : MonoBehaviour
{
    void OnCollisionStay(Collision  coll)
    {
        
        if(coll.gameObject.tag  == "Player" && !coll.gameObject.GetComponent<Move2D>().isMoving)
        {
            Debug.Log("STAY");
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && ! other.GetComponent<Move2D>().isMoving)
        {
            Debug.Log("TRIGGERSTAY");

        }
    }
}

