using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tables
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private List<Seat> _seats;

        public void FindSeats()
        {
            _seats = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Seat seat))
                {
                    _seats.Add(seat);
                }
            }

            PrefabUtility.SavePrefabAsset(gameObject);
        }

        public bool HasFreeSeat()
        {
            for (int i = 0; i < _seats.Count; i++)
            {
                if (_seats[i].GetStatus())
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public Seat GetFreeSeat()
        {
            for (int i = 0; i < _seats.Count; i++)
            {
                if (_seats[i].GetStatus())
                {
                    continue;
                }

                _seats[i].SetStatus(true);
                print($"GET FREE SEAT {i}");
                return _seats[i];
            }

            return null;
        }
    }
}