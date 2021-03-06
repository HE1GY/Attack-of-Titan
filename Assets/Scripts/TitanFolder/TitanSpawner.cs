using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace TitanFolder
{
    public class TitanSpawner:MonoBehaviour,ITitanSpawner
    {
        [SerializeField] private List<Transform> _spawnPoints;

        private Titan.Factory _titanFactory;

        [Inject]
        public void Constructor(Titan.Factory titanFactory)
        {
            _titanFactory = titanFactory;
        }

        public void SpawnTitan(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Titan titan= _titanFactory.Create();
                
                Vector3 spawnPoint=GetRandomSpawnPosition();
                
                titan.transform.position = spawnPoint;
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            int index = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[index].position;
        }
    }
}