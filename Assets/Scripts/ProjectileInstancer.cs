using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ProjectileInstancer : MonoBehaviour
{
    public GameObject projectilePrefab;
    // Start is called before the first frame update

    private void Start()
    {
        TouchHandler.OnSwipedUp += InstanceProjectile;
    }

    private void InstanceProjectile()
    {
        Vector3 projPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.3f);
        Quaternion projectileRotation = transform.rotation;
        var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.Rotate(-10, 0, 0);
    }
}
