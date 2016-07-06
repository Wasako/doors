using UnityEngine;
using System.Collections;

public class DrawForward : MonoBehaviour {


	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine( this.transform.position, this.transform.position + (this.transform.right * 100f) );
	}
	/*void OnDrawGizmo(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine( this.transform.position, this.transform.position + (this.transform.right * 100f) );
	}*/
}
