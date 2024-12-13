using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    public int pointsOnPickup = 10;

    [SerializeField]
    private GameObject onCollectEffect;
    [SerializeField]
    private GameObject onUseEffect;

    [SerializeField]
    private float rotationSpeed = .5f;

    public AudioClip soundPickup;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPickUp();
        }
    }

    public override void OnPickUp()
    {
        // Instantiate the particle effect
        Instantiate(onCollectEffect, transform.position, transform.rotation);

        FindObjectOfType<AudioManager>().PlayOnce(soundPickup);

        ScoreManager.Instance.AddScore(pointsOnPickup);
        OnDispose();
    }
    public void OnUse()
    {
        // Instantiate the particle effect
        Instantiate(onUseEffect, transform.position, transform.rotation);
    }
    public override void OnDispose()
    {
        Destroy(this.gameObject);
    }







}
