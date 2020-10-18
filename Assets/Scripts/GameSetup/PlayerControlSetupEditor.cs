using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerControlSetup))]
public class PlayerControlSetupEditor : Editor
{
    void OnSceneGUI() {
        PlayerControlSetup p = target as PlayerControlSetup;
        for (int i = 0; i < p.SpawnPositions.Length; i++) {
            Vector3 v = p.SpawnPositions[i];

            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPos = Handles.PositionHandle(v, Quaternion.identity);
            if (EditorGUI.EndChangeCheck()) {
                p.SpawnPositions[i] = newTargetPos;
                // p.Update();
            }
        }
    }
}
