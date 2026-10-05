using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scavenge1ButtonScript : MonoBehaviour
{
    private Text ActionText;

    //private Text IndicatorText;
    //private Text IndicatorText2;
    //private Text IndicatorText3;
    //private Text IndicatorText4;
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
    
         //Transform canvasTransform = transform.parent.parent;
         //Transform indicatorTransform = canvasTransform.Find(PlayerUI.name + "/WoodText");
         //IndicatorText = indicatorTransform.GetComponent<Text>();
         //Transform indicatorTransform2 = canvasTransform.Find(PlayerUI.name + "/BananaText");
         //IndicatorText2 = indicatorTransform2.GetComponent<Text>();
         //Transform indicatorTransform3 = canvasTransform.Find(PlayerUI.name + "/StoneText");
         //IndicatorText3 = indicatorTransform3.GetComponent<Text>();
         //Transform indicatorTransform4 = canvasTransform.Find(PlayerUI.name + "/HealthText");
         //IndicatorText4 = indicatorTransform4.GetComponent<Text>();

        PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();

        Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value = "Cut Tree";
        ActionText.text = "Action: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value;
        Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value = "BananaForest";
        PlaceText.text = "Place: " + Player.GetComponent<Variables>().declarations.GetDeclaration("Place").value;
        // every resource variable
        //var resource = Player.GetComponent<Variables>().declarations.GetDeclaration("Wood");
        //var resource2 = Player.GetComponent<Variables>().declarations.GetDeclaration("Banana");
        //var resource3 = Player.GetComponent<Variables>().declarations.GetDeclaration("Stone");
        //var resource4 = Player.GetComponent<Variables>().declarations.GetDeclaration("Health");
        //resource.value = (int)resource.value + 1;
        //if ((int)resource.value > 5)
        //{
        //    resource.value = 5;
        //}

        bananaForestAction.SetActive(false);
        //round update after fourth player
        System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value + 1;
        PlayerText.text = "Player" + (int)System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value;
        Player = GameObject.Find("Player" + System.GetComponent<Variables>().declarations.GetDeclaration("Turn").value.ToString());
        System.GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value + 1;
        //resource = Player.GetComponent<Variables>().declarations.GetDeclaration("Wood");
        //update stat
        //IndicatorText.text = "Woods: " + resource.value.ToString();
        //IndicatorText2.text = "Bananas: " + resource2.value.ToString();
        //IndicatorText3.text = "Stones: " + resource3.value.ToString();
        //IndicatorText4.text = "Health: " + resource4.value.ToString();
    }
}
