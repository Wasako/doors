using UnityEngine;
using System.Collections;

public class Bodypart : MonoBehaviour {
    public string bodyName = ""; 

	// Use this for initialization
	void Start () {
        if (GameManager.GetSingleton().IsStuffOwned(bodyName))
        {
            Destroy(gameObject);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetSingleton().AddStuff(bodyName);
            Destroy(this.gameObject);
        }
    }

}
