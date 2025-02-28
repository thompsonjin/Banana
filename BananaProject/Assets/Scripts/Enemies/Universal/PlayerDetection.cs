using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

  [SerializeField] private BaseEnemy e_AI;

    private void Start()
    {
        e_AI = this.transform.parent.gameObject.GetComponent<BaseEnemy>();
    }

    void OnTriggerEnter2D(Collider2D col)
  {
    if(col.gameObject.tag == "Player")
    {
        e_AI.SetPatrol(false);
    }
  }
}
