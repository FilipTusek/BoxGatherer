using GathererScripts;
using UnityEngine;

namespace StateMachineScripts
{
    public class DroppingBoxState : State
    {
        public DroppingBoxState(GathererAI gathererAI, StateMachine stateMachine) : base(gathererAI, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            DropTheBox();
            _stateMachine.ChangeState(_stateMachine.SearchingForBox);
        }

        private void DropTheBox()
        {
            Transform boxTransform = _gathererAI.TargetBox.transform;
            boxTransform.SetParent(null);
            boxTransform.position = _gathererAI.TargetContainer.transform.position + Vector3.up * (_gathererAI.TargetContainer.localScale.y + 3 * boxTransform.localScale.y);
            _gathererAI.TargetBox.Rigidbody.simulated = true;
            _gathererAI.TargetBox.gameObject.layer = LayerMask.NameToLayer("Default");
            _gathererAI.TargetBox = null;
            _gathererAI.IsCarryingBox = false;
        }
    }
}