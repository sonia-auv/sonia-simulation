//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class MissionTimerMsg : Message
    {
        public const string k_RosMessageName = "sonia_common/MissionTimer";
        public override string RosMessageName => k_RosMessageName;

        public string mission;
        public string uniqueID;
        public byte timeout;
        public byte status;
        //  list of status:
        //  1 - Starting
        //  2 - Completed
        //  3 - Timed out
        //  4 - Failed

        public MissionTimerMsg()
        {
            this.mission = "";
            this.uniqueID = "";
            this.timeout = 0;
            this.status = 0;
        }

        public MissionTimerMsg(string mission, string uniqueID, byte timeout, byte status)
        {
            this.mission = mission;
            this.uniqueID = uniqueID;
            this.timeout = timeout;
            this.status = status;
        }

        public static MissionTimerMsg Deserialize(MessageDeserializer deserializer) => new MissionTimerMsg(deserializer);

        private MissionTimerMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.mission);
            deserializer.Read(out this.uniqueID);
            deserializer.Read(out this.timeout);
            deserializer.Read(out this.status);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.mission);
            serializer.Write(this.uniqueID);
            serializer.Write(this.timeout);
            serializer.Write(this.status);
        }

        public override string ToString()
        {
            return "MissionTimerMsg: " +
            "\nmission: " + mission.ToString() +
            "\nuniqueID: " + uniqueID.ToString() +
            "\ntimeout: " + timeout.ToString() +
            "\nstatus: " + status.ToString();
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