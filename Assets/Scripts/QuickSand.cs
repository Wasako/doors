using UnityEngine;
using System.Collections;

public class QuickSand : MonoBehaviour
{
    public float fallingTime = 2f;
    Vector3 StartingColliderPosition;

    void Awake()
    {
        StartingColliderPosition = GetComponent<Collider>().transform.position;
    }

    void OnCollisionStay(Collision  coll)
    {
        if(coll.gameObject.tag  == "Player" && !coll.gameObject.GetComponent<Move2D>().isMoving)
        {
            GetComponent<Collider>().transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

}

