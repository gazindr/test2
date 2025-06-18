using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransform : MonoBehaviour
{


    bool isMoving = false;
    public RectTransform button;
    Vector2 startPos;
    private void Start()
    {
        startPos = button.position;
    }
    public void ChangeMoving(bool b)
    {
        isMoving = b;
    }
    public float speed;
    void Update()
    {
        if (isMoving) 
        {
            button.position = new Vector2(button.position.x + (speed * Random.Range(-5, 5)), button.position.y + (speed * Random.Range(-5, 5)));
        }
        if (button.localPosition.x > Screen.width/2 || button.localPosition.x < -Screen.width / 2 || button.localPosition.y > Screen.height / 2 || button.localPosition.y < -Screen.height / 2)
        {
            button.position = startPos;
        }

    }
}
