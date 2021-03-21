using GathererScripts;
using UnityEngine;

namespace StateMachineScripts
{
    public class PickingUpBoxState : State
    {
        public PickingUpBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            PickUpTheBox();
            _stateMachine.ChangeState(_stateMachine.MovingTowardsContainer);
        }

        private void PickUpTheBox()
        {
            _gathererAI.TargetBox.transform.SetParent(_gathererAI.transform);
            _gathererAI.TargetBox.Rigidbody.simulated = false;
            var boxTransform = _gathererAI.TargetBox.Rigidbody.transform;
            boxTransform.localPosition = new Vector3(boxTransform.localPosition.x, 0);
            _gathererAI.IsCarryingBox = true;
        }
    }
}