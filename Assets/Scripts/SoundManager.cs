using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip enemyGroundExplosion, jump, enemyShot, bulletShot, mosquitoDeath;
    static AudioSource audioSrc;
    private static string LastSound;
    // Start is called before the first frame update
    void Start()
    {
        enemyGroundExplosion= Resources.Load<AudioClip>("GroundEnemyExplosion");
        enemyShot = Resources.Load<AudioClip>("enemyShot");
        jump = Resources.Load<AudioClip>("jump");
        bulletShot= Resources.Load<AudioClip>("shot");
        audioSrc = GetComponent<AudioSource>();
        mosquitoDeath = Resources.Load<AudioClip>("mosquitoDeath");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        LastSound = clip;
        switch (clip)
        {
            case "GroundEnemyExplosion":
                audioSrc.PlayOneShot(enemyGroundExplosion);
                break;
            case "Jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "EnemyShot":
                audioSrc.PlayOneShot(enemyShot);
                break;
            case "BulletExplode":
                audioSrc.PlayOneShot(bulletShot);
                break;

            case "MosquitoDeath":
                audioSrc.PlayOneShot(mosquitoDeath);
                break;
        }

        //Debug.Log(clip);

    }

    public static void StopSound(string clip)
    {
        if (LastSound == clip)
            audioSrc.Stop();
    }
}
