using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Personagem : MonoBehaviour
{
    /// <summary>
    /// Variáveis
    /// </summary>
    /// Pega o componente Rigidbody2D
    public Rigidbody2D Corpo;

    /// recebe a velocidade do personagem
    public float velocidade;

    /// Pega o componente SpriteRenderer 
    public SpriteRenderer ImagemPersonagem;

    /// quantidade de pulo que meu perso realizou
    public int qtd_pulo = 0;

    //Controlar quandoi posso pular novamente
    private float meuTempoPulo = 0;

    //bool que me diz se posso pular
    public bool pode_pular = true;

    //Vida do personagem
    public int vida = 10;
    private float meuTempoDano = 0;
    private bool pode_dano = true;

    //Barra de hp
    private Image BarraHp;

    //Moedas
    public int Moedas = 0;
    //Moeda coletada e texto
    private Text Moeda_Texto;


    void Start()
    {
        BarraHp = GameObject.FindGameObjectWithTag("hp_barra").GetComponent<Image>();
        Moeda_Texto = GameObject.FindGameObjectWithTag("MoedaTexto").GetComponent<Text>();
    }


    void Update()
    {
        Mover();
        Pular();
        Apontar();
        Dano();
    }


    ///Responsavel por mover o personagem
    void Mover()
    {
        velocidade = Input.GetAxis("Horizontal") * 8;
        Corpo.velocity = new Vector2(velocidade, Corpo.velocity.y);
    }
    /// Responsável por apotar para onde o personagem está se movendo
    void Apontar()
    {
        if (velocidade > 0)
        {
            ImagemPersonagem.flipX = false;
        }
        else if (velocidade < 0)
        {
            ImagemPersonagem.flipX = true;
        }
    }

    void Pular()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && pode_pular == true)
        {
            pode_pular = false;
            qtd_pulo++;
            if (qtd_pulo <= 1)
            { 
                AcaoPulo();
            }
        }
        if (pode_pular == false)
        {
            TemporizadorPulo();
        }
    }

    void AcaoPulo()
    {
        //Zera velocidade de queda para o pulo
        Corpo.velocity = new Vector2(velocidade, 0);
        //Adiciona força para pular
        Corpo.AddForce(transform.up * 380f);
    }

    //Gatilhos
    void OnTriggerEnter2D(Collider2D gatilho)
    {
        if (gatilho.gameObject.tag == "Pisavel")
        {
            qtd_pulo = 0;
            pode_pular = true;
            meuTempoPulo = 0;
        }

        if (gatilho.gameObject.tag == "moeda")
        {
            Destroy(gatilho.gameObject);
            Moedas++;
            Moeda_Texto.text = Moedas.ToString();
        }
    }

    void TemporizadorPulo()
    {
        meuTempoPulo += Time.deltaTime;
        if (meuTempoPulo > 0.5f)
        {
            pode_pular = true;
            meuTempoPulo = 0;
        }
    }

    //Dano
    void Dano()
    {
        if (pode_dano == false)
        {
            TemporizadorDano();
        }
    }

    //Verifica dano
    //Colisão física
    void OnCollisionStay2D(Collision2D colisao)
    {
        if (colisao.gameObject.tag == "Inimigo")
        {
            if (pode_dano == true)
            {
                vida--;
                PerderHp();
                pode_dano = false;
                ImagemPersonagem.color = UnityEngine.Color.red;
            }
        }
    }

    void TemporizadorDano()
    {
        meuTempoDano += Time.deltaTime;
        if (meuTempoDano > 0.5f)
        {
            pode_dano = true;
            meuTempoDano = 0;
            ImagemPersonagem.color = UnityEngine.Color.white;
        }
    }

    void PerderHp()
    {
        int vida_parabarra = vida * 10;
        BarraHp.rectTransform.sizeDelta = new Vector2(vida_parabarra, 30);
    }
}



