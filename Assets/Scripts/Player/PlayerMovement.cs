using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public LayerMask floorMask;
    
    private Vector3 direction;
    private Animator anim;
    private Rigidbody rb;
    private float camRayLength = 100f;
    private float currentSpeed;
    private bool isMultiplied = false;
    private float timeLeft;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction.Set(h, 0f, v);
        if (isMultiplied && timeLeft >= 0)
        {
            IndicatorController.instance.SetPowerUpDurationText(timeLeft);
            timeLeft -= Time.deltaTime;
        }
        else
        {
            ResetSpeed();
        }
    }

    private void FixedUpdate()
    {
        Move(direction);
        Turning();
        Animating(direction);
        rb.velocity = Vector3.zero;
    }

    public void MultiplySpeed(float multiplier, float duration)
    {
        timeLeft = duration;
        currentSpeed = speed * multiplier;
        isMultiplied = true;
        IndicatorController.instance.ActivatePowerUp();
    }

    private void ResetSpeed()
    {
        isMultiplied = false;
        currentSpeed = speed;
        IndicatorController.instance.DeactivatePowerUp();
    }

    public void Move(Vector3 direction)
    {
        Vector3 movement = direction.normalized * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(rotation);
        }
    }

    public void Animating(Vector3 direction)
    {
        bool isWalking = direction != Vector3.zero;
        anim.SetBool("IsWalking", isWalking);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
