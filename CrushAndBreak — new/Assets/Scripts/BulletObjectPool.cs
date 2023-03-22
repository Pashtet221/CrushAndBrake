using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    public static BulletObjectPool Instance;

    public GameObject[] bulletPrefabs;
    public int bulletPoolSize = 20;

    private List<GameObject>[] bulletPools;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bulletPools = new List<GameObject>[bulletPrefabs.Length];

        for (int i = 0; i < bulletPrefabs.Length; i++)
        {
            bulletPools[i] = new List<GameObject>();

            for (int j = 0; j < bulletPoolSize; j++)
            {
                GameObject bullet = Instantiate(bulletPrefabs[i]);
                bullet.SetActive(false);
                bulletPools[i].Add(bullet);
            }
        }
    }

    public GameObject GetBullet(int index, Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in bulletPools[index])
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefabs[index], position, rotation);
        newBullet.SetActive(true);
        bulletPools[index].Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(int index, GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
