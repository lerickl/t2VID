using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class personajeController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    public BoxCollider2D plataform;
    public Text scoretext ;
    private int score=0;
    private int vidas=3;
    public Text vidastext ;
    public float fuerzaSalto = 8;
    public float velocidad = 5;
    private bool EstaSaltando = false;
    private bool EstaMuerto = false;
    private bool EstaDestruido = false;

    public bool EstaEnEscalera= false;
    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CORRER = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_SLIDE = 3;
    private const int ANIMATION_PLANEAR = 4;
    private const int ANIMATION_ESCALERA= 5;
    private const int ANIMATION_SHURIKEN=6;
    private const int ANIMATION_MUERE=7;
    private bool estaPlaneando=false;
    public GameObject shurikenDerecha;
    public GameObject shurikenIzquierda;

    private int cont=0;
 
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        scoretext.text = "PUNTAJE: " + score;
        vidastext.text = "Vidas: " + vidas;
        if (EstaMuerto != true & EstaDestruido==false)
        {
            if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
            {
                CambiarAnimacion(ANIMATION_SALTAR);
                Saltar();
                EstaSaltando = true;
            }
            
            else if (Input.GetKey(KeyCode.RightArrow))//Si presiono la tecla rigtharrow voy a ir hacia la derecha
            {
                rb.velocity = new Vector2(velocidad, rb.velocity.y);//velocidad de mi objeto
                CambiarAnimacion(ANIMATION_CORRER);//Accion correr 
                spriteRenderer.flipX = false;//Que mi objeto mire hacia la derecha
                if(Input.GetKey(KeyCode.C))
                {
                    
                    CambiarAnimacion(ANIMATION_SLIDE);
                }
                 if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
                {
                    CambiarAnimacion(ANIMATION_SALTAR);
                    Saltar();
                    EstaSaltando = true;
                }
            }
            
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                CambiarAnimacion(ANIMATION_CORRER);
                spriteRenderer.flipX = true;
                if(Input.GetKey(KeyCode.C))
                {
                    
                    CambiarAnimacion(ANIMATION_SLIDE);
                }
                 if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
                {
                    CambiarAnimacion(ANIMATION_SALTAR);
                    Saltar();
                    EstaSaltando = true;
                }
            //}else if (Input.GetKey(KeyCode.X))
            //{
            //    CambiarAnimacion(ANIMATION_ATAQUE_ESPADA);
            } else 
            {
                CambiarAnimacion(ANIMATION_QUIETO);//Metodo donde mi objeto se va a quedar quieto
                rb.velocity = new Vector2(0, rb.velocity.y);//Dar velocidad a nuestro objeto
            }
            if(Input.GetKey(KeyCode.C))
            {
                
                CambiarAnimacion(ANIMATION_SLIDE);
            }
            if (Input.GetKeyDown(KeyCode.A))
                {

                    if (!spriteRenderer.flipX)
                    {
                        var position = new Vector2(transform.position.x + 1.5f, transform.position.y - .5f);
                        Instantiate(shurikenDerecha, position, shurikenDerecha.transform.rotation);
                    }
                    else
                    {
                        var position = new Vector2(transform.position.x - 2.5f, transform.position.y - .5f);
                        Instantiate(shurikenIzquierda, position, shurikenIzquierda.transform.rotation);
                    }
                    
                }
            if(Input.GetKey(KeyCode.X))
            {
                CambiarAnimacion(ANIMATION_PLANEAR);
                estaPlaneando=true;
                
            }else{estaPlaneando=false;} 
            
             
            
            if(EstaEnEscalera==true)
            {
                CambiarAnimacion(ANIMATION_ESCALERA);
                if(Input.GetKey(KeyCode.UpArrow)){
                    rb.velocity= Vector2.up * 2;
                }
                if(Input.GetKey(KeyCode.DownArrow)){
                    rb.velocity= Vector2.up * -2;
                    
                }
            }

        }
        else if(EstaDestruido==false)
        {
            //CambiarAnimacion(ANIMATION_MORIR);
            //rb.gravityScale = 1f;
            CambiarAnimacion(ANIMATION_MUERE);
            StartCoroutine("esperar");
            Destroy(this.gameObject, 1f);
            EstaDestruido = true; 
            rb.gravityScale = 5;
        }
    }
    IEnumerator esperar(){
        yield return new WaitForSeconds(5);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        EstaSaltando = false;
        if(cont==2){
            if(estaPlaneando==false){
                EstaMuerto=true;
                vidas--;
                }
        }
        cont=0;
         
        /*if (other.gameObject.tag == "Enemy")
        {
            esIntangible = true;
        }*/
    }
    public void IncrementerPuntajeEn10()
    {
        score += 10;
    }
    
    private void Saltar()
    {
        CambiarAnimacion(ANIMATION_SALTAR);
        rb.velocity = Vector2.up * fuerzaSalto;//Vector 2.up es para que salte hacia arriba
    }
    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
     private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "escalera")
        {
            EstaEnEscalera=true;
            rb.gravityScale=0;
            Debug.Log("escaleara activa");
        }

            //IncrementerPuntajeEn5();
    }
     private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("escalera")&&EstaEnEscalera)
        {
            rb.gravityScale=1;
            
            EstaEnEscalera=false;
             
            //controller.EstaEnEscalera = enEscalera;
            //plataform.enabled =true;
            //if(!controller.isGroundder)
        }
    }
    private void OnTriggerEnter2D(Collider2D coll){

        if(coll.gameObject.name=="Square"){
            cont++;
            Debug.Log("Mensaje de inicio"+cont);
        }
    }
}
