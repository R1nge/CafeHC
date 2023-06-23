using System.Collections.Generic;
using AI;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform home;
    [SerializeField] private Transform[] waypoints;
    private readonly List<CustomerMovement> _customers = new();

    public int GetLastIndex() => _customers.Count;

    public Transform[] GetWaypoints() => waypoints;

    public Vector3 Home() => home.position;

    public void AddCustomer(CustomerMovement customer) => _customers.Add(customer);

    public void RemoveCustomer()
    {
        _customers.RemoveAt(0);
        for (int i = 0; i < _customers.Count; i++)
        {
            _customers[i].MoveToNextWaypoint();
        }
    }
}