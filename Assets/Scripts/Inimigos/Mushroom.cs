using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{

    public float velocidade = 0.01f;
    public float distInicial = -0.5f;
    public float distFinal = 2f;

    public SpriteRenderer ImagemCogumelo;
    // Start is called before the first frame update
    void Start()
    {
        ImagemCogumelo = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Andar();
    }

    void Andar()
    {
        transform.position = new Vector3(transform.position.x + velocidade, transform.position.y, transform.position.z);
        // mudar velocidade 

        //Para trás
        if (transform.position.x > distFinal)
        {
            velocidade = velocidade * -1f;
            ImagemCogumelo.flipX = true;

        }

        //Para frente
        if (transform.position.x < distInicial)
        {
            velocidade = velocidade * -1f;
            ImagemCogumelo.flipX = false;

        }
    }


}


