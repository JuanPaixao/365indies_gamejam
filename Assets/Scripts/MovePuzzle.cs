using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
    public string pieceStatus = "idle";
    public string checkPlacement;
    public Transform edgeParticles;
    private Vector2 _initialPosition;
    private PuzzleManager _puzzleManager;
    public AudioClip cableFlop, cablesSmoke;
    void Start()
    {
        _puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        _initialPosition = this.transform.position;
    }
    void Update()
    {
        Select();
    }
    void Select()
    {
        if (pieceStatus == "pickedup")
        {
            Vector2 objPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = objPos;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            checkPlacement = "yes";
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (other.gameObject.name == this.gameObject.name && checkPlacement == "yes" && other.CompareTag("Put"))
            {
                //  other.GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                transform.position = other.gameObject.transform.position;
                pieceStatus = "locked";
                Instantiate(edgeParticles, other.gameObject.transform.position, Quaternion.identity);
                checkPlacement = "no";
                AudioManager.instance.PlayOneShot(cablesSmoke, 0.75f);
                _puzzleManager.SetScore();
            }
        }
    }
    void OnMouseDown()
    {
        pieceStatus = "pickedup";
    }
    void OnMouseUp()
    {
        checkPlacement = "no";
        if (pieceStatus != "locked")
        {
            this.transform.position = _initialPosition;
            pieceStatus = "idle";
            AudioManager.instance.PlayOneShot(cableFlop, 0.75f);
        }
    }
}
