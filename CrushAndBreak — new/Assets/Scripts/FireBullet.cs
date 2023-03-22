using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GunData gunData;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();
        damageable?.TakeDamage(gunData.damage);
        if (collision.gameObject.CompareTag("Prefab"))
        {

        }
        if(collision.gameObject.CompareTag("Floor"))
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}