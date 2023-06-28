using UnityEngine;
using ExitGames.Client.Photon;
using System;

public class TransformSerialization
{
    public static void RegisterTransform()
    {
        PhotonPeer.RegisterType(
            typeof(Transform),
            (byte)'T',
            SerializeTransform,
            DeserializeTransform
        );
    }

    private static byte[] SerializeTransform(object obj)
    {
        Transform transform = (Transform)obj;
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        byte[] data = new byte[28];
        Buffer.BlockCopy(BitConverter.GetBytes(position.x), 0, data, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(position.y), 0, data, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(position.z), 0, data, 8, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rotation.x), 0, data, 12, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rotation.y), 0, data, 16, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rotation.z), 0, data, 20, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(rotation.w), 0, data, 24, 4);

        return data;
    }

    private static object DeserializeTransform(byte[] data)
    {
        Vector3 position = new Vector3(
            BitConverter.ToSingle(data, 0),
            BitConverter.ToSingle(data, 4),
            BitConverter.ToSingle(data, 8)
        );
        Quaternion rotation = new Quaternion(
            BitConverter.ToSingle(data, 12),
            BitConverter.ToSingle(data, 16),
            BitConverter.ToSingle(data, 20),
            BitConverter.ToSingle(data, 24)
        );

        GameObject dummyObject = new GameObject();
        Transform transform = dummyObject.transform;
        transform.position = position;
        transform.rotation = rotation;

        return transform;
    }
}
