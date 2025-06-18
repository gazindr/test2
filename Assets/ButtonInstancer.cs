using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstancer : MonoBehaviour
{
    bool isInstanced = false;
    Coroutine coroutine;
    private void Start()
    {
        transform.localScale = Vector3.zero;
    }
    public void SetButton()
    {
        isInstanced = true;
        transform.localScale = Vector3.one;
        coroutine = StartCoroutine(ChangeSize());
    }
    IEnumerator ChangeSize()
    {
        while (transform.localScale.x > 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z - 0.01f);
            yield return new WaitForSeconds(0.025f);
        }
        transform.localScale = Vector3.zero;
        coroutine = null;
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        StopAllCoroutines();
    }

}
