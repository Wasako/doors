using UnityEngine;
using System.Collections;

public class GetSideHit : MonoBehaviour
{
	public Collider coll;
	public Rigidbody rigid;

	public bool verbose = true;
    void OnCollisionEnter(Collision collision)
    {
		if(verbose)Debug.Log( "GetSideHit: OnCollisionEnter:"+ ReturnDirection(collision.gameObject, this.gameObject));
    }

    public enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }



    public bool ifTop(GameObject obj)
    {
		var hitDir = ReturnHitPointDirection(obj, this.gameObject);
		var move2D= this.gameObject.GetComponent<Move2D>();
		if(verbose && move2D==null )Debug.Log( "GetSideHit: ifTop; Hit direction : " + hitDir );
		if(verbose && move2D!=null )Debug.Log( "GetSideHit: ifTop; Hit direction : " + hitDir + "; up="+move2D.hitDirUp );
		if( move2D!=null ){
			if( hitDir == move2D.hitDirUp || hitDir == HitDirection.None) {
				return true;
			}
		} else {
			if( hitDir == HitDirection.Top || hitDir == HitDirection.None) {
				return true;
			}
		}
        return false;
    }

    private HitDirection ReturnHitPointDirection(GameObject  objectFrom, GameObject objectHit)
    {
        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;

        if(Physics.Raycast(objectFrom.transform.position, - objectFrom.transform.up, out MyRayHit))
        {
            if (MyRayHit.collider.gameObject == objectHit) hitDirection = HitDirection.Bottom;
        }

        if (Physics.Raycast(objectFrom.transform.position, objectFrom.transform.up, out MyRayHit))
        {
            if (MyRayHit.collider.gameObject == objectHit) hitDirection = HitDirection.Top;
        }

        if (Physics.Raycast(objectFrom.transform.position, objectFrom.transform.right, out MyRayHit))
        {
            if (MyRayHit.collider.gameObject == objectHit) hitDirection = HitDirection.Right;
        }

        if (Physics.Raycast(objectFrom.transform.position, -objectFrom.transform.right, out MyRayHit))
        {
            if (MyRayHit.collider.gameObject == objectHit) hitDirection = HitDirection.Left;
        }

        return hitDirection;


    }



    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitDirection hitDirection = HitDirection.None;
        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if( MyRayHit.collider != null ) {
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