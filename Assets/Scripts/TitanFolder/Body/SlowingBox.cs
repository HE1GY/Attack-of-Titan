using System;
using Player;
using TimeService;
using UnityEngine;
using Zenject;

namespace TitanFolder.Body
{
    public class SlowingBox : MonoBehaviour
    {
        [SerializeField] private float _slowDownTo=0.05f;
        [SerializeField] private float _returningTime=2;
        private ITimeService _timeService;
        
        [Inject]
        private void Constructor(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Maneuvering maneuvering))
            {
                _timeService.SlowDownTo(_slowDownTo);
                _timeService.ReturnToDefault(_returningTime);
            }
        }
    }
}
