using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {

     public GravityChanger gravityChanger;

    bool canChange = false;
    public void ReverseTheGravity()
    {
        gravityChanger.gravityMultiply *= -1;
        switch(gravityChanger.RotDirection)
        {
            case GravityChanger.RotateDirection.Rot_0:
                gravityChanger.RotDirection = GravityChanger.RotateDirection.Rot_180;

                break;

            case GravityChanger.RotateDirection.Rot_180:
                gravityChanger.RotDirection = GravityChanger.RotateDirection.Rot_0;

                break;

            case GravityChanger.RotateDirection.Rot_270:
                gravityChanger.RotDirection = GravityChanger.RotateDirection.Rot_90;
                break;

            case GravityChanger.RotateDirection.Rot_90:
                gravityChanger.RotDirection = GravityChanger.RotateDirection.Rot_270;

                break;

            default:

                break;

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        canChange = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            canChange = false;
    }

    void Update()
    {

        if(canChange )
        {
            if(Input.GetKeyDown(KeyCode.E))
            ReverseTheGravity();
        }
    }


}
