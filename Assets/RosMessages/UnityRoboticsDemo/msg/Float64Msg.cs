//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.UnityRoboticsDemo
{
    [Serializable]
    public class Float64Msg : Message
    {
        public const string k_RosMessageName = "unity_robotics_demo_msgs/Float64";
        public override string RosMessageName => k_RosMessageName;

        public double data;

        public Float64Msg()
        {
            this.data = 0.0;
        }

        public Float64Msg(double data)
        {
            this.data = data;
        }

        public static Float64Msg Deserialize(MessageDeserializer deserializer) => new Float64Msg(deserializer);

        private Float64Msg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.data);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.data);
        }

        public override string ToString()
        {
            return "Float64Msg: " +
            "\ndata: " + data.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}