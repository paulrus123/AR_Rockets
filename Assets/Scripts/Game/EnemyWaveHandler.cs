using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveHandler : MonoBehaviour
{
    public static int wave = 0;

    public static int numEnemiesInWave;
    public static float spawnTime;
    public static int numEnemiesLeft;
    public static int numEnemiesLeftToSpawn;

    public enum WaveState {inProgress, notInProgress};
    public static WaveState waveState;

    Text botsLeftText;


    // Start is called before the first frame update
    void Start()
    {
        waveState = WaveState.notInProgress;
        setWaveParameters(1);
        botsLeftText = GameObject.Find("EnemiesLeft").GetComponent<Text>();
    }

    private void Update()
    {
        botsLeftText.text = "# Bots: " + numEnemiesLeft;
    }

    public static void SetWaveState(WaveState state)
    {
        waveState = state;
    }

    public static void setWaveParameters(int waveNum)
    {
        switch(waveNum)
        {
            case 1:
                numEnemiesInWave = 5;
                spawnTime = 4.0f;
                break;
            case 2:
                numEnemiesInWave = 8;
                spawnTime = 3.0f;
                break;
            case 3:
                numEnemiesInWave = 10;
                spawnTime = 2.0f;
                break;
            case 4:
                numEnemiesInWave = 12;
                spawnTime = 1.5f;
                break;
            case 5:
                numEnemiesInWave = 15;
                spawnTime = 1.0f;
                break;
            case 6:
                numEnemiesInWave = 18;
                spawnTime = 0.75f;
                break;
            case 7:
                numEnemiesInWave = 20;
                spawnTime = 0.5f;
                break;
            default:
                numEnemiesInWave = 5;
                spawnTime = 5.0f;
                break;
        }

        wave = waveNum;
        numEnemiesLeft = numEnemiesInWave;
        numEnemiesLeftToSpawn = numEnemiesInWave;
    }
}
