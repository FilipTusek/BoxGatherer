﻿using UnityEngine;

namespace StateMachineScripts
{
    public class StateMachine
    {
        public State CurrentState { get; set; }
        
        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState = newState;
            newState.Enter();
        }
    }
}
