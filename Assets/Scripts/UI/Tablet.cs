using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace UI
{
    public class Tablet : MonoBehaviour
    {
        [SerializeField] private InputScrollView _inputScrollView;
        
        private ITitanFactory _titanFactory;

        [Inject]
        public void  Construct(ITitanFactory titanFactory)
        {
            _titanFactory = titanFactory;
        }

        private void OnEnable()
        {
            _inputScrollView.Spawn += _titanFactory.SpawnTitan;
        }

        private void OnDisable()
        {
            _inputScrollView.Spawn -= _titanFactory.SpawnTitan;
        }
    }
}
