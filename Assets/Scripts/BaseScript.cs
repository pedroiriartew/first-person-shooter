using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    private HUD_Script hudBar;

    private Game_Manager gmReference;

    private int currentHP;
    [SerializeField] private int maxHP;

    private void Awake()
    {
        hudBar = FindObjectOfType<HUD_Script>();
        gmReference = FindObjectOfType<Game_Manager>();
    }

    private void Start()
    {
        currentHP = maxHP;
        hudBar.SetHealthMax(maxHP);
    }

    public void ReceiveDamage(int dmg)
    {
        currentHP -= dmg;

        hudBar.SetHealthHUD(currentHP);

        Audio_Manager._instance.PlayBaseHit();

        if(currentHP<=0)
        {
            Derrota();
        }
    }

    void Derrota()
    {
        Debug.Log("Derrotatres");
        GameOverScript.GameOverLoad(gmReference.GetActualWave());
    }

}
