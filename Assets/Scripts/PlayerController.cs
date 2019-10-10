using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    private Animator anim;
    private Rigidbody2D rb2d;


    public Transform posPe;
    [HideInInspector] public bool tocaChao = false;
    [HideInInspector] public bool jump;
    [HideInInspector] private bool atacando;

    public float Velocidade;
    public float ForcaPulo = 1000f;
    [HideInInspector] public bool viradoDireita = true;


    public Image vida;
    private MensagemControle MC;

    public Transform firePoint;
    public GameObject esquerdaTiro;
    public GameObject direitaTiro;


    Stack<bool> armaspilha = new Stack<bool>();


    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        firePoint = transform.Find("FirePoint");

        GameObject mensagemControleObject = GameObject.FindWithTag("MensagemControle");
        if (mensagemControleObject != null)
        {
            MC = mensagemControleObject.GetComponent<MensagemControle>();
        }
    }


    // Update is called once per frame
    void Update()
    {

        tocaChao = Physics2D.Linecast(transform.position, posPe.position, 1 << LayerMask.NameToLayer("chao"));

        if (Input.GetKeyDown("space") && tocaChao)
        {
            jump = true;

        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            atacando = true;

        }
    }


    void FixedUpdate()
    {
        float translationY = 0;
        float translationX = Input.GetAxis("Horizontal") * Velocidade;
        transform.Translate(translationX, translationY, 0);
        transform.Rotate(0, 0, 0);
        if (translationX == 0 || !tocaChao)
        {
            anim.SetTrigger("parado");
        }
        else
        {
            anim.SetTrigger("corre");
        }


        if (jump)
        {
            anim.SetTrigger("pula");
            rb2d.AddForce(new Vector2(0f, ForcaPulo));
            jump = false;
        }

        if (atacando)
        {
            anim.SetTrigger("atirar");
            atacando = false;

            if (viradoDireita)
            {
                Instantiate(direitaTiro, firePoint.position, Quaternion.identity);
            }

            if (!viradoDireita)
            {
                Instantiate(esquerdaTiro, firePoint.position, Quaternion.identity);
            }

        }


        /*if (atacando && armado2)
        {
            anim.SetTrigger("atirar");
            atacando = false;

            if (viradoDireita)
            {
                Instantiate(direitaTiro, firePoint.position, Quaternion.identity);
            }

            if (!viradoDireita)
            {
                Instantiate(esquerdaTiro, firePoint.position, Quaternion.identity);
            }

        }*/

        //Programar o pulo Aqui! 

        if (translationX > 0 && !viradoDireita)
        {
            Flip();
        }
        else if (translationX < 0 && viradoDireita)
        {
            Flip();
        }
    }


    void Flip()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }


    public void SubtraiVida()
    {
        vida.fillAmount -= 0.1f;
        if (vida.fillAmount <= 0)
        {
            MC.GameOver();
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "arma1")
        {


        }
    }

}


