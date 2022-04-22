using System;
using System.Collections.Generic;
using TitanFolder.PhysicsBody;
using TitanFolder.States;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace TitanFolder
{
    public  class StateMachine
    {
        private readonly TitanAnimation _titanAnimation;
        private readonly FieldOfView _fieldOfView;
        private readonly BodyRotator _bodyRotator;
        private readonly Rig _hadRig;

        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine(TitanAnimation titanAnimation, FieldOfView fieldOfView, BodyRotator bodyRotator, Rig hadRig)
        {
            _titanAnimation = titanAnimation;
            _fieldOfView = fieldOfView;
            _bodyRotator = bodyRotator;
            _hadRig = hadRig;

            InitStates();
            SetStatesByDefault();
        }

        public void EnterState<T>() where T : IState
        {
            if (_currentState != GetState<T>())
            {
                _currentState?.Exite();
                _currentState = GetState<T>();
                _currentState.Enter();
            }
        }
        public void EnterState<T>(Transform target) where T : ITargetableState
        {
            if (_currentState != GetState<T>())
            {
                _currentState?.Exite();
                if (GetState<T>() is ITargetableState targetableState)
                {
                    targetableState.Enter(target);
                    _currentState = targetableState;
                }
                else
                {
                    throw new Exception("There is not implementation ITargetableState");
                }
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
            _states[typeof(AttackState)] = new AttackState(_titanAnimation,_hadRig,this);
        }
    }
}