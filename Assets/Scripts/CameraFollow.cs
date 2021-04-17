using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] float smoothDelay = 0.125f;
    [SerializeField] Vector3 minBound, maxBound;


    void FixedUpdate()
    {
        if (player != null)
        {
            // Coge la posición del jugador
            Vector3 playerPos = new Vector3(player.transform.position.x, 0, -10);

            Vector3 boundPos;

            if (!GameManager.instance.getRecovery())
            {
                boundPos = new Vector3(
                    Mathf.Clamp(playerPos.x, minBound.x, maxBound.x),
                    Mathf.Clamp(playerPos.y, minBound.y, maxBound.y),
                    Mathf.Clamp(playerPos.z, minBound.z, maxBound.z));

                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                Quaternion.Euler(-30, 0, 0),
                Time.deltaTime * 5f);
            }
            else
            {
                boundPos = new Vector3(
                    Mathf.Clamp(playerPos.x, minBound.x, maxBound.x),
                    Mathf.Clamp(playerPos.y, -100, 100),
                    Mathf.Clamp(playerPos.z, minBound.z, maxBound.z));

                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                Quaternion.Euler(30, 0, 0),
                Time.deltaTime * 5f);
            }
            //Chekea los bounds


            // Si el jugador está activo (vivo), la cámara le sigue en un intervalo de tiempo dado por delay
            if (player.gameObject.activeSelf)
                transform.position = Vector3.Lerp(transform.position, boundPos, smoothDelay * Time.fixedDeltaTime);
        }
    }

}
