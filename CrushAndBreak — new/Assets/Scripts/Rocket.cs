using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    private Rigidbody2D rb;

    [Header("ExplosionArea")]
    [SerializeField] private float fieldOfImpact;
    [SerializeField] private float force;
    [SerializeField] private LayerMask layerToHit;
    
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject fire;

    [Header("CameraShake")]
    [SerializeField] private float shakingDuration;
    [SerializeField] private float oscillationАrequency;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Prefab"))
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            Explode();
        }
        if(collision.gameObject.CompareTag("Floor"))
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }


    public void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);
        Instantiate(explosion, transform.position, Quaternion.identity);
       // Destroy(explosion, 2);
        Camera.main.DOShakePosition(shakingDuration, oscillationАrequency);

        foreach(Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);

            if(obj.GetComponent<Target>())
            {
                obj.GetComponent<Target>().TakeDamage(gunData.damage);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }
}
