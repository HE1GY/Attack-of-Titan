using System;
using UnityEngine;

namespace OmniDirectionalMobilityFolder
{
    public interface IHookableWeapon
    {
        Transform ShootPoint { get; }
        event Action Hooked;
        event Action UnHooked;

        void ResetWeapon();
    }
}