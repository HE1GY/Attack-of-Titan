using Player;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MoveBloom : MonoBehaviour
{
    [SerializeField] private Maneuvering _maneuvering;
    private PostProcessVolume _postProcessVolume;

    private void Awake()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        _postProcessVolume.weight = _maneuvering.GetVelocity()/10;
    }
}
