using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChooseCharacter : MonoBehaviour
{
    enum CharacterInfo
    {
        character01 = 0,
        character02,
        character03,
    };
    
    public int CharInfo = 0;
    public static ChooseCharacter Instance;
    public GameObject playerSC;
    public GameObject spawnSC;
    public GameObject gameover;

    private void Awake()
    {
        ChooseCharacter.Instance = this;
        gameover.SetActive(false);
        playerSC.GetComponent<PlayerCtrl>().enabled = false;
        spawnSC.GetComponent<EnemySpawn>().enabled = false;
    }
    public void ChooseCharacter1()
    {
        CharInfo = (int)(CharacterInfo.character01);
        gameStart();
    }
    public void ChooseCharacter2()
    {
        CharInfo = (int)(CharacterInfo.character02);
        gameStart();
    }
    public void ChooseCharacter3()
    {
        CharInfo = (int)(CharacterInfo.character03);
        gameStart();
    }
    private void gameStart()
    { 
        this.gameObject.SetActive(false);
        playerSC.GetComponent<PlayerCtrl>().enabled = true;
        spawnSC.GetComponent<EnemySpawn>().enabled = true;
    }
}
