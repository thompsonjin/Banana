using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropABanana : MonoBehaviour
{
  public GameObject banana;
  public void DropBanana()
  {
      Instantiate(banana,transform.position,Quaternion.identity);

  }
}
