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
    float posZ = 0; // Posición z
    float diveReach = 0; // Valor absoluto de la z al bucear (para el salto) 
     
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

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
        direction = (mousePos - tr.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        direction.Normalize();

        if (Input.GetMouseButton(0))
        {
            if (posZ <= 0)
            {
                posZ -= 1.0f;
                diveReach = -posZ;
            }
        }
        else 
        {
            if (posZ < diveReach)
                posZ += 1.0f;
            else if (posZ >= diveReach)
            {
                diveReach = 0;
                if (posZ > 0)
                    posZ -= 1.0f;
                else
                    posZ = 0;
            }
        }

        rb.velocity = new Vector2(direction.x * speed, 0);
        Debug.Log("p " + posZ);
        //anim[0].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
        //anim[1].SetFloat("Speed", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
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
