using UnityEngine;
using System.Collections;

public class BreakableBlock : MonoBehaviour {
    public int Durability = 3;

    Sprite sprite;

    void Awake()
    {
        sprite = GetComponent<Sprite>();
    }
   
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {

        }
    }


}
