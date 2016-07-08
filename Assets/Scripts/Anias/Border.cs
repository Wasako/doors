using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {
    /*
    we are using property with a private setter to make sure 
    that visible is depends only on the visiblility of the object o the screen
    */
    public bool Visible { get; private set; }
    /*
    The compiler translates line above  onto something like this:
    private bool visible;

    public bool Visible
    {
        get { return visible; }
        private set { visible = value; }
    }
    */

    void OnBecameVisible()
    {
        Visible = true;
    }
    void OnBecameInvisible()
    {
        Visible = false;
    }
}
