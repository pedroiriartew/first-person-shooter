using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausaScript : MonoBehaviour
{

    public static bool gamePaused = false;
     

    [SerializeField] GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }


    void GetInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                Resume();
            else
                Pause();

        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;// Cuando pausamos le quitamos el LockState al cursor, acá lo volvemos a restringir para seguir jugando como veníamos haciendo
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;//Esto es porque Al saltar desde pause a LoadMenu el timeScale sigue en 0 y queda el menu pausado
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

}
