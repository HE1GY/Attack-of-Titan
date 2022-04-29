using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TitanFolder.Body
{
    public class Chest : MonoBehaviour
    {

        [SerializeField] private LayerMask _ground;
        public event Action TouchTheGround;

        private void OnCollisionEnter(Collision collision)
        {
            print("collision");
            /*if (collision.gameObject.layer == _ground)*/
            {
                TouchTheGround?.Invoke();
                print("ground");
            }
        }
    }
}
