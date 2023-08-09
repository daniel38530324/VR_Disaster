using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] float speed = 50;
    [SerializeField] GameObject bulletObj;
    [SerializeField] Transform fontOfGun;

   public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, fontOfGun.position, fontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * fontOfGun.forward;
        Destroy(spawnedBullet, 5);
    }
}
