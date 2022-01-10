using RosMessageTypes.UnityRoboticsDemo;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;

public class RosDisplayService : MonoBehaviour
{
    [SerializeField]
    string display_ServiceName = "/display_srv"; // or display_srv
    [SerializeField]
    string trial_ServiceName = "/trial_srv"; // or trial_srv

    public GameObject main;

    void Start()
    {
        // register the services with ROS
        ROSConnection.GetOrCreateInstance().ImplementService<ChangeDisplayServiceRequest, ChangeDisplayServiceResponse>(display_ServiceName, ChangeDisplay);
        ROSConnection.GetOrCreateInstance().ImplementService<TrialParamServiceRequest,TrialParamServiceResponse>(trial_ServiceName, TrialParam);
    }

    private ChangeDisplayServiceResponse ChangeDisplay(ChangeDisplayServiceRequest req)
    {
        // process the service request
        Debug.Log("Received display change to: " + req.flag);
        main.GetComponent<MainLoop>().display_flag = req.flag;
        ChangeDisplayServiceResponse changeDisplayResponse = new ChangeDisplayServiceResponse();

        return changeDisplayResponse;
    }

    private TrialParamServiceResponse TrialParam(TrialParamServiceRequest req)
    {
        // process the service request
        Debug.Log("Received social_flag change to: " + req.social_flag);
        main.GetComponent<MainLoop>().social_flag = req.social_flag;
        main.GetComponent<MainLoop>().bar_heights = req.bar_heights;
        TrialParamServiceResponse trialResponse = new TrialParamServiceResponse();

        return trialResponse;
    }
}
