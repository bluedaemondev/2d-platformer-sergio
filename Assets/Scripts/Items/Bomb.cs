using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    [SerializeField]
    private float secondsToExplode = 3;
    [SerializeField]
    private float explosionRadius = 1.5f;
    [SerializeField]
    private float explosionForce = 10f;
    [SerializeField]
    private LayerMask interactionLayers;


    Animator _animator;
    private float timer;

    [SerializeField]
    private GameObject prefab;

    public AudioClip clipExplode;

    public static void PlaceBombAt(Vector3 position, float dir, GameObject prefab)
    {
        //GameObject.Instantiate()
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        timer = secondsToExplode;
        _animator.SetTrigger("ignite");
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            _animator.SetTrigger("boom");
        }
    }

    private void OnDrawGizmos()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (timer > 0)
        {
            Gizmos.color = transparentGreen;
        }
        else
        {
            Gizmos.color = transparentRed;
        }


        // when selected, draw a gizmo in the position of, and matching radius of, the explosion collider
        Gizmos.DrawSphere(transform.position, explosionRadius);

    }
    public void BoomAnimEnded()
    {
        OnDispose();
    }
    public override void OnDispose()
    {
        var affectedGOs = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.one, 0f, interactionLayers);
        Debug.Log(affectedGOs.Length);

        foreach (var go in affectedGOs)
        {
            Health health;
            go.transform.TryGetComponent<Health>(out health);

            if (health != null)
            {
                //Debug.Log(health.name + " dice AAAAgghhh!!");
                health.TakeDamage(health.MaxHP);
            }

            if (go.rigidbody != null)
            {
                go.rigidbody.AddForce((go.transform.position - this.transform.position).normalized * explosionForce, ForceMode2D.Impulse);
            }
            FindObjectOfType<AudioManager>().PlayOnce(clipExplode);

            Destroy(gameObject, .2f);
        }
    }

   

    public override void OnPickUp()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Spawn a Bomb at given transform, 1 tile in front of it (if possible)
    /// </summary>
    /// <param name="originTransform"></param>
    public void OnUse(Transform originTransform)
    {
        Instantiate(prefab, originTransform.position, originTransform.rotation);
    }


}
