using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public Transform[] pathNodes;

    [Range(0.05f, 1f)]
    public float speed = 1;
    Vector3 targetPos;

    public bool goReverseOnFinish;

    private Vector3 nextRbPos;

    [SerializeField]
    private CharController2D controller;
    private Rigidbody2D _rigidbody;
    private Vector3 moveDirection;

    private void Awake()
    {
        this._rigidbody = GetComponent<Rigidbody2D>();

        if (controller == null)
            GameObject.FindGameObjectWithTag("Player").TryGetComponent(out controller);

    }

    void Start()
    {
        targetPos = pathNodes[0].position;
    }

    void Update()
    {
        ClampToNearPathNodes();
        var useLerping = Vector2.Distance(targetPos, transform.position) < 1.5f;
        if (useLerping)
        {
            nextRbPos = Vector3.Lerp(transform.position, targetPos, speed * Time.fixedDeltaTime);
        }
        else
        {
            nextRbPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
        }

        transform.position = nextRbPos;
    }

    private void OnDrawGizmosSelected()
    {
        for (int n = 0; n < pathNodes.Length; n++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pathNodes[n].position, .32f);
            Gizmos.color = Color.green;
            if (n + 1 < pathNodes.Length)
                Gizmos.DrawLine(pathNodes[n].position, pathNodes[n + 1].position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.LoadPlatform(this._rigidbody);
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            print("send rb vel" + _rigidbody.velocity);
            collision.rigidbody.velocity = _rigidbody.velocity;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.LoadPlatform(null);
            collision.transform.SetParent(null);
        }
    }

    void ClampToNearPathNodes()
    {
        if (Vector2.Distance(transform.position, pathNodes[0].position) < .1f)
        {
            targetPos = pathNodes[1].position;
        }
        if (Vector2.Distance(transform.position, pathNodes[1].position) < .1f)
        {
            targetPos = pathNodes[0].position;
        }

        moveDirection = (targetPos - transform.position).normalized;
    }
}
