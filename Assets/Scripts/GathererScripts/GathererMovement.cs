using System;
using Scripts.Events;
using UnityEngine;

namespace GathererScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GathererMovement : MonoBehaviour
    {
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _breakingDistance;

        [Space(5.0f)] 
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private float _rotationSnapMargin = 5f;
            
        private Rigidbody2D _rigidbody;

        private Vector2 _targetPosition;

        private bool _isMoving = false;
        private bool _hasCorrectFacing = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!_isMoving) return;
            Rotate();
            if(!_hasCorrectFacing) return;
            Move();
        }

        public void GoTo(Vector2 targetPosition)
        {
            _hasCorrectFacing = false;
            _targetPosition = targetPosition;
            _isMoving = true;
        }

        public void Stop()
        {
            _isMoving = false;
            _rigidbody.velocity = Vector2.zero;
        }

        private void Move()
        {
            if (GetDistanceToTarget() < _breakingDistance) {
                if (_rigidbody.velocity.magnitude > 0.1f)
                    _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, Vector2.zero, Time.fixedDeltaTime * _acceleration);
                else {
                    _rigidbody.velocity = Vector2.zero;
                    _isMoving = false;
                    EventManager.OnGathererTargetReached.OnEventRaised?.Invoke();
                }
            }
            else 
                _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, GetDirectionTowardsTarget() * _maxVelocity, Time.fixedDeltaTime * _acceleration);
        }

        private void Rotate()
        {
            if (_hasCorrectFacing) return;
            if (GetDirectionTowardsTarget().x < 0) {
                print("TARGET LEFT");
                if (transform.eulerAngles.y < 180 + _rotationSnapMargin)
                    transform.Rotate(Vector2.up * (Time.fixedDeltaTime * _rotationSpeed));
                else {
                    transform.eulerAngles = Vector2.up * 180;
                    _hasCorrectFacing = true;
                }
            }
            else {
                print("TARGET RIGHT");
                if (transform.eulerAngles.y > _rotationSnapMargin || transform.eulerAngles.y > 360 - _rotationSnapMargin)
                    transform.Rotate(Vector2.up * (Time.fixedDeltaTime * -_rotationSpeed));
                else {
                    transform.eulerAngles = Vector2.zero;
                    _hasCorrectFacing = true;
                }
            }
        }

        private float GetDistanceToTarget()
        {
            return Vector2.Distance(_rigidbody.position, _targetPosition);
        }

        private Vector2 GetDirectionTowardsTarget()
        {
            return new Vector2(_targetPosition.x - _rigidbody.position.x, 0).normalized;
        }
    }
}
