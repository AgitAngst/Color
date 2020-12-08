using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Load();
        gameObject.GetComponent<AudioSource>().volume = SaveLoad.currentMusicVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load()
    {
        SaveLoad.LoadFile();
    }
}
