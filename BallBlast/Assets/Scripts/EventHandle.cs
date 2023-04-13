using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventHandle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string obj;
    bool pressed;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(pressed)
        {
            if(obj=="left")
            {
                //Cannon.instance.LeftMove();
            }
            if(obj=="right")
            {
            //    Cannon.instance.RightMove();
            }

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
