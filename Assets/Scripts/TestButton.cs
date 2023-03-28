using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGenerator))]
public class TestButton : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    
        TileGenerator generator = (TileGenerator)target;
        if(GUILayout.Button("Tile Generation")) {
            generator.TileSet();
        }

        TileGenerator delete = (TileGenerator)target;
        if(GUILayout.Button("Tile Delete")) {
            generator.Delete();
        }
    }
}
