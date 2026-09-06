using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Unity.GraphToolkit.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private Text clicktext;
    private Text clicktext2;
    private Text clicktext3;

    [SerializeField] private GameObject bananaForestAction;

    private int num = 0;
    private int num2 = 0;
    private int num3 = 0;
    private void Awake()
    {
        _mainCamera = Camera.main;
        clicktext = GameObject.Find("BananaText").GetComponent<Text>();
        clicktext2 = GameObject.Find("WoodText").GetComponent<Text>();
        clicktext3 = GameObject.Find("StoneText").GetComponent<Text>();
    }

    private int cd = 0;

    async void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(pos: (Vector3)Mouse.current.position.ReadValue()));


        if (!rayHit.collider) return;
        var objec = rayHit.collider.gameObject;
        //  Debug.Log(rayHit.collider.gameObject.name);
        //Debug.Log("Clicked");
        //Debug.Log(objec.transform.position);
        //float ran = UnityEngine.Random.Range(1, 10) / 10f;
        //objec.transform.position = objec.transform.position + new Vector3(ran, 0, 0);
        if (cd == 0) 
        {
        cd = 1;
            if (objec.GetComponent<SpriteRenderer>())
                {
                objec.GetComponent<SpriteRenderer>().enabled = false;
                objec.GetComponent<BoxCollider2D>().enabled = false;
            }
            var varia = objec.GetComponent<Variables>();
            var reID = varia.declarations.GetDeclaration("ResourceID");
            if (varia != null)
            {
               // Debug.Log(reID.value);
                if ((int)reID.value == 1)
                {
                    num = num + 1;
                //    clicktext.text = "Bananas: " + num.ToString();

                    bananaForestAction.SetActive(true);
                    objec.GetComponent<SpriteRenderer>().color = new Color(0.2352941f, 1f, 0f, 1f);

                }
                if ((int)reID.value == 2)
                {
                    num2 = num2 + 1;
                    clicktext2.text = "Woods: " + num2.ToString();
                }
                if ((int)reID.value == 3)
                {
                    num3 = num3 + 1;
                    clicktext3.text = "Stones: " + num3.ToString();
                }

            }


        await Task.Delay(500);
            if (objec.GetComponent<SpriteRenderer>())
            {
                objec.GetComponent<SpriteRenderer>().enabled = true;
                objec.GetComponent<BoxCollider2D>().enabled = true;
            }
            cd = 0;
      }
    }
}
