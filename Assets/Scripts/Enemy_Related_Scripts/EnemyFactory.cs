using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory _instance = null;
    [SerializeField] private GameObject[] enemyArray; 

    private void Awake()
    {
        _instance = GetInstance();
    }

    public EnemyFactory GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(_instance);
            _instance = this;
        }
        return _instance;
    }

    public GameObject RequestEnemy(int enemy)
    {
        
        GameObject goMesh;

        switch (enemy)
        {
            case 1:
                goMesh = Instantiate(enemyArray[0], Vector3.zero, Quaternion.identity);
                goMesh.AddComponent<Enemy_Normal>();                
                break;
            case 2:                
                goMesh = Instantiate(enemyArray[1], Vector3.zero, Quaternion.identity);
                goMesh.AddComponent<Enemy_Explosive>();
                break;
            case 3:
                goMesh = Instantiate(enemyArray[2], Vector3.zero, Quaternion.identity);
                goMesh.AddComponent<Enemy_Heavy>();                
                break;
            default:
                goMesh = null;
                break;
        }

        return goMesh;
    }
}
