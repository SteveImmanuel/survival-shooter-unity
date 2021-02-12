using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mainVcam;
    public GameObject killVcam;
    public GameObject deathVcam;
    
    private bool isGameOver = false;
    [HideInInspector]
    public static CameraController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Update()
    {
        if (PlayerHealth.currentHealth <= 0 && !isGameOver)
        {
            isGameOver = true;
            ActivateDeathCam();
        }
    }

    public void ActivateMainCam()
    {
        mainVcam.SetActive(true);
        killVcam.SetActive(false);
        deathVcam.SetActive(false);
    }

    public void ActivateKillCam()
    {
        mainVcam.SetActive(false);
        killVcam.SetActive(true);
        deathVcam.SetActive(false);
    }

    public void ActivateDeathCam()
    {
        mainVcam.SetActive(false);
        killVcam.SetActive(false);
        deathVcam.SetActive(true);
    }
}
