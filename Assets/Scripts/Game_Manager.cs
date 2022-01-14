using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private const int finalWave = 7;
    [SerializeField] private int actualWave = 1;

    private float startingTimer = 10f;
    [SerializeField] private float currentTime = 0f;

    [SerializeField] private int enemyCount = 0;
    private int enemiesToSpawn = 0;

    private bool enemiesDisabled = false;

    private SpawnerScript spawnerInstance = null;

    private HUD_Script hudReference;

    private void Start()
    {
        currentTime = startingTimer;

        enemiesToSpawn = enemyCount;

        spawnerInstance = FindObjectOfType<SpawnerScript>();        

        hudReference = FindObjectOfType<HUD_Script>();
        hudReference.SetActualWaveText(actualWave);
        hudReference.SetCountdownText(currentTime);
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        hudReference.SetCountdownText(currentTime);

        if(currentTime <= 0 || enemiesDisabled)
        {
            enemiesDisabled = false;
            IncreaseActualWaveAndEnemyCount();

            hudReference.SetActualWaveText(actualWave);
            spawnerInstance.SpawnEnemies(enemyCount);
        }               
    }

    private void IncreaseActualWaveAndEnemyCount()
    {
        actualWave++;

        if (actualWave == 1)
        {
            currentTime = startingTimer;
            enemiesToSpawn += 3;
            enemyCount = enemiesToSpawn;
        }
        else
        {
            if (actualWave <= 3)
            {
                currentTime = startingTimer + 10f;
                enemiesToSpawn += actualWave;
                enemyCount = enemiesToSpawn;

            }
            else
            {
                if (actualWave < finalWave)
                {
                        currentTime = startingTimer + 25f;
                        enemiesToSpawn += actualWave;
                        enemyCount = enemiesToSpawn;
                }
                else
                {
                        if (actualWave == finalWave)
                        {
                            currentTime = 999f;
                            enemiesToSpawn += actualWave;
                            enemyCount = enemiesToSpawn;
                        }
                        else
                        {
                            GameOverScript.GameOverLoad(actualWave);
                        }
                }

            }
        }
    }


    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public int GetActualWave()
    {
        return actualWave;
    }
    public int GetFinalWave()
    {
        return finalWave;
    }

    public void DeadEnemies()
   {
        enemyCount--;

        if (enemyCount <= 0)
        {
            enemiesDisabled = true;
        }
   }

}
