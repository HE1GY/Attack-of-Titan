using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace TitanFolder
{
    public class TitanSpawner:MonoBehaviour,ITitanSpawner
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _count=5;
        
        private MonoPool<Titan> _titanPool;

        [Inject]
        public void Constructor(Titan.Factory titanFactory)
        {
            _titanPool = new MonoPool<Titan>(_count, gameObject.transform, titanFactory);
        }

        public void SpawnTitan(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Titan titan= _titanPool.GetElement();
                titan.transform.position =GetRandomSpawnPosition();
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            int index = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[index].position;
        }
    }
}