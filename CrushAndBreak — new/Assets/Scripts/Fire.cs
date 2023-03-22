using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fire : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    private Rigidbody2D rb;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject fire;



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
        Instantiate(explosion, transform.position, Quaternion.identity);
      //  Destroy(explosion, 2);

        if(fire != null)
        {
            for(int i = 0; i < 5; i++)
            {
                Instantiate(fire, transform.position, Quaternion.identity);
            }
        }
    }
}
