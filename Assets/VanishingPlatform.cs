using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class VanishingPlatform : MonoBehaviour
{
    [Header("Componentes (se cargan auto)")]
    public Collider2D _collider;
    public Rigidbody2D _rigidbody;
    public Animator _animator;

    [Header("Caida")]
    public float maxDistanceBeforeDestroy = 20;
    
    [Range(0.1f, 5f)]
    public float timeBeforeFall = 2;
    private float timer = 0;
    private bool hasPassenger = false;

    // Start is called before the first frame update
    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(hasPassenger && timer <= 0)
        {
            _animator.SetBool("iniciar caida", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("iniciar contacto", true);
            hasPassenger = true;
            timer = timeBeforeFall;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("iniciar contacto", false);
            hasPassenger = false;
            timer = 0;
        }
    }

}
