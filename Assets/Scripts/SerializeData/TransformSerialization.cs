using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.IO;

public class TransformSerialization : MonoBehaviour
{
    static public void RegisterTransform()
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

        using (MemoryStream stream = new MemoryStream()) {
            using (BinaryWriter writer = new BinaryWriter(stream)) {
                writer.Write(transform.localPosition.x);
                writer.Write(transform.localPosition.y);
                writer.Write(transform.localPosition.z);

                writer.Write(transform.localRotation.x);
                writer.Write(transform.localRotation.y);
                writer.Write(transform.localRotation.z);
                writer.Write(transform.localRotation.w);

                writer.Write(transform.localScale.x);
                writer.Write(transform.localScale.y);
                writer.Write(transform.localScale.z);
            }

            return stream.ToArray();
        }
    }

    private static object DeserializeTransform(byte[] data)
    {
        Transform transform = new GameObject().transform;

        using (MemoryStream stream = new MemoryStream(data)) {
            using (BinaryReader reader = new BinaryReader(stream)) {
                float posX = reader.ReadSingle();
                float posY = reader.ReadSingle();
                float posZ = reader.ReadSingle();
                transform.localPosition = new Vector3(posX, posY, posZ);

                float rotX = reader.ReadSingle();
                float rotY = reader.ReadSingle();
                float rotZ = reader.ReadSingle();
                float rotW = reader.ReadSingle();
                transform.localRotation = new Quaternion(rotX, rotY, rotZ, rotW);

                float scaleX = reader.ReadSingle();
                float scaleY = reader.ReadSingle();
                float scaleZ = reader.ReadSingle();
                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            }
        }

        return transform;
    }
}
