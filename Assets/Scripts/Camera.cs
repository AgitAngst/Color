
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour {

    [SerializeField] Transform Player;
    public static AudioSource audioMusic;
    private int rand;
    [SerializeField] string musicPath = "Music/";



    // Use this for initialization
    void Start () {
        
        audioMusic = GetComponent<AudioSource>();
        PlayMusic();
       // audioMusic.volume = slider.value;
       
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Player.position.y,transform.position.z);
        }
        /*if (audioMusic.volume < 0.2f)
        {
            audioMusic.volume += 0.01f * Time.deltaTime;
        }*/

        
    }
    public void PlayMusic()
        {
            rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    audioMusic.clip = Resources.Load<AudioClip>(musicPath + "POL-aurora-borealis-short");
                    audioMusic.Play();
                    break;
                case 1:
                    audioMusic.clip = Resources.Load<AudioClip>(musicPath + "POL-change-short");
                    audioMusic.Play();
                    break;
                case 2:
                    audioMusic.clip = Resources.Load<AudioClip>(musicPath + "POL-goodbye-short");
                    audioMusic.Play();
                    break;
                case 3:
                    audioMusic.clip = Resources.Load<AudioClip>(musicPath + "POL-symmetries-short");
                    audioMusic.Play();
                    break;
                default:
                    audioMusic.clip = Resources.Load<AudioClip>(musicPath + "POL-aurora-borealis-short");
                    audioMusic.Play();
                    return;

            }
        }
}
