using UnityEngine;

public class Protect : MonoBehaviour
{
    #region DEFAULT STATUS
    public static int life;
    private float randC0, randC1, randC2;
    private SpriteRenderer spr_Item;

    void Start()
    {
        life = 3;

        // RANDOM COLOR
        spr_Item = GetComponent<SpriteRenderer>();
        randC0 = Random.Range(0f, 1f);
        randC1 = Random.Range(0f, 1f);
        randC2 = Random.Range(0f, 1f);
        spr_Item.color = new Color(randC0, randC1, randC2, 1);
    }
    #endregion

    #region COLLISIONS
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BOSS" || collision.tag == "BulletBoss")
        {
            life--;

            if (life == 0)
            {
                PlayerManager.protect = false;
            }
        }
    }
    #endregion
}