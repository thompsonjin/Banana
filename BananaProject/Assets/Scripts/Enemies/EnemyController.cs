using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [Header("Partol Speed")]
    public float partolSpeed;
    [Header("Run Speed")]
    public float runSpeed;
    [Header("Attack Speed")]
    public float attackSpeed;
    [Header("Rador Range")]
    public float radorRange;
    [Header("Attack Rane")]
    public float attackRane;
    [Header("HP")]
    [SerializeField] float hp;
    [Header("AP")]
    public float ap;
    public List<GameObject> patrolPoints=new List<GameObject>();

    public UnityEvent onDie;
    public int patrolPointIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        patrolPointIndex = 1;
    }

    public void BeAttack(int ap)
    {
        bool isFront = (transform.localScale.x > 0) != (player.localScale.x > 0);

        if (gameObject.CompareTag("GoliathOrangutan"))
        {
            if ((transform.localScale.x>0)==(player.localRotation.y>=-10))
            {

                //Can't attack from the shield side
                return;
            }
        }
        Debug.Log(11);
        hp -= ap;
        if (hp<=0)
        {
            if (onDie!=null)
            {
                onDie?.Invoke();
            }
           
            Destroy(gameObject);
            //drop banana
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
  public  void SetFaceDir(int dir)
    {
        if(dir==1)
        {
            transform.localScale = new Vector3(1,1,1);

        }
        else if(dir==-1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }
 public   void SetMoveSpeed(Vector2 speed)
    {
        GetComponent<Rigidbody2D>().velocity = speed;
        
    }
}
