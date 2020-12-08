using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //can't add second component
public class ObstacleMover : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
    [Range(0,1)] [SerializeField] float movementFactor; //0 not move, 1 full move
    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; //grows continuly
        const float tau = Mathf.PI * 2f;
        float rawSineWave = Mathf.Sin(cycles * tau);
        print(rawSineWave);

        movementFactor = rawSineWave / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}
