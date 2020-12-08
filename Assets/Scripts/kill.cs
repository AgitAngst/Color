using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Кто-то есть");
        if (collision.tag == "Score")
        {
            Destroy(collision.gameObject);
        }
       
    }
}
