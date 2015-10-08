using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class TextController : MonoBehaviour
{
    private Dictionary<string, string> textBackUp = new Dictionary<string, string>();
    private Dictionary<string, string> text = new Dictionary<string, string>();

    private static TextController instance;
    public static TextController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        textBackUp.Add("IntroMsg", "Se você é como eu e adora desvendar enigmas, vai adorar este jogo. Vamos viajar juntos através do tempo e conhecer diferentes civilizações.\n"
                                 + "Cada civilização antiga tinha uma forma diferente de contar os números. "
                                 + "Neste jogo, você vai ter que descobir como os números são representados em diferentes códigos para conhecer os monumentos históricos.\n" 
                                 + "Preste bastante atenção aos enigmas e bom jogo!");

        textBackUp.Add("Tutorial1Part1", "Clique no número que deseja completar.");
        textBackUp.Add("Tutorial1Part2", "Depois, arraste os símbolos egípcios correspondentes que formarão os números com as respostas.");
        textBackUp.Add("Tutorial2Part1", "Clique no número que deseja completar.");
        textBackUp.Add("Tutorial2Part2", "Depois, arraste os símbolos romanos correspondentes que formarão os números com as respostas.");
        textBackUp.Add("Tutorial3Part1", "Clique no número que deseja completar.");
        textBackUp.Add("Tutorial3Part2", "Depois, arraste os símbolos chineses correspondentes que formarão os números com as respostas.");

        textBackUp.Add("GameLvl1Tela1", "Estamos no Egito antigo. No interior da pirâmide você encontrará uma porta. Preciso da sua ajuda. " 
                                      + "Siga em frente e preencha com os símbolos egípcios o cartaz para desvendar o enigma dos Faraós.");
        textBackUp.Add("GameLvl1Tela2", "Na última colheita desta província imperial, foram obtidos os seguintes produtos: " 
                                      + "15000 barris de trigo e 72500 barris de cevada para alimentação e 2855 barris de linho para a produção de roupas. " 
                                      + "Também foram colhidas 5000 frutas, contando tâmaras, maçãs, bananas e uvas.");

        textBackUp.Add("GameLvl2Tela1", "Estamos diante de uma coluna romana. Nela foram escritos os resultados de uma batalha por território de um dos grandes imperadores da época. " 
                                      + "Siga em frente e complete a coluna com os símbolos romanos para desvendar o enigma!");
        textBackUp.Add("GameLvl2Tela2", "Na última batalha, no ano 52 a.C., foram feitos 9505 prisioneiros. " 
                                      + "A campanha do imperador contava com apenas 4 legiões, somando 24 mil homens, fora as tropas auxiliares, com mais de 55 mil soldados.");

        textBackUp.Add("GameLvl3Tela1", "Sempre tive vontade de conhecer a China. Sua cultura milenar é realmente incrível. Ao lado da porta, vemos uma placa de calcular conhecida como quadrado mágico. " 
                                      + "Se você somar todos os números verticalmente, horizontalmente ou na diagonal, o resultado será sempre 15!\n" 
                                      + "Complete o quadrado mágico com os símbolos chineses e desvende este enigma!");

        textBackUp.Add("FailLvl1", "Lembre-se que o sistema numérico egípcio é baseado em agrupamentos. Para escrever os diferentes números, você pode usar até nove símbolos de cada valor.");
        textBackUp.Add("FailLvl2", "Lembre-se:  repetindo cada símbolo duas ou três vezes, nunca mais que três, o número fica duas ou três vezes maior. Os símbolos V, L e D nunca se repetem. " 
                                 + "Os algarismos de menor valor colocados à esquerda eram subtraídos do algarismo de maior valor.");
        textBackUp.Add("FailLvl3", "Lembre-se que o resultado da soma de todos os números é sempre 15, tanto na vertical, na horizontal ou na diagonal.");
        textBackUp.Add("FailTimeIsOver", "<size=50><b>Ops!</b></size>\n\nNão foi desta vez. Quer tentar de novo?");

        textBackUp.Add("FeedbackLvl", "<size=50><b>Muito bom!</b></size>\n\nVocê desvendou este enigma. Siga para o próximo!");
        textBackUp.Add("FeedbackFinal", "<size=50><b>Parabéns!</b></size>\n\nVocê é muito bom em enigmas e sabe tudo de símbolos e representações matemáticas.");

        textBackUp.Add("PauseMsg", "<size=50><b>Ufa!</b></size>\n\nPausa para descansar!");

        textBackUp.Add("ResetMsg", "Quer mesmo reiniciar o jogo?");
        textBackUp.Add("QuitMsg", "Quer mesmo sair do jogo?");

        StartCoroutine(LoadText());
    }

    IEnumerator LoadText()
    {
        string url = "";
#if UNITY_EDITOR
        url = MySettings.editorUrl + MySettings.urlText;
#elif UNITY_STANDALONE
        if (File.Exists(Application.dataPath + "/Resources" + MySettings.urlText))
        {
            url = "file://" + Application.dataPath + "/Resources" + MySettings.urlText;
        }
#else
        url = Application.dataPath + MySettings.urlText;
#endif

        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("ERROR getting " + url + " file!");
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                JSONObject newText = new JSONObject(www.text);
                foreach (string key in newText.keys)
                {
                    text.Add(key, newText.GetField(key).str.Replace("\\n", System.Environment.NewLine));
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }

    public string GetText(string textFor)
    {
        if (text.ContainsKey(textFor))
        {
            return text[textFor];
        }
        else
        {
            return textBackUp[textFor];
        }
    }

}
