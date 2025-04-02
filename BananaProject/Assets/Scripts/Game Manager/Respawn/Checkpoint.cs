using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] int order;

    private void Awake()
    {
        ActivatedCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckpointManager.NextCheckpoint();

            Destroy(transform.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    public void ActivatedCheck()
    {
        if(CheckpointManager.checkpointNum >= order)
        {
            if(transform.gameObject.GetComponent<BoxCollider2D>() != null)
            {
                Destroy(transform.gameObject.GetComponent<BoxCollider2D>());
            }
        }
    }
}
