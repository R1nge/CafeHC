using System;
using System.Collections.Generic;
using Tables;
using UnityEngine;

namespace AI
{
    public class CustomerStateManager : MonoBehaviour
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;
        private CustomerMovement _customerMovement;
        private CustomerInventoryUI _inventoryUI;
        private Waypoints _waypoints;
        private TableManager _tableManager;
        private CustomerInventory _customerInventory;

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
            _tableManager = FindObjectOfType<TableManager>();
            _inventoryUI = GetComponent<CustomerInventoryUI>();
            _customerInventory = GetComponent<CustomerInventory>();

            _states = new Dictionary<Type, IState>
            {
                [typeof(CustomerInQueueState)] = new CustomerInQueueState(this, _customerMovement, _waypoints),
                [typeof(CustomerMakeOrderState)] = new CustomerMakeOrderState(_inventoryUI),
                [typeof(CustomerSearchForFreeTableState)] = new CustomerSearchForFreeTableState(this, _tableManager, _customerMovement, _waypoints),
                [typeof(CustomerPlaceOrderState)] = new CustomerPlaceOrderState(this, _customerInventory),
                [typeof(CustomerGoHomeState)] = new CustomerGoHomeState(_customerMovement, _waypoints.GetHome())
            };
        }

        public void SetCustomerInQueue()
        {
            SetState(GetState<CustomerInQueueState>());
        }

        public void SetCustomerOrderState()
        {
            SetState(GetState<CustomerMakeOrderState>());
        }

        public void SetCustomerSearchForFreeTable()
        {
            SetState(GetState<CustomerSearchForFreeTableState>());
        }

        public void SetCustomerGoHome()
        {
            SetState(GetState<CustomerGoHomeState>());
        }

        public void SetCustomerPlaceOrder()
        {
            SetState(GetState<CustomerPlaceOrderState>());
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

        private void OnTriggerStay(Collider other)
        {
            _currentState?.OnTriggerStay(other);
        }
    }
}