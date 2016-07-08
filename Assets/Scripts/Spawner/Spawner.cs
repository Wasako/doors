using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawningObjects = new List<GameObject>();
    public float speed = 500;
    public float minHeigh = -2;
    public float maxHeight = 2;
    public int maxObject = 10;
    public int currentObject = 0;
    public float nextWave = 2;

    //float counter = 0;
    GameObject spawningParent;

	// Use this for initialization
	void Start () {
        spawningParent = new GameObject();
        spawningParent.transform.SetParent( transform, false);
        StartCoroutine("SpawnObj");

    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (currentObject < maxObject && (nextWave < counter||counter==0)) {
            GameObject tempObject;
            tempObject = Instantiate(spawningObjects[Random.Range(0, spawningObjects.Count)], new Vector3(0, Random.Range(minHeigh, maxHeight), 0), transform.rotation) as GameObject;
            tempObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * speed);
            tempObject.transform.SetParent( spawningParent.transform, false);
            tempObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,5));
            currentObject++;

            counter = 0;
        }
        else
        {
            counter += Time.deltaTime; 
        }
        */

	}

    IEnumerator SpawnObj()
    {
        for (;;){
            GameObject tempObject;
            tempObject = Instantiate(spawningObjects[Random.Range(0, spawningObjects.Count)], new Vector3(0, Random.Range(minHeigh, maxHeight), 0), transform.rotation) as GameObject;
            tempObject.GetComponent<Rigidbody2D>().AddForce(-transform.right * speed);
            tempObject.transform.SetParent(spawningParent.transform, false);
            tempObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 5));
            currentObject++;

            yield return new WaitForSeconds(nextWave);
        }
   
    }

    public void Removed() {
        currentObject--;
    }
}
