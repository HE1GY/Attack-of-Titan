using UnityEngine;

namespace Titan.States
{
    public interface IState
    {
        void Enter();
        void UpdateState();
        void Exite();
    }

    public interface ITargetableState:IState
    {
        void Enter(Transform target);
    }
}