using UnityEngine;
using System.Collections;
using System;


    public class FollowTargetWithinBorders : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0f, 7.5f, 0f);
        public Border borderTop, borderBottom, borderLeft, borderRight;
        float vX, vY;
        float halfHeight;
        float halfWidth;

        void Start()
        {
             halfHeight = gameObject.GetComponent<Camera>().orthographicSize;
             halfWidth = halfHeight * gameObject.GetComponent<Camera>().aspect;

             Debug.Log("gameObject.GetComponent<Camera>().aspect" + gameObject.GetComponent<Camera>().aspect+" "+ halfWidth);
        }
        private void LateUpdate()
        {
           
            if (target == null)
            {
                Debug.LogError("FollowTarget : No referance in FollowTarget for what to follow - check inspecgtor that a referance to the Player is set.");
                return;
            }
            MoveCameraOnAxis();
        }
        void MoveCameraOnAxis()
        {
         
            Vector3 t = target.position;

            float nextY = t.y + offset.y;
            float nextX = t.x + offset.x;
            float nextZ = t.z + offset.z;

            float bottomDistance = Math.Abs(borderBottom.transform.position.y - t.y);
            float topDistance = Math.Abs(borderTop.transform.position.y - t.y);
            float leftDistance = Math.Abs(borderLeft.transform.position.x - t.x);
            float rightDistance = Math.Abs(borderRight.transform.position.x - t.x);

        if (borderTop.Visible && topDistance < halfHeight) 
            transform.position = new Vector3(nextX, borderTop.transform.position.y - halfHeight , nextZ);
        else if (borderBottom.Visible && bottomDistance < halfHeight) 
            transform.position = new Vector3(nextX, borderBottom.transform.position.y + halfHeight, nextZ);
        if (borderLeft.Visible && leftDistance < halfWidth)
            transform.position = new Vector3(borderLeft.transform.position.x + halfWidth, nextY, nextZ);
        else if (borderRight.Visible && rightDistance < halfWidth)
            transform.position = new Vector3(borderRight.transform.position.x - halfWidth, nextY, nextZ);
        else
            transform.position = new Vector3(nextX, nextY, nextZ);
    }
    }

