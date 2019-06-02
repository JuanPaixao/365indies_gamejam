using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{

    public int puzzleScore;
    public List<string> list;
    public bool red, blue, isFinal;
    public int maxScore;
    public string nextScene, finalScene;
    public DialogueSystem dialogue;
    public void SetScore()
    {
        puzzleScore++;
        Scene scene = SceneManager.GetActiveScene();
        finalScene = scene.name;
    }
    public void AddPiece(string s)
    {
        list.Add(s);
    }
    void Update()
    {
        if (puzzleScore == maxScore)
        {
            StartCoroutine(ChangeScene());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            list.Add("red");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            list.Add("blue");
        }
        foreach (string color in list)
        {
            if (color.Equals("red"))
            {
                red = true;
            }
            else if (color.Equals("blue"))
            {
                blue = true;
            }
        }
        if (blue && !red)
        {
            Debug.Log("Sucess");
        }
        else if (red && !blue)
        {
            Debug.Log("Sucess");
        }
    }
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        if (isFinal)
        {
            Debug.Log("Final");
            dialogue.LoadFinal();
        }
        else
        {
            Debug.Log("!Final");
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }
    }
}
