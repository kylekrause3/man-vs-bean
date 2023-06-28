using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public class PhotonObject : MonoBehaviourPunCallbacks
{
    public static PhotonView photonViewInstance;

    private void Awake()
    {
        photonViewInstance = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Notice that this will only work if the gameObject is already active once it is deactive this will not work to find the gameObject anymore
    /// FUCK ME IN THE ASS IT WON'T WORK WITH INACTIVE OBJECTS
    /// </summary>
    /// <param name="gameObjectName"></param>
    /// <param name="state"></param>
    [PunRPC]
    public void SetGameObjectActive(string gameObjectName, bool state)
    {
        GameObject targetGameObject = GameObject.Find(gameObjectName);

        /*
        Transform root = transform.root;
        GameObject[] gameObjectsArr = root.GetComponentsInChildren<Transform>(includeInactive: true)
                .Where(t => t.gameObject != root.gameObject && t.gameObject.activeInHierarchy)
                .Select(t => t.gameObject)
                .ToArray();
        */

        /*
        //Debug.Log("------------------- These are the local objects -------------------");
        foreach (GameObject localObject in gameObjectsArr) {
            //Debug.Log("Local Object: " + localObject.name + " | gameObjectName: " + gameObjectName);
            if (localObject.name.Equals(gameObjectName)) {
                //Debug.Log("FUCKIGN | Local Object: " + localObject.name + " | gameObjectName: " + gameObjectName);
                localObject.SetActive(state);
                targetGameObject = localObject;
                break;
            }
        }
        */

        if (targetGameObject == null) {
            Debug.Log("It is very likely that you are trying to find an object that is deactivated");
        } else {
            targetGameObject.SetActive(state);
        }
    }

    /// <summary>
    /// Supports MonoBehaviour, MonoBehaviourPun, MonoBehaviourPunCallbacks anything else will resault in an error
    /// </summary>
    /// <param name="scriptName"></param>
    /// <param name="state"></param>
    [PunRPC]
    public void EnableScript(string scriptName, bool state)
    {
        MonoBehaviour scriptMono = GetComponent(scriptName) as MonoBehaviour;
        if (scriptMono != null) {
            scriptMono.enabled = state;
            return;
        }
        MonoBehaviourPun scriptMonoPun = GetComponent(scriptName) as MonoBehaviourPun;
        if (scriptMonoPun != null) {
            scriptMonoPun.enabled = state;
            return;
        }
        MonoBehaviourPunCallbacks scriptMonoPunCallbacks = GetComponent(scriptName) as MonoBehaviourPunCallbacks;
        if (scriptMonoPunCallbacks != null) {
            scriptMonoPunCallbacks.enabled = state;
            return;
        }
        CharacterController scriptCharacterController = GetComponent(scriptName) as CharacterController;
        if (scriptCharacterController != null) {
            scriptCharacterController.enabled = state;
            return;
        }
        CapsuleCollider scriptCapsuleCollider = GetComponent(scriptName) as CapsuleCollider;
        if (scriptCapsuleCollider != null) {
            scriptCapsuleCollider.enabled = state;
            return;
        }

        Debug.Log("Script not found please add the component to the list moron: " + scriptName);
    }
}
