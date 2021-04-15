using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 15;
    [SerializeField]
    float depth = 100;
    //Animator[] anim;
    Transform tr;
    Rigidbody2D rb;
    SpriteRenderer sp;
    Vector3 mousePos;
    Vector2 direction;
    float posZ = 0; // Posición z
    float diveReach = 0; // Valor absoluto de la z al bucear (para el salto) 
    float alphaValue;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        alphaValue = sp.color.a;
        Cursor.visible = true;
    }

    void Update()
    {

        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
        direction = (mousePos - tr.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        tr.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        direction.Normalize();

        if (Input.GetMouseButton(0) && posZ > -depth)
        {
            if (posZ <= 0)
            {
                posZ -= 1.0f;
                if (tr.localScale.x + (posZ / 1000) > 0.4 && tr.localScale.x + (posZ / 1000) < 1.6)
                {
                    tr.localScale = new Vector3(tr.localScale.x + (posZ / 1000), tr.localScale.y + (posZ / 1000), tr.localScale.z);
                    if (sp.color.a - 0.02 > 0.6) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.02f);
                }
                diveReach = -posZ;
            }
        }
        else
        {
            if (posZ < diveReach)
            {
                posZ += 1.0f;
                if (tr.localScale.x + (posZ / 1000) > 0.4 && tr.localScale.x + (posZ / 1000) < 1.6)
                {
                    tr.localScale = new Vector3(tr.localScale.x + (posZ / 1000), tr.localScale.y + (posZ / 1000), tr.localScale.z);
                    if(sp.color.a + 0.015 < 1.2) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + 0.015f);
                }
            }
            else if (posZ >= diveReach)
            {
                diveReach = 0;
                if (posZ > 0)
                {
                    posZ -= 1.0f;
                    if (posZ < 75 && tr.localScale.x - (posZ / 1000) >= 1)
                    {
                        tr.localScale = new Vector3(tr.localScale.x - (posZ / 1000), tr.localScale.y - (posZ / 1000), tr.localScale.z);
                        if (sp.color.a - 0.03 > alphaValue) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.03f);
                    }
                }
                else
                {
                    posZ = 0;
                    sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alphaValue);
                }
            }
        }

        rb.velocity = new Vector2(direction.x * speed, 0);

        Debug.Log("p " + sp.color.a);
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
