using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Action<Collider2D> EnemyContact { get; set; }

    //void OnCollisionEnter(Collider2D collider)
    //{
    //    EnemyContact?.Invoke(collider);
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyContact?.Invoke(collision);
    }
}
