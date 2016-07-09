using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {
    public Animator textMenu;
    public Animator introMovie;
    //public Animation introMovie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void introPlay()
    {

        textMenu.SetTrigger("playTrigger");
    }
    public void introContinue()
    {

        textMenu.SetTrigger("continueTrigger");
        Debug.Log("Continue");
    }
    public void startMovie()
    {
        introMovie.enabled = true;
    }
    public void startGame ()
    {
        int i = Application.loadedLevel;
        Application.LoadLevel(i + 1);
    }

}
