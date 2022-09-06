using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public Sprite FlashLightSprite;

    public static ItemAssets Instance;


    private void Awake()
    {
        Instance = this;
    }

}
