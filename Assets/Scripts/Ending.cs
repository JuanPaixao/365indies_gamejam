using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
		StartCoroutine(Return());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator Return()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
