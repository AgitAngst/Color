using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorObstacles : MonoBehaviour {

    [SerializeField] Component[] spriteColors; // все компоненты
    private Player player;
    int rand;
    Color[] colors = new Color[4];
    SpriteRenderer sr;
    
    // Use this for initialization
    void Start () {
        spriteColors = GetComponentsInChildren<SpriteRenderer>(); //собираем всю дочерние компоненты
        player = GameObject.Find("Player").GetComponent<Player>();

        colors[0] = player.blue;
        colors[1] = player.yellow;
        colors[2] = player.pink;
        colors[3] = player.purple;
     

        setRandomColor();
    }

    public void setRandomColor()
    {
        
        foreach (SpriteRenderer sre in spriteColors)
        {
            //sr = spriteColors[Random.Range(0, spriteColors.Length)].GetComponent<SpriteRenderer>();
            
            sre.color = colors[rand = Random.Range(0, colors.Length)];
            switch (rand)
            {
                case 0:
                    sre.tag = "Blue";
                    break;
                case 1:
                    sre.tag = "Yellow";
                    break;
                case 2:
                    sre.tag = "Pink";
                    break;
                case 3:
                    sre.tag = "Purple";
                    break;
            }
        }



    }
    }
