using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyValidator : MonoBehaviour
{
    public Action<GameObject> ObjectDestroyed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Garbage"))
        {
            ObjectDestroyed?.Invoke(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
