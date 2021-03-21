using GathererScripts;
using UnityEngine;
using UnityEngine.UIElements;
using Utils.Events;

namespace StateMachineScripts
{
    public class MovingTowardsContainerState : State
    {
        public MovingTowardsContainerState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            MoveTowardsTargetContainer();
            EventManager.OnGathererTargetReached.OnEventRaised += GoToNextState;
        }

        public override void Exit()
        {
            base.Exit();
            EventManager.OnGathererTargetReached.OnEventRaised -= GoToNextState;
        }

        private void GoToNextState()
        {
            _stateMachine.ChangeState(_stateMachine.DroppingBoxState);
        }

        private void MoveTowardsTargetContainer()
        {
            var container = _gathererAI.TargetContainer;
            var containerPosition = container.position;
            var targetPosition = containerPosition - (containerPosition - _gathererAI.transform.position).normalized.x * new Vector3(container.localScale.x / 2f, 0, 0);
            _gathererAI.GathererMovement.GoTo(targetPosition);
        }
    }
}