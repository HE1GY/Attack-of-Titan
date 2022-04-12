using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    private void Start()
    {
        Physics.gravity = new Vector3(0, -100, 0);
    }
}
