using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Slider slider;
    private AudioSource aus;

    private void Start()
    {
        Load();
        slider = gameObject.GetComponent<Slider>();
        aus = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        aus.volume = slider.value = SaveLoad.currentMusicVolume;
    }

    private void Update()
    {
        aus.volume = slider.value;
        Save();
    }

    public void ValueChangeCheck()
    {
        SaveLoad.currentMusicVolume = slider.value;
        Debug.Log(SaveLoad.currentMusicVolume);
    }

    public void Save()
    {
        SaveLoad.SaveFile();
    }

    public void Load()
    {
        SaveLoad.LoadFile();
    }

}
