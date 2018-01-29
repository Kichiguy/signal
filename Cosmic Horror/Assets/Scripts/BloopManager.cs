using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloopManager : MonoBehaviour {

    public int BloopCooldown;
    public AudioClip[] Bloops;
    public AudioSource Big;
    public AudioClip Yes;
    public AudioClip No;
    public AudioClip Final;
    public AudioSource Massive;
    public AudioSource Crash;
    public AudioSource Arise;
    public int reponseTime;

    public Text AudioDebugText;
    public Text KeyWordDebugText;
    public Text ExpectedDebugText;
    public GameController gameController;
    public BoatController boatController;
    public bool paused;
    public static bool[] currentAnswer;

    private int currentBloop;
    private AudioSource bloop;
    private int initialCooldown;
    private bool[] behindTheCurtain;
    private int Hastur;
    private bool AskQuestion;

    //Tap dictionary 
    //private enum Taps { HelloQuestion, HelloAnswer, WhatIsYourName };
    private bool[] Hello = { true, true, true, false, true, true, false, true };
    private bool[] Tomorrow = { true, false, true, false, true, true, true, true };
    private bool[] Name = { true, false, true, true, true, false, true, false };
    private bool[] Secret = { true, true, true, false, true, true, true, false };
    private bool[] Letters = { true, true, true, true, false, true, false, true };
    private bool[] Sane = { true, true, false, true, true, true, true, false };
    private bool[] Dance = { true, true, false, true, true, true, false, true };
    private bool[] Ship = { true, false, true, false, true, false, true, true };
    //private Dictionary<bool[], Taps> TapDictionary;

    //private Taps ExpectedAnswer;
    private bool[] ExpectedAnswer;

    // Use this for initialization
    void Start ()
    {
        Hastur = 1;
        AskQuestion = false;
        ExpectedAnswer = Hello;
        ExpectedDebugText.text = DebugText(ExpectedAnswer);
        //TapDictionary = new Dictionary<bool[], Taps>();
        currentBloop = 0;
        paused = false;
        //TapDictionary.Add(HelloQuestionTaps, Taps.HelloQuestion);
        //TapDictionary.Add(HelloAnswerTaps, Taps.HelloAnswer);
        //ExpectedAnswer = Taps.HelloQuestion;
        initialCooldown = BloopCooldown;
        BloopCooldown = 250; //This is a frame count before the bloop will play again, starting from when the bloop begins playing
        bloop = GetComponent<AudioSource>();
        bloop.clip = Bloops[currentBloop];
        AudioDebugText.text = Bloops[currentBloop].name.ToString();
        KeyWordDebugText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (BloopCooldown <= 0 && !bloop.isPlaying) //Checks to see if the current bloop in the cycle should play
        {
            bloop.Play();
            BloopCooldown = initialCooldown;
        }

        if (paused == false && !bloop.isPlaying && bloop.clip != Bloops[currentBloop]) //Checks to see if the bloop should change and, if so, changes
        {
            bloop.clip = Bloops[currentBloop];
            AudioDebugText.text = Bloops[currentBloop].name.ToString();
        }
        if(Time.timeScale > 0) BloopCooldown--;

        if (currentAnswer != null)
        {
            behindTheCurtain = currentAnswer;
            ReadTapper(behindTheCurtain);
        }
        currentAnswer = null;
    }

    public void PauseBloops() //Pauses bloop and sets a Paused state that is checked by Update functions
    {
        if (bloop.isPlaying)
        {
            bloop.Pause();
            paused = true;
        }
    }

    public void ResumeBloops() //Unpauses bloop and set a Paused state that is checked by Update functions
    {
        if(paused)
        {
            paused = false;
            bloop.Play();
        }
    }

    public void AdvanceBloop()
    {
        if (ExpectedAnswer == Hello)
        {
            bloop.Stop();
            boatController.OnReply();
            Big.Play();
            ExpectedAnswer = Tomorrow;
            currentBloop++;
            BloopCooldown = reponseTime;
            ExpectedDebugText.text = DebugText(ExpectedAnswer);
        }
        else if (ExpectedAnswer == Tomorrow)
        {
            boatController.OnReply();
            Big.Play();
            bloop.Stop();
            BloopCooldown = reponseTime;
            AskQuestion = true;
            ExpectedAnswer = null;
            currentBloop++;
            ExpectedDebugText.text = "Ask a Question";
        }
    }
    
    private void AskAQuestion(bool[] tapCode)
    {
        string toCheck = DebugText(tapCode);

        bloop.Stop();
        switch (toCheck)
        {
            case "11101110":
                bloop.Stop();
                Big.Play();
                BloopCooldown = reponseTime;
                AnswerYes();
                ExpectedDebugText.text = "Yes";
                break;
            case "11110101":
                bloop.Stop();
                Big.Play();
                BloopCooldown = reponseTime;
                AnswerNo();
                ExpectedDebugText.text = "No";
                break;
            case "11011110":
                bloop.Stop();
                Big.Play();
                BloopCooldown = reponseTime;
                AnswerYes();
                ExpectedDebugText.text = "Yes";
                break;
            case "11011101":
                bloop.Stop();
                Big.Play();
                BloopCooldown = reponseTime;
                AnswerYes();
                ExpectedDebugText.text = "Yes";
                break;
            case "10101011":
                bloop.Stop();
                Big.Play();
                BloopCooldown = reponseTime;
                AnswerNo();
                ExpectedDebugText.text = "No";
                break;
            default:
                boatController.OnFailure();
                ExpectedAnswer = Tomorrow;
                ExpectedDebugText.text = DebugText(Tomorrow);
                currentBloop--;
                boatController.OnReply();
                BloopCooldown = 100;
                break;
        }
    }
    
    public void ReadTapper(bool[] tapCode)
    {
        string expectedResult = null;
        string actualResult = DebugText(tapCode);

        if (ExpectedAnswer != null) expectedResult = DebugText(ExpectedAnswer);
        if (expectedResult == actualResult) AdvanceBloop();
        else if (actualResult == DebugText(Name))
        {
            
            if (Hastur <= 2)
            {
                boatController.End(Hastur);
                Big.Play();
            }
            if (Hastur == 2)
            {
                boatController.End(Hastur);
                bloop.Stop();
                bloop.clip = Final;
                bloop.Play();
                Crash.Play();
            }
            if (Hastur > 2)
            {
                Arise.Play();
                StartCoroutine(Delay());
            }
            Hastur++;
        }
        else if (ExpectedAnswer == null) AskAQuestion(tapCode);
        else boatController.OnFailure();
        KeyWordDebugText.text = DebugText(tapCode);

    }

    IEnumerator Delay()
    {
       
        yield return new WaitForSeconds(2f);
        Massive.Play();
        boatController.End(Hastur);
    }

    private void AnswerYes()
    {
        bloop.PlayOneShot(Yes);
        BloopCooldown = 400;
        boatController.OnReply();
    }

    private void AnswerNo()
    {
        bloop.PlayOneShot(No);
        BloopCooldown = 400;
        boatController.OnReply();
    }

    private string DebugText(bool[] ToParse)
    {
        string returnValue = "";
        foreach(bool item in ToParse)
        {
            if (item) returnValue = returnValue + "1";
            else returnValue = returnValue + "0";
        }
        return returnValue;
    }
}
