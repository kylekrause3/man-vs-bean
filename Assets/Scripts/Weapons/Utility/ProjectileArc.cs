using UnityEngine;
using System.Collections;
using Photon.Pun;

public class ProjectileArc : MonoBehaviourPunCallbacks
{
    public GameObject objectThrown;
    public float throwForce = 10f;
    public AnimationCurve throwCurve;

    public float curveDuration = 1f;

    [PunRPC]
    public void throwObject()
    {
        GameObject thrownObject = Instantiate(objectThrown, transform.position, transform.rotation);
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

        if (rb != null) {
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);

            // Apply the curve to the object's trajectory
            StartCoroutine(ApplyCurve(rb));
        }
        Destroy(thrownObject, 5);
    }

    public void throwObjectRPC()
    {
        photonView.RPC("throwObject", RpcTarget.All);
    }

    private IEnumerator ApplyCurve(Rigidbody rb)
    {
        float elapsedTime = 0f;

        while (elapsedTime < curveDuration) {
            // Calculate the normalized time value (0 to 1) based on the elapsed time
            float normalizedTime = elapsedTime / curveDuration;

            // Apply the curve value to modify the trajectory
            float curveValue = throwCurve.Evaluate(normalizedTime);
            rb.AddForce(Vector3.up * curveValue, ForceMode.Acceleration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
