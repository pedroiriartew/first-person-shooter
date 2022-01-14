using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    private static int wave;

    [SerializeField] private GameObject winGO;
    [SerializeField] private GameObject loseGO;

    [SerializeField] private Text loseText;

    public static void GameOverLoad(int actualWave)
    {
        wave = actualWave;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void Start()
    {
        Audio_Manager._instance.PlayHorrorThemeMusic();
        Cursor.lockState = CursorLockMode.None;
        loseGO.SetActive(false);
        winGO.SetActive(false);
        HUDInfo();
    }

    private void HUDInfo()
    {
        if( wave !=11)
        {
            loseGO.SetActive(true);
            loseText.text = "We´ve survived " + wave.ToString() + " waves of 7";
        }
        else
        {
            winGO.SetActive(true);
        }
        
    }
    public void LoadMenu()
    {        
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
