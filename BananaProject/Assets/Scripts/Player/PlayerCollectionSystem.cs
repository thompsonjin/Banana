using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionSystem : MonoBehaviour
{
    PlayerController pControl;

    private void Start()
    {
        pControl = GetComponent<PlayerController>();
    }

    //Methods to detect shards
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana Coin"))
        {
            pControl.GiveBanana(1);
            Destroy(collision.gameObject);
        }

        //Shadow kick shard detection
        if (collision.gameObject.CompareTag("Shadow Shard"))
        {
            pControl.EnableShadowKick();
            Destroy(collision.gameObject);
        }

        //Ground Pound shard detection
        if (collision.gameObject.CompareTag("Pound Shard"))
        {
            pControl.EnableGroundPound();
            Destroy(collision.gameObject);
        }

        //Banana Gun shard detection
        if (collision.gameObject.CompareTag("Gun Shard"))
        {
            pControl.EnableBananaGun();
            Destroy(collision.gameObject);
        }
    }
}
