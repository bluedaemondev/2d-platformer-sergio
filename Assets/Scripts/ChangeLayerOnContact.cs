using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnContact : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactions;
    [SerializeField]
    private int occludeLayer;
    [SerializeField]
    private int exposeLayer;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("xx");
        if(col.IsTouchingLayers(interactions))
        {
            var renderer = collision.GetComponent<SpriteRenderer>();
            exposeLayer = renderer.sortingOrder;

            renderer.sortingOrder = occludeLayer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (col.IsTouchingLayers(interactions))
        //{
            var renderer = collision.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = exposeLayer;
        //}
    }
    //private void OnTriggerStay(Collider other)
    //{
        
    //}
}
