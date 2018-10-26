using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region DEFAULT STATUS
    private Vector3 d;
    private float x, y, speed;
    private Rigidbody2D rb2d_Player;
    private SpriteRenderer spr_Player;
    private Sprite[] sp_Player;
    private Animator ani_Player;
    private AudioSource audio_Source;
    private AudioClip audio_Clip0, audio_Clip1, audio_Clip2, audio_Clip3;
    public static bool appear, loser, shot, shot2, protect, lifes;
    public static int life;
    public GameObject bullet;
    public GameObject [] powerUps;

    void Start()
    {
        protect = false;
        loser = false;
        shot = true;
        shot2 = false;
        speed = 10;
        rb2d_Player = GetComponent<Rigidbody2D>();
        sp_Player = Resources.LoadAll<Sprite>("Sprites/Player/players");
        spr_Player = GetComponent<SpriteRenderer>();
        spr_Player.sprite = sp_Player[PlayerPrefs.GetInt("Player")];
        ani_Player = GetComponent<Animator>();
        ani_Player.SetInteger("Default", 0);
        audio_Source = GetComponent<AudioSource>();
        audio_Clip0 = Resources.Load<AudioClip>("Audios/Sfxs/0");   // APARECER
        audio_Clip1 = Resources.Load<AudioClip>("Audios/Sfxs/1");   // MOVIMENTAR
        audio_Clip2 = Resources.Load<AudioClip>("Audios/Sfxs/2");   // ATIRAR
        audio_Clip3 = Resources.Load<AudioClip>("Audios/Sfxs/3");   // MORRER

        if (LevelManager.condition == 0)
        {
            life = 5;
        }
        if (LevelManager.condition == 1)
        {
            life = 1;
        }
        if (LevelManager.condition == 2)
        {
            life = 15;
        }

        InvokeRepeating("Shot", 0, 0.5f);

        if (PlayerPrefs.GetInt("AUDIO") == 1)
        {
            audio_Source.PlayOneShot(audio_Clip0);
        }
    }

    void Shot()
    {
        if (shot == true && LevelManager.condition != 1)
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

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        shot = false;
        loser = true;
    }

    void Update()
    {
        MoveKeyBoard();

        #region CHECK POWER UP PROTECT 
        if (protect == false)
        {
            powerUps[0].SetActive(protect);
        }
        else
        {
            powerUps[0].SetActive(protect);
        }
        #endregion

        #region CHECK POWER UP SHOT2 
        if (shot2 == false)
        {
            powerUps[1].SetActive(shot2);
            powerUps[2].SetActive(shot2);
        }
        else
        {
            powerUps[1].SetActive(shot2);
            powerUps[2].SetActive(shot2);
        }
        #endregion
    }
    #endregion

    #region CONTROLLERS
    void OnMouseDown()
    {
        if (PlayerPrefs.GetInt("CONTROLLER") == 0)
        {
            if (loser == false)
            {
                ani_Player.SetInteger("Default", 1);

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip1);
                }

                d = Camera.main.WorldToScreenPoint(transform.position);
                x = Input.mousePosition.x - d.x;
                y = Input.mousePosition.y - d.y;
            }
        }
    }
    void OnMouseDrag()
    {
        if (PlayerPrefs.GetInt("CONTROLLER") == 0)
        {
            if (loser == false)
            {
                ani_Player.SetInteger("Default", 1);

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip1);
                }

                Vector2 l = new Vector2(Input.mousePosition.x - x, Input.mousePosition.y - y);
                Vector2 w = Camera.main.ScreenToWorldPoint(l);
                transform.position = w;
            }
        }
    }
    void MoveKeyBoard()
    {
        if (PlayerPrefs.GetInt("CONTROLLER") == 1)
        {
            if (loser == false)
            {
                ani_Player.SetInteger("Default", 1);

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    rb2d_Player.velocity = Vector2.left * speed;

                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        audio_Source.PlayOneShot(audio_Clip1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    rb2d_Player.velocity = Vector2.right * speed;

                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        audio_Source.PlayOneShot(audio_Clip1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    rb2d_Player.velocity = Vector2.up * speed;

                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        audio_Source.PlayOneShot(audio_Clip1);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    rb2d_Player.velocity = Vector2.down * speed;

                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        audio_Source.PlayOneShot(audio_Clip1);
                    }
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    rb2d_Player.velocity = Vector2.zero * 0;
                }
            }
        }
    }
    #endregion

    #region COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletNPC" || collision.tag == "NPC" || collision.tag == "BOSS")
        {
            if (life > 0)
            {
                life--;
                if (life == 0)
                {
                    loser = true;
                    shot = false;
                    ani_Player.SetInteger("Default", 2);

                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        audio_Source.PlayOneShot(audio_Clip3);
                    }
                }
            }
        }
        if (collision.tag == "Coin")
        {
            if (LevelManager.condition == 0 || LevelManager.condition == 2)
            {
                LevelManager.win = true;
            }
            if (LevelManager.condition == 1)
            {
                ItemManager.coin--;
            }
        }
        if (collision.tag == "PowerUp")
        {
            ItemManager.r = Random.Range(0, 2);
            Debug.Log(ItemManager.r);
        }
        if (collision.tag == "Life")
        {
            life++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                audio_Source.PlayOneShot(audio_Clip3);
            }

            loser = true;
            shot = false;
        }
    }

    void PlayerDie()
    {
        gameObject.SetActive(false);
    }
    #endregion
}