using UnityEngine;

public class LevelActivate : MonoBehaviour
{
    public AudioSource music;
    public MechaHarambe m_Haram;
    public Collider2D col;

    private bool hasTriggeredLocally = false;

    private void Start()
    {
        //Create BossMusicManager if it doesn't exist
        if (BossMusicManager.Instance == null)
        {
            new GameObject("BossMusicManager").AddComponent<BossMusicManager>();
        }

        //If boss is already activated (player died and respawned)
        if (BossMusicManager.Instance.IsBossActivated())
        {
            //Start boss immediately without playing music again
            m_Haram.start = true;

            //Disable checkpoint so it doesnt trigger again
            if (col != null)
            {
                col.enabled = false;
            }

            hasTriggeredLocally = true;
        }
    }

    private void Update()
    {
        if (!hasTriggeredLocally)
        {
            if (col == null)
            {
                m_Haram.start = true;

                //Start boss music via manager
                if (music != null && music.clip != null)
                {
                    BossMusicManager.Instance.StartBossMusic(music.clip, music.volume);
                }

                hasTriggeredLocally = true;
            }
        }
    }
}