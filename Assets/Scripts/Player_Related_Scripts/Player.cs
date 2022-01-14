using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables para el movimiento. Los ejes y la velocidad.
    private float z = 0, x = 0;
    private float velocidad = 12f;
    private Vector3 move;// Vector que apunta a la dirección en la que me voy a mover;
    private bool salto = false;

    private CharacterController referenciaCC = null; //El componente CC de nuestro objeto FPSPlayer

    //Vector velocidad, se utiliza para hacer saltar al jugador o aumenta en caso de estar cayendo.
    private Vector3 velocidadGravedad;
    private float gravedad = -19.6f;
    [SerializeField] private float alturaSalto = 4f;

    //GroundCheck distinto a lo que veníamos haciendo.
    [SerializeField] private Transform groundCheck;//Objeto vacío que utiliza un check sphere para revisar si está colisionando o no
    private float groundDistance = 0.4f; // radio de la esfera groundCheck
    [SerializeField] private LayerMask groundMask;//El layer mask que tiene como referencia el Layout Ground
    private bool isGrounded = false;



    public event System.Action OnScope;// Evento que me permite llamar al método Scope() de la clase ScopeScript.
    public event System.Action Shoot;//Evento que me permite llamar a método Disparar() de la clase SniperScript.
    public event System.Action Reload;

    private Animator animatorAWPReference;

    private void Awake()
    {
        referenciaCC = GetComponent<CharacterController>();
        animatorAWPReference = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
    }


    public void Movement()
    {
        referenciaCC.Move(move * velocidad * Time.deltaTime);//Este movimiento se encarga principalmente de los ejes X y Z. El Y es modificado en Jump() y Gravedad()
        JumpAction();
        Gravedad();
    }

    private void GetInput()
    {
        z = Input.GetAxis("Horizontal");//A y D
        x = Input.GetAxis("Vertical");// W y S 
        salto = Input.GetButtonDown("Jump"); // Jump es registrado como SpaceBar, easy peasy.

        animatorAWPReference.SetFloat("Blend", z);//Acá uso la variable Z para hacer los blendings de las animaciones
        Debug.Log(animatorAWPReference.GetFloat("Blend"));

        move = transform.right * z + transform.forward * x; // Right es el eje rojo o X del transform, Forward es el eje azul o Z. La diferencia entre el Transform y el Vector3 es que el primero considera la rotación mientras que el vector3 no.


        if (Input.GetMouseButton(0))//Left Mouse Button--> Disparo
        {
            Shoot?.Invoke();
        }


        if (Input.GetMouseButtonDown(1))//Right Mouse Button--> Scope
        {
            OnScope?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload?.Invoke();
        }
    }

    /// <summary>
    /// JumpAction() toma el input salto y nuestro isGrounded para impulsar al jugador en el eje Y. Esto automáticamente pone en false a isGrounded.
    /// Entonces en Gravedad() ponemos el referenciaCC.Move y le pasamos velocidadGravedad, que es modificada en este método, ejerciendo el impulso entonces
    /// </summary>

    public void JumpAction()
    {
        if (salto && isGrounded)
        {
            velocidadGravedad.y = Mathf.Sqrt(alturaSalto * -2f * gravedad);
        }
    }

    /// <summary>
    ///     GRAVEDAD()
    /// 
    ///     El método Gravedad() utiliza un CheckSphere para detectar colisiones contra objetos cuyo layout sea igual a Ground.
    ///     Para detectar que realmente esté en el suelo, isGrounded debe ser verdadero y la velocidad de la gravedad tiene que ser menor a 0.
    ///     De lo contrario, la velocidadGravedad.y va a sumarse en cada frame (por ser una aceleración) y además va a mover a nuestro player con ese vector cómo parámetro
    ///
    /// 
    /// </summary>


    private void Gravedad()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//CheckSphere crea una esfera invisible alrededor del primer parámetro (groundCheck.position), con un radio determinado (groundDistance) y sólo colisiona con lo que tengamos asignado en groundMask

        if (isGrounded && velocidadGravedad.y < 0)
        {
            velocidadGravedad.y = -2f;//Como el groundCheck tiene un radio de 0.6 más o menos, es posible que detecte que está en el suelo antes de que el jugador realmente lo esté. Por eso un -2, para asegurar que el objeto realmente toque el suelo y no desacelerarlo abruptamente.
        }
        else
        {
            velocidadGravedad.y += gravedad * Time.deltaTime;
            referenciaCC.Move(velocidadGravedad * Time.deltaTime);
        }

    }
}
