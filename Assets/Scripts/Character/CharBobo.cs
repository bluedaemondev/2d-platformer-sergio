using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBobo : MonoBehaviour
{
    [SerializeField]
    private float speed = 200;
    [SerializeField]
    private float jumpForce = 1000;
    
    private Vector2 movDirection;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool jump;

    InputActions input;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputActions>();
    }

    // Update is called once per frame
    void Update()
    {
        movDirection.x = input.movement.x * speed * Time.deltaTime;
        jump = Input.GetButtonDown("Jump") && isGrounded;
    }

    private void FixedUpdate()
    {
        rb.velocity += movDirection;
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
