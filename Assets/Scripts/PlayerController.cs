using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float depth = 15;
    [SerializeField]
    float maxDif = 1.2f;
    [SerializeField]
    Transform nextPiece;
    [SerializeField]
    float amplitude = 0.5f;
    [SerializeField]
    float coleteo = 2.0f;
    //Animator[] anim;
    Transform tr;
    Rigidbody2D rb;
    SpriteRenderer sp;
    Vector3 mousePos;
    Vector2 direction;
    float posZ = 0; // Posición z
    float diveReach = 0; // Valor absoluto de la z al bucear (para el salto) 
    float alphaValue;

    const float maxAngle = 30;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        alphaValue = sp.color.a;

        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        if (nextPiece == null)
        {
            mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
            mousePos.y = 3;
            mousePos.x = mousePos.x + Mathf.Sin((Time.time * coleteo)) * amplitude;

        }
        else {
            mousePos = nextPiece.position; 
        }
        direction = (mousePos - tr.position);

        if (direction.x >= maxDif) direction.x = maxDif;
        else if (direction.x <= -maxDif) direction.x = -maxDif;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        if (angle < maxAngle && angle > -maxAngle)
            tr.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        direction.Normalize();

        //propongo hacer un lerpeo ajajaja

        if (Input.GetMouseButton(0) && tr.position.z >= 0) //profundidad maxima
        {            
            if(tr.position.z + 0.25f <= depth) tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z + 0.25f);
            /*if (tr.localScale.x + (posZ / 2000) > 0.8 && tr.localScale.x + (posZ / 2000) < 1.3)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y * 0.989f, tr.position.z);
                tr.localScale = new Vector3(tr.localScale.x + (posZ / 2000), tr.localScale.y + (posZ / 1000), tr.localScale.z);
            }*/
            if (sp.color.a - 0.02 > 0.6) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.02f);
            diveReach = -tr.position.z;
        }
        else if (tr.position.z > diveReach / 3)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z - 0.5f);
            if (tr.position.z <= diveReach / 3)
                diveReach = 0;
            if (sp.color.a + 0.02 < 1.2) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + 0.02f);
        }


        else if(tr.position.z < 0)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z + 0.25f);
            if (tr.position.z >= 0)
                tr.position = new Vector3(tr.position.x, tr.position.y, 0);
            if (sp.color.a - 0.02 > 0.85) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.02f);
            else sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.85f);
        }
            //if (posZ < diveReach) //subida y bajada
            //{
            //    posZ += 1.0f;
            //    if (tr.localScale.x + (posZ / 2000) > 0.8 && tr.localScale.x + (posZ / 2000) < 1.3)
            //    {
            //        tr.position = new Vector3(tr.position.x, tr.position.y / 0.989f, tr.position.z);
            //        tr.localScale = new Vector3(tr.localScale.x + (posZ / 2000), tr.localScale.y + (posZ / 1000), tr.localScale.z);
            //        if (sp.color.a + 0.015 < 1.2) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + 0.015f);
            //    }
            //}
            //else if (posZ >= diveReach) //cuando llega al maximo de arriba
            //{
            //    diveReach = 0; //reseteo de la profundidad
            //    if (posZ > 0)
            //    {
            //        posZ -= 1.0f;
            //        if (posZ < 75 && tr.localScale.x - (posZ / 2000) >= 1)
            //        {
            //            tr.position = new Vector3(tr.position.x, tr.position.y * 0.989f, tr.position.z);
            //            tr.localScale = new Vector3(tr.localScale.x - (posZ / 2000), tr.localScale.y - (posZ / 1000), tr.localScale.z);
            //            if (sp.color.a - 0.03 > alphaValue) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.03f);
            //        }
            //    }
            //    else
            //    {
            //        posZ = 0;
            //        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, alphaValue);
            //    }
            //}
        rb.velocity = new Vector2(direction.x * speed, 0);


        if (Input.GetMouseButton(1) && posZ > -depth)
        {

        }
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
