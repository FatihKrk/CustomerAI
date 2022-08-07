using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    public bool deneme;
    public CustomerAI cstmr;

    void OnTriggerEnter(Collider other)
    {
        if (other == cstmr.chair.GetComponent<BoxCollider>())
            deneme = true;
    }
}
