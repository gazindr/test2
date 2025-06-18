using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInteraction : MonoBehaviour
{

    public UnityEvent eventPressed; // Событие, которое срабатывает при нажатии
    bool isReady = true;
    Coroutine coroutine;
    public void OnClick()
    {
        if (isReady && gameObject.activeInHierarchy)
        {
            
            
            isReady = false;
            //if (coroutine == null) 
            coroutine = StartCoroutine(WaitForNewClick());
            eventPressed.Invoke();
        }
        
    }
    IEnumerator WaitForNewClick()
    {
        yield return new WaitForSeconds(0.1f);
        isReady = true;
        coroutine = null;
    }
    private void OnEnable()
    {
        isReady = true;
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
