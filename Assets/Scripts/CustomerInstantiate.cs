using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInstantiate : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform whereToInstantiate;

    public float instantiateTime = 2f;

    void Update()
    {
        instantiateTime -= Time.deltaTime;
        if (instantiateTime <= 0)
            ResetTimeAndInstantiate();
    }

    void ResetTimeAndInstantiate()
    {
        instantiateTime = 2f;
        Instantiate(projectile, whereToInstantiate.position, whereToInstantiate.rotation);
    }
}
