using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugs_AntPoint {
    public Vector3 Position;
    public float Strength = 1f;
    public MiniBugs_AntPoint FromPoint;
    public List<MiniBugs_AntPoint> ToPoints = new List<MiniBugs_AntPoint>();
    // If tofood is true, then travelling to this point will lead you to food.
    public bool ToFood = false;

    public MiniBugs_AntPoint(Vector3 position, MiniBugs_AntPoint fromPoint) {
        this.Position = position;
        this.FromPoint = fromPoint;
    }

    public MiniBugs_AntPoint GetNextPoint() {
        if (ToPoints.Count == 0) {
            return null;
        } else {
            return ToPoints[UnityEngine.Random.Range(0, ToPoints.Count - 1)];
        }
    }
}

public class MiniBugs_AntPointManager : MonoBehaviour {
    [HideInInspector]
    public List<MiniBugs_AntPoint> AntPoints = new List<MiniBugs_AntPoint>();

    public MiniBugs_AntPoint AddPoint(Vector3 pos, MiniBugs_AntPoint fromPoint) {
        MiniBugs_AntPoint newPoint = new MiniBugs_AntPoint(pos, fromPoint);
        AntPoints.Add(newPoint);
        if (fromPoint != null) {
            fromPoint.ToPoints.Add(newPoint);
        }

        return newPoint;
    }

    public MiniBugs_AntPoint GetNearestAntPoint(Vector3 pos, float radius) {
        if (AntPoints.Count > 0) {
            MiniBugs_AntPoint closest = null;
            float dist = 0f;
            foreach (MiniBugs_AntPoint point in AntPoints) {
                Vector3 to = pos - point.Position;
                to.z = 0f;
                if (to.magnitude <= radius && (closest == null || to.magnitude < dist)) {
                    closest = point;
                    dist = to.magnitude;
                }
            }

            return closest;
        } else {
            return null;
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }
}
