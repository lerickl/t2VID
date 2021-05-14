using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public personajeController personaje;
     private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        personaje = FindObjectOfType<personajeController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.left * 5;
        spriteRenderer.flipX =true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "shurikenDerecha"||other.gameObject.name == "shurikenIzquierda")
            Destroy(this.gameObject);
            //personaje.IncrementerPuntajeEn10();

    }
}
