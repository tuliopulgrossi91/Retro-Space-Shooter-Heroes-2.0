using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region DEFAULT STATUS
    public GameObject bullet;
    public static bool shot, loser;
    private Animator ani_NPC;
    private AudioSource audio_Source;
    private AudioClip audio_Clip0, audio_Clip1, audio_Clip2, audio_Clip3;
    private Rigidbody2D rb2d;
    private SpriteRenderer spr_NPC;
    public static float speed, randC0, randC1, randC2;
    public static int life;
    private int m;

    void Start()
    {
        m = Random.Range(0, 2);
        shot = true;
        loser = false;
        speed = 5;
        life = 15;

        rb2d = GetComponent<Rigidbody2D>();
        spr_NPC = GetComponent<SpriteRenderer>();
        ani_NPC = GetComponent<Animator>();
        ani_NPC.SetInteger("Default", 0);
        audio_Source = GetComponent<AudioSource>();
        audio_Clip0 = Resources.Load<AudioClip>("Audios/Sfxs/0");   // APARECER
        audio_Clip1 = Resources.Load<AudioClip>("Audios/Sfxs/1");   // MOVIMENTAR
        audio_Clip2 = Resources.Load<AudioClip>("Audios/Sfxs/2");   // ATIRAR
        audio_Clip3 = Resources.Load<AudioClip>("Audios/Sfxs/3");   // MORRER

        InvokeRepeating("Shot", 0, 0.5f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip0);
        }

        Moves();
    }

    void Update()
    {
        // RANDOM COLOR
        randC0 = Random.Range(0f, 1f);
        randC1 = Random.Range(0f, 1f);
        randC2 = Random.Range(0f, 1f);
        spr_NPC.color = new Color(randC0, randC1, randC2, 1);

        if (loser == false)
        {
            ani_NPC.SetInteger("Default", 1);
        }
    }

    void Moves()
    {
        if (m == 0)
        {
            Invoke("ChangeLeft", 0.6f);
        }
        else
        {
            Invoke("ChangeDown", 0.6f);
        }
    }

    void ChangeLeft()
    {
        rb2d.velocity = Vector2.left * speed;
        Invoke("ChangeRight", 0.6f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip1);
        }
    }

    void ChangeRight()
    {
        rb2d.velocity = Vector2.right * speed;
        Invoke("ChangeLeft", 0.6f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip1);
        }
    }

    void ChangeUp()
    {
        rb2d.velocity = Vector2.up * speed;
        Invoke("ChangeDown", 0.6f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip1);
        }
    }

    void ChangeDown()
    {
        rb2d.velocity = Vector2.down * speed;
        Invoke("ChangeUp", 0.6f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip1);
        }
    }

    void Shot()
    {
        if (shot == true)
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                audio_Source.PlayOneShot(audio_Clip2);
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
    #endregion

    #region COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletPlayer")
        {
            life--;

            if (life < 0)
            {
                loser = true;
                shot = false;
                ani_NPC.SetInteger("Default", 2);

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip3);
                }
                if (life == 0)
                {
                    loser = true;
                }
            }
        }
    }

    void NPCDie()
    {
        Destroy(gameObject);
    }
    #endregion
}