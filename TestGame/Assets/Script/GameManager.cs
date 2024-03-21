using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    CharacterSelect,
    CharacterSelect_GamePlay_Directing,
    GamePlay,

    None
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameState _gameState = GameState.None;

    private void Awake()
    {
        _gameState = GameState.None;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            this.SetState(GameState.CharacterSelect);
        }


        switch (_gameState)
        {
            case GameState.CharacterSelect:
                DoCharacterSelect();
                break;

            case GameState.CharacterSelect_GamePlay_Directing:
                DoCharacterSelect_GamePlay_Directing();
                break;

            case GameState.GamePlay:
                DoGamePlay();
                break;
        }
    }

    public void SetState(GameState gameState)
    {
        if (_gameState == gameState)
        {
            return;
        }


        _gameState = gameState;

        switch (_gameState)
        {
            case GameState.CharacterSelect:
                ChangeCharacterSelect();
                break;

            case GameState.CharacterSelect_GamePlay_Directing:
                ChangeCharacterSelect_GamePlay_Directing();
                break;

            case GameState.GamePlay:
                ChangeGamePlay();
                break;
        }
    }



    void ChangeCharacterSelect()
    {
        StartCoroutine(ChangeCharacterSelectCoroutine());
    }

    IEnumerator ChangeCharacterSelectCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        SetState(GameState.CharacterSelect_GamePlay_Directing);
    }


    void ChangeCharacterSelect_GamePlay_Directing()
    {
    }

    void ChangeGamePlay()
    {
    }


    void DoCharacterSelect()
    {
        UIManager.Instance.SetText(nameof(DoCharacterSelect));
    }

    void DoCharacterSelect_GamePlay_Directing()
    {
        UIManager.Instance.SetText(nameof(DoCharacterSelect_GamePlay_Directing));
    }

    void DoGamePlay()
    {
        UIManager.Instance.SetText(nameof(DoGamePlay));
    }
}
