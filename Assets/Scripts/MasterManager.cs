using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterManager : MonoBehaviour
{
    #region DEFAULT STATUS
    [Header("Game Objects 8")] public GameObject[] objects;
    [Header("Texts 4")] public Text[] txts;
    [Header("Button Hero")] public Button btn_Hero;
    [Header("Sprite Heros 14")] private Sprite[] spritesHero;
    private string[] infos = new string[3] { "Controllers: mouse (click and drag) keyboard (A S D W /ARROWS).", "If you get out of the screen you die.", "Press enter to pause game." };
    private string[] missions = new string[2] { "Defeat the enemies and collect the coin.", "Collect the coins." };
    private string[] credits = { "programmer" + "\n" + "design" + "\n" + "sprite" + "\n" + "by tulio pulgrossi", "sfx by chiptone", "background" + "\n" + "by solarsystemscopetype", "font by kenney", "Music by Eric Matyas" + "\n" + "www.soundimage.org" };
    private int i, r;

    /// <summary>
    /// objects 0 - PANEL BASE
    /// objects 1 - PANEL SETTINGS
    /// objects 2 - PANEL MASTER
    /// objects 3 - BUTTON CONFIRM
    /// objects 4 - PANEL SELECT
    /// objects 5 - PANEL MISSIONS
    /// objects 6 - BUTTON MISSION 1
    /// objects 7 - BUTTON MISSION 2
    /// 
    /// txts 0 - TEXT PANEL
    /// txts 1 - TEXT BACK
    /// txts 2 - TEXT INFOS
    /// txts 3 - TEXT MISSIONS
    /// </summary>

    void Start()
    {
        i = 0;
        Time.timeScale = 1; // TIME ON
        spritesHero = Resources.LoadAll<Sprite>("Sprites/Player/players"); // LOAD ALL SPRITES PLAYER
        btn_Hero.image.sprite = spritesHero[PlayerPrefs.GetInt("Player")];

        #region CHECK LEVEL
        // LEVEL 0 WIN
        if (PlayerPrefs.GetInt("LEVEL1") == 1)
        {
            objects[6].SetActive(true);
        }
        // LEVEL 1 WIN
        if (PlayerPrefs.GetInt("LEVEL2") == 1)
        {
            objects[7].SetActive(true);
        } 
        #endregion

        // SHOW INFORMATIONS
        InvokeRepeating("Infos", 0.5f, 3.0f);
    }
    #endregion

    #region SELECT HERO
    public void SelectPlayer()
    {
        i++;

        if (i == spritesHero.Length)
        {
            i = 0;
        }

        btn_Hero.image.sprite = spritesHero[i];
        PlayerPrefs.SetInt("Player", i); // SAVE PLAYER SELECT
    }

    public void SelectRandomPlayer()
    {
        objects[5].SetActive(true); // PANEL MISSION ON
        r = Random.Range(0, 14); // RECEIVE RANDOM VALUE
        PlayerPrefs.SetInt("Player", r); // SAVE PLAYER SELECT
    }
    #endregion

    #region MASTER UI SELECT
    public void ButtonSelectMaster(int i)
    {
        #region CLICK MODE
        if (i == 0)
        {
            objects[4].SetActive(true); // SHOW PANEL SELECT
        }
        if (i == 1 || i == 2) // CHECK PANELS
        {
            objects[i].SetActive(true); // SHOW PANEL
        }
        if (i == 3)
        {
            objects[1].SetActive(false); // BACK PANEL SETTINGS
        }
        if (i == 4)
        {
            GameObject.FindGameObjectWithTag("Panel").SetActive(false); // BACK PANEL
        }
        if (i == 5)
        {
            Application.Quit(); // EXIT
        }
        if (i == 6)
        {
            objects[5].SetActive(true); // SHOW PANEL MISSIONS
        }
        if (i == 7)
        {
            SceneManager.LoadScene(1); // LOAD LEVEL 
        }
        #endregion

        #region SELECT MODE
        if (i == 8)
        {
            txts[0].text = "";
            InvokeRepeating("Texts", 1f, 3f); // INFO CREDITS
            objects[3].SetActive(false);
            txts[1].text = "Back";
        }
        if (i == 9)
        {
            txts[0].text = "Are you sure?"; // EXIT QUESTION
            objects[3].SetActive(true);
            txts[1].text = "No";
        }
        if (i == 10)
        {
            LevelManager.condition = 0; // MISSION 0
        }
        if (i == 11)
        {
            LevelManager.condition = 1; // MISSION 1
        }
        if (i == 12)
        {
            LevelManager.condition = 2; // MISSION 2
        }
        #endregion

        #region ENTER MODE
        if (i == 13)
        {
            txts[3].text = "" + missions[0]; // TEXT MISSIONS 0 AND 2
        }
        if (i == 14)
        {
            txts[3].text = "" + missions[1]; // TEXT MISSION 1
        }
        #endregion

        #region EXIT MODE
        if (i == 15)
        {
            txts[3].text = "";
        }
        if (i == 16)
        {
            CancelInvoke("Texts");
        }
        #endregion

        #region CLICK MODE
        if (i == 17)
        {
            objects[4].SetActive(false); // BACK PANEL SELECT
        }
        #endregion
    }
    #endregion

    // TEXT INFOS
    void Infos()
    {
        txts[2].text = "" + infos[Random.Range(0, 3)];
    }

    // TEXT PANEL
    void Texts()
    {
        txts[0].text = "" + credits[Random.Range(0, 5)];
    }
}