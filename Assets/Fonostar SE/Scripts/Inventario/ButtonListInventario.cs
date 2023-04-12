using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonListInventario : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    private GameObject buttonRetornar;

    private List<GameObject> buttons;

    private void Start() {
        buttonRetornar = GameObject.Find("ButtonRetornar");
        buttonRetornar.GetComponent<Button>().onClick.AddListener(delegate{Inicial.trocarCena();});
        GenerateList();
    }

    private void GenerateList() {
        if (GetComponentInChildren<ButtonListButton>() != null)
        {
            ButtonListButton[] BLB = GetComponentsInChildren<ButtonListButton>();
            foreach (ButtonListButton i in BLB)
            {
                Destroy(i.gameObject);
            }
        }

        for(char c = 'A'; c<= 'Z'; c++)
        {

            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.gameObject.name = "Botao"+c;
            button.SetActive(true);
            GameObject quantidade = GameObject.Find("Botao"+c+"/Quantidade/");
            button.GetComponentInChildren<ButtonListButton>().SetText(c.ToString());
            var str = PlayerPrefs.GetString("LetrasInventario");
            var i = str.IndexOf(c);
            string j = "";

            if (i == -1)
            {
                // Num achou
            }
            else
            {
                do
                {
                    j = j + c;
                    i = str.IndexOf(c, i + 1);
                } while (i != -1);
            }
            quantidade.GetComponent<TextMeshProUGUI>().SetText("x"+j.Length);
            button.transform.SetParent(buttonTemplate.transform.parent, false);

        }
    }

}
