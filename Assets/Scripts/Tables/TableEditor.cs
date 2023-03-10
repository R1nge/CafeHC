using UnityEditor;
using UnityEngine;

namespace Tables
{
    [CustomEditor(typeof(Table))]
    class TableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            Table myTable = (Table)target;
            if (GUILayout.Button("Find all seats"))
            {
                myTable.FindSeats();
            }
        }
    }
}