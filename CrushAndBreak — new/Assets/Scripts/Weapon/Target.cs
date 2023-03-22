using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable {

    public static Target instance {get; private set;}

    public float health;

    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;


    private void Awake()
    {
        instance = this;

        spriteRend = GetComponentInChildren<SpriteRenderer>();
        matBlink = Resources.Load("TileBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    void Start()
    {
        GameDataManager.LoadCoins();
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        MatBlink();
        if (health <= 0)
        {
            Destroy(gameObject);
            GetCoins();
        } 
        else
        {
            Invoke("ResetMaterial", .2f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
            GetCoins();
        }
    }

    private void MatBlink()
    {
        spriteRend.material = matBlink;
    }

    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }


    private void GetCoins()
    {
        GameDataManager.AddCoins (1) ;
        GameSharedUI.Instance.UpdateCoinsUIText () ;
    }


    void OnApplicationQuit()
    {
        GameDataManager.ExitGame();
    }
}
