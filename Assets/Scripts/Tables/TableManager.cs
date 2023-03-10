using UnityEngine;

namespace Tables
{
    public class TableManager : MonoBehaviour
    {
        [SerializeField] private Table[] tables;

        public Table GetFreeTable()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[i].HasFreeSeat())
                {
                    return tables[i];
                }
            }

            return null;
        }
    }
}