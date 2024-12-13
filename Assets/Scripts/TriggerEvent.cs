using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onTriggerEnter;
    public UnityEngine.Events.UnityEvent onTriggerStay;
    public UnityEngine.Events.UnityEvent onTriggerExit;

    //public UnityEngine.Events.UnityEvent onCollisionEnter;
    //public UnityEngine.Events.UnityEvent onCollisionStay;
    //public UnityEngine.Events.UnityEvent onCollisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        onTriggerStay?.Invoke();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
        
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
        
    //}
}
