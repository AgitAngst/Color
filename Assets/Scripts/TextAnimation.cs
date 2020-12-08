using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private RectTransform rt;

    // Use this for initialization
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();

        // Update is called once per frame
        
    }
    void Update()
    {
        // rt.anchoredPosition += new Vector2(0, 15f * Time.deltaTime);
    
    if(rt.anchoredPosition.y >= 106)
        {
            rt.anchoredPosition += new Vector2(0, 25f * -Time.deltaTime);
            if(rt.anchoredPosition.y <= 78)
            return;
        }
    }


    }

