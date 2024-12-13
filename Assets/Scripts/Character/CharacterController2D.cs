using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(InputActions), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private float groundCheckDistance = .24f;
    [SerializeField] private float ceilingCheckDistance = .24f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float acceleration = 1f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 10;
    [Tooltip("The gravity multiplier added when jump is released early")]
    [SerializeField] private float jumpEndEarlyGravityModifier = 3;
    [SerializeField] private float jumpBuffer = .15f;
    [SerializeField] private float coyoteTime = .18f;

    [Header("Physics")]
    [SerializeField] private float groundDrag = 1f;
    [SerializeField] private float airDrag = .11f;
    [SerializeField] private float fallAcceleration = 110;
    [SerializeField] private float maxFallSpeed = 40;


    private Vector2 _targetVelocity;
    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    private InputActions input;
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif

    private void Awake()
    {
        _col = GetComponent<CapsuleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        input = GetComponent<InputActions>();
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

        input = GetComponent<InputActions>();
    }

    private void OnDrawGizmos()
    {
        // Foot Ground check
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundCheckDistance, 0), .15f);
        // Head Ceiling check
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + ceilingCheckDistance, 0), .15f);
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
    }

    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, groundCheckDistance, groundLayers);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, ceilingCheckDistance, groundLayers);

        // Hit a Ceiling
        if (ceilingHit) _targetVelocity.y = Mathf.Min(0, _targetVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            //GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y)); // Fall impact particles, sound, animation
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = Time.time;
            //GroundedChanged?.Invoke(false, 0); // Ground take-off - animation, sound, etc x jumping
        }
    }

    #endregion


    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && Time.time < _timeJumpWasPressed + jumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && Time.time < _frameLeftGrounded + coyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !input.jump && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _targetVelocity.y = jumpForce;

        //Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (input.movement.x == 0)
        {
            var deceleration = _grounded ? groundDrag : airDrag;
            _targetVelocity.x = Mathf.MoveTowards(_targetVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _targetVelocity.x = Mathf.MoveTowards(_targetVelocity.x, input.movement.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _targetVelocity.y <= 0f)
        {
            _targetVelocity.y = -0f; // -1.5f = _stats.GroundingForce
        }
        else
        {
            var inAirGravity = fallAcceleration;
            if (_endedJumpEarly && _targetVelocity.y > 0) inAirGravity *= jumpEndEarlyGravityModifier;
            _targetVelocity.y = Mathf.MoveTowards(_targetVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    private void ApplyMovement() => _rb.velocity = _targetVelocity;
}
