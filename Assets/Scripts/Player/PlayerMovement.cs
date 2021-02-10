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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction.Set(h, 0f, v);
    }

    private void FixedUpdate()
    {
        Move(direction);
        Turning();
        Animating(direction);
    }

    private void Move(Vector3 direction)
    {
        Vector3 movement = direction.normalized * speed * Time.fixedDeltaTime;
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

    private void Animating(Vector3 direction)
    {
        bool isWalking = direction != Vector3.zero;
        anim.SetBool("IsWalking", isWalking);
    }
}
