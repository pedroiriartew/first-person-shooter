using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Flare : MonoBehaviour
{
    [SerializeField] private GameObject objetoPool;
    [SerializeField] private int cantMax;
    [SerializeField] private GameObject[] arrayPool;
    private Vector3 cementerio;

    private void Awake()
    {
        cementerio = new Vector3(-1000, -1000, -1000);
        InstanciarPool();
    }

    private void InstanciarPool()
    {
        arrayPool = new GameObject[cantMax];


        for (int i = 0; i < arrayPool.Length; i++)
        {
            arrayPool[i] = Instantiate(objetoPool);
            arrayPool[i].transform.position = cementerio;
            arrayPool[i].SetActive(false);
        }
    }

    public GameObject RequestGO()
    {
        for (int i = 0; i < arrayPool.Length; i++)
        {
            if (!arrayPool[i].activeSelf)
            {
                arrayPool[i].SetActive(true);

                return arrayPool[i];
            }
        }

        return null;
    }

    public void RecoverGO()
    {
        for(int i = 0; i < arrayPool.Length; i++)
        {
            arrayPool[i].transform.position = cementerio;
            arrayPool[i].SetActive(false);
        }
    }

}
