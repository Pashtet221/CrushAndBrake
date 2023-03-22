using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    public static Bomb instance{get; private set;}
    
    [SerializeField] private float fieldOfImpact;
    [SerializeField] private float force;
    [SerializeField] private LayerMask layerToHit;

    public int bombs = 2;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bombs = 2;
    }

    public void Explode()
    {
        if(bombs != 0)
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

        foreach(Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
        bombs -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
