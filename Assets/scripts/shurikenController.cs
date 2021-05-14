using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenController : MonoBehaviour
{
    // Start is called before the first frame updatepublic float velocityX = 10f;
    private Rigidbody2D rb;
    public float velocityX = -10f;
    private personajeController PersonajeController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PersonajeController = FindObjectOfType<personajeController>();
        
        Destroy(gameObject, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * velocityX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "zombi")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            PersonajeController.IncrementerPuntajeEn10();
        }
    }
}
