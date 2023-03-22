using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructableTiles : MonoBehaviour
{
    public Tilemap tilemap;
    Rigidbody2D rb;

    public Vector3Int localPlace {get; private set;}


    private void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {

                Debug.Log(hit.point);
                hitPosition.x = hit.point.x - 0.1f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.1f * hit.normal.y;

                tilemap.SetColor(localPlace, Color.white);
           }
        }
    }
}
