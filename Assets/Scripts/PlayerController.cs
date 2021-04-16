using UnityEngine;
//Maneja el movimiento en las 4 direcciones
//Tambien acciona las animaciones correspondiente a cada direccion
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float depth = 10;
    [SerializeField]
    float maxDif = 1.2f;
    [SerializeField]
    GameObject sombra;
    public Transform nextPiece;
    [SerializeField]
    float amplitude = 0.5f;
    [SerializeField]
    float coleteo = 2.0f;
    //Animator[] anim;
    Transform tr;
    Rigidbody2D rb;
    SpriteRenderer sp;
    Transform sTr;
    Rigidbody2D sRb;
    SpriteRenderer sSp;
    Vector3 mousePos;
    Vector2 direction;
    float diveReach = 0; // Valor absoluto de la z al bucear (para el salto) 
    float timeAtTop;
    float initY;
    const float maxAngle = 30;
    const float timeToDrop = 0.35f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();

        sTr = sombra.GetComponent<Transform>();
        sRb = sombra.GetComponent<Rigidbody2D>();
        sSp = sombra.GetComponent<SpriteRenderer>();

        Cursor.visible = true;

        initY = tr.position.y;
    }

    void FixedUpdate()
    {
        if (nextPiece == null)
        {
            mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
            mousePos.y = initY+4;
            mousePos.x = mousePos.x + Mathf.Sin((Time.time * coleteo)) * amplitude;

        }
        else
        {
            mousePos = nextPiece.position;
        }
        direction = (mousePos - tr.position);

        if (direction.x >= maxDif) direction.x = maxDif;
        else if (direction.x <= -maxDif) direction.x = -maxDif;

        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        if (angle < maxAngle && angle > -maxAngle)
        {
            tr.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            sTr.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        direction.Normalize();

        if (Input.GetMouseButton(0) && tr.position.z >= 0) //profundidad maxima
        {
            if (tr.position.z + 0.25f <= depth)
            {
                tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z + 0.25f);
                sTr.position = new Vector3(sTr.position.x, sTr.position.y, sTr.position.z + 0.2f);
            }
            if (sp.color.a - 0.02 > 0.6) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.02f);
            if (sSp.color.a + 0.02 < 0.4) sSp.color = new Color(sSp.color.r, sSp.color.g, sSp.color.b, sSp.color.a + 0.01f);
            diveReach = -tr.position.z;
        }
        else if (tr.position.z > diveReach / 4)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z - 0.25f);
            sTr.position = new Vector3(sTr.position.x, sTr.position.y, sTr.position.z - 0.2f);
            if (tr.position.z <= diveReach / 4)
            {
                diveReach = 0;
                timeAtTop = Time.time;
            }
            if (sp.color.a + 0.02 < 1.2) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a + 0.02f);
            if (sSp.color.a - 0.04 > 0.1) sSp.color = new Color(sSp.color.r, sSp.color.g, sSp.color.b, sSp.color.a - 0.005f);
        }
        else if (tr.position.z < 0 && Time.time - timeAtTop >= timeToDrop)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, tr.position.z + 0.15f);
            sTr.position = new Vector3(sTr.position.x, sTr.position.y, sTr.position.z + 0.12f);
            if (tr.position.z >= 0)
                tr.position = new Vector3(tr.position.x, tr.position.y, 0);
            if (sp.color.a - 0.02 > 0.85) sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - 0.02f);
            else sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.85f);
            if (sSp.color.a + 0.04 < 0.25) sSp.color = new Color(sSp.color.r, sSp.color.g, sSp.color.b, sSp.color.a + 0.01f);            
        }

        rb.velocity = new Vector2(direction.x * speed, 0);
        sRb.velocity = new Vector2(direction.x * speed, 0);
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
