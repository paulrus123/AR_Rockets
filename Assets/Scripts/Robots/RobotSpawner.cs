using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    float minX = 0.0f;
    float maxX = 0.0f;
    float minY = 0.0f;
    float maxY = 0.0f;
    float minZ = 0.0f;
    float maxZ = 0.0f;

    float timer = 0.0f;

    public GameObject RobotPrefab;
    bool minMaxSet = false; //todo: change to state machine instead of flag

    // Start is called before the first frame update
    void Start()
    {
        PlayingFieldHandler.OnPositionsDetermined += calculateMinMax;
    }

    public void calculateMinMax(Vector3[] points)
    {
        float[] xFloats = new float[points.Length];
        float[] yFloats = new float[points.Length];
        float[] zFloats = new float[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            xFloats[i] = points[i].x;
            yFloats[i] = points[i].y;
            zFloats[i] = points[i].z;
        }
        maxX = Mathf.Max(xFloats);
        maxZ = Mathf.Max(zFloats);
        maxY = Mathf.Max(yFloats);

        minX = Mathf.Min(xFloats);
        minY = Mathf.Min(yFloats);
        minZ = Mathf.Min(zFloats);

        minMaxSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!minMaxSet)
            return;
        if (GameStateHandler.gamestate != GameStateHandler.GameState.PLAYING)
            return;
        if (EnemyWaveHandler.numEnemiesLeft == 0 || EnemyWaveHandler.numEnemiesLeftToSpawn == 0)
            return;

        timer += Time.deltaTime;
        if(timer > EnemyWaveHandler.spawnTime)
        {
            timer = 0.0f;
            spawnRobot();
            EnemyWaveHandler.numEnemiesLeftToSpawn--;
        }
    }

    void spawnRobot()
    {
        //X,Y,Z position to spawn robot
        float x;
        float y;
        float z;

        //Get midpoint of X,Y,Z of playing area
        float avgX = (minX + maxX) / 2.0f;
        float avgY = (minY + maxY) / 2.0f;
        float avgZ = (minZ + maxZ) / 2.0f;

        //Get a random value lower than the midpoint (with a 0.2f buffer to account for house)
        float x_low = Random.Range(minX, avgX-0.2f);
        float z_low = Random.Range(minZ, avgZ - 0.2f);
        float y_low = Random.Range(minY, avgY - 0.2f);

        //Get a random value higher than the midpoint (with a 0.2f buffer to account for house)
        float x_high = Random.Range(avgX + 0.2f, maxX);
        float z_high = Random.Range(avgZ + 0.2f, maxZ);
        float y_high = Random.Range(avgY + 0.2f, maxY);

        //randomly choose between high or low val
        int x_index = Random.Range(0, 1);
        int y_index = Random.Range(0, 1);
        int z_index = Random.Range(0, 1);

        if (x_index == 0)
            x = x_low;
        else
            x = x_high;

        if (y_index == 0)
            y = y_low;
        else
            y = y_high;

        if (z_index == 0)
            z = z_low;
        else
            z = z_high;

        y += 3.0f; //drop robot from height

        Vector3 pos = new Vector3(x, y, z);
        GameObject robot = Instantiate(RobotPrefab, pos, Quaternion.identity);
    }
}
