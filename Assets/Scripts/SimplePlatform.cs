using UnityEngine;
using System.Collections;

public class SimplePlatform : MonoBehaviour {
	void OnDrawGizmosSelected(){

		Vector3 v3 = this.transform.up;
		Gizmos.color = Color.green;
		Gizmos.DrawLine( this.transform.position, this.transform.position + (v3 * 5f) );
	}


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
