using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scavenge1ButtonScript : MonoBehaviour
{
    private Text ActionText;
    private Text IndicatorText;
    private Text RoundText;
    private SpriteRenderer Places;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject System;
    [SerializeField] private GameObject bananaForestAction;
    public void OnButtonClick()
    {
        ActionText = GameObject.Find("ActionText").GetComponent<Text>();
        IndicatorText = GameObject.Find("WoodText").GetComponent<Text>();
        RoundText = GameObject.Find("RoundText").GetComponent<Text>();
        Places = GameObject.Find("BananaForest").GetComponent<SpriteRenderer>();

        ActionText.text = "P1 Action: Cut Tree";
        Player.GetComponent<Variables>().declarations.GetDeclaration("Action").value = "Cut Tree";
        var wood = Player.GetComponent<Variables>().declarations.GetDeclaration("Wood");
        wood.value = (int)wood.value + 1;
        IndicatorText.text = "Woods: " + wood.value.ToString();
        System.GetComponent<Variables>().declarations.GetDeclaration("Round").value = (int)System.GetComponent<Variables>().declarations.GetDeclaration("Round").value + 1;
        RoundText.text = "Round " + System.GetComponent<Variables>().declarations.GetDeclaration("Round").value.ToString();
        bananaForestAction.SetActive(false);
        Places.color = new Color(1f, 1f, 1f, 1f);
    }
}
