using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class CustomerStateManager : MonoBehaviour
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;
        private CustomerMovement _customerMovement;
        private Waypoints _waypoints;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            SetDefault();
        }

        private void Init()
        {
            _customerMovement = GetComponent<CustomerMovement>();
            _waypoints = FindObjectOfType<Waypoints>();

            _states = new Dictionary<Type, IState>
            {
                [typeof(CustomerInQueueState)] = new CustomerInQueueState(this, _customerMovement, _waypoints),
                [typeof(CustomerAtCounterState)] = new CustomerAtCounterState(this, _customerMovement, _waypoints),
                [typeof(CustomerSearchForFreeTableState)] = new CustomerSearchForFreeTableState()
            };
        }

        public void SetCustomerInQueue()
        {
            SetState(GetState<CustomerInQueueState>());
        }

        public void SetCustomerAtCounter()
        {
            SetState(GetState<CustomerAtCounterState>());
        }

        public void SetCustomerSearchForFreeTable()
        {
            SetState(GetState<CustomerSearchForFreeTableState>());
        }

        private void SetState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void SetDefault()
        {
            SetCustomerInQueue();
        }

        private IState GetState<T>() where T : IState
        {
            var type = typeof(T);
            return _states[type];
        }

        private void Update()
        {
            _currentState?.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentState?.OnTriggerEnter(other);
        }
    }
}