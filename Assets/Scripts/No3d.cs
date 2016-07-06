using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class No3d : MonoBehaviour {
	void Update () {
		if( !Application.isPlaying ) {
			for( int i=0; i<this.transform.childCount; i++){
				var ch = this.transform.GetChild(i);
				var tf = ch.transform;
				var angles = tf.rotation.eulerAngles;
				if(  Mathf.Abs( 0 - angles.x ) > 0.0001f || Mathf.Abs( 0 - angles.y ) > 0.0001f ) {
					angles.x = 0f;
					angles.y = 0f;
					tf.rotation = Quaternion.Euler( angles );
				}
			}
		}
	}
}
