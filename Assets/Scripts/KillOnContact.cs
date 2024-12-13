using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnContact : MonoBehaviour
{
    public bool usePlayerTag = true;
    public LayerMask interactions;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usePlayerTag)
        {
            if (collision.CompareTag("Player"))
            {
                FindObjectOfType<GameManager>().OnDeath();
                return;
            }
        }
        else
        {
            if (GetComponent<Collider2D>().IsTouchingLayers(interactions))
            {
                collision.GetComponent<Health>().Die();
            }
        }
    }
}
