using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{

    private Queue<string> _sentences;
    public Text dialogueText, nameText;
    [SerializeField] private int _sceneNumber = 0;
    public GameObject[] dialogues, panel;
    [SerializeField] private int _dialogNumber = 0;
    [SerializeField] private int _panelNumber = 0;
    public bool isDecisionScene, isFinal;
    public static bool stoleGold, turnedOnRedCable, bossOffer;
    public string nextScene;
    public GameObject Joe, Bigboss, Clint;
    public AudioClip clip;

    void Awake()
    {
        _sentences = new Queue<string>();
    }
    void Start()
    {
        if (!isFinal)
        {
            dialogues[_dialogNumber].SetActive(true);
        }
        Debug.Log("stole gold: " + stoleGold + "  turnedOnRedCable +" + turnedOnRedCable + " accepted the offer" + bossOffer);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.NPCName;
        ActiveDialogueSprite(nameText.text);
        _sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        else
        {
            AudioManager.instance.PlayOneShot(clip, 0.3f);
            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(DialogueRoutine(sentence));
        }
    }
    public void EndDialog()
    {
        _dialogNumber += 1;

        if (_dialogNumber < dialogues.Length)
        {
            dialogues[_dialogNumber].SetActive(true);
        }
        else
        {
            if (isDecisionScene)
            {
                panel[0].SetActive(true);
                _panelNumber++;
            }
            else
            {
                Scene thisScene = SceneManager.GetActiveScene();
                string sceneName = thisScene.name;
                if (sceneName == "Dialogue_09" || sceneName == "Dialogue_08.yes")
                {
                    LoadFinal();
                }
                else
                {
                    LoadNextScene(nextScene);
                }
            }

        }
    }
    public void LoadNextScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
    public void StoleGolds(bool stole)
    {
        stoleGold = stole;
    }
    public void TurnedOnRedCable(bool redCable)
    {
        turnedOnRedCable = redCable;
    }
    public void AcceptedOffer(bool offer)
    {
        bossOffer = offer;
    }
    public void LoadFinal()
    {
        if (stoleGold && !turnedOnRedCable)
        {
            SceneManager.LoadScene("Dialogue_10- Neutral_Ending", LoadSceneMode.Single);
        }
        else if (stoleGold && turnedOnRedCable)
        {
            SceneManager.LoadScene("Dialogue_10- Bad_Ending", LoadSceneMode.Single);
        }
        else if (!stoleGold && turnedOnRedCable)
        {
            SceneManager.LoadScene("Dialogue_10- Good_Ending", LoadSceneMode.Single);
        }
        else if (!stoleGold && !turnedOnRedCable && bossOffer)
        {
            SceneManager.LoadScene("Dialogue_10- Good_Ending", LoadSceneMode.Single);
        }
        else if (!stoleGold && !turnedOnRedCable && !bossOffer)
        {
            SceneManager.LoadScene("Dialogue_10- Perfect_Ending", LoadSceneMode.Single);
        }

    }
    private IEnumerator DialogueRoutine(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void ActiveDialogueSprite(string name)
    {
        if (Joe != null)
        {
            if (name == "Detetive Joe")
            {
                Joe.SetActive(true);
            }
            else
            {
                Joe.SetActive(false);
            }
        }

        if (Clint != null)
        {
            if (name == "Clint")
            {
                Clint.SetActive(true);
            }
            else
            {
                Clint.SetActive(false);
            }
        }

        if (Bigboss != null)
        {
            if (name == "Big Boss")
            {
                Bigboss.SetActive(true);
            }
            else
            {
                Bigboss.SetActive(false);
            }
        }
    }
}

