using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TitanFolder.Body
{
    public class Body : MonoBehaviour
    {
        public event Action Touch;

        private void OnCollisionEnter(Collision collision)
        {
            Touch?.Invoke();
        }
    }
}