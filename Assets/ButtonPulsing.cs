using UnityEngine;

public class PulsatingObject : MonoBehaviour
{
    [SerializeField]  float pulseSpeed = 1f; 
    [SerializeField]  float pulseAmount = 0.2f; 
    private void Update()
    {
        
        float scaleFactor = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = Vector3.one * scaleFactor;
        
    }
}