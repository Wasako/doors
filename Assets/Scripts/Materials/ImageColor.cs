using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour {
    
    SpriteRenderer currentMaterial;
    MusicController musicController;
    static public float speed = -1;
    public bool changeColor = true;
    public Color additionalColorValue = new Color(0.0f, 0.0f, 0.0f);
    public Color removingColorValue = new Color(0.0f, 0.0f, 0.0f);
    public bool isInverted = false;
    public float alphaColor = 0.2f;
    public bool isRGB = true;

	// Use this for initialization
	void Start () {
	    currentMaterial = GetComponent<SpriteRenderer>();
        musicController = GameObject.Find("Audio").GetComponent<MusicController>();

	}
	
	// Update is called once per frame
	void Update () {
        if(changeColor) {
            if (isInverted && isRGB)
            {
                currentMaterial.color = new Color(1 - musicController.red + additionalColorValue.r - removingColorValue.r,
                    1 - musicController.green + additionalColorValue.g - removingColorValue.g,
                    1 - musicController.blue + additionalColorValue.b - removingColorValue.b);
            }
            else if(isRGB) 
            {
                currentMaterial.color = new Color(musicController.red + additionalColorValue.r - removingColorValue.r,
                    musicController.green + additionalColorValue.g - removingColorValue.g,
                    musicController.blue + additionalColorValue.b - removingColorValue.b);
            }
            else if (isInverted)
            {
                float h = (1 - musicController.red + musicController.green + musicController.blue) % 1.0f;
                float s = additionalColorValue.r + additionalColorValue.g + additionalColorValue.b;
                float v = 1 - removingColorValue.r - removingColorValue.g - removingColorValue.b;
                currentMaterial.color = Color.HSVToRGB(h, s, v);

            } else {

                float h = (musicController.red + musicController.green + musicController.blue) % 1.0f;
                float s = additionalColorValue.r + additionalColorValue.g + additionalColorValue.b;
                float v = 1 - removingColorValue.r - removingColorValue.g - removingColorValue.b;
                currentMaterial.color = Color.HSVToRGB(h, s, v);

            }
            currentMaterial.color = new Color(currentMaterial.color.r, currentMaterial.color.g, 
                currentMaterial.color.b, alphaColor);
        }

	}

}
