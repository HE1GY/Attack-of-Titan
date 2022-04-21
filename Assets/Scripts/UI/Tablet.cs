using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace UI
{
    public class Tablet : MonoBehaviour
    {
        [SerializeField] private InputScrollView _inputScrollView;
        [SerializeField] private Animator _animator;

        [SerializeField] private InputActionReference _tunOffTurnOn;
        
        private ITitanSpawner _titanSpawner;
        private bool _turnOn;
        
        private static readonly int TurnOn = Animator.StringToHash("TurnOn");


        [Inject]
        public void  Construct(ITitanSpawner titanSpawner)
        {
            _titanSpawner = titanSpawner;
        }

        private void OnEnable()
        {
            _tunOffTurnOn.action.performed +=ctx=> TurnOnTurnOff();
            _inputScrollView.Spawn += _titanSpawner.SpawnTitan;
        }

        private void OnDisable()
        {
            _tunOffTurnOn.action.performed -=ctx=> TurnOnTurnOff();
            _inputScrollView.Spawn -= _titanSpawner.SpawnTitan;
        }


        private void TurnOnTurnOff()
        {
            _turnOn = !_turnOn;
            _animator.SetBool(TurnOn,_turnOn);
        }
    }
}
