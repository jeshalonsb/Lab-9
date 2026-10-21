using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public BulletPool bulletPool;

    public float fireRate = 0.2f;

    private float nextFireTime;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
        }
    }
private void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();

        if (bullet == null) return;

        Vector3 direction = cameraMovement.transform.forward;
        Vector3 spawnPosition = cameraMovement.transform.position + direction * 0.5f;

        bullet.transform.SetPositionAndRotation(spawnPosition,Quaternion.LookRotation(direction));

        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Shoot(direction);

        nextFireTime = Time.time + fireRate;
    }
}

