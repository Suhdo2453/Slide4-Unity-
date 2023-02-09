using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestrucTilemap : MonoBehaviour
{
    public Tilemap destrucTilemap;
    private void Start()
    {
        destrucTilemap = GetComponent<Tilemap>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                destrucTilemap.SetTile(destrucTilemap.WorldToCell(hitPosition), null);
            }
        }
        
    }
}
