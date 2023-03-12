using Tables;
using UnityEditor;
using UnityEngine;

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
            PrefabUtility.SaveAsPrefabAsset(myTable.gameObject, $"Assets/Prefabs/{myTable.gameObject.name}.prefab");
        }
    }
}