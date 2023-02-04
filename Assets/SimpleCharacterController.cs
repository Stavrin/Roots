using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public GameObject weapon;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Shoot()
    {
       // GameObject bullet = Instantiate(weapon, transform.position + transform.forward, transform.rotation);
       // Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
       // bulletRigidbody.AddForce(transform.forward * 1000f);
    }
}
