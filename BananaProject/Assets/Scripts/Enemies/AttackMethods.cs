using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethods : MonoBehaviour
{
   public void RoboMonkeyAttack()
   {
      Transform enemy = transform.parent.parent;
      float ap=  GetComponentInParent<EnemyController>().ap;
      Vector2 boxCenter = enemy.position + new Vector3(1, 0, 0) *( enemy.localScale.x > 0 ? 1.44f : -1.44f);
       Collider2D  player= Physics2D.OverlapBox(boxCenter, new Vector2(1.38f, 1.25f), 0,LayerMask.GetMask("Player"));
       if (player!=null)
       {
          player.gameObject.GetComponent<PlayerController>().TakeDamage();
       }

   }
   public void RobotBaboonAttack()
   {
      Transform enemy = transform.parent.parent;
      float ap=  GetComponentInParent<EnemyController>().ap;
      Vector2 boxCenter = enemy.position + new Vector3(1, 0, 0) *( enemy.localScale.x > 0 ? 1.44f : -1.44f);
      Collider2D  player= Physics2D.OverlapBox(boxCenter, new Vector2(1.38f, 1.25f), 0,LayerMask.GetMask("Player"));
      if (player!=null)
      {
         player.gameObject.GetComponent<PlayerController>().TakeDamage();
      }

   }
   
   public void WaterGunChimpAttack()
   {
      GameObject prefab = Resources.Load<GameObject>("Water");
      float ap=  GetComponentInParent<EnemyController>().ap;
      GameObject bullet=    Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
      bullet.GetComponent<WaterLine>().flyRight=transform.parent.parent.localScale.x>0;
      bullet.GetComponent<WaterLine>().ap = ap;
   }
   public void LaserProboscisMonkeyAttack()
   {
      float ap=  GetComponentInParent<EnemyController>().ap;
      GameObject prefab = Resources.Load<GameObject>("Laser");
    GameObject bullet=  Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
    bullet.GetComponent<Laser>().flyRight=transform.parent.parent.localScale.x>0;
    bullet.GetComponent<Laser>().ap = ap;
      
   }
   public void GoliathOrangutanAttack()
   {
      Transform enemy = transform.parent.parent;
      float ap=  GetComponentInParent<EnemyController>().ap;
      Vector2 boxCenter = enemy.position + new Vector3(1, 0, 0) *( enemy.localScale.x > 0 ? 1.44f : -1.44f);
      Collider2D  player= Physics2D.OverlapBox(boxCenter, new Vector2(1.38f, 1.25f), 0,LayerMask.GetMask("Player"));
      if (player!=null)
      {
         player.gameObject.GetComponent<PlayerController>().TakeDamage();
      }
   }
   public void GangstaRapSakiMonkeyAttack()
   {
      float ap=  GetComponentInParent<EnemyController>().ap;
      GameObject prefab = Resources.Load<GameObject>("Base");
      GameObject bullet= Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
      bullet.GetComponent<Base>().flyRight=transform.parent.parent.localScale.x>0;
      bullet.GetComponent<Base>().ap = ap;
   }
}
