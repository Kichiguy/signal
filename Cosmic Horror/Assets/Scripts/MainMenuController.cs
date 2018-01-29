using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject MainScreen;
    public GameObject CreditsScreen;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ShipCabin");
    }

    public void Credits()
    {
        MainScreen.SetActive(false);
        CreditsScreen.SetActive(true);
    }

    public void ReturnToMain()
    {
        MainScreen.SetActive(true);
        CreditsScreen.SetActive(false);
    }
}
