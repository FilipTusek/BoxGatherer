using StateMachineScripts;
using UnityEngine;

namespace GathererScripts
{
    public class GathererAI : MonoBehaviour
    {
        [Header("Box Detection")] 
        [SerializeField] private float _detectionRadius = 50f;
        [SerializeField] private LayerMask _boxLayerMask;
    
        [Header("Container References")] 
        [SerializeField] private Transform _redContainer;
        [SerializeField] private Transform _blueContainer;

        public Transform RedContainer => _redContainer;
        public Transform BlueContainer => _blueContainer;
        public float DetectionRadius => _detectionRadius;
        public LayerMask BoxLayerMask => _boxLayerMask;

        public Box TargetBox { get; set; }
        public Transform TargetContainer { get; set; }
        public GathererMovement GathererMovement { get; private set; }

        public bool IsCarryingBox { get; set; } 
    
        private void Start()
        {
            GathererMovement = GetComponent<GathererMovement>();
            StateMachine stateMachine = new StateMachine();
            stateMachine.Initialize(this);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}
