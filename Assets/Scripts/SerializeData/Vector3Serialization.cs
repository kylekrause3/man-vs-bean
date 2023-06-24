using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;

public class Vector3Serialization : MonoBehaviour
{
    static public void RegisterVector3()
    {
        // Register the Vector3 serialization methods
        PhotonPeer.RegisterType(
            typeof(Vector3),  // Custom type class
            (byte)'V',        // Custom type code (choose an unused byte code)
            SerializeVector3, // Serialization method
            DeserializeVector3 // Deserialization method
        );
    }

    private static byte[] SerializeVector3(object obj)
    {
        Vector3 vector = (Vector3)obj;
        byte[] data = new byte[12];
        byte[] xBytes = BitConverter.GetBytes(vector.x);
        byte[] yBytes = BitConverter.GetBytes(vector.y);
        byte[] zBytes = BitConverter.GetBytes(vector.z);
        xBytes.CopyTo(data, 0);
        yBytes.CopyTo(data, 4);
        zBytes.CopyTo(data, 8);
        return data;
    }

    private static object DeserializeVector3(byte[] data)
    {
        float x = BitConverter.ToSingle(data, 0);
        float y = BitConverter.ToSingle(data, 4);
        float z = BitConverter.ToSingle(data, 8);
        Vector3 vector = new Vector3(x, y, z);
        return vector;
    }
}
