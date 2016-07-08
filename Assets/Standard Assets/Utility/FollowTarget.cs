using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);

		private void LateUpdate() {
			if( target==null ) {	
				var go = GameObject.Find("Player");
				if( go!=null ) {
					target = go.transform;
					return;
				}
				//target = Move2D.Player.transform;
				Debug.LogError("FollowTarget : No referance in FollowTarget for what to follow - check inspecgtor that a referance to the Player is set.");
				return;
			}
            transform.position = target.position + offset;
        }
    }
}
