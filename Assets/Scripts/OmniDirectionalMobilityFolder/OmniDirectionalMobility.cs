using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


namespace OmniDirectionalMobilityFolder
{
    public class OmniDirectionalMobility : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _maxDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField]private Transform _player;
        [SerializeField] private RopeVisualization _ropeVisualization;
        
        [SerializeField] private Transform _pointer;//Test
        
        private GrapplerGun _grapplerGun;

        private void Awake()
        {
            _grapplerGun = new GrapplerGun(_shootPoint, _maxDistance, _layerMask,_player,_pointer,_ropeVisualization);
        }
        

        public void Hook()
        {
            _grapplerGun.StartGrappling();
        }

        public void UnHook()
        {
            _grapplerGun.StopGrappling();
        }
    }
}