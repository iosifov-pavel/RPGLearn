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
    [SerializeField] TextMeshProUGUI speaker;
    [SerializeField] GameObject answerPrefab;
    [SerializeField] Transform choicesRoot;
    [SerializeField] GameObject playerAnswerPanel;
    [SerializeField] Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpeaker>();
        player.OnDialogueUpdated+=UpdateUI;
        nextButton.onClick.AddListener(player.Next);
        quitButton.onClick.AddListener(delegate(){player.CloseDialogue();});
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        gameObject.SetActive(player.IsActive());
        if(!player.IsActive()) return;
        playerAnswerPanel.SetActive(player.HasAnswers());
        speaker.text = player.GetAINAme();
        AIText.text = player.GetText();
        nextButton.gameObject.SetActive(player.HasNext() && !player.HasAnswers());
        foreach (Transform item in choicesRoot)
        {
            Destroy(item.gameObject);
        }
        if(player.HasAnswers()){
            player.SetAnswers();
            foreach(DialogueNode choiseNode in player.GetChoices()){
                GameObject newButton = Instantiate(answerPrefab,choicesRoot);
                Text newText = newButton.transform.GetChild(0).GetComponent<Text>();
                string additionText ="";
                if(choiseNode.GetChildrens().Count==0) additionText = " (End of a Dialog)";
                newText.text = choiseNode.GetText()+additionText;
                newButton.GetComponent<Button>().onClick.AddListener(()=>player.NextForCurrentNode(choiseNode));
            }
        }
        else if(!player.HasNext())
        {
            playerAnswerPanel.SetActive(true);
            GameObject newButton = Instantiate(answerPrefab, choicesRoot);
            Text newText = newButton.transform.GetChild(0).GetComponent<Text>();
            newText.text = "End of a Dialog";
            newButton.GetComponent<Button>().onClick.AddListener(player.CloseDialogue);
        }
    } 
}
