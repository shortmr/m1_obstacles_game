using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    public GameObject preX;
    public GameObject gameX;
    public GameObject postX;
    public GameObject preY;
    public GameObject gameY;
    public GameObject postY;
    public GameObject mX;
    public GameObject mY;
    public GameObject m1_postX_secondary;
    public GameObject m1_postY_secondary;
    public GameObject obsX;
    public GameObject obsY;

    public int stage;
    public double[] bar_heights;

    public int display_flag;
    public bool social_flag;

    private int prev_display_flag;
    private bool prev_social_flag;
    private int max_stage;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        max_stage = 10;

        // Set flags to 0 for start
        prev_display_flag = 0;
        display_flag = 0;

        social_flag = false;
        prev_social_flag = false;

        // Initialize pre-trial presentation
        preX.SetActive(true);
        preY.SetActive(true);

        gameX.SetActive(false);
        gameY.SetActive(false);
        postX.SetActive(false);
        postY.SetActive(false);

        mX.GetComponent<JointStateSubscriber>().enabled = false;
        mY.GetComponent<JointStateSubscriber>().enabled = false;
        m1_postX_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = false;
        m1_postY_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = false;
    }

    void Update()
    {
        if (display_flag != prev_display_flag)
        {
            if (stage <= max_stage) {
                if (display_flag == 1)
                {
                    // Initialize pre-trial presentation
                    preX.SetActive(true);
                    preY.SetActive(true);

                    gameX.SetActive(false);
                    gameY.SetActive(false);
                    postX.SetActive(false);
                    postY.SetActive(false);

                    mX.GetComponent<JointStateSubscriber>().enabled = false;
                    mY.GetComponent<JointStateSubscriber>().enabled = false;

                    obsX.GetComponent<RectTransform>().localPosition = new Vector3(-395, -300f + ((float)bar_heights[stage]*100), 0);
                    obsY.GetComponent<RectTransform>().localPosition = new Vector3(-395, -300f + ((float)bar_heights[stage]*100), 0);

                }
                else if (display_flag == 2) {

                    // Initialize game
                    gameX.SetActive(true);
                    gameY.SetActive(true);
                    preX.SetActive(true);
                    preY.SetActive(true);

                    postX.SetActive(false);
                    postY.SetActive(false);

                    mX.GetComponent<JointStateSubscriber>().enabled = true;
                    mY.GetComponent<JointStateSubscriber>().enabled = true;
                }
                else if (display_flag == 3) {
                    // Initialize post-trial presentation
                    postX.SetActive(true);
                    postY.SetActive(true);
                    preX.SetActive(true);
                    preY.SetActive(true);

                    gameX.SetActive(false);
                    gameY.SetActive(false);

                    mX.GetComponent<JointStateSubscriber>().enabled = false;
                    mY.GetComponent<JointStateSubscriber>().enabled = false;
                    stage += 1;
                }
                prev_display_flag = display_flag;
            }
            else {
                // Initialize blank screen
                postX.SetActive(false);
                postY.SetActive(false);
                preX.SetActive(false);
                preY.SetActive(false);
                gameX.SetActive(false);
                gameY.SetActive(false);

                mX.GetComponent<JointStateSubscriber>().enabled = false;
                mY.GetComponent<JointStateSubscriber>().enabled = false;
            }
        }

        if (social_flag != prev_social_flag)
        {
            if (social_flag) {
                m1_postX_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = true;
                m1_postY_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = true;
            }
            else {
                m1_postX_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = false;
                m1_postY_secondary.GetComponent<TerminalFeedbackDisplay>().enabled = false;
            }
            prev_social_flag = social_flag;
        }
    }
}
