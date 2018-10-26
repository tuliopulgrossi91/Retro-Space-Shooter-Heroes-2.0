using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region DEFAULT STATUS
    [Header("Game Objects 8")] public GameObject[] Object;
    [Header("Texts 8")] public Text[] textUI;
    public static bool pause, win;
    public static int score, condition;
    private readonly float[] times = new float[3] { 45f, 0f, 120f };
    private float time;

    /// <summary>
    /// Object0 - pause
    /// Object1 - settings
    /// Object2 - coin
    /// Object3 - powerup
    /// Object4 - life
    /// Object5 - npcs
    /// Object6 - boss
    /// Object7 - life boss
    /// 
    /// textUI0 - time
    /// textUI1 - score npc
    /// textUI2 - score coin
    /// textUI3 - level select
    /// textUI4 - life player
    /// textUI5 - life boss
    /// textUI6 - pause text
    /// textUI7 - text action
    /// </summary>

    void Start()
    {
        #region CHECK MISSIONS
        win = false;
        score = 15;
        Time.timeScale = 1;

        for (int i = 0; i < 3; i++)
        {
            if (condition == i)
            {
                textUI[3].text = "" + condition.ToString();
                time = times[condition];
            }
        }
        Debug.Log("Mission: " + condition);
        #endregion
    }

    void Update()
    {
        textUI[4].text = "" + PlayerManager.life.ToString(); // LIFE PLAYER
        textUI[2].text = "" + ItemManager.coin.ToString(); // SCORE COIN

        if (PlayerManager.loser == false) // PLAYER IS ALIVE
        {
            PauseGame(); // CHECK PAUSE GAME
        }

        #region CHECK PLAYER DIE
        if (time < 0 || PlayerManager.loser == true) // TIME < 0 OR PLAYER DIE
        {
            Time.timeScale = 0; // STOP TIME
            Object[0].SetActive(true); // PANEL PAUSE ON
            textUI[6].text = "Game Over"; // PAUSE TEXT
            textUI[7].text = "Retry"; // TEXT ACTION = RETRY
        }
        #endregion

        #region CHECK PLAYER WINNER
        if (win == true)
        {
            Time.timeScale = 0;
            Object[0].SetActive(true); // PANEL PAUSE ON
            textUI[6].text = "Congratulations"; // PAUSE TEXT
            textUI[7].text = "Menu"; // TEXT ACTION = MENU


            if (condition == 0)
            {
                PlayerPrefs.SetInt("LEVEL1", 1);
            }
            if (condition == 1)
            {
                PlayerPrefs.SetInt("LEVEL2", 1);
            }
        }
        #endregion

        #region SCORE STATUS
        if (score == 0)
        {
            if (condition == 0)
            {
                PlayerManager.shot = false; // PLAYER SHOT OFF
                Object[2].SetActive(true); // COIN ON
                Object[5].SetActive(false); // NPCS OFF
                time -= Time.deltaTime; // TIMER 
                textUI[0].text = "" + Mathf.Round(time);
            }
            if (condition == 1)
            {
                Object[5].SetActive(false); // NPCS OFF
            }
            if (condition == 2)
            {
                Object[5].SetActive(false); // NPCS OFF
                Object[6].SetActive(true); // BOSS ON
                Object[7].SetActive(true); // LIFE BOSS ON
                textUI[5].text = "" + BossManager.life.ToString(); // BOSS LIFES
                time -= Time.deltaTime; // TIMER 
                textUI[0].text = "" + Mathf.Round(time);

                if (BossManager.loser == true)
                {
                    PlayerManager.shot = false; // PLAYER SHOT OFF
                    Object[2].SetActive(true); // COIN ON
                    textUI[5].text = ""; // LIFE BOSS OFF
                    textUI[1].text = ""; // SCORE NPC OFF
                    Object[6].SetActive(false); // BOSS OFF
                }

                #region CHECK POWERUP AND LIFE
                if (ItemManager.check_PowerUp == false && ItemManager.check_Life == false)
                {
                    if (BossManager.loser == false)
                    {
                        Object[3].SetActive(true); // POWERUP ON
                        Object[4].SetActive(true); // LIFE ON
                    }
                }
                if (ItemManager.check_PowerUp == true)
                {
                    Object[3].SetActive(false); // POWERUP OFF
                }
                if (ItemManager.check_Life == true)
                {
                    Object[4].SetActive(false); // LIFE OFF
                }
                #endregion
            }
        }
        else
        {
            if (condition == 0 || condition == 2)
            {
                Object[2].SetActive(false); // COIN OFF
                time -= Time.deltaTime; // TIMER 
                textUI[0].text = "" + Mathf.Round(time);
                textUI[1].text = "" + score.ToString(); // SCORE NPC
            }
            if (condition == 1)
            {
                Object[2].SetActive(true); // COIN ON
                textUI[1].text = ""; // SCORE NPC
            }
        }
        #endregion
    }
    #endregion

    #region UI MANAGER
    public void ButtonAction()
    {
        #region CLICK MODE
        if (win == false)
        {
            // RETRY - RELOAD SCENE
            Scene game = SceneManager.GetActiveScene();
            SceneManager.LoadScene(game.buildIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // BACK TO MENU
        }
        #endregion
    }

    public void ButtonSelect(int i)
    {
        #region CLICK MODE
        if (i == 1)
        {
            Object[1].SetActive(true); // PANEL SETTINGS ON
        }
        if (i == 2)
        {
            Object[1].SetActive(false); // PANEL SETTINGS OFF
        }
        #endregion
    }

    // PAUSE GAME 
    void PauseGame()
    {
        // PANEL PAUSE ON
        if (Input.GetKeyDown(KeyCode.Return) && pause == false)
        {
            Object[0].SetActive(true);
            Time.timeScale = 0;
            pause = !pause;
        }
        // PANEL PAUSE OFF
        else if (Input.GetKeyDown(KeyCode.Return) && pause == true)
        {
            Object[0].SetActive(false);
            Time.timeScale = 1;
            pause = !pause;
        }
    }
    #endregion
}