using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EyeController : MonoBehaviour {
    public RectTransform image;
    public Vector2 dimentionX = new Vector2(0,1);
    public float delay = 2;
    public float counter = 0;
    public float eyecounter = 0;
    bool isOpening = true;

	// Use this for initialization
	void Start () {
        dimentionX = new Vector2(image.anchorMin.x, image.anchorMax.x);
	}
	
	// Update is called once per frame
	void Update () {

        if (counter > delay)
        {
            if(isOpening) {
                image.anchorMin.Set(Mathf.Lerp(image.anchorMin.x, dimentionX.x-(dimentionX.y - dimentionX.x)/2, Time.deltaTime), image.anchorMin.y);
                image.anchorMax.Set(Mathf.Lerp(image.anchorMax.x, dimentionX.y-(dimentionX.y - dimentionX.x)/2, Time.deltaTime), image.anchorMin.y);
            }
            else if (!isOpening)
            {
                image.anchorMin.Set(Mathf.Lerp(image.anchorMin.x, dimentionX.x, Time.deltaTime), image.anchorMin.y);
                image.anchorMax.Set(Mathf.Lerp(image.anchorMax.x, dimentionX.y, Time.deltaTime), image.anchorMin.y);
                if (image.anchorMax.x >= dimentionX.y)
                {
                counter = 0;
                }
            }

        }
        else
        {
            counter += Time.deltaTime;
        }

	}
}
