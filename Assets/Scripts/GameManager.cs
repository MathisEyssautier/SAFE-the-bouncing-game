//GAMEMANAGER FOR PROJECT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;

    public int spawnRate = 6;
    public int BonusSpawnRate = 10;
    public List<GameObject> spawnPrefabs;
    public List<GameObject> spawnBonus;

    public TextMeshProUGUI gameOvertext;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject scoreScreen;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        scoreScreen.SetActive(true);
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        StartCoroutine(SpawnBonus());
    }


    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            int index = Random.Range(0, spawnPrefabs.Count);
            Instantiate(spawnPrefabs[index], RandomWindowIndex(), spawnPrefabs[index].transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
    IEnumerator SpawnBonus()
    {
        while (isGameActive)
        {
            int index = Random.Range(0, spawnBonus.Count);
            Instantiate(spawnBonus[index], RandomWindowIndex(), spawnBonus[index].transform.rotation);
            yield return new WaitForSeconds(BonusSpawnRate);
        }
    }

    public Vector3 RandomWindowIndex()
    {
        float[] Zpos = new float[6] { -16, -12.5f, -9, -4.5f, -1.2f, 2f };
        float[] Ypos = new float[2] { 12, 18 };
        Vector3 windowIndex = new Vector3(5, Ypos[Random.Range(0, Ypos.Length)], Zpos[Random.Range(0, Zpos.Length)]);
        return windowIndex; 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOvertext.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
