using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] float smoothDelay = 0.125f;
    [SerializeField] Vector3 minBound, maxBound;
    public Image panel;
    bool flashStarted = true;
    float blancoBienTemporizador = 7.5f;

    [SerializeField] float maxReductionTime = 4;
    float reductionTime = 0;
    bool speedBoosted = true;

    private void Start()
    {
        Color fixedColor = panel.color;
        fixedColor.a = 1;
        panel.color = fixedColor;
        panel.CrossFadeAlpha(0f, 0f, true);

        //panel.canvasRenderer.SetAlpha(1f);
        //.CrossFadeAlpha(1, 10f, true);
    }

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

                if (!flashStarted)
                {
                    panel.CrossFadeAlpha(0f, 1f, true);
                    flashStarted = true;
                    speedBoosted = true;
                    blancoBienTemporizador = 7.5f;
                }
            }
            else
            {
                blancoBienTemporizador -= Time.deltaTime;
                boundPos = new Vector3(
                    Mathf.Clamp(playerPos.x, minBound.x, maxBound.x),
                    Mathf.Clamp(playerPos.y, -100, 100),
                    Mathf.Clamp(playerPos.z, minBound.z, maxBound.z));

                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                Quaternion.Euler(30, 0, 0),
                Time.deltaTime * 5f);

                if (flashStarted)
                {

                    if (blancoBienTemporizador <= 0)
                    {
                        panel.CrossFadeAlpha(1f, 2.5f, true);
                        flashStarted = false;
                    }
                    if (speedBoosted)
                    {
                        //Vector3 newScale = new Vector3(player.transform.localScale.x, player.transform.localScale.y * 1.5f, player.transform.localScale.z);

                        //player.transform.localScale = Vector3.Lerp(transform.localScale, newScale, smoothDelay);

                        GameManager.instance.parallaxMultiplier(5f);
                        //GameManager.instance.speedMultiplier(1.5f);

                        reductionTime = maxReductionTime;

                        GameManager.instance.setHasToRestoreWaterfall (true);
                        speedBoosted = false;
                    }
                }


                if (reductionTime > 0.2/*maxReductionTime/2*/)
                {
                    reductionTime -= Time.deltaTime;
                        Camera.main.fieldOfView += 0.05f;
                }
                if (reductionTime > 0.0f)
                {
                    reductionTime -= Time.deltaTime;
                        Camera.main.fieldOfView -= 0.01f;
                }

            }
            //Chekea los bounds

            // Si el jugador está activo (vivo), la cámara le sigue en un intervalo de tiempo dado por delay
            if (player.gameObject.activeSelf)
                transform.position = Vector3.Lerp(transform.position, boundPos, smoothDelay * Time.fixedDeltaTime);
        }
    }

    public void ChangePlayer(GameObject p)
    {
        player = p;
        Debug.Log(p.name);
    }
}
