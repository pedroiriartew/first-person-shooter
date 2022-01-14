using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    private float timer = 0;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        LoadMenu();
    }


    void LoadMenu()
    {
        if(timer >= 3f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
