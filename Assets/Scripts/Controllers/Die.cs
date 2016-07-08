using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {
    public bool testDie = false;
    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(testDie && other.tag == "Player") {
            other.transform.position = startPosition;
        }
        else if (other.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}
