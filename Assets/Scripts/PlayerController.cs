using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 8f;
    public float horizontal;
    public float vertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * horizontal * speed * Time.deltaTime);
        transform.Translate(Vector2.up * vertical * speed * Time.deltaTime);
    }
}
