using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class CoolBotController : MonoBehaviour
{
    [SerializeField]
    bool corriente = false;
    [SerializeField]
    float fuerzaCorriente = 1.0f;

    public bool isHead = false;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float depth = 15;
    [SerializeField]
    float maxDif = 1.2f;
    public Transform nextPiece;
    [SerializeField]
    float amplitude = 0.5f;
    [SerializeField]
    float coleteo = 2.0f;
    //Animator[] anim;
    Transform tr;
    Rigidbody2D rb;
    SpriteRenderer sp;
    Vector3 mousePos;
    Vector3 direction;
    float posZ = 0; // Posición z
    float diveReach = 0; // Valor absoluto de la z al bucear (para el salto) 
    float alphaValue;
    float minDistance = 0.6f;
    float distance;
    [SerializeField]
    float RotationSpeed = 1.0f;
    [SerializeField]
    float anguloColeteo = 60.0f;

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
        if (isHead)
        {
            mousePos = GetComponent<CoolCarpaBotFollow>().destino;
            //mousePos.y = 3;
            //mousePos.x = mousePos.x + Mathf.Sin((Time.time * coleteo)) * amplitude;

        }
        else {
            mousePos = nextPiece.position; 
        }
        distance = Vector3.Distance(mousePos, transform.position);
        direction = (mousePos - tr.position);

        if (direction.x >= maxDif) direction.x = maxDif;
        else if (direction.x <= -maxDif) direction.x = -maxDif;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        //if (angle < maxAngle && angle > -maxAngle)
        if (nextPiece == null)
            tr.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(new Vector3(0, 0, angle + (anguloColeteo * Mathf.Sin((Time.time * coleteo))))),
                Time.deltaTime * RotationSpeed);
        else
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

        if (distance > minDistance)
        {
            // rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            if (nextPiece == null)
                rb.velocity = transform.right * speed;
            else
            {
                rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            }
            if (corriente)
                rb.velocity = rb.velocity + (Vector2.down * fuerzaCorriente);


        }
        else
        {
            //que lerpea la velocidad a 0
            //rb.velocity = Vector2.Lerp(rb.velocity,new Vector2(0,0), 0.5f); ;

        }


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
