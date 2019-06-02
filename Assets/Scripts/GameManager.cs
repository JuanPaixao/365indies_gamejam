using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        DialogueSystem.bossOffer = false;
        DialogueSystem.stoleGold = false;
        DialogueSystem.turnedOnRedCable = false;
        SceneManager.LoadScene("Dialogue_01", LoadSceneMode.Single);
    }
    public void GoToMenu()
    {
        StartCoroutine(Menu());
    }
    private IEnumerator Menu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Ending", LoadSceneMode.Single);
    }
}
