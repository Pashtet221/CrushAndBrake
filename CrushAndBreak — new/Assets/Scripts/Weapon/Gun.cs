using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour {

    public static Gun instance {get; private set;}
    
    [Header("References")]
    [SerializeField] public GunData gunData;

    [Header("Shooting")]
    [SerializeField] private Transform muzzle;
    private int ammountOfBullets;

    [Header("Reloading")]
    [SerializeField] private bool autoReload = false;

    private AudioManager sound;
    float timeSinceLastShot;

    public int thisCurrentAmmo;
    
    [Header("Sounds")]
    public string shootSoundName;
    public string reloadSoundName;

    public int bulletIndex;

    

    private void Awake()
    {
        instance = this;

        thisCurrentAmmo = gunData.currentAmmo = gunData.magSize;
        sound = FindObjectOfType<AudioManager>();
    }


    private void Start() {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        switch(gunData.shotType)
        {
            case GunData.ShotType.Single:
            ammountOfBullets = 1;
            break;
            case GunData.ShotType.Spread:
            ammountOfBullets = Random.Range(4,5);
            break;
        }
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        gunData.reloading = true;
        sound.Play(reloadSoundName);

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void Shoot() {

        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                for(int i = 0; i < ammountOfBullets; i++)
                {
                   GameObject b = BulletObjectPool.Instance.GetBullet(bulletIndex, muzzle.position, muzzle.rotation);

                    Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
                    Vector2 dir = transform.rotation * Vector2.right;
                    Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-gunData.spread, gunData.spread);
                    brb.velocity = (dir + pdir) * gunData.bulletSpeed;

                    FindObjectOfType<AudioManager>().Play(shootSoundName);

                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                }
            }
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;

        if (gunData.currentAmmo <= 0 && autoReload)
        {
            StartReload();
        }
    }

    private void OnGunShot() {  }
}
