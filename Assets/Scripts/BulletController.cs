using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 20;
    public Vector3 direction;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {

        Destroy(gameObject);
        }
    }
}
