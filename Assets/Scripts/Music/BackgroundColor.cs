using UnityEngine;
using System.Collections;

public class BackgroundColor : MonoBehaviour {
    MusicController musicController;
    public Color additionalColorValue = new Color(0.0f, 0.0f, 0.0f);
    public Color removingColorValue = new Color(0.0f, 0.0f, 0.0f);
    public bool isInverted = false;
    Color currentColor;

	// Use this for initialization
    void Start()
    {
        musicController = GameObject.Find("Audio").GetComponent<MusicController>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isInverted)
        {
            currentColor = new Color(1 - musicController.red + additionalColorValue.r - removingColorValue.r,
                1 - musicController.green + additionalColorValue.g - removingColorValue.g,
                1 - musicController.blue + additionalColorValue.b - removingColorValue.b);
        }
        else
        {
            currentColor = new Color(musicController.red + additionalColorValue.r - removingColorValue.r,
                musicController.green + additionalColorValue.g - removingColorValue.g,
                musicController.blue + additionalColorValue.b - removingColorValue.b);
        }
        GetComponent<Camera>().backgroundColor = currentColor;

	}
}
