using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMethods : MonoBehaviour
{
   public void RoboMonkeyAttack()
   {
      
   }
   public void RobotBaboonAttack()
   {
      
   }
   
   public void WaterGunChimpAttack()
   {
      GameObject prefab = Resources.Load<GameObject>("Water");
      GameObject bullet=    Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
      bullet.GetComponent<WaterLine>().flyRight=transform.parent.parent.localScale.x>0;
   }
   public void LaserProboscisMonkeyAttack()
   {
      GameObject prefab = Resources.Load<GameObject>("Laser");
    GameObject bullet=  Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
    bullet.GetComponent<Laser>().flyRight=transform.parent.parent.localScale.x>0;
      
   }
   public void GoliathOrangutanAttack()
   {
    
   }
   public void GangstaRapSakiMonkeyAttack()
   {
      GameObject prefab = Resources.Load<GameObject>("Base");
      GameObject bullet= Instantiate(prefab, transform.parent.parent.position, Quaternion.identity);
      bullet.GetComponent<Base>().flyRight=transform.parent.parent.localScale.x>0;
   }
}
