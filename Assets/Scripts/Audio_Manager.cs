using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager _instance = null;

    [SerializeField] private AudioSource reloadSound;
    [SerializeField] private AudioSource rifleShotSound;
    [SerializeField] private AudioSource explosionSound;

    [SerializeField] private AudioSource horrorThemeMusic;

    [SerializeField] private AudioSource baseHitSound;

    private void Awake()
    {
        _instance = GetInstance();
    }

    public Audio_Manager GetInstance()
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

    public void PlayReloadSound()
    {
        reloadSound.Play();
    }

    public void PlayRifleShotSound()
    {
        rifleShotSound.Play();
    }

    public void PlayExplosionSound()
    {
        explosionSound.Play();
    }

    public void PlayHorrorThemeMusic()
    {
        horrorThemeMusic.Play();
    }

    public void PlayBaseHit()
    {
        baseHitSound.Play();
    }

    public void StopHorrorThemeMusic()
    {
        horrorThemeMusic.Stop();
    }

}
