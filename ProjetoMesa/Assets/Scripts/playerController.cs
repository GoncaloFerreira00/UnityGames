using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject cubo;
    private float Speed=20.0f;
    int cubos = 0;
    int maxCubos = 200;
    bool grounded = true;
    public int points;
    public TextMeshPro txt;
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 movimento = new Vector3(moveH, 0, moveV);

        rb.AddForce(movimento * Speed);
        if (cubos <= 0) GerarCubos(++maxCubos);

        if(Input.GetKeyDown("space") && grounded == true){
            grounded = false;
            rb.AddForce(0, 8 * Speed,0);
        }

        if(rb.position.y < 0.6){
            grounded = true;
        }   

        
                
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        cubos--;
        points = points + 1;
        // Debug.Log("Morto");
        txt.text = "" + points;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cubo")) {
            Destroy(collision.gameObject);
        }
    }

    void GerarCubos(int quantos)
    {
        for (int i=0; i< quantos; i++)
        {
            Vector3 randomposition = new Vector3(Random.Range(-9.0f, 9.0f), 0.5f, Random.Range(-9.0f, 9.0f));
            Instantiate(cubo, randomposition, Quaternion.identity);
        }
        cubos = quantos;
    }
}
