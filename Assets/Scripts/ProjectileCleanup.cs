using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleanup : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(DestroySequence());

    }

    //projectiles are destroyed 5 seconds after spawn
    IEnumerator DestroySequence()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.transform.gameObject);
    }
}
