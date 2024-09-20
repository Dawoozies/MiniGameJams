using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabsTest : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public GameObject menuContent;
    public List<Element> elements = new();
    public bool offScreen;
    private void Update()
    {
        if(offScreen)
        {
            foreach(Element e in elements)
            {
                if(e.screenState == Element.ScreenState.OnScreen)
                    e.RequestStateChange(Element.ScreenState.TransitionToOffScreen);
            }
        }
        else
        {
            foreach (Element e in elements)
            {
                if (e.screenState == Element.ScreenState.OffScreen)
                    e.RequestStateChange(Element.ScreenState.TransitionToOnScreen);
            }
        }
    }
}
