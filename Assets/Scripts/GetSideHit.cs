using UnityEngine;
using System.Collections;

public class GetSideHit : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(ReturnDirection(collision.gameObject, this.gameObject));
    }

    private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }

    public bool ifTop(GameObject obj)
    {
        if(ReturnDirection(obj, this.gameObject) == HitDirection.Top ||  ReturnDirection(obj, this.gameObject) == HitDirection.None)
        {
            return true;
        }
        return false;
    }

    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {

                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up) { hitDirection = HitDirection.Top; }
                if (MyNormal == -MyRayHit.transform.up) { hitDirection = HitDirection.Bottom; }
                if (MyNormal == MyRayHit.transform.forward) { hitDirection = HitDirection.Forward; }
                if (MyNormal == -MyRayHit.transform.forward) { hitDirection = HitDirection.Back; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = HitDirection.Right; }
                if (MyNormal == -MyRayHit.transform.right) { hitDirection = HitDirection.Left; }
            }
        }
        return hitDirection;
    }
}