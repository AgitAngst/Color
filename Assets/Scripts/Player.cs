﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Player : MonoBehaviour {
    [SerializeField] string currentColor;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Rigidbody2D Circle;
    [SerializeField] SpriteRenderer sr;
    public Color blue;
    public Color yellow;
    public Color pink;
    public Color purple;
    [SerializeField] int score = 0;
    [SerializeField] int maxScore = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Text floatingScoreText;
    [SerializeField] Text maxScoreText;
    [SerializeField] GameObject[] obstacle;
    [SerializeField] GameObject currentObstacle;
    [SerializeField] GameObject colorChanger;
    [SerializeField] float distance = 4f;
    private rotation rotm;
    private int obstacleCount;
    public AudioClip sound;
    private AudioSource audioSource;
    private Touch touch;

    void Start() {
        setRandomColor();
        audioSource = GetComponent<AudioSource>();
        Camera.audioMusic.volume = SaveLoad.currentMusicVolume;
        Load();
        maxScore = SaveLoad.currentScore;
        maxScoreText.text = "Record: " + maxScore.ToString();


    }

    void Update() {

        if (Input.GetButton("Jump") || Input.GetMouseButtonDown(0))
        {
            Circle.velocity = Vector2.up * jumpForce;
        }

        if (Input.touchCount == 1)
        {
            Circle.velocity = Vector2.up * jumpForce;
            return;
        }

        /*if (touch.phase == TouchPhase.Began)
        {
            Circle.velocity = Vector2.up * jumpForce;
            return;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            return;
        }*/


        menu();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ColorChange")
        {
            setRandomColor();
            Instantiate(colorChanger, new Vector2(transform.position.x, currentObstacle.transform.position.y), transform.rotation);
            //Destroy(colorChanger.gameObject);
            Scores(5);
            scoreText.text = "Score: " + score.ToString();
            audioSource.PlayOneShot(sound, 1f);

            Destroy(collision.gameObject);

            return;
        }
        if (collision.tag == "Score")
        {
            Scores(1);


            collision.GetComponent<CircleCollider2D>().enabled = false;
            // Destroy(collision.gameObject);
            currentObstacle = Instantiate(obstacle[Random.Range(0, obstacle.Length)], new Vector2(transform.position.x, transform.position.y + distance), Quaternion.identity);
            obstacleCount++;
            Debug.Log(currentObstacle);
            rotm = GameObject.Find(currentObstacle.name).GetComponent<rotation>();
            rotm.randomRotate = Random.Range(0, 1);//передаем рандом в rotation.cs
                                                   //currentObstacle.AddComponent<rotation>();
            return;
        }

        if (collision.tag != currentColor)
        {
            Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //загружаем и создаем индекс. Нужно подключить using UnityEngine.SceneManagement;
            Scores(-score);
            maxScore = SaveLoad.currentScore;
            maxScoreText.text = "Record: " + maxScore.ToString();
        }


    }


    void setRandomColor()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                currentColor = "Blue";
                sr.color = blue;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = yellow;
                break;
            case 2:
                currentColor = "Pink";
                sr.color = pink;
                break;
            case 3:
                currentColor = "Purple";
                sr.color = purple;
                break;
        }


    }

    public void Scores(int i)
    {
        score += i;
        if (maxScore <= score)
        {
            maxScore = score;
            maxScoreText.text = "Record: " + maxScore.ToString();

        }

        scoreText.text = "Score: " + score.ToString();
        maxScoreText.text = "Record: " + maxScore.ToString();
    }

    void chanceToSpawn()
    {

    }

    void FindDuplicates()
    {

        for (int i = 0; i <= obstacleCount; i++)
        {
            if (currentObstacle != null)
            {
                float dist = Vector2.Distance(Circle.transform.position, currentObstacle.transform.position);
                if (dist >= 9 & currentObstacle.tag == "Score")
                {
                    Destroy(currentObstacle.gameObject, 0);
                    Debug.Log("Расстояние:" + dist);
                    obstacleCount--;
                }
            }
        }
        Debug.Log(obstacleCount);
    } // Не работает

    void Save()
    {
        SaveLoad.currentScore = maxScore;
        SaveLoad.currentTimePlayed += Time.time;
        SaveLoad.totalScore += score;
        //SaveLoad.currentMusicVolume =;
        //SaveLoad.currentSoundVolume =;
        SaveLoad.SaveFile();
    }
    void Load()
    {
        SaveLoad.LoadFile();
        //Camera.audioMusic.volume = SaveLoad.currentMusicVolume;
    }

    void menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
