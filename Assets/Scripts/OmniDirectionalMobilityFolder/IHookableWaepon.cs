using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace OmniDirectionalMobilityFolder
{
    public interface IHookableWaepon
    {
        Transform ShootPoint { get;}
        event Action Hooked;
        event Action UnHooked;
    }
}