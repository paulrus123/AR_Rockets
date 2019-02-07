using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    public GameStateHandler gameStateHandler;

    public int health = 100;
    public ScoreHandler scoreHandler;
    public GameObject tree;
    public GameObject garden;
    public GameObject bulletPrefab;

    float faceGardenTime = 3.0f; //timer counts down from 0
    bool gardenFaced = false;

    float shootTimer = 0.0f; //timer counts up

    // Start is called before the first frame update
    void Start()
    {
        garden = GameObject.Find("House");
        health = 100;
        scoreHandler = GameObject.Find("Score").GetComponent<ScoreHandler>();
        gameStateHandler = GameObject.Find("GameStateManager").GetComponent<GameStateHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            EnemyWaveHandler.numEnemiesLeft--;
            if(EnemyWaveHandler.numEnemiesLeft <= 0) //wave complete
            {
                gameStateHandler.SetGameState(GameStateHandler.GameState.READYTOPLAY);
                EnemyWaveHandler.setWaveParameters(++EnemyWaveHandler.wave);
            }
            Destroy(gameObject);
        }
        if(faceGardenTime > 0)
        {
            faceGardenTime -= Time.deltaTime;
        }
        else //if (!gardenFaced)
        {
            LookAtGarden();
            gardenFaced = true;
        }

        shootTimer += Time.deltaTime;
        if(shootTimer > 4.0f)
        {
            shootTimer = 0.0f;
            shootAtGarden();
        }

    }

    void shootAtGarden()
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        bullet.transform.rotation = gameObject.transform.rotation;

        Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.forward * 40.0f);
    }

    public void LookAtGarden()
    {
        if (garden != null)
        {
            transform.LookAt(garden.transform);
        }
    }

    public void decreaseHealth(int amount)
    {
        health -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            decreaseHealth(100);
            scoreHandler.AddPoints(10);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            //decreaseHealth(100);
        }
    }
}