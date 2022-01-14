using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{

    [SerializeField] private int damage = 100;
    [SerializeField] private float fireRate = 1.20f;//Ratio de disparo. A mayor fireRate, más rápido dispara el arma.

    private float proximoDisparo = 0f;// Esta variable se utiliza como bandera entre disparos.

    [SerializeField] private int municionMax = 8;
    private int municiónActual;
    [SerializeField] private float tiempoRecarga = 2.1f;
    private WaitForSeconds waitSecondsOnReload;
    private bool isReloading = false;
    private Animator animatorC;

    private HUD_Script hudReference;


    [SerializeField] private Camera referenciaMainCamara;

    private Pool_Flare poolClass;

    [SerializeField] private LayerMask layerReference;

    private void Awake()
    {
        if(referenciaMainCamara==null)
        {
            Debug.Log("This is not working. Call Brent");
        }

        animatorC = GetComponent<Animator>();
        poolClass = GetComponent<Pool_Flare>();

        FindObjectOfType<Player>().Shoot += Disparar;
        FindObjectOfType<Player>().Reload += Recargar;
    }

    private void Start()
    {
        municiónActual = municionMax;
        waitSecondsOnReload = new WaitForSeconds(tiempoRecarga);// waitSecondOnReload guarda un New WaitForSeconds para no tener que estar creando espacio en memoria todo el tiempo

        hudReference = FindObjectOfType<HUD_Script>();
        hudReference.SetActualAmmoText(municionMax);
    }


    private void Update()
    {
        if(municiónActual<=0)
        {
            Recargar();
        }
    }

    /// <summary>
    /// Recargar() se asegura que no se esté recargando en ese momento.
    /// Luego inicia una corrutina que retrasa un par de segundos la actualización de la municion y el false del reloading
    /// </summary>

    void Recargar()
    {
        if(!isReloading)
        {

            isReloading = true;
            poolClass.RecoverGO();//Recupero todos los GO activos y los devuelvo al cementerio
            animatorC.SetBool("Reloading?", isReloading);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        Audio_Manager._instance.PlayReloadSound();
        yield return waitSecondsOnReload;
        isReloading = false;
        animatorC.SetBool("Reloading?", isReloading);
        municiónActual = municionMax;
        hudReference.SetActualAmmoText(municiónActual);
    }

    void Disparar()
    {
        if (Time.time >= proximoDisparo && municiónActual!=0 && !MenuPausaScript.gamePaused)
        {
            proximoDisparo = Time.time + 1f / fireRate;// esta cuenta me asegura que tenga que pasar cierto tiempo entre los disparos, aunque el jugador mantenga presionado el botón de disparar. Simplemente al tiempo que pasó desde que se inició la aplicación le sumo 1 dividido mi fireRate, para poder tenér un margen en el que el jugador no puede disparar.

            municiónActual--;

            hudReference.SetActualAmmoText(municiónActual);
            Audio_Manager._instance.PlayRifleShotSound();
            
            if (Physics.Raycast(referenciaMainCamara.transform.position, referenciaMainCamara.transform.forward, out RaycastHit hit, Mathf.Infinity, layerReference))
            {
                /* flare son las partículas que se spawnean con cada disparo*/
                GameObject flare = poolClass.RequestGO();
                flare.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));//LookRotation crea una determinada rotación al pasarle una dirección (un Vector3)

                Enemy target = hit.transform.GetComponent<Enemy>();

                if(target!=null)
                {
                    Debug.Log("Lastimando Cubos");
                    target.ReceiveDmg(damage);
                }

            }
        }
        
    } 
}
