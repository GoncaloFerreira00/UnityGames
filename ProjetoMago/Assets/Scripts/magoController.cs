using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class magoController : MonoBehaviour
{
    public float speed;
    public float movimento;
    private Rigidbody2D rb;
    private bool viradoDireita = true;
    private bool xplode1 = false;
    private bool xplode2 = false;
    private Animator anim;
    public Transform groundCheck;
    public Collision2D where;
    public LayerMask WhatIsGround;
    private float groundRadius = 0.2f;
    private bool grounded;
    private bool doubleJump = false;
    private BoxCollider2D bc;
    //-----
    public GameObject aberta;
    public GameObject fechada;
    public GameObject GameOver;

    private int pedrinhas = 0;

    void Start()
    {
        speed = 10.0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        aberta.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (grounded|| doubleJump))
        {
            if (!grounded) doubleJump = false;
            rb.AddForce(new Vector2(0.0f,500.0f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //baixar o boneco
            anim.SetBool("Abaixar", true);
            bc.size = new Vector2(0.2331447f, 0.16f);
            bc.offset = new Vector2(0.03f, -0.1f);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //subir o boneco
            anim.SetBool("Abaixar", false);
            bc.size = new Vector2(0.2331447f, 0.3355599f);
            bc.offset = new Vector2(0.03f, 0.004f);
        }
    }
    void FixedUpdate()
    {
         movimento = Input.GetAxis("Horizontal");
         anim.SetFloat("Speed", Mathf.Abs(movimento));
         rb.velocity=new Vector2(movimento*speed, rb.velocity.y);
         anim.SetFloat("VSpeed", Mathf.Abs(rb.velocity.y));

         if (rb.velocity.y < -14.0f)
        {
            anim.SetBool("morre", true);
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, 
             groundRadius, WhatIsGround);
        if (grounded)
            doubleJump = true;
         if (!grounded) return;
         if ((viradoDireita && movimento < 0) || (!viradoDireita && movimento > 0)) Flip();

         if (rb.position.y > 3.10){
             xplode1 = true;
         }else if(rb.position.y < -2.40){
            xplode2 = true;
         }else{
             xplode1 = false;
             xplode2 = false; 
         }

        if(xplode1 == true && xplode2 == true){
            Debug.Log("Foi-se");
            GameOver.SetActive(true);
         }
    }

    void Flip()
    {
        viradoDireita = !viradoDireita;
        Vector3 escala;
        escala = transform.localScale;
        escala.x = escala.x*(-1);
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Calhau")
        { 
            Destroy(collision.gameObject);
            pedrinhas++;
            if (pedrinhas == 4)
            {
                fechada.SetActive(false);
                aberta.SetActive(true);
            }
            if (pedrinhas == 5)
            {
                GameOver.SetActive(true);
               
            }
        }
            
    }

}
