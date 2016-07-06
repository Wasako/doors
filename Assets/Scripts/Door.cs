using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public bool Over = false;
	public string DoorLeadsTo = "asdsad";

	//  public float actualDis = 0f;
	void Awake()
	{
		Over=false;
	}


	void Update()
	{

		if(Input.GetKeyDown(KeyCode.UpArrow) && Over){
			Debug.Log("next level");
			SetNextLevel(DoorLeadsTo);
		}
	}


	void SetNextLevel( string name ) {
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
	
	}



	void OnTriggerEnter(Collider coll)
	{
		Debug.Log("moved over");
		if (coll.gameObject.tag == "Player"    ) {
			Debug.Log("moved over");
			Over=true;
			//minDis = (coll.gameObject.transform.position - gameObject.transform.position).magnitude;
		}
	}


	void OnTriggerExit(Collider  coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			Over=false;

	

		}
	}




}

