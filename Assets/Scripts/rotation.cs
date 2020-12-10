using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

    [Range(0,1000)][SerializeField] float rotationSpeed = 100f;
    public bool rotateRight = true;
    float period = 2f;
    
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
           // randomRotate = Random.Range(-2, 2);
        }
        Rotation();
    }

    void Rotation()
    {

        switch (rotateRight)
        {
            case true:
                transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
                break;
            case false:
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                break;
            default:
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                return;

        }

    }

    void ChangeRotation()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; //grows continuly
        const float tau = Mathf.PI * 2f;
        float rawSineWave = Mathf.Sin(cycles * tau);

        transform.Rotate(0,0,rawSineWave / 2f + 0.5f);
    }
}
