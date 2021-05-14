using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject Jugador;

    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Jugador != null)
        {
            _transform.position = new Vector3(Jugador.transform.position.x, Jugador.transform.position.y, _transform.position.z/*, _transform.position.z*/);//Mover la camara en posicion x,y,z el jugador se mueva en x, en y y z no lo estoy haciendo quehaga nada
        }

    }
}
