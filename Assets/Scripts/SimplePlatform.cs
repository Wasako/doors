using UnityEngine;
using System.Collections;

public class SimplePlatform : MonoBehaviour {

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
            if (coll.gameObject.GetComponent<GetSideHit>().ifTop(this.gameObject))
                coll.gameObject.GetComponent<Move2D>().jumped = false;
    }
}
