using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    private float timer =4f;
    private float deltaT = 0;
    private int _amountEnemies = 0;

    public void SpawnEnemies(int amountEnemies)
    {
        _amountEnemies = amountEnemies;
    }


    private void Update()
    {
        deltaT += Time.deltaTime;
        if (_amountEnemies == 0 && deltaT < timer) return;
        

        if (deltaT >= timer)
        {        
           int pickedEnemy = Random.Range(1, 4);
           float randomSpawn = Random.Range(-5f, 5f);

            GameObject gameObjSpawned = EnemyFactory._instance.RequestEnemy(pickedEnemy);
            gameObjSpawned.transform.position = transform.position + (transform.right * randomSpawn);

            _amountEnemies--;
            deltaT = 0;
        }

    }

}