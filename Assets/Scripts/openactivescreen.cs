using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openactivescreen : MonoBehaviour
{
    public GameObject gameobject;
    // Start is called before the first frame update

    public void opengameobject()
    {
        gameobject.SetActive(true);
    }
}
