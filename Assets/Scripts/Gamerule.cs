using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class Gamerule : MonoBehaviour
{
     public List<string> BananaForest = new List<string>();
      private GameObject AdaptivePlayer;
    void Update()
    {
        if((int)GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value >= 4)
        {

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
                        BananaForest.Add(AdaptivePlayer.ToString());
                        }
                    }   
                }
            }
            Debug.Log("Start");
            foreach(string plr in BananaForest)
            {
            Debug.Log(plr);
            }
            Debug.Log("End");
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("ReadyPlayer").value = 0;
            
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
                    }
                }    
            }
            BananaForest.Clear();
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Turn").value = 1;
            GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Round").value = (int)GameObject.Find("System").GetComponent<Variables>().declarations.GetDeclaration("Round").value + 1;
        }
    }
}
