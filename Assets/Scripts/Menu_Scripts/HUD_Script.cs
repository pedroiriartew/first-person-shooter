using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Script : MonoBehaviour
{
    private Slider hpSlider;

    [SerializeField] private Text actualWaveText;
    [SerializeField] private Text countDownText;
    [SerializeField] private Text actualAmmoText;

    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
    }

    public void SetHealthHUD(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetHealthMax(int max)
    {
        hpSlider.maxValue = max;
        hpSlider.value = max;
    }

    public void SetCountdownText(float num)
    {
        countDownText.text = num.ToString("0");
    }
    public void SetActualAmmoText(int num)
    {
        actualAmmoText.text = num.ToString();
    }
    public void SetActualWaveText(int num)
    {
        actualWaveText.text = num.ToString();
    }

}
