using UnityEngine;

public class Bullet2Player : MonoBehaviour
{
    #region DEFAULT STATUS
    public GameObject bullet;
    private AudioSource audio_Source;
    private AudioClip audio_Clip4;

    void Start()
    {
        audio_Source = GetComponent<AudioSource>();
        audio_Clip4 = Resources.Load<AudioClip>("Audios/Sfxs/4");
        InvokeRepeating("NextBullet", 0, 0.5f);
    }

    void Update()
    {
        if (BossManager.life == 0)
        {
            PlayerManager.shot2 = false;
            CancelInvoke("NextBullet");
        }
    }

    void NextBullet()
    {
        if (PlayerManager.shot2 == true)
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                audio_Source.PlayOneShot(audio_Clip4);
            }

            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    #endregion

    #region COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BOSS" || collision.tag == "BulletBoss")
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                audio_Source.PlayOneShot(audio_Clip4);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                audio_Source.PlayOneShot(audio_Clip4);
            }
        }
    }
    #endregion
}