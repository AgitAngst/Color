using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

    [SerializeField] float rotationSpeed = 100f;
    public float randomRotate = 1;
    [SerializeField] float rotateTimer = 3f;
    [SerializeField] float period = 2f;
    
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
           // randomRotate = Random.Range(-2, 2);
        }
        setRandomRotation();
    }

    void setRandomRotation()
    {
        rotateTimer -= Time.deltaTime;

        switch ((int)randomRotate)
        {
            case 0:
                transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                break;
            case 1:
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                break;
            default:
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
                return;

        }

       Debug.Log(randomRotate);
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
