using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivate : MonoBehaviour
{
    public AudioSource music;
    public MechaHarambe m_Haram;

    public Collider2D col;

    private void Start()
    {
        music.clip.LoadAudioData();
    }
    private void Update()
    {
        if(col == null)
        {
            if(!music.isPlaying && m_Haram.start != true)
            {
                music.Play();
                m_Haram.start = true;
            }
        }
    }
}
