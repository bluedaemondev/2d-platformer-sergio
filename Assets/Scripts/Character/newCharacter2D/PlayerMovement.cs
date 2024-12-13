using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public float velocidadDeMovimiento = 5;
    public float fuerzaDeSalto = 10;
    private bool grounded;

    public float movimiento = 1;

    public Vector3 initialPos;
    public InputActions input;

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float inpH = input.movement.x;
        movimiento = inpH * velocidadDeMovimiento * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.AddForce(Vector2.up * fuerzaDeSalto, ForceMode2D.Impulse);
        }



    }
    private void FixedUpdate()
    {
        _rigidbody.velocity += new Vector2(movimiento, 0);
    }

    internal void PauseMovement(float pause)
    {
        //if (pause == -1)
        //    pause = float.MaxValue;

        StartCoroutine(PausePlayer(pause - pause * .25f));
    }

    private IEnumerator PausePlayer(float pause)
    {
        float inMovSpeed = velocidadDeMovimiento;
        float inJumpSpeed = fuerzaDeSalto;

        velocidadDeMovimiento = 0;
        fuerzaDeSalto = 0;

        yield return new WaitForSeconds(pause);

        velocidadDeMovimiento = inMovSpeed;
        fuerzaDeSalto = inJumpSpeed;

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
