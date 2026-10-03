using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BananaForestTarget : MonoBehaviour
{
    [SerializeField] private GameObject bananaForestAction;
  public void OnButtonClick()
    {
             bananaForestAction.SetActive(true);
    }
}
