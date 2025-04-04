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
            Debug.Log("Collected banana coin");
            pControl.IncreaseMaxBananas();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }

        //Shadow kick shard detection
        if (collision.gameObject.CompareTag("Shadow Shard"))
        {
            pControl.EnableShadowKick();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }

        //Ground Pound shard detection
        if (collision.gameObject.CompareTag("Pound Shard"))
        {
            pControl.EnableGroundPound();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }

        //Banana Gun shard detection
        if (collision.gameObject.CompareTag("Gun Shard"))
        {
            pControl.EnableBananaGun();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }

        //Banana shield shard detection
        if (collision.gameObject.CompareTag("Shield Shard"))
        {
            pControl.EnableBananaShield();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }

        //Banana shield shard detection
        if (collision.gameObject.CompareTag("Charge Shard"))
        {
            pControl.EnableCharge();
            Destroy(collision.gameObject);
            pControl.PlayPickup();
        }
    }
}
