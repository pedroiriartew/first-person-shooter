using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeScript : MonoBehaviour
{
    private Animator animator;
    private bool scopeActive = false;
    [SerializeField] private GameObject scopeOverlay;// Esta es la referencia de la imagen de la mira.
    private WaitForSeconds waitSecondsOnScope;
    [SerializeField] private float zoom;//Esta es una variable que modifica el FOV de la cámara del sniper(referenciaSniperCamara)
    private float unZoomed;

    [SerializeField] private Camera referenciaMainCamara;
    [SerializeField] private GameObject referenciaSniperCamara;

    private HUD_Script hudReference;
    private void Awake()
    {
        if (referenciaMainCamara == null)
        {
            Debug.LogError("MAIN CAMERA IS NOT WORKING. Call Brent");
        }
        if(referenciaSniperCamara == null)
        {
            Debug.LogError("Al pedo tenés dos cámaras si la referencia no la usas maestro");
        }

        animator = GetComponent<Animator>();

        FindObjectOfType<Player>().OnScope += Scope;

    }


    void Start()
    {
        waitSecondsOnScope = new WaitForSeconds(0.15f);//Estos 0.15 segundos vienen de la duración de la transición.
        hudReference = FindObjectOfType<HUD_Script>();
    }

    /// <summary>
    ///         SCOPE()
    ///         
    ///     Al llamar a Scope(), la variable scopeActive cambia su valor y reproduce la transición entre animaciones.
    ///     
    ///     Cuando es true, Comienza una corrutina que activa la imagen(scopeOverlay), desactiva la cámara que renderiza el arma
    ///     Aumenta el FOV de la cámara principal y guarda el valor que tenía anteriormente en la variable unZoomed.
    ///     
    ///     Nos necesario iniciar una Corrutina a la hora de quitar el scope porque la transición debería ser casi instantanea y el jugador debería quitar de su campo de visión
    ///     el scope y comenzar a ver el arma nuevamente.
    /// 
    /// </summary>
    void Scope()
    {

        scopeActive = !scopeActive;
        animator.SetBool("Scoping?", scopeActive);

        if (scopeActive)
            StartCoroutine(OnScope());
        else
            OnUnescoped();
    }

    IEnumerator OnScope()
    {
        yield return waitSecondsOnScope;

        hudReference.gameObject.SetActive(false);
        scopeOverlay.SetActive(true);

        referenciaSniperCamara.SetActive(false);

        unZoomed = referenciaMainCamara.fieldOfView;
        referenciaMainCamara.fieldOfView = zoom;
    }

    void OnUnescoped()
    {
        hudReference.gameObject.SetActive(true);

        scopeOverlay.SetActive(false);

        referenciaSniperCamara.SetActive(true);
        referenciaMainCamara.fieldOfView = unZoomed;
    }
}
