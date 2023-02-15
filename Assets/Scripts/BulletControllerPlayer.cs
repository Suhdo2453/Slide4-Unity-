using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerPlayer : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    public Vector3 direction;
    public float damage;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("Damage", damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
