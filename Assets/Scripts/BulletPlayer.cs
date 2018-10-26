using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    #region DEFAULT STATUS
    private SpriteRenderer sprtRend;
    private Rigidbody2D rb2d;
    private AudioSource audio_Source;
    private AudioClip audio_Clip4;
    private float speed, randC0, randC1, randC2;

    void Start()
    {
        speed = 10;
        sprtRend = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        audio_Source = GetComponent<AudioSource>();
        audio_Clip4 = Resources.Load<AudioClip>("Audios/Sfxs/4");

        randC0 = Random.Range(0f, 1f);
        randC1 = Random.Range(0f, 1f);
        randC2 = Random.Range(0f, 1f);
        sprtRend.color = new Color(randC0, randC1, randC2, 1);
    }

    void LateUpdate()
    {
        if (PlayerManager.shot == true)
        {
            rb2d.velocity = Vector2.up * speed;
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
        if (collision.tag == "NPC" || collision.tag == "BulletNPC")
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