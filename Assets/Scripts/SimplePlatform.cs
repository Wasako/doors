using UnityEngine;
using System.Collections;

public class SimplePlatform : MonoBehaviour {

    void OnCollisionEnter(Collision coll) {
		if( coll.gameObject.tag == "Player" ) {
			var sideHit = coll.gameObject.GetComponent<GetSideHit>();
			if( sideHit!=null ) {
				if(sideHit.ifTop( this.gameObject )) {
					coll.gameObject.GetComponent<Move2D>().jumped = false;
				}
			}
		}
    }
}
