using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using JointState = RosMessageTypes.Sensor.JointStateMsg;

public class JointStateSubscriber : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int max_count = 4000;
    private bool plot;
    private bool startup = true;

    public string m1;
    public float bias = 0.8f;
    public bool visualFeedback;
    public Queue<float> pos;

    void OnEnable() {
        if (startup) {
            lineRenderer = GetComponent<LineRenderer>();
            ROSConnection.GetOrCreateInstance().Subscribe<JointState>("/" + m1 + "/", StreamData);
            startup = false;
        }
        lineRenderer.positionCount = 1;
        pos = new Queue<float>(lineRenderer.positionCount);
        pos.Enqueue(0f);
        plot = true;
    }

    void OnDisable() {
        plot = false;
    }

    void StreamData(JointState d) {
        if (pos.Count <= max_count) {
            pos.Enqueue((float)d.position[0]);
            lineRenderer.positionCount = lineRenderer.positionCount + 1;
        }
    }

    void DrawPlot() {
        float x = 0f;
        int i = 0;
        foreach (float value in pos)
        {
            x += i * 0.0025f;
            lineRenderer.SetPosition(i, new Vector3(0.064f*x, 150.0f*value, 0) - new Vector3(0, 150.0f*0.8f, 0));
            i += 1;
        }
    }

    private void Update()
    {
        if (plot & visualFeedback) {
            DrawPlot();
        }
    }
}
