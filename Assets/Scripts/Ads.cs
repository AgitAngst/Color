using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Ads : MonoBehaviour {

    [SerializeField] Text coinText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowAD()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions()
            { resultCallback = HandleResult});
        }
    }

    public void HandleResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Finished");
                break;

            case ShowResult.Failed:
                Debug.Log("Failed");
                break;

            case ShowResult.Skipped:
                Debug.Log("Skipped");
                break;
        }
    }
}
