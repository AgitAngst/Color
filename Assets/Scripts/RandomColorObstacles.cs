using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class RandomColorObstacles : MonoBehaviour {
    
    public bool enableRandomColors = false;
    public Disc[] elementColors;
    private Player player;
    Color[] colors = new Color[4];

    void Start () {
        player = GameObject.Find("Player").GetComponent<Player>();

        colors[0] = player.blue;
        colors[1] = player.yellow;
        colors[2] = player.pink;
        colors[3] = player.purple;
        setRandomColor();
    }

    public void setRandomColor()
    {
        if (enableRandomColors)
        {

            foreach (Disc colors in elementColors)
            {
                int rnd = Random.Range(0, this.colors.Length);

                colors.Color = this.colors[rnd];

                switch (rnd)
                {
                    case 0:
                        colors.tag = "Blue";
                        player.SetColor(0);
                        break;
                    case 1:
                        colors.tag = "Yellow";
                        player.SetColor(1);

                        break;
                    case 2:
                        colors.tag = "Pink";
                        player.SetColor(2);

                        break;
                    case 3:
                        colors.tag = "Purple";
                        player.SetColor(3);

                        break;
                }
            }
        }
        



    }
    }
