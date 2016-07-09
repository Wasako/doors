using UnityEngine;
using System.Collections;

public class TextureMover : MonoBehaviour {
    float textureMove;
    Material currentMaterial;
    MusicController musicController;
    static public float speed = -1;
    public bool changeColor = true;
    public bool xAxis = false;
    public bool yAxis = true;
    public Color additionalColorValue = new Color(0.0f, 0.0f, 0.0f);
    public Color removingColorValue = new Color(0.0f, 0.0f, 0.0f);
    public bool isInverted = false;
    public bool isRGB = true;
    public float hp = 1;

	// Use this for initialization
	void Start () {
	    currentMaterial = GetComponent<MeshRenderer>().material;
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
                float s = additionalColorValue.r + additionalColorValue.g + additionalColorValue.b - 
                    removingColorValue.r - removingColorValue.g - removingColorValue.b;
                float v = hp;
                currentMaterial.color = Color.HSVToRGB(h, s, v);

            } else {

                float h = (musicController.red + musicController.green + musicController.blue) % 1.0f;
                float s = additionalColorValue.r + additionalColorValue.g + additionalColorValue.b - 
                    removingColorValue.r - removingColorValue.g - removingColorValue.b;
                float v = hp;
                currentMaterial.color = Color.HSVToRGB(h, s, v);

            }

        }

        textureMove += Time.deltaTime*speed;
        if (xAxis || yAxis)
        {
            currentMaterial.SetTextureOffset("_MainTex", new Vector2( textureMove, textureMove));
        }
        else if (xAxis)
        {
            currentMaterial.SetTextureOffset("_MainTex", new Vector2( textureMove, 0));
        }
        else if (yAxis)
        {
            currentMaterial.SetTextureOffset("_MainTex", new Vector2( 0, textureMove));
        }

        if (textureMove > 0.2f)
        {
            textureMove -= 0.2f;
        }

	}
}
