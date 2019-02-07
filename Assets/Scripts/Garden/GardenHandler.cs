using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GardenHandler : MonoBehaviour
{
    public int gardenHealth = 1000;
    public GameObject gardenHealthText;
    public Camera m_Camera;
    GameStateHandler gameStateHandler;

    // Start is called before the first frame update
    void Start()
    {
        PlayingFieldHandler.OnPositionsDetermined += placeGarden;
        gameStateHandler = GameObject.Find("GameStateManager").GetComponent<GameStateHandler>();
        gameObject.tag = "Tree";
    }

    // Update is called once per frame
    void Update()
    {
        gardenHealthText.GetComponent<TextMesh>().text = "Health: " + gardenHealth;

        if(gardenHealth <= 0)
        {
            //GameOver
            gameStateHandler.SetGameState(GameStateHandler.GameState.GAMEOVER);
        }
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        gardenHealthText.transform.LookAt(gardenHealthText.transform.position + m_Camera.transform.rotation * Vector3.forward,
            Vector3.up);
    }

    public void GardenHit(int amount)
    {
        gardenHealth -= amount;
    }

    void placeGarden(Vector3[] points)
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
        float middleX = xFloats.Average();
        float middleY = yFloats.Average();
        float middleZ = zFloats.Average();
        gameObject.transform.position = new Vector3(middleX, middleY, middleZ);
        gameObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        gameObject.tag = "Tree";
    }
}
