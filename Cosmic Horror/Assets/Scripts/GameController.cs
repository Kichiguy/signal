using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    public GameObject PauseMenu;
    public GameObject ResumeGameButton;
    public Text GameOverText;
    public Text PauseText;
    public BloopManager bloopManager;
    public AudioSource[] EnvironmentalAudio;

    private bool gameActive;

    private int pauseDelay;
	// Use this for initialization
	void Start () {
        gameActive = true;
        pauseDelay = 0;
        Time.timeScale = 1;
        foreach (AudioSource audio in EnvironmentalAudio) audio.Play();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameActive)
        {
            if (Time.timeScale != 0 && Input.GetKeyDown(KeyCode.Escape)) //When Escape key is pressed, if the game is not paused, the game becomes paused
            {
                PauseGame();
                pauseDelay = 10; //Amount of frames that must pass before the Escape key becomes active again
            }

            if (Time.timeScale == 0 && pauseDelay <= 0 && Input.GetKeyDown(KeyCode.Escape)) ResumeGame(); //When the Escape key is pressed, if the game is paused, the game resumes

            pauseDelay--;
        }
	}

    public void PauseGame() //Pauses time, all sound effects, and brings up the pause menu
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        GameOverText.gameObject.SetActive(false);
        bloopManager.PauseBloops();
        foreach (AudioSource audio in EnvironmentalAudio) audio.Pause();
    }

    public void GameOver() //Pause time, all sound effects, and brings up a modified version of the pause menu
    {
        gameActive = false;
        Time.timeScale = 0;
        bloopManager.PauseBloops();
        foreach (AudioSource audio in EnvironmentalAudio) audio.Stop();
        PauseMenu.SetActive(true);
        PauseText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(true);
        ResumeGameButton.SetActive(false);
    }

    public void ResumeGame() //Resumes time, all sound effects, and closes the pause menu
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        foreach (AudioSource audio in EnvironmentalAudio) audio.UnPause();
        bloopManager.ResumeBloops();
    }

    public void StartGame() //Loads the main scene from the beginning
    {
        SceneManager.LoadScene("ShipCabin");
    }

    public void ReturnToMain() //Returns to the main menu
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void AdvanceButton() //Debug control for advancing the bloop sequence
    {
        bloopManager.ReadTapper(new bool[] { true,false,true,true,true,false,true,false});
    }
   
}
