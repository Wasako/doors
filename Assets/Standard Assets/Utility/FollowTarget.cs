using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);


        private void LateUpdate()
        {
			if(target==null) {
				Debug.LogError("FollowTarget : No referance in FollowTarget for what to follow - check inspecgtor that a referance to the Player is set.");
				return;
			}
            transform.position = target.position + offset;
        }
    }
}
