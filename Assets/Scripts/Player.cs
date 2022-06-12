using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Shapes;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public bool doNotReloadScene;
    public string currentColor;
    public GameObject topPrefab;
    private float topHeightReached;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Rigidbody2D Circle;
    [SerializeField] Disc playerColor;
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
    GameObject previousObstacle;
    public bool spawnObstaclesConstantly;
    public bool spawnColoChangerConstantly;
    [SerializeField] GameObject colorChanger;
    [SerializeField] float distance = 4f;
    private rotation rotm;
    private int obstacleCount;
    public AudioClip sound;
    private AudioSource audioSource;
    private Touch touch;
    bool isInsideObstacke = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        //Camera.audioMusic.volume = SaveLoad.currentMusicVolume;   #TODO SAVE MUSIC VOLUME
        Load();
        maxScore = SaveLoad.currentScore;
        maxScoreText.text = "Record: " + maxScore.ToString();
        topPrefab.transform.position = new Vector3(-6, SaveLoad.topHeight, 0); 
        SetRandomColor();
        
        Debug.Log("Высота:" +  SaveLoad.topHeight);

    }

    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        if (Input.GetButton("Jump") || Input.GetMouseButtonDown(0))
        {
            Circle.velocity = Vector2.up * jumpForce;

            if (topHeightReached <= gameObject.transform.position.y)
            {
                topHeightReached = gameObject.transform.position.y;

            }
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
           // Scores(5);
           if (spawnObstaclesConstantly)
           {
               currentObstacle = Instantiate(obstacle[Random.Range(0, obstacle.Length)],
                   new Vector2(transform.position.x, transform.position.y + distance),
                   Quaternion.identity);
               
               rotm = currentObstacle.GetComponentInChildren<rotation>();
               int randomRotation = Random.Range(0, 2);//передаем рандом в rotation.cs
               if (rotm.enableRandomRotation)
               {
                   switch (randomRotation)
                   {
                       case 0:
                           rotm.rotateRight = true;
                           break;
                       case 1:
                           rotm.rotateRight = false;
                           break;
                       default:
                           break;
                   }
               }
           }
            
            if (spawnColoChangerConstantly)
            {
                Instantiate(colorChanger, new Vector2(transform.position.x, 
                        currentObstacle.transform.position.y),
                    transform.rotation);
            }

            scoreText.text = "Score: " + score.ToString();
            audioSource.PlayOneShot(sound, 1f);
          
            SetRandomColor();

            
            Destroy(collision.gameObject);

            


            
            

            //currentObstacle.AddComponent<rotation>();
            return;
        }
        /*if (collision.tag == "Obstacle")
        {
             //Scores(1);
            collision.GetComponent<CircleCollider2D>().enabled = false;
            isInsideObstacke = true;
            previousObstacle = collision.gameObject;
            obstacleCount++;
            Debug.Log(collision.name);
            //collision.transform.DOShakeScale(.3f, .25f, 10, 50, true);
 

        }*/
        
        if (collision.tag == "Score")
        {
          Scores(1);
             Destroy(collision.gameObject);
        }


        else if (collision.CompareTag(currentColor) == false) 
        {
            Save();
            if (!doNotReloadScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //загружаем и создаем индекс. Нужно подключить using UnityEngine.SceneManagement;
                Scores(-score);
                maxScore = SaveLoad.currentScore;
                maxScoreText.text = "Record: " + maxScore.ToString();
            }
            
        }
        if (collision.tag == currentColor)
        {
            PlaySound(collision.gameObject);
            collision.GetComponentInParent<rotation>().transform.
                DOShakeScale(.1f, .05f, 0, 100, false);

            //collision.transform.DOShakeScale(.3f, .10f, 5, 50, true); - shake only right color
        }

        


    }

    public void SetColor(int color)
    {
        //int rand = Random.Range(0, 4);

        switch (color)
        {
            case 0:
                currentColor = "Blue";
                playerColor.Color = blue;
                break;
            case 1:
                currentColor = "Yellow";
                playerColor.Color = yellow;
                break;
            case 2:
                currentColor = "Pink";
                playerColor.Color = pink;
                break;
            case 3:
                currentColor = "Purple";
                playerColor.Color = purple;
                break;
        }


    }

    public void SetRandomColor()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                currentColor = "Blue";
                playerColor.Color = blue;
                break;
            case 1:
                currentColor = "Yellow";
                playerColor.Color = yellow;
                break;
            case 2:
                currentColor = "Pink";
                playerColor.Color = pink;
                break;
            case 3:
                currentColor = "Purple";
                playerColor.Color = purple;
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

    void PlaySound(GameObject target)
    {
        var audioSource = target.GetComponentInParent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
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
        SaveLoad.topHeight = topHeightReached;
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
