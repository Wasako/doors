using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
    /* Divide frequency for 1028 bits
     * 
     * Frequency:
     * 20 - 100 Bass/Kick main frequency
     * 1 500 - 2 000 Voice
     * 200 - 500 warm color
     * 1 000 - 5 000 Mixed voice, green color
     * 10 000 - 20 000 determine cold sound, but is almost 0 into, so better take from 7 000 to 20 000, blue color
     * 
     * Bits calculated:
     * 1 - 5 Bass
     * 77 - 102 Voice
     * 10 - 26 Warm
     * 51 - 256 Middle
     * 358 - 1024 Cold
     * 
    */
    public AudioSource audio;
    float[] spectrum = new float[1024];
    public float multiplyRed = 8.5f / 7;
    public float multiplyGreen = 1.7f/5;
    public float multiplyBlue = 2.7f/5;
    public float bass;
    public float voice;
    public float red;
    public float green;
    public float blue;

    // Use this for initialization
	void Start () {
        audio.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
    void Update()
    {

        bass = 0;
        spectrum = audio.GetSpectrumData( spectrum.Length, 0, FFTWindow.BlackmanHarris);
        for (int i = 1; i < 6; i++)
        {
            bass += spectrum[i];
        }
        voice *= 2;
        //print("Bass " + testData);

        voice = 0;
        for (int i = 77; i < 103; i++)
        {
            voice += spectrum[i];
        }
        voice *= 5;
        //print("Voice "+testData);

        red = 0;
        for (int i = 10; i < 27; i++)
        {
            red += spectrum[i];
        }
        red *= multiplyRed;
        //print("Red " + testData);

        green = 0;
        for (int i = 51; i < 257; i++)
        {
            green += spectrum[i];
        }
        green *= multiplyGreen;
        //print("Green " + testData);

        blue = 0;
        for (int i = 358; i < 1024; i++)
        {
            blue += spectrum[i];
        }
        blue *= multiplyBlue;
        //print("Blue " + testData);
	
	}
}
