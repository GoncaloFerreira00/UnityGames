using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public Camera cam = new Camera();
    private float maxWidth;
    public GameObject[] balls;
    public float timeLeft;
    public Text txtTimeLeft;
    public GameObject txtGameOver;
    public GameObject SplashScreen;
    public GameObject startButton;
    public hatController hc;
    private bool playing;
    public GameObject[] coracoes;
    private int nvidas;

    void Start()
    {
        nvidas = 3;
        SplashScreen.SetActive(true);
        startButton.SetActive(true);
        hc.setPodesandar(false);
        txtGameOver.SetActive(false);
        if (cam == null) cam = Camera.main;
        Vector3 CantoSD = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(CantoSD);
        maxWidth = targetWidth.x - balls[0].GetComponent<Renderer>().bounds.extents.x;
        timeLeft = 25.0f;
        playing = false;
        
    }

    public void StartGame()
    {
        SplashScreen.SetActive(false);
        startButton.SetActive(false);
        playing = true;
        UpdateVidas();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        hc.setPodesandar(true);
        yield return new WaitForSeconds(2.0f);
       
        while (timeLeft>0)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth),
                                     transform.position.y, 0.0f);
            GameObject ball = balls[Random.Range(0, balls.Length)];
            Instantiate(ball, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
        txtGameOver.SetActive(true);
        hc.setPodesandar(false);
        playing = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
                txtTimeLeft.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
            }
            else{
                    SceneManager.LoadScene(1);
                    timeLeft = 25.0f;
            }
        }
    }
    public void ChangeLife(int numero)
    {
        nvidas += numero;
        nvidas = Mathf.Clamp(nvidas, 0, 3);
        UpdateVidas();
    }

    public int CheckLifes(){
        return nvidas;
    }

    public void GameOver_(){
        txtGameOver.SetActive(true);
        hc.setPodesandar(false);
    }

    public void AddTime(float tempo)
    {
        timeLeft += tempo;
        if (timeLeft < 0) timeLeft = 0;
        txtTimeLeft.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }

    public void UpdateVidas()
    {
      for(int i=0; i<3; i++)
        {
            if (i <= nvidas - 1) coracoes[i].SetActive(true);
            else coracoes[i].SetActive(false);
        }
    }


}
