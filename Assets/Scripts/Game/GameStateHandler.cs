using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    public GameObject PlayingFieldPrefab;

    GameObject playingFieldInstance;

    public GameObject ReadyToPlayPanel;

    GameObject[] anchors;
    GameObject[] beacons;

    public enum GameState
    {
        PLACINGMARKERS,
        READYTOPLAY,
        PLAYING,
        RESETMARKERS,
        GAMEOVER
    };

    public static GameState gamestate = GameState.PLACINGMARKERS;

    private void Start()
    {
        SetGameState(GameState.PLACINGMARKERS);
    }

    public void SetGameState(GameState state)
    {
        gamestate = state;
        OnStateEnter(state);
    }

    public void SetState(int state)
    {
        SetGameState((GameState)state);
    }

    void OnStateEnter(GameState state)
    {
        switch(state)
        {
            case GameState.PLACINGMARKERS:
                playingFieldInstance = Instantiate(PlayingFieldPrefab);
                ReadyToPlayPanel.SetActive(false);
                break;
            case GameState.READYTOPLAY:
                EnemyWaveHandler.SetWaveState(EnemyWaveHandler.WaveState.notInProgress);
                ReadyToPlayPanel.SetActive(true);
                break;
            case GameState.RESETMARKERS:
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadSceneAsync(0);
                SetGameState(GameState.PLACINGMARKERS);
                break;
            case GameState.PLAYING:
                EnemyWaveHandler.SetWaveState(EnemyWaveHandler.WaveState.inProgress);
                ReadyToPlayPanel.SetActive(false);
                break;
            case GameState.GAMEOVER:
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadSceneAsync(2);
                break;
            default:
                break;
        }
    }
}
