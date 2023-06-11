using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    
    public static ItemAssets Instance { get; private set; }

    private void Awake()//"[start and awake are] Almost the same thing; Awake occurs before Start. Idea is to set non dependency references (self relying only) in Awake and stuff that relies on others in Start."
    {
        Instance = this;
    }


    public static void prtins()
    {
        Debug.Log(Instance);
    }

    #region assets
    //for inventory
    //where we write all of our asset sprites
    //see item: getSprite when adding more sprites
    public Sprite JumpBoostSprite;
    public Sprite SpeedBoostSprite;
    public Sprite RegenBoostSprite;
    public Sprite AttackBoostSprite;
    #endregion

    #region 3d Prefabs
    public GameObject JumpBoostPrefab;
    public GameObject SpeedBoostPrefab;
    public GameObject RegenBoostPrefab;
    public GameObject AttackBoostPrefab;
    #endregion
}
