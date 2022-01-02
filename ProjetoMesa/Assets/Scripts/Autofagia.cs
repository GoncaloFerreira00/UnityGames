using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autofagia : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cubo")){
            Destroy(collision.gameObject);
        } 
    
    }
}

