using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed;
    private GameObject focalPoint;
    public bool hasPowerUp;
    public float PowerUpStrenght = 50.0f;
    public GameObject powerUpIndicator;
    public bool morto = false;
    public TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        TextMesh textObject = GameObject.Find("txt").GetComponent<TextMesh>();
        textObject.text = "";
        speed = 5.0f;
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!morto)
        {
            float forwardInput = Input.GetAxis("Vertical");
            playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

            if (hasPowerUp)
                powerUpIndicator.transform.position = transform.position + new Vector3(0.0f, -0.5f, 0.0f);

            if (playerRb.position.y < -10.0f)
            {
                Destroy(gameObject);
                TextMesh textObject = GameObject.Find("txt").GetComponent<TextMesh>();
                textObject.text = "Game Over";
                morto = true;
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCount());
            Destroy(other.gameObject);
        }
    }

    public IEnumerator PowerUpCount()
    {
        yield return new WaitForSeconds(10.0f);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromMe = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayFromMe * PowerUpStrenght, ForceMode.Impulse);
        }
    }
}
