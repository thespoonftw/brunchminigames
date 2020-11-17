using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MiniBugs_AntPointManager))]
public class MiniBugs_AntPointEditor : Editor {
    private MiniBugs_AntPointManager apm;

    public void OnSceneGUI() {
        apm = this.target as MiniBugs_AntPointManager;
        for (int i = 0; i < apm.AntPoints.Count; i++) {
            MiniBugs_AntPoint p = apm.AntPoints[i];
            Handles.DrawWireDisc(
                p.Position,
                Vector3.forward,
                1f
            );

            for (int j = 0; j < apm.AntPoints[i].ToPoints.Count; j++) {
                Handles.DrawLine(
                    apm.AntPoints[i].ToPoints[j].Position,
                    apm.AntPoints[i].Position
                );
            }
        }
    }
}
