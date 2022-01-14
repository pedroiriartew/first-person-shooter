using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    /// <summary>
    /// Para llamar a estas funciones, vamos al Button (de cada función), OnCLick(), elegimos este script en la ventana apropiada y luego elegimos la función. Es necesario previo a esto seleccionar el game object Main Menu.
    /// </summary>

    private void Start()
    {
        Audio_Manager._instance.PlayHorrorThemeMusic();
    }

    //Cambiar Game por Button tiene más sentido, no?...
    public void PlayGame()
    {
        Audio_Manager._instance.StopHorrorThemeMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Build index hace referencia a la cola de Scenes. Entonces básicamente seteamos la escena del Menu (index 1) y hacemos + 1, lo que nos llevaría a MainGameScene
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();//Al "buildear" el juego en un .exe, esto causaría que se cerrase, pero como no sé como corno hacerlo meti un debug.log y será problema en el futuro. Al menos ya está hecho.
    }

}
