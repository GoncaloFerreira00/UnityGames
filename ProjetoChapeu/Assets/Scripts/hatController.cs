using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Camera cam = new Camera();
    private float maxWidth;
    private bool podesandar;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>(); 
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 CantoSD = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(CantoSD);
        maxWidth = targetWidth.x - GetComponent<Renderer>().bounds.extents.x;
    }

    void FixedUpdate()
    {
        Vector3 rawPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPos = new Vector2(rawPos.x, 0.0f);
        float larguraM = Mathf.Clamp(rawPos.x, -maxWidth, maxWidth);
        targetPos.x = larguraM;
        if (podesandar) rb.MovePosition(targetPos);
    }
        
    public void setPodesandar(bool modo)
    {
        podesandar = modo;
        rb.MovePosition(new Vector2(0.0f, 0.0f));
    }
}
