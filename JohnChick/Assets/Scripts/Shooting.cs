﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> muzzle = new List<Transform>();
    [SerializeField] private float forwardForce;

    private int curMuzzle = 0;
    float bulletTimeScale = 1.0f;

    [Header("Bullet")]
    [SerializeField] private float bulletsPerSec;
    [SerializeField] private float bulletLife;
    [SerializeField] private float bulletSize;

    public void StartShooting(GameObject pPrefab = null)
    {
        if (pPrefab != null)
            bulletPrefab = pPrefab;

        StartCoroutine("meShooting");
    }

    public void StopShooting()
    {
        StopCoroutine("meShooting");
    }

    IEnumerator meShooting()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = muzzle[curMuzzle].position;
            bullet.transform.forward = muzzle[curMuzzle].forward;
            bullet.transform.localScale *= bulletSize;
            if (!bullet.CompareTag("PlayerBullet"))
            {
                bulletTimeScale = GetComponent<FastAndSlowEffect>().timeScale;
                bullet.GetComponent<FastAndSlowEffect>().timeScale = bulletTimeScale;
            }
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * forwardForce * bulletTimeScale, 0, transform.forward.z * forwardForce * bulletTimeScale), ForceMode.Impulse);
            Destroy(bullet, bulletLife);

            CameraShaker.Instance.ShakeOnce(1, 2, 0.1f, 0.15f);

            curMuzzle++;
            if (curMuzzle == muzzle.Count)
                curMuzzle = 0;

            yield return new WaitForSeconds( 1f / bulletsPerSec);
        }
    }
}
