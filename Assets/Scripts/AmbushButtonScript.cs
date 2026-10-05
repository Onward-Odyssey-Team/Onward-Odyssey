using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmbushButtonScript : MonoBehaviour
{
    private Text ActionText;
    private Text PlayerText;
    private Text PlaceText;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private GameObject System;
    [SerializeField] private GameObject bananaForestAction;
    public void OnButtonClick()
    {
        Player = GameObject.Find("Player" + System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value.ToString());
        PlayerUI = GameObject.Find("Player" + System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value.ToString() + "UI");
        ActionText = GameObject.Find("ActionText").GetComponent<Text>();
        PlaceText = GameObject.Find("PlaceText").GetComponent<Text>();


        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();

        Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value = "Ambush";
        ActionText.text = "Action: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value;
        Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value = "BananaForest";
        PlaceText.text = "Place: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value;

        bananaForestAction.SetActive(false);
        //round update after fourth player
        System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value + 1;
        PlayerText.text = "Player" + (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value;
        Player = GameObject.Find("Player" + System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value.ToString());
          System.GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value + 1;
    }
}
