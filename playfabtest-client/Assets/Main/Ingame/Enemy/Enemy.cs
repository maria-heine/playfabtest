using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
