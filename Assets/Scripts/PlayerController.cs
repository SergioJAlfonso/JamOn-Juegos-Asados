using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 15;
    //Animator[] anim;
    Transform tr;
    Rigidbody2D rb;
    Vector3 mousePos;
    Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        //anim[0] player //anim[1] sword
        //anim = GetComponentsInChildren<Animator>();
        Cursor.visible = true;
    }

    void Update()
    {


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - tr.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, 0);

        //anim[0].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        //anim[1].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
    }

    //Por si queremos modificar la espid
    public void MulSpeed(int x)
    {
        speed *= x;
    }
    public void DivSpeedReset(int x)
    {
        speed /= x;
    }

}
