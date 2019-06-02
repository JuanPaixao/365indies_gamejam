using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Claw : MonoBehaviour
{

    private Rigidbody2D _rb;
    public float speed;
    public List<string> objects;
    public int objectsOnbox, rocks;
    public Animator _animator;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        float movHor = Input.GetAxis("Horizontal");
        float movVer = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(movHor, movVer) * speed * Time.deltaTime;

        if (rocks == 5 && objectsOnbox >= 5)
        {
            StartCoroutine(BadEnding());
        }
        else if (objectsOnbox >= 5)
        {
            StartCoroutine(NextScene());
        }
    }
    public void BoxedObject(string boxed)
    {
        objectsOnbox++;
        objects.Add(boxed);
    }
    public void WithObject(bool bol)
    {
        _animator.SetBool("withObject", bol);
    }
    private IEnumerator BadEnding()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Dialogue_02.5 BadEnding", LoadSceneMode.Single);
    }
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Dialogue_03", LoadSceneMode.Single);
    }
}
