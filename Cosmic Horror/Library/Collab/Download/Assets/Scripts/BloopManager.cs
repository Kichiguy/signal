using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloopManager : MonoBehaviour {

    public int BloopCooldown;
    public AudioClip[] Bloops;
    public AudioClip Tap;

    private int currentBloop;
    private AudioSource bloop;

	// Use this for initialization
	void Start () {
        currentBloop = 0;
        bloop = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BloopCooldown <= 0)
        {
            bloop.PlayOneShot(Bloops[currentBloop]);
        }
	}
}
