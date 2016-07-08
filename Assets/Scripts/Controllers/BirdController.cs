using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {
    float counter;
    Vector2 startPosition;
    public float sinMultiple = 4;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        counter = (counter + Time.deltaTime*sinMultiple*2) % (Mathf.PI * 2);
        transform.position = new Vector2(transform.position.x, startPosition.y + Mathf.Sin(counter)/sinMultiple);
	    
	}
}
