using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmbushButtonScript : MonoBehaviour
{
    private Text ActionText;
    private Text RoundText;
    private Text PlayerText;
    private Text PlaceText;
    private SpriteRenderer Places;
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


        RoundText = GameObject.Find("RoundText").GetComponent<Text>();
        Places = GameObject.Find("BananaForest").GetComponent<SpriteRenderer>();
        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();

        Debug.Log(Player);
        Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value = "Ambush";
        ActionText.text = "Action: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value;
        Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value = "BananaForest";
        PlaceText.text = "Place: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value;

        bananaForestAction.SetActive(false);
        Places.color = new Color(1f, 1f, 1f, 1f);
        //round update after fourth player
        System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value + 1;
        if ((int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value >= 5)
        {
            System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value = 1;
            System.GetComponent<Variables>().declarations.GetDeclaration("Round").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("Round").value + 1;
            RoundText.text = "Round " + System.GetComponent<Variables>().declarations.GetDeclaration("Round").value.ToString();
        }
        PlayerText.text = "Player" + (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value;
        Player = GameObject.Find("Player" + System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value.ToString());
    }
}
