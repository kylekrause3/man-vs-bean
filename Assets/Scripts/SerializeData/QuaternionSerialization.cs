using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class QuaternionSerialization : MonoBehaviour
{
    static public void RegisterQuaternion()
    {
        PhotonPeer.RegisterType(
            typeof(Quaternion),
            (byte)'Q',
            SerializeQuaternion,
            DeserializeQuaternion
        );
    }

    private static byte[] SerializeQuaternion(object obj)
    {
        Quaternion quaternion = (Quaternion)obj;
        byte[] data = new byte[16];
        byte[] xBytes = BitConverter.GetBytes(quaternion.x);
        byte[] yBytes = BitConverter.GetBytes(quaternion.y);
        byte[] zBytes = BitConverter.GetBytes(quaternion.z);
        byte[] wBytes = BitConverter.GetBytes(quaternion.w);
        xBytes.CopyTo(data, 0);
        yBytes.CopyTo(data, 4);
        zBytes.CopyTo(data, 8);
        wBytes.CopyTo(data, 12);
        return data;
    }

    private static object DeserializeQuaternion(byte[] data)
    {
        float x = BitConverter.ToSingle(data, 0);
        float y = BitConverter.ToSingle(data, 4);
        float z = BitConverter.ToSingle(data, 8);
        float w = BitConverter.ToSingle(data, 12);
        Quaternion quaternion = new Quaternion(x, y, z, w);
        return quaternion;
    }
}
