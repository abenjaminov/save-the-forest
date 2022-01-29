using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Credits;
    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnCredits()
    {
        Credits.SetActive(true);
    }

    public void OnCloseCredits()
    {
        Credits.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
