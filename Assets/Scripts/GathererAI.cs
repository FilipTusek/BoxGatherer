using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    
    [Header("Movement")]
    [SerializeField] private float _maxSpeed;

    [Header("Box Detection")] 
    [SerializeField] private float _detectionRadius = 50f;
    [SerializeField] private LayerMask _boxLayerMask;
    
    [Header("Container References")] 
    [SerializeField] private Transform _redContainer;
    [SerializeField] private Transform _blueContainer;

    private StateMachine _stateMachine;
    private IdleState _idleState;
    private SearchingForBoxState _searchingForBox;
    private MovingTowardsBoxState _movingTowardsBox;
    private PickingUpBoxState _pickingUpBox;
    private MovingTowardsContainerState _movingTowardsContainer;
    private DroppingBoxState _droppingBoxState;

    private Box _targetBox;
    public Transform TargetContainer { get; set; }
    
    private void OnEnable()
    {
        EventManager.OnGathererActiveStatusChanged.OnEventRaised += ToggleGatheringState;
    }

    private void OnDisable()
    {
        EventManager.OnGathererActiveStatusChanged.OnEventRaised -= ToggleGatheringState;
    }

    private void Start()
    {
        _stateMachine = new StateMachine();

        _idleState = new IdleState(this, _stateMachine);
        _searchingForBox = new SearchingForBoxState(this, _stateMachine);
        _movingTowardsBox = new MovingTowardsBoxState(this, _stateMachine);
        _pickingUpBox = new PickingUpBoxState(this, _stateMachine);
        _movingTowardsContainer = new MovingTowardsContainerState(this, _stateMachine);
        _droppingBoxState = new DroppingBoxState(this, _stateMachine);
        
        _stateMachine.Initialize(_idleState);
    }

    private void ToggleGatheringState(bool state)
    {
        if(state)
            _stateMachine.ChangeState(_searchingForBox);
        else
            _stateMachine.ChangeState(_idleState);
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetTargetBox()
    {
        var boxes = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _boxLayerMask);
        if (boxes == null || boxes.Length == 0) {
            _stateMachine.ChangeState(_idleState);
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

        _targetBox = closestBox;
        _stateMachine.ChangeState(_movingTowardsBox);
    }
}
