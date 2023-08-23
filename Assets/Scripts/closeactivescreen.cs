using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeactivescreen : MonoBehaviour
{
    public GameObject gameobject;
    // Start is called before the first frame update

    public void closegameobject() {
        gameobject.SetActive(false);
    }
}
