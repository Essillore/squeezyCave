using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSqueeze : MonoBehaviour
{
    public Rigidbody2D sphereRigidbody;
    public PlayerInput playerInput;
    public CapsuleCollider2D colliderCapsule;
    public float horizontal;
    public float vertical;
    public float speed = 5f;
    public float jumpForce = 5f;
    private Vector3 scaleChange, positionChange, colliderChange;

    // Start is called before the first frame update
    void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        colliderCapsule = GetComponent<CapsuleCollider2D>();

        scaleChange = new Vector3(1.5f, 0.5f, 0);
        positionChange = new Vector3(0.0f, -0.5f, 0);
        colliderChange = new Vector3(1f, 0.5f, 0);
    }
    void Update()
    {
        //transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
        sphereRigidbody.velocity = new Vector2(horizontal * speed, sphereRigidbody.velocity.y);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
        Debug.Log("Jump! " + context.phase);
        sphereRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Moved " + context);
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
   
    public void Squeze(InputAction.CallbackContext context)
    {
        Debug.Log("Squeezed");
        if (context.performed)
        {
            this.transform.localScale = scaleChange;
            this.transform.position = transform.position - positionChange;
            colliderCapsule.size = colliderCapsule.size * colliderChange;
            
        }
        if (context.canceled)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.transform.position = transform.position + positionChange;
            colliderCapsule.size = colliderCapsule.size / colliderChange;
        }
    }

}
