using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneController : MonoBehaviour {

    public Animation EnterSceneAnimation;
    public GameObject EnterSceneImg;

	// Use this for initialization
	void Start () {
        AnimationEvent animationEvent = new AnimationEvent
        {
            functionName = "PlayOver",
            time = 1.0f
        };
        EnterSceneAnimation.clip.AddEvent(animationEvent);
        EnterSceneAnimation.Play();     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayOver()
    {
        Debug.Log("in");
        EnterSceneImg.gameObject.SetActive(false);
    }
}
