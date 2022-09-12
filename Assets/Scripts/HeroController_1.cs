using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController_1 : MonoBehaviour
{

    [Header("Animation Variables")]                                                             //"Pesta�a" con t�tulo en el Inspector
    [Header("Checker Variables")]                                //Cabecera del ComboBox "Variables"  
    [SerializeField] LayerChecker_1 footA;                  //Instanciamento a la Clase "LayerChecker_1" = footA
    [SerializeField] LayerChecker_1 footB;                  //Instanciamento a la Clase "LayerChecker_1" = footB
    [SerializeField] AnimatorController_1 animatorController;               //Instanciamiento de Clase alias "animatorController"

    [Header("Rigid Variables")]
    [SerializeField] private float jumpForce;               //Agregamos una variable flotante para agrear furza al salto
    
    private Rigidbody2D rigidbody2D_;                       //Variable de instanciamiento
    private bool jumpPressed = false;                       //variable usadas para saber si se apret� la barra espaciadora
                                                            //y es personaje salt�.
    private bool playerIsOnGround;                          //Variable privada tipo Bool, el Heroe esta tocando el piso?

    //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.           


    
    [SerializeField] private float speed_;                  //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
    [SerializeField] private Vector2 movementDirection;     //"SerializeField" significa que desde el inspector podemos  manipular o ver su valor.
                        


    void Start()
    {
        animatorController.Play(AnimationId.Idle);
        rigidbody2D_ = GetComponent<Rigidbody2D>();         //Instanciando la variable.

 
    }

    // Update is called once per frame
    void Update()


    {
        HandleIsGrounding();                                 //Invoca al m�todo "HandleIsGrounding" (El h�roe est� tocando el piso?). 
        HandleJump();                                       //invocando el m�todo "HandleJump" (agregamos furza vertical hacia arriba al Hero)
        HandleControls();                                    //invocando el m�todo "HandleControls" (abre el puerto de entrada del teclado)
        HandleMovement();                                    //invocando el m�todo "HandleMovement" (multiplica el valor de "x" por "speed".
        HandleFlip();                                       //invocando el m�todo "HandleFlip" (rota el personaje a la izquierda o a la derecha)
    }

    void HandleControls()
    {
        jumpPressed = Input.GetButtonDown("Jump");          //asociamos la variable "jumpPressed" a la barra espaciadora
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    }

    void HandleMovement()
    {
        if (playerIsOnGround)
         { 
         if (Mathf.Abs(rigidbody2D_.velocity.x) > 0)                         //comprobamos si se esta moviendo en el eje "X"
         {
             animatorController.Play(AnimationId.Run); //ejecuta en el script AnimatorController_1 el m�todo "Play"
                                                       //mandando dentro de la variable "AnimationAI" el clip "Run"
         }
            else  //de otro modo ejecuta en el script AnimatorController_1 el m�todo "Play"
                  //mandando dentro de la variable "AnimationAI" el clip "Idle"
            {
                animatorController.Play(AnimationId.Idle);
            }
        }
        rigidbody2D_.velocity = new Vector2(movementDirection.x * speed_, rigidbody2D_.velocity.y);

     
    }
    void HandleFlip()
    {
        if (rigidbody2D_.velocity.magnitude > 0)                //S�lo si el personaje se est� moviendo ejecuta estas lineas...
        {
            if (rigidbody2D_.velocity.x >= 0)                           //si la velocidad en "x" es mayor que cero ejecuta la siguiente linea....
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);            //No rotes
            }
            else                                                                            //de otro modo ejecuta las siguientes lineas.....
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);              //rota en "y" 180�
            }
        }
    }
    void HandleJump()           //M�todo para agregarle fuerza la RigidBody del Hero
    {



        if (jumpPressed && playerIsOnGround)
            
        {
             this.rigidbody2D_.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
             animatorController.Play(AnimationId.Idle);
        }
    }

    void HandleIsGrounding()
    {
         playerIsOnGround = footA.isTouching || footB.isTouching; //Falta comentar..........
    }

}
