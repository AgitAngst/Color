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
    [SerializeField] private bool isGameActive = false;
    [SerializeField] private bool isCanControl = true;

    public bool doNotReloadScene;
    public string currentColor;
    public GameObject topPrefab;
    private float _topHeightReached;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] private Rigidbody2D circle;
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
    [SerializeField] private GameObject finishUI;
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
    bool _isInsideObstacke = false;
    private Rigidbody2D rb;
    private ColorObstacle _colorObstacle;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        //Camera.audioMusic.volume = SaveLoad.currentMusicVolume;   #TODO SAVE MUSIC VOLUME
        Load();
        maxScore = SaveLoad.currentScore;
        maxScoreText.text = "Record: " + maxScore.ToString();
        topPrefab.transform.position = new Vector3(-6, SaveLoad.topHeight, 0); 
        SetRandomColor();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("Высота:" +  SaveLoad.topHeight);
        isCanControl = true;
        
    }

    void Update()
    {
        if (isGameActive)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        } 
        PlayerControl();
        rb.simulated = isCanControl;
    }

    private void PlayerControl()
    {

        if (isCanControl)
        {
            isGameActive = true;
            if (Input.GetButton("Jump") || Input.GetMouseButtonDown(0))
            {

                circle.velocity = Vector2.up * jumpForce;
                if (_topHeightReached <= gameObject.transform.position.y)
                {
                    _topHeightReached = gameObject.transform.position.y;

                }
            }

            if (Input.touchCount == 1)
            {
                circle.velocity = Vector2.up * jumpForce;
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
        }
        
        menu();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Finish":
                finishUI.SetActive(true);
                isCanControl = false;
                gameObject.GetComponent<Rigidbody2D>().simulated = false;
                break;
            case "ColorChange":
                if (spawnObstaclesConstantly)
                {
                    currentObstacle = Instantiate(obstacle[Random.Range(0, obstacle.Length)],
                        new Vector2(transform.position.x, transform.position.y + distance),
                        Quaternion.identity);
               
                    rotm = currentObstacle.GetComponentInChildren<rotation>();
                    int randomRotation = Random.Range(0, 1);//передаем рандом в rotation.cs
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
                // Scores(5);
          
            
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
                break;
            case "Score":
                Scores(1);
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }

        if (collision.GetComponent<ColorObstacle>())
        {
            
            if (collision.CompareTag(currentColor) )
            {
                PlaySound(collision.gameObject);
                collision.GetComponentInParent<rotation>().transform.
                    DOShakeScale(.1f, .05f, 0, 100, false);
                //collision.GetComponent<ColorObstacle>().SetColor(blue);
                collision.GetComponent<ColorObstacle>().Initialize();
                //collision.transform.DOShakeScale(.3f, .10f, 5, 50, true); - shake only right color
            }
            else if (collision.CompareTag(currentColor) == false) 
            {
                Save();
                isCanControl = false;
                isGameActive = false;
                if (!doNotReloadScene)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //загружаем и создаем индекс. Нужно подключить using UnityEngine.SceneManagement;
                    Scores(-score);
                    maxScore = SaveLoad.currentScore;
                    maxScoreText.text = "Record: " + maxScore.ToString();
                }
            
            }
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
                float dist = Vector2.Distance(circle.transform.position, currentObstacle.transform.position);
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
        SaveLoad.topHeight = _topHeightReached;
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
