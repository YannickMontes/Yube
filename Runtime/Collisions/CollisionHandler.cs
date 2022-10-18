using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEnter = null;
    public Action<Collider2D> OnTriggerStay = null;
    public Action<Collider2D> OnTriggerExit = null;
    public Action<Collision2D> OnCollisionEnter = null;
    public Action<Collision2D> OnCollisionStay = null;
    public Action<Collision2D> OnCollisionExit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerStay?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStay?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }
}