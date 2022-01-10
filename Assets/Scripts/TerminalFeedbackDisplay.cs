using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalFeedbackDisplay : MonoBehaviour
{
    private LineRenderer primaryRenderer;
    private Queue<float> traj;
    private bool plot;
    private bool startup = true;

    public string m1;
    public float bias = 0.8f;

    void OnEnable() {
        if (startup) {
            primaryRenderer = GetComponent<LineRenderer>();
            startup = false;
        }
        else {
            traj = GameObject.Find(m1).GetComponent<JointStateSubscriber>().pos;
            primaryRenderer.positionCount = traj.Count;
            DrawFeedback(primaryRenderer, traj);
        }

    }

    void OnDisable() {
        primaryRenderer.positionCount = 0;
    }

    void DrawFeedback(LineRenderer renderer, Queue<float> trajectory) {
        float x = 0f;
        int i = 0;
        foreach (float value in trajectory)
        {
            //x += i * 0.0025f;
            //renderer.SetPosition(i, new Vector3(0.064f*x, 150.0f*value, 0) - new Vector3(0, 150.0f*0.8f, 0));
            x += i * 10f/traj.Count;
            renderer.SetPosition(i, new Vector3(0.064f*x, 150.0f*value, 0) - new Vector3(0, 150.0f*0.8f, 0));
            i += 1;
        }
    }
}