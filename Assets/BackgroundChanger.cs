using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    public Image background;
    public void ChangeImageColor(float f)
    {
        background.color = new Color(1f, f, 1f);
    }
    public void RandomizeColor()
    {        
        background.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

}
