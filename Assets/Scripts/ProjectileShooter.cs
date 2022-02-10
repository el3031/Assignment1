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
    
    // Start is called before the first frame update
    void Start()
    {
        shootButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward.normalized * fudge, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * 1f * projectileForce);
    }
    
}
