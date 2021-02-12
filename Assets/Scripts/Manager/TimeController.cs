using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float slowDownFactor = 0.2f;
    public PlayerMovement playerMovement;

    private float defaultFixedDeltaTime;
    [HideInInspector]
    public static TimeController instance;
    private int enemyKilled = 0;

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

    private void Start()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SlowDown();
        }
    }

    private void SlowDown()
    {
        playerMovement.enabled = false;
        CameraController.instance.ActivateKillCam();
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = defaultFixedDeltaTime * slowDownFactor;
        StartCoroutine(ResetTimeScale());
    }

    IEnumerator ResetTimeScale()
    {
        yield return new WaitForSeconds(1 * slowDownFactor);
        while(Time.timeScale < 1)
        {
            Time.timeScale += Time.unscaledDeltaTime;
            yield return new WaitForSeconds(Time.unscaledDeltaTime);
        }
        CameraController.instance.ActivateMainCam();
        Time.timeScale = 1;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
        playerMovement.enabled = true;
    }

    public void AddKill()
    {
        enemyKilled += 1;
        if (enemyKilled % 5 == 0)
        {
            SlowDown();
        }
    }
}
