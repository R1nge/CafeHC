using UnityEngine;

namespace Tables
{
    public class Seat : MonoBehaviour
    {
        [SerializeField] private Transform position;
        private bool _isOccupied;

        public void SetStatus(bool value) => _isOccupied = value;

        public bool GetStatus() => _isOccupied;

        public Vector3 GetPosition() => position.position;
    }
}