using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroundManager : MonoBehaviour
{
    public int groundCpt = 0;
    private GameManager gameManager;
    public TextMeshProUGUI lifeText;
    private PlayerController playerContr;

    void Start()
    {
        playerContr = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = "Lifes : " + (5 - groundCpt);
        if (groundCpt > 4)
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Good") && gameManager.isGameActive)
        {
            groundCpt++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Powerup") || collision.gameObject.CompareTag("Bad"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            playerContr.isOnGround = true;
        }
    }
}
