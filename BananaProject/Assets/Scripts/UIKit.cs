using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class UIKit 
{
    public static   void Open(string prefab,Transform parent)
    {
      GameObject temp=  GameObject.Instantiate(Resources.Load<GameObject>("prefab"));
      temp.transform.parent = parent;

    }
    public  static void Close(GameObject  pannel)
    {
        GameObject.Destroy(pannel);

    }
}
