using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssets : MonoBehaviour
{
    public static EnemyAssets Instance { get; private set; }

    void Awake ()
    {
        Instance = this;
    }


    #region 3d Prefabs
    public GameObject defaultEnemy;
    #endregion
}
