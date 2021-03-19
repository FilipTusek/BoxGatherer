using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GathererScripts;
using Scripts.Events;
using StateMachineScripts;
using UnityEngine;

public class GathererAI : MonoBehaviour
{
    private enum GathererState
    {
        SearchingForBox,
        MovingTowardsBox,
        PickingUpBox,
        MovingTowardsContainer,
        DroppingBox
    }
    
    [Header("Box Detection")] 
    [SerializeField] private float _detectionRadius = 50f;
    [SerializeField] private LayerMask _boxLayerMask;
    
    [Header("Container References")] 
    [SerializeField] private Transform _redContainer;
    [SerializeField] private Transform _blueContainer;

    public Box TargetBox { get; private set; }
    public Transform TargetContainer { get; set; }
    public GathererMovement GathererMovement { get; private set; }
    public StateMachine StateMachine { get; set; }
    public IdleState IdleState{ get; set; }
    public SearchingForBoxState SearchingForBox{ get; set; }
    public MovingTowardsBoxState MovingTowardsBox{ get; set; }
    public PickingUpBoxState PickingUpBox{ get; private set; }
    public MovingTowardsContainerState MovingTowardsContainer{ get; set; }
    public DroppingBoxState DroppingBoxState{ get; set; }
    
    public bool IsCarryingBox { get; private set; } 
    
    private void OnEnable()
    {
        EventManager.OnGathererActiveStatusChanged.OnEventRaised += ChangeGatheringState;
        EventManager.OnGathererTargetReached.OnEventRaised += ExitCurrentState;
    }

    private void OnDisable()
    {
        EventManager.OnGathererActiveStatusChanged.OnEventRaised -= ChangeGatheringState;
        EventManager.OnGathererTargetReached.OnEventRaised -= ExitCurrentState;
    }

    private void Start()
    {
        StateMachine = new StateMachine();

        IdleState = new IdleState(this, StateMachine);
        SearchingForBox = new SearchingForBoxState(this, StateMachine);
        MovingTowardsBox = new MovingTowardsBoxState(this, StateMachine);
        PickingUpBox = new PickingUpBoxState(this, StateMachine);
        MovingTowardsContainer = new MovingTowardsContainerState(this, StateMachine);
        DroppingBoxState = new DroppingBoxState(this, StateMachine);

        GathererMovement = GetComponent<GathererMovement>();
        
        StateMachine.Initialize(IdleState);
    }

    private void ChangeGatheringState(bool state)
    {
        if(state)
            StateMachine.ChangeState(SearchingForBox);
        else
            StateMachine.ChangeState(IdleState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void ExitCurrentState()
    {
        StateMachine.CurrentState.Exit();
    }
    
    public void SetTargetBox()
    {
        var boxes = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _boxLayerMask);
        if (boxes == null || boxes.Length == 0) {
            StateMachine.ChangeState(IdleState);
            return;
        }
        
        var closestDistance = float.MaxValue;
        Box closestBox = null;
        
        foreach (var box in boxes) {
            var distance = Vector2.Distance(transform.position, box.transform.position);
            if (closestDistance > distance) {
                closestDistance = distance;
                closestBox = box.GetComponent<Box>();
            }
        }

        TargetBox = closestBox;
        StateMachine.ChangeState(MovingTowardsBox);
    }

    public void SetTargetContainer()
    {
        if (TargetBox == null) return;
        TargetContainer = TargetBox.TypeOfBox == Box.BoxType.Blue ? _blueContainer : _redContainer;
    }

    public void PickUpBox()
    {
        TargetBox.transform.SetParent(transform);
        TargetBox.Rigidbody.simulated = false;
        var boxTransform = TargetBox.Rigidbody.transform;
        boxTransform.localPosition = new Vector3(boxTransform.localPosition.x, 0);
        IsCarryingBox = true;
        StateMachine.ChangeState(MovingTowardsContainer);
    }

    public void DropTheBox()
    {
        Transform boxTransform = TargetBox.transform;
        boxTransform.SetParent(null);
        boxTransform.position = TargetContainer.transform.position + Vector3.up * (TargetContainer.localScale.y + 3 * boxTransform.localScale.y);
        TargetBox.Rigidbody.simulated = true;
        TargetBox.gameObject.layer = LayerMask.NameToLayer("Default");
        TargetBox = null;
        IsCarryingBox = false;
        StateMachine.ChangeState(SearchingForBox);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
