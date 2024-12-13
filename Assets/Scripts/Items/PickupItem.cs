using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemTypes type;
    private float animSpeed = 1;
    public AudioClip clipPickup;

    private void Update()
    {
        transform.Rotate(0, animSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.TryGetComponent(out CharController2D x);
            if (x) x.PickUpItem(type);

            FindObjectOfType<AudioManager>().PlayOnce(clipPickup);

            Destroy(this.gameObject);
        }
    }
}
