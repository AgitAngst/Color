using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Shapes;
using UnityEngine;

public class ColorObstacle : MonoBehaviour, IColor
{
    public Color Blue { get; set; } 
    public Color Pink { get; set; } 
    public Color Yellow { get; set; }
    public Color Purple { get; set; }

    public Color currentColor;

    public void Initialize()
    {
        Blue = new Color(0.2078432f, 0.8862746f, 0.9490197f, 1f);

        Pink = new Color(1f, 0f, 0.5019608f, 1f);

        Yellow = new Color(0.9647059f, 0.8745099f, 0.05490196f, 1f);

        Purple = new Color(0.5490196f, 0.07450981f, 0.9843138f, 1f);


        switch (gameObject.tag)
        {
            case "Blue":
                SetColor(Blue);
                Debug.Log("sadada");
                break;
            case "Yellow":
                SetColor(Yellow);
                break;
            case "Pink":
                SetColor(Pink);
                break;
            case "Purple":
                SetColor(Purple);
                break;
            default:
                break;
                
        }
    }
    
    public void SetColor(Color color)
    {
        if (gameObject.GetComponent<Disc>() != null)
        {
            GameObject o;
            (o = gameObject).GetComponent<Disc>().Color = color;
            if (color == Blue)
            {
                //o.tag = "Blue";
                o.name = "Blue";
                currentColor = Blue;
            }
            if (color == Yellow)
            {
                //o.tag = "Yellow";
                o.name = "Yellow";
                currentColor = Yellow;

            }if (color == Pink)
            {
               // o.tag = "Pink";
                o.name = "Pink";
                currentColor = Pink;

            }if (color == Purple)
            {
               // o.tag = "Purple";
                o.name = "Purple";
                currentColor = Purple;

            }
        }
        
        if (gameObject.GetComponent<Line>() != null)
        {
            gameObject.GetComponent<Line>().Color = color;
        }
    }

    private void Start()
    {
        Initialize();
    }
}
