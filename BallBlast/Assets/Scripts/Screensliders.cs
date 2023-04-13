using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screensliders : MonoBehaviour
{
    [SerializeField]BoxCollider2D left;
    [SerializeField] BoxCollider2D right;

    private void Start()
    {
        float screenwidth=Game.Instance.screenWidth;

        left.transform.position = new Vector3(-screenwidth - left.size.x / 2, 0, 0);
        right.transform.position = new Vector3(screenwidth + right.size.x / 2, 0, 0);


        Destroy(this);
    }
}
