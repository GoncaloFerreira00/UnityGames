using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public Text txtScore;
    public gameController gc;
    // Start is called before the first frame update
    void Start()
    {
        // score = 0;
        UpdateScore();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        UpdateScore();
        Destroy(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomba"))
        {
            score -= 5;
            UpdateScore();
        }
        if (collision.gameObject.CompareTag("Coracao"))
        {
            gc.ChangeLife(1);
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.CompareTag("Relogio"))
        {
            gc.AddTime(10.0f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Caveira"))
        {
            gc.ChangeLife(-1);
            Destroy(collision.gameObject);
        }

        if(gc.CheckLifes() <= 0){
            gc.GameOver_();
        }
    }
    private void UpdateScore()
    {
        txtScore.text = "Score:\n" + score;
    }

}
