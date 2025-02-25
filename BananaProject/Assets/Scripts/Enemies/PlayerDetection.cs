using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

  [SerializeField] private BaseEnemy e_AI;

  void OnTriggerEnter2D(Collider2D col)
  {
    if(col.gameObject.tag == "Player")
    {
        e_AI.SetPatrol(false);
    }
  }
}
