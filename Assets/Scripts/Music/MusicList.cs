using UnityEngine;
using System.Collections.Generic;

public class MusicList : MonoBehaviour {
    public List<AudioClip> audioList;
    public AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.loop = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (audioList.Count > 1 && !audio.isPlaying)
        {
            audio.clip = audioList[Random.Range(0,audioList.Count)];
            audio.Play();
        }

	}
}
