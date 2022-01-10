using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Float64 = RosMessageTypes.Std.Float64Msg;

public class TimeBar : MonoBehaviour
{
    // Subscribes to time
    private bool fill;
    private bool startup = true;
    private Vector3 startPos;
    private float startY;
    private float screen_width = 1280.0f;
    private float percent_time;

    public float trial_duration = 15.0f;

    void OnEnable() {
        if (startup) {
            startPos = GetComponent<RectTransform>().localPosition;
            startY = startPos.y;
            ROSConnection.GetOrCreateInstance().Subscribe<Float64>("/trial_time", GetTime);
            startup = false;
        }
        GetComponent<RectTransform>().localPosition = startPos;
        fill = true;
    }

    void OnDisable() {
        fill = false;
    }

    void GetTime(Float64 d) {
        percent_time = (float)d.data/(trial_duration);
    }

    void FillBar() {
        if (GetComponent<RectTransform>().localPosition.x < 0) {
            GetComponent<RectTransform>().localPosition = new Vector3((-screen_width*(1f-percent_time)), startY, 0);
        }
    }

    private void Update()
    {
        if (fill) {
            FillBar();
        }
    }
}
