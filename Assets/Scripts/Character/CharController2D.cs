using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController2D : MonoBehaviour
{
    public PlayerAnimator _animator;

    [SerializeField]
    float groundCheckRadius = .54f;
    [SerializeField]
    LayerMask groundLayers;

    [SerializeField, Tooltip("Max speed un U/s. ")]
    float speed = 4.6f;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 300;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 755;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 50;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 2;

    BoxCollider2D boxCollider;
    Vector2 velocity;

    //[Tooltip("Set to true when the character intersects a collider beneath them in the previous frame.")]
    private bool grounded;
    // in a moving platform
    private bool isPassenger;
    // moving platform rigidbody for velocity/inertia
    private Rigidbody2D platformRb;

    private Inventory playerInventory;
    [SerializeField]
    private PlayerMovement movement;

    [SerializeField]
    private InventoryHUD invHUD;
    //private Inventory.ItemTypes selectedItem;

    private InputActions input;

    private Health health;

    private float switchCooldown;
    private float useItemCooldown;

    public GameObject bombPrefab;

    private bool saltoSecConsumed;
    private int jumpCount = 0;


    [SerializeField]
    private float jumpCooldown = .5f;
    private float timerJumpCooldown;



    private void Awake()
    {
      
        boxCollider = GetComponent<BoxCollider2D>();
        input = GetComponent<InputActions>();
        health = GetComponent<Health>();
        invHUD = FindObjectOfType<InventoryHUD>();
        _animator = GetComponent<PlayerAnimator>();

        // healthHud= findobjectoftype<health>();

        playerInventory = new Inventory(true, false, false, invHUD);
    }

    private void Update()
    {

        float movimiento = input.movement.x;

        if (movimiento != 0)
        {
            _animator.Walk(true);
        }
        else
        {
            _animator.Walk(false);
        }

        Vector3 scale = transform.localScale;

        if (movimiento < 0)
            scale.x = -1;
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;

        if ((grounded || !saltoSecConsumed) && timerJumpCooldown <= 0)
        {
            velocity.y = 0;
            Jump(); // leo y aplico salto
        }
        UseItem(); // leo y aplico Interactuar
        SwitchItem(); // leo y aplico Cambiar item

        HandlePhysics();

        transform.Translate(velocity * Time.deltaTime);

        CheckGround();

        if(timerJumpCooldown > 0)
        {
            timerJumpCooldown -= Time.deltaTime;
        }
    }




    private void CheckGround()
    {
        grounded = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider || hit.isTrigger)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y <= 0)
                {
                    grounded = true;
                    jumpCount = 0;
                    saltoSecConsumed = false;

                    isPassenger = platformRb != null && hit.attachedRigidbody == platformRb;
                }
            }
        }
    }

    private void HandlePhysics()
    {
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;

        if (input.movement.x != 0)
        {
            velocity.x = Mathf.Lerp(velocity.x, speed * input.movement.x, 0.9f + acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.Lerp(velocity.x, 0, 0.1f + deceleration * Time.deltaTime);
        }

        if (isPassenger && grounded)
        {
            if (platformRb.velocity.y <= 0)
            {
                velocity.y = platformRb.velocity.y * Time.deltaTime;
            }
            else
            {
                // If platform moves up, counteract by applying gravity directly
                velocity.y += Physics2D.gravity.y * GetComponent<Rigidbody2D>().mass * Time.deltaTime;
            }

            // Apply horizontal platform velocity
            velocity.x += platformRb.velocity.x;
        }
        else
        {
            // Apply gravity normally when not on a platform or in the air
            velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (input.jump)
         {
            timerJumpCooldown = jumpCooldown;
            jumpCount++;
            if(jumpCount > 1)
            {
                saltoSecConsumed = true;
                Debug.Log("saltaste con segundo salto");
            }
            velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            isPassenger = false; // Temporarily disengage from platform on jump
        }
    }
    private void UseItem()
    {
        useItemCooldown -= Time.deltaTime;

        if (!input.interact || useItemCooldown > 0)
            return;

        Debug.Log(playerInventory.SelectedItem);

        if (playerInventory.SelectedItem != ItemTypes.Empty
            && playerInventory.HasItem(playerInventory.SelectedItem))
        {
            // añadir control con bool. se pudo usar? count > 0?

            switch (playerInventory.SelectedItem)
            {
                case ItemTypes.Bomb:
                    Debug.Log("Bomb");
                    Instantiate(bombPrefab, transform.position, transform.rotation);
                    //Bomb.PlaceAt(transform.position, Mathf.Sign(input.movement.x));
                    break;
                case ItemTypes.Key:
                    Debug.Log("Llave");
                    // Physics2D.CircleCast(transform.position); <== usar Key.OpenDoor(string DoorName)
                    break;

                case ItemTypes.Food:
                    Debug.Log("Comida");
                    health.Heal(1);

                    break;
            }

            useItemCooldown = 2f;
            playerInventory.RemoveItem(playerInventory.SelectedItem);
        }
    }

    //public void SwitchItem()
    //{
    //    if (timerSwitchCooldown > 0)
    //        timerSwitchCooldown -= Time.deltaTime;

    //    if(input.switchItem && timerSwitchCooldown <= 0)
    //    {   // tocaste el boton y no estas en cooldown
    //        playerInventory.SwitchActiveItem();
    //        playerInventory.UpdateUI(); // actualizar la interfaz
    //        timerSwitchCooldown = switchCooldown;
    //    }

    //}




    public void SwitchItem()
    {
        switchCooldown -= Time.deltaTime;

        if (input.switchItem && switchCooldown <= 0)
        {
            if ((int)playerInventory.SelectedItem + 1 < Enum.GetNames(typeof(ItemTypes)).Length)
            {
                playerInventory.SelectedItem++;
            }
            else
            {
                playerInventory.SelectedItem = 0;
            }

            playerInventory.UpdateUI();

            switchCooldown = 2f;
        }
    }

    public void PickUpItem(ItemTypes itemType)
    {
        playerInventory.AddItem(itemType);
    }

    public void LoadPlatform(Rigidbody2D rb)
    {
        platformRb = rb;

        if (platformRb != null)
        {
            isPassenger = true;
        }
        else
        {
            isPassenger = false;
        }
    }
}