using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet
    [SerializeField] private Transform firepoint; // Firepoint
    [SerializeField] private float fireForce = 10f;
    [SerializeField] private float fireInterval = 2f; // Time interval between shots
    private TurretScript turretScript;

    private void Start()
    {
        turretScript = GetComponentInParent<TurretScript>();
        if (turretScript == null)
        {
            Debug.LogError("TurretScript not found in parent.");
            return;
        }

        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            if (turretScript.Detected)
            {
                Fire();
            }
            yield return new WaitForSeconds(fireInterval);
        }
    }

    public void Fire()
    {
        if (firepoint == null)
        {
            Debug.LogError("Firepoint is not assigned in the TurretWeapon script.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        if (bullet.GetComponent<Rigidbody2D>() == null)
        {
            Debug.LogError("Rigidbody2D component not found in the bullet prefab.");
            return;
        }
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Reset velocity
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.right * fireForce, ForceMode2D.Impulse);
        Debug.Log("Bullet fired from " + firepoint.name + " with force: " + fireForce);
    }
}
