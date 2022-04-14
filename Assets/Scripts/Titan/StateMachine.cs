using System;
using System.Collections.Generic;
using Titan.PhysicsBody;
using Titan.States;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Titan
{
    public  class StateMachine
    {
        private readonly TitanAnimation _titanAnimation;
        private readonly FieldOfView _fieldOfView;
        private BodyRotator _bodyRotator;
        private ParticleSystem _steamPartical;
        private Rig _hadRig;
        
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine(TitanAnimation titanAnimation, FieldOfView fieldOfView, BodyRotator bodyRotator, ParticleSystem steamPartical, Rig hadRig)
        {
            _titanAnimation = titanAnimation;
            _fieldOfView = fieldOfView;
            _bodyRotator = bodyRotator;
            _steamPartical = steamPartical;
            _hadRig = hadRig;

            InitStates();
            SetStatesByDefault();
        }

        public void EnterState<T>() where T : IState
        {
            _currentState?.Exite();
            _currentState = GetState<T>();
            _currentState.Enter();
        }
        public void EnterState<T>(Transform target) where T : ITargetableState
        {
            _currentState?.Exite();
            if (GetState<T>() is ITargetableState targetableState)
            {
                targetableState.Enter(target);
                _currentState=targetableState;
            }
            else
            {
                throw new Exception("There is not implementation ITargetableState");
            }
        }

        public void UpdateState()
        {
            _currentState.UpdateState();   
        }

        private void SetStatesByDefault()
        {
            EnterState<SleepingState>();
        }

        private IState GetState<T>() where T:IState
        {
            return _states[typeof(T)];
        }

        private void InitStates()
        {
            _states = new Dictionary<Type, IState>();
            _states[typeof(SleepingState)] = new SleepingState(_titanAnimation,_fieldOfView,this);
            _states[typeof(ChaseState)] = new ChaseState(_titanAnimation,_bodyRotator,_fieldOfView,this);
            _states[typeof(SteamAttackState)] = new SteamAttackState(_titanAnimation,_steamPartical);
            _states[typeof(HandAttackState)] = new HandAttackState(_titanAnimation,_hadRig);
            _states[typeof(LegAttackState)] = new LegAttackState(_titanAnimation);
        }
    }
}