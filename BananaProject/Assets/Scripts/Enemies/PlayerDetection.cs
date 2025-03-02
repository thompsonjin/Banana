using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

  [SerializeField] private RoboMonkeyAI r_AI;

  void OnTriggerEnter2D(Collider2D col)
  {
    if(col.gameObject.tag == "Player")
    {
        r_AI.SetPatrol(false);
    }
  }
}
