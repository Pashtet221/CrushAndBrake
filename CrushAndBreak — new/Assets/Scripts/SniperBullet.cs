using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        damageable?.TakeDamage(gunData.damage);
        if (collision.gameObject.CompareTag("Prefab"))
        {
           // Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Floor"))
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}