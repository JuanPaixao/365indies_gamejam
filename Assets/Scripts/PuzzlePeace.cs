using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePeace : MonoBehaviour
{
    public bool selected;
    private Rigidbody2D _rb;
    void Start()
    {
		_rb = GetComponent<Rigidbody2D>();
    }
    public void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = target;
            selected = true;
        }
        else
        {
            selected = false;
        }
    }
}
