using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    private void Start()
    {
        Waiter();
        print("Hello");
    }


    private async void Waiter()
    {
        await StopAnimation();
        _animator.StartPlayback();
    } 

    private async Task StopAnimation()
    {
        await Task.Delay(5000);
    }
}
