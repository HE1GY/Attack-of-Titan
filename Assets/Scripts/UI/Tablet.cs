using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI
{
    public class Tablet : MonoBehaviour
    {
        [SerializeField] private InputScrollView _inputScrollView;
        
        private ITitanSpawner _titanSpawner;

        [Inject]
        public void  Construct(ITitanSpawner titanSpawner)
        {
            _titanSpawner = titanSpawner;
        }

        private void OnEnable()
        {
            _inputScrollView.Spawn += _titanSpawner.SpawnTitan;
        }

        private void OnDisable()
        {
            _inputScrollView.Spawn -= _titanSpawner.SpawnTitan;
        }
    }
}
