using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BoatController : MonoBehaviour {

	public Animator boat;
	public GameObject steam;
	public GameObject spout;
	public Light lamp;
	public ParticleSystem sparks;
	public GameObject tentacles;
    public GameController _gameController;

    private bool live;
    private int timer;

	// Use this for initialization
	void Start () {
        live = false;
        timer = 300;
	}
	
	// Update is called once per frame
	void Update () {
		if(live)
        {
            timer--;
        }
        if (timer <= 0) _gameController.GameOver();
	}

	//to be called when invalid message played
	public void OnFailure()
	{
		sparks.Play ();
	}


	//to be called on correct reply
	public void OnReply(){
		boat.SetTrigger ("Reply");
		lamp.GetComponent<Animator>().SetTrigger ("Reply");
	}


	//to be played each time "name" is signalled, with index 1-3 for each time
	public void End(int index){
		boat.SetLayerWeight (1, Mathf.Clamp01 (index * 0.34f));
		boat.SetTrigger ("End");
		if (index >= 2) {
			steam.SetActive(true);
		}
		if (index >= 3) {
			spout.SetActive(true);
			tentacles.SetActive (true);
            live = true;
		}
	}
}
