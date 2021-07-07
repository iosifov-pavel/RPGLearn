using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    PlayerSpeaker player;
    [SerializeField] Button nextButton;
    [SerializeField] TextMeshProUGUI AIText;
    [SerializeField] GameObject answerPrefab;
    [SerializeField] Transform choicesRoot;
    [SerializeField] GameObject playerAnswerPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeaker>();
        nextButton.onClick.AddListener(Next);
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        playerAnswerPanel.SetActive(player.HasAnswers());
        AIText.text = player.GetText();
        nextButton.gameObject.SetActive(player.HasNext() && !player.HasAnswers());
        foreach (Transform item in choicesRoot)
        {
            Destroy(item.gameObject);
        }
        if(player.HasAnswers()){
            foreach(DialogueNode choiseNode in player.GetChoices()){
                GameObject newButton = Instantiate(answerPrefab,choicesRoot);
                Text newText = newButton.transform.GetChild(0).GetComponent<Text>();
                newText.text = choiseNode.GetText();
                newButton.GetComponent<Button>().onClick.AddListener(delegate(){NextForAnswer(choiseNode);});
            }
        }
        else if(!player.HasNext())
        {
            playerAnswerPanel.SetActive(true);
            GameObject newButton = Instantiate(answerPrefab, choicesRoot);
            Text newText = newButton.transform.GetChild(0).GetComponent<Text>();
            newText.text = "End of a Dialog";
            newButton.GetComponent<Button>().onClick.AddListener(Close);
        }
    }

    public void Next(){
        player.Next();
        UpdateUI();
    }

    public void NextForAnswer(DialogueNode node){
        player.NextForCurrentNode(node);
        UpdateUI();
    }

    public void Close(){
        player.CloseDialogue();
    }    
}
