using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Gamerule : MonoBehaviour
{
    public List<string> BananaForest = new List<string>();
    private GameObject AdaptivePlayer;
    private Text PlayerText;
    private Text RoundText;
    private Text IndicatorText;
    private Text IndicatorText2;
    private Text IndicatorText3;
    private Text IndicatorText4;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private GameObject ResultUi;
    async void Update()
    {
        int once = 0;
        if (once == 0)
        {
         once = 1;
         //updatestat
            for(int i = 1;i<=4;i++)
            {
                PlayerUI = GameObject.Find("Player"+i+"UI");
                GameObject canvasObject = GameObject.Find("Canvas");
                Transform canvasTransform = canvasObject.transform;
                Transform indicatorTransform = canvasTransform.Find(PlayerUI.name + "/WoodText");
                IndicatorText = indicatorTransform.GetComponent<Text>();
                Transform indicatorTransform2 = canvasTransform.Find(PlayerUI.name + "/BananaText");
                IndicatorText2 = indicatorTransform2.GetComponent<Text>();
                Transform indicatorTransform3 = canvasTransform.Find(PlayerUI.name + "/StoneText");
                IndicatorText3 = indicatorTransform3.GetComponent<Text>();
                Transform indicatorTransform4 = canvasTransform.Find(PlayerUI.name + "/HealthText");
                IndicatorText4 = indicatorTransform4.GetComponent<Text>();

                var playerVars = GameObject.Find("Player"+i).GetComponent<Variables>();
                IndicatorText.text = "Woods: " + (int)playerVars.declarations.GetDeclaration("Wood").value;
                IndicatorText2.text = "Bananas: " + (int)playerVars.declarations.GetDeclaration("Banana").value;
                IndicatorText3.text = "Stones: " + (int)playerVars.declarations.GetDeclaration("Stone").value;
                IndicatorText4.text = "Health: " + (int)playerVars.declarations.GetDeclaration("Health").value;
            }
        }

        if((int)GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value >= 4)
        {
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value = 0;
            for(int i = 1;i<=4;i++)
            {
                AdaptivePlayer = GameObject.Find("Player"+i.ToString());
                if (AdaptivePlayer != null)
                {
                    var playerVars = AdaptivePlayer.GetComponent<Variables>();
                    if (playerVars != null && playerVars.declarations.IsDefined("Place"))
                    {
                        if (playerVars.declarations.GetDeclaration("Place").value != null && playerVars.declarations.GetDeclaration("Place").value.ToString() == "BananaForest")
                        {
                        BananaForest.Add(AdaptivePlayer.name);
                        }
                    }   
                }
            }

            Debug.Log("Start");
            foreach(string plr in BananaForest)
            {
            Debug.Log(plr);
            }
            Debug.Log(BananaForest.Count);
            Debug.Log("End");

            
            //bananaforest event started when player gone into the place > 0
            if (BananaForest.Count > 0)
            {
                //clone the result ui
                GameObject cloneui = Instantiate(ResultUi, ResultUi.transform.parent);
                cloneui.SetActive(true);
                cloneui.name = "CurrentResultUI";
                Text titleres = cloneui.transform.Find("TitleResult").GetComponent<Text>();
                Text playerres = cloneui.transform.Find("AllPlayerResult").GetComponent<Text>();
                Text changeres = cloneui.transform.Find("Changelog").GetComponent<Text>();

                titleres.text = "Banana Forest";
                if (BananaForest.Count == 1)
                {
                    playerres.text = BananaForest[0];
                }
                if (BananaForest.Count == 2)
                {
                    playerres.text = BananaForest[0] + " VS " + BananaForest[1];
                }
                if (BananaForest.Count == 3)
                {
                    playerres.text = BananaForest[0] + " VS " + BananaForest[1] + " VS " + BananaForest[2];
                }
                if (BananaForest.Count == 4)
                {
                    playerres.text = BananaForest[0] + " VS " + BananaForest[1] + " VS " + BananaForest[2] + " VS " + BananaForest[3];
                }
                //is there ambush or not?
                int ambush = 0;
                foreach(string plr in BananaForest)
                {
                    if (GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("Action").value.ToString() == "Ambush")
                    {
                     ambush++;
                    }
                }
                 if (BananaForest.Count == 1)
                {
                    foreach(string plr in BananaForest)
                    {
                        if (GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("Action").value.ToString() == "Cut Tree")
                         {
                         GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value = (int)GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value + 1;
                         changeres.text = "Gain +1 wood";
                         }
                        if (GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("Action").value.ToString() == "Gather Banana")
                         {
                         GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value = (int)GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value + 3;
                         changeres.text = "Gathered +3 bananas";
                         }
                    }
                }
                 if (BananaForest.Count == 2)
                {
                    if(ambush <= 0)
                    {
                      GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;
                      GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;
                      changeres.text = "a Fight happened, both took each other's attacks";
                    }
                     if(ambush == 1)
                    {
                    int ambushedplr = -1;
           
                      for(int i = 0;i < BananaForest.Count;i++)
                        {
                            
                        if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Ambush")
                            {
                                ambushedplr = i;
                            }
                        }
                        for(int i = 0;i < BananaForest.Count;i++)
                        {
                            Debug.Log("This is the " + i + " Loop/" + BananaForest.Count);
                        if(i != ambushedplr)
                            {
                            GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;
                            if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Gather Banana")
                                {
                                GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value = (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value + 3;
                                changeres.text = "an Ambush happened, " + BananaForest[ambushedplr] + " attacked " + BananaForest[i] + " and took 3 bananas";
                                }
                            if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Cut Tree")
                                {
                                GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value = (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value + 1;
                                changeres.text = "an Ambush happened, " + BananaForest[ambushedplr] + " attacked " + BananaForest[i] + " and took 1 wood";
                                }
                            }
                        }
                    }
                     if(ambush == 2)
                    {
                    GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;
                    GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[1]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[0]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;  
                      changeres.text = "They ambushed on each other, both took each other's attacks";
                    }
                }
                 if (BananaForest.Count > 2)
                {
                    if(ambush != 1)
                    {
                       foreach(string plr in BananaForest)
                        {
                        GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(plr).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + 1;
                        }
                         changeres.text = "Chaos happened everyone took 1 damage";
                    }
                     if(ambush == 1)
                    {
                       int ambushedplr = -1;
                      for(int i = 0;i < BananaForest.Count;i++)
                        {
                        if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Ambush")
                            {
                                ambushedplr = i ;
                            }
                        }
                        for(int i = 0;i < BananaForest.Count;i++)
                        {
                        if(i != ambushedplr)
                            {
                            GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value = (int)GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("StoredDamage").value + (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("Damage").value;
                            if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Gather Banana")
                                {
                                GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value = (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredBanana").value + 3;
                                }
                            if (GameObject.Find(BananaForest[i]).GetComponent<Variables>().declarations.GetDeclaration("Action").value == "Cut Tree")
                                {
                                GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value = (int)GameObject.Find(BananaForest[ambushedplr]).GetComponent<Variables>().declarations.GetDeclaration("StoredWood").value + 1;
                                }
                                 changeres.text = BananaForest[ambushedplr] + " ambushed on everyone and stole everything!";
                            }
                        }  
                    }
                }
             await Task.Delay(4000);
            Destroy(cloneui);
            }

            //clear all action
            for(int i = 1;i<=4;i++)
            {
                AdaptivePlayer = GameObject.Find("Player"+i.ToString());
                if (AdaptivePlayer != null)
                {
                    var playerVars = AdaptivePlayer.GetComponent<Variables>();
                    if (playerVars != null && playerVars.declarations.IsDefined("Place"))
                    {
                    playerVars.declarations.GetDeclaration("Place").value = null;
                    playerVars.declarations.GetDeclaration("Action").value = null;
                    //pullstored data
                    playerVars.declarations.GetDeclaration("Wood").value = (int)playerVars.declarations.GetDeclaration("Wood").value + (int)playerVars.declarations.GetDeclaration("StoredWood").value;
                    playerVars.declarations.GetDeclaration("Banana").value = (int)playerVars.declarations.GetDeclaration("Banana").value + (int)playerVars.declarations.GetDeclaration("StoredBanana").value;
                    if ((int)playerVars.declarations.GetDeclaration("Banana").value > 5)
                        {
                        playerVars.declarations.GetDeclaration("Banana").value = 5;
                        }
                    playerVars.declarations.GetDeclaration("Stone").value = (int)playerVars.declarations.GetDeclaration("Stone").value + (int)playerVars.declarations.GetDeclaration("StoredStone").value;
                    playerVars.declarations.GetDeclaration("Health").value = (int)playerVars.declarations.GetDeclaration("Health").value - (int)playerVars.declarations.GetDeclaration("StoredDamage").value;

                    playerVars.declarations.GetDeclaration("StoredWood").value = 0;
                    playerVars.declarations.GetDeclaration("StoredBanana").value = 0;
                    playerVars.declarations.GetDeclaration("StoredStone").value = 0;
                    playerVars.declarations.GetDeclaration("StoredDamage").value = 0;

                        if ((int)playerVars.declarations.GetDeclaration("Banana").value > 0)
                        {
                        playerVars.declarations.GetDeclaration("Banana").value = (int)playerVars.declarations.GetDeclaration("Banana").value - 1;
                        }else
                        {
                        playerVars.declarations.GetDeclaration("Health").value = (int)playerVars.declarations.GetDeclaration("Health").value - 1; 
                        }
                    }
                }    
            }
            BananaForest.Clear();
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Turn").value = 1;
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Round").value = (int)GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Round").value + 1;
            RoundText = GameObject.Find("RoundText").GetComponent<Text>();
            RoundText.text = "Round " + GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Round").value.ToString();

            PlayerText = GameObject.Find("PlayerText").GetComponent<Text>();
            PlayerText.text = "Player" + GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Turn").value;
             //updatestat
            for(int i = 1;i<=4;i++)
            {
                PlayerUI = GameObject.Find("Player"+i+"UI");
                GameObject canvasObject = GameObject.Find("Canvas");
                Transform canvasTransform = canvasObject.transform;
                Transform indicatorTransform = canvasTransform.Find(PlayerUI.name + "/WoodText");
                IndicatorText = indicatorTransform.GetComponent<Text>();
                Transform indicatorTransform2 = canvasTransform.Find(PlayerUI.name + "/BananaText");
                IndicatorText2 = indicatorTransform2.GetComponent<Text>();
                Transform indicatorTransform3 = canvasTransform.Find(PlayerUI.name + "/StoneText");
                IndicatorText3 = indicatorTransform3.GetComponent<Text>();
                Transform indicatorTransform4 = canvasTransform.Find(PlayerUI.name + "/HealthText");
                IndicatorText4 = indicatorTransform4.GetComponent<Text>();

                var playerVars = GameObject.Find("Player"+i).GetComponent<Variables>();
                IndicatorText.text = "Woods: " + (int)playerVars.declarations.GetDeclaration("Wood").value;
                IndicatorText2.text = "Bananas: " + (int)playerVars.declarations.GetDeclaration("Banana").value;
                IndicatorText3.text = "Stones: " + (int)playerVars.declarations.GetDeclaration("Stone").value;
                IndicatorText4.text = "Health: " + (int)playerVars.declarations.GetDeclaration("Health").value;
            }

        }
    }
}
