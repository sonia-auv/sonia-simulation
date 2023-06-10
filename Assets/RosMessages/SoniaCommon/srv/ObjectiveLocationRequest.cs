//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.SoniaCommon
{
    [Serializable]
    public class ObjectiveLocationRequest : Message
    {
        public const string k_RosMessageName = "sonia_common/ObjectiveLocation";
        public override string RosMessageName => k_RosMessageName;

        public const byte HYDROPHONE = 0;
        public const byte DICE = 1;
        public const byte SONAR = 2;
        public const byte HYDROPHONE_20 = 20;
        public const byte HYDROPHONE_21 = 21;
        public const byte HYDROPHONE_22 = 22;
        public const byte HYDROPHONE_23 = 23;
        public const byte HYDROPHONE_24 = 24;
        public const byte HYDROPHONE_25 = 25;
        public const byte HYDROPHONE_26 = 26;
        public const byte HYDROPHONE_27 = 27;
        public const byte HYDROPHONE_28 = 28;
        public const byte HYDROPHONE_29 = 29;
        public const byte HYDROPHONE_30 = 30;
        public const byte HYDROPHONE_31 = 31;
        public const byte HYDROPHONE_32 = 32;
        public const byte HYDROPHONE_33 = 33;
        public const byte HYDROPHONE_34 = 34;
        public const byte HYDROPHONE_35 = 35;
        public const byte HYDROPHONE_36 = 36;
        public const byte HYDROPHONE_37 = 37;
        public const byte HYDROPHONE_38 = 38;
        public const byte HYDROPHONE_39 = 39;
        public const byte HYDROPHONE_40 = 40;
        public const byte DICE_1 = 1;
        public const byte DICE_2 = 2;
        public const byte DICE_3 = 3;
        public const byte DICE_4 = 4;
        public const byte DICE_5 = 5;
        public const byte DICE_6 = 6;
        public byte objective;
        public byte sub_objective;

        public ObjectiveLocationRequest()
        {
            this.objective = 0;
            this.sub_objective = 0;
        }

        public ObjectiveLocationRequest(byte objective, byte sub_objective)
        {
            this.objective = objective;
            this.sub_objective = sub_objective;
        }

        public static ObjectiveLocationRequest Deserialize(MessageDeserializer deserializer) => new ObjectiveLocationRequest(deserializer);

        private ObjectiveLocationRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.objective);
            deserializer.Read(out this.sub_objective);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.objective);
            serializer.Write(this.sub_objective);
        }

        public override string ToString()
        {
            return "ObjectiveLocationRequest: " +
            "\nobjective: " + objective.ToString() +
            "\nsub_objective: " + sub_objective.ToString();
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
