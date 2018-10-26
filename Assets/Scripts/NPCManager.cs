using UnityEngine;

public class NPCManager : MonoBehaviour
{
    #region DEFAULT STATUS
    private float x, y, t;
    private Vector2 obj;
    public GameObject prefab_NPC;
    public static bool d;

    void Start()
    {
        NextNPC(); // NEW NPC
    }

    void Update()
    {
        if (LevelManager.score > 0)
        {
            if (d == true) // SE DESTRUIDO
            {
                d = !d; // RECEBE FALSO
                NextNPC(); // CHAMAR NOVO OBJETO 
            }
            if (NPC.i == true) // FICOU FORA DA TELA
            {
                NPC.i = !NPC.i; // NÃO FICOU FORA DA TELA
                NextNPC(); // CHAMAR NOVO OBJETO
            }
        }
    }

    void NextNPC()
    {
        if (LevelManager.condition != 1 || BossManager.loser == false)
        {
            if (d == false || NPC.i == true) // SE NÃO DESTRUIDO OU FICOU FORA DA TELA
            {
                // RECEBE NOVA POSICÃO RANDOMICA
                x = Random.Range(-8f, 8f);
                y = Random.Range(0f, 3f);
                obj = new Vector2(x, y);

                // É INSTANCIADO
                Instantiate(prefab_NPC, obj, Quaternion.identity);

                // SE TIVER VIDAS RECEBE 3
                if (NPC.lifes == true)
                {
                    NPC.life = 3;
                }
                // SENAO RECEBE 1
                else
                {
                    NPC.life = 1;
                }
            }
        }
    }
    #endregion
}