using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject FirstPannel;
    [SerializeField] GameObject SecondPannel;

    public void EnableSecondPannel()
    {
        FirstPannel.SetActive(false);
        SecondPannel.SetActive(true);
    }
}
