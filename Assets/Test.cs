using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("有人进来了");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("有人进来了");
    }
}
