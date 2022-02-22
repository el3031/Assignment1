using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private Button shootButton;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fudge;
    [SerializeField] private float projectileForce;
    
    //once the projectile is activated, enable the control button
    void Start()
    {
        shootButton.interactable = true;
    }

    //instantiate a projectile and shoot it forward
    public void OnShoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward.normalized * fudge, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * 1f * projectileForce);
    }
    
}
