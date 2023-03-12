using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Tables
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private List<Seat> seats;
        
        [Conditional("UNITY_EDITOR")]
        public void FindSeats()
        {
            seats = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out Seat seat))
                {
                    seats.Add(seat);
                }
            }
        }

        public bool HasFreeSeat()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                if (seats[i].GetStatus())
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public Seat GetFreeSeat()
        {
            for (int i = 0; i < seats.Count; i++)
            {
                if (seats[i].GetStatus())
                {
                    continue;
                }

                seats[i].SetStatus(true);
                print($"GET FREE SEAT {i}");
                return seats[i];
            }

            return null;
        }
    }
}