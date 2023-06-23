using System;
using UnityEngine;

namespace Tables
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Table[] tables;
        private Table _lastTable;

        public event Action<Table> OnFreeUpEvent;

        private void Awake()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].OnFreeUpEvent += OnFreeUp;
            }

            _lastTable = tables[0];
        }

        private void OnFreeUp(Table table)
        {
            OnFreeUpEvent?.Invoke(table);
        }

        public Table GetFreeTable()
        {
            if (_lastTable.GetFreeSeatsAmount() != 0)
            {
                return _lastTable;
            }

            foreach (var table in tables)
            {
                var seats = table.GetFreeSeatsAmount();
                var currentSeats = _lastTable.GetFreeSeatsAmount();
                if (seats != 0 && currentSeats < seats)
                {
                    _lastTable = table;
                    return _lastTable;
                }
            }

            return null;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].OnFreeUpEvent -= OnFreeUp;
            }
        }
    }
}