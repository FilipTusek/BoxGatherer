using GathererScripts;
using Unity.Collections.LowLevel.Unsafe;

namespace StateMachineScripts
{
    public abstract class State
    {
        protected GathererAI _gathererAI;
        protected StateMachine _stateMachine;

        public State(GathererAI gathererAI, StateMachine stateMachine)
        {
            _gathererAI = gathererAI;
            _stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }
    }
}
