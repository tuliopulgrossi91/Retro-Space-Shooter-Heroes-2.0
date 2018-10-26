using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region DEFAULT STATUS
    private float x, y;
    private AudioSource audio_Source;
    private AudioClip audio_Clip5, audio_Clip6, audio_Clip7;
    public static int coin, r;
    public static bool check_PowerUp, check_Life;

    void Start()
    {
        Debug.Log(gameObject.name);
        check_PowerUp = false;
        check_Life = false;
        audio_Source = GetComponent<AudioSource>();
        audio_Clip5 = Resources.Load<AudioClip>("Audios/Sfxs/5");   // COIN
        audio_Clip6 = Resources.Load<AudioClip>("Audios/Sfxs/6");   // POWERUP
        audio_Clip7 = Resources.Load<AudioClip>("Audios/Sfxs/7");   // LIFE

        RandomItem();

        if (LevelManager.condition == 0 || LevelManager.condition == 2)
        {
            if (gameObject.name == "Coin")
            {
                coin = 1;
            }
        }
        if (LevelManager.condition == 1)
        {
            if (gameObject.name == "Coin")
            {
                coin = 15;
                InvokeRepeating("RandomItem", 0, 1);
            }
        }
    }

    void Update()
    {
        if (coin == 0 && LevelManager.condition == 1)
        {
            if (gameObject.name == "Coin")
            {
                CancelInvoke("RandomItem");
            }
        }
    }

    void RandomItem()
    {
        x = Random.Range(-8f, 8f);
        y = Random.Range(0f, 3f);
        transform.position = new Vector2(x, y);
    }
    #endregion

    #region COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            #region CHECK COIN
            if (gameObject.name == "Coin")
            {
                if (coin > 0 && LevelManager.condition == 1)
                {
                    RandomItem();
                }
                if (coin == 0)
                {
                    LevelManager.win = true;
                }
                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip5);
                }
            }
            #endregion
            #region CHECK POWERUP
            if (gameObject.name == "PowerUp")
            {
                check_PowerUp = true;

                if (r == 0)
                {
                    PlayerManager.shot2 = true;
                }
                else
                {
                    PlayerManager.protect = true;
                }
                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip6);
                }
            }
            #endregion

            #region CHECK LIFE
            if (gameObject.name == "Life")
            {
                check_Life = true;

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    audio_Source.PlayOneShot(audio_Clip7);
                }
            }
            #endregion
        }
    }
    #endregion
}