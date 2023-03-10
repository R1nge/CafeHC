using System;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private readonly List<CustomerMovement> _customers = new();
    public event Action OnCustomerRemoved;

    public int GetLastIndex() => _customers.Count;

    public Transform[] GetWaypoints() => waypoints;

    public void AddCustomer(CustomerMovement customer) => _customers.Add(customer);

    public void RemoveCustomer()
    {
        _customers.RemoveAt(0);
        OnCustomerRemoved?.Invoke();
    }
}