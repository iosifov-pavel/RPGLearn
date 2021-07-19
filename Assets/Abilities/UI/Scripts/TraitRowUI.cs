using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TraitRowUI : MonoBehaviour
{
    [SerializeField] Traits traitType;
    [SerializeField] TextMeshProUGUI traitValue;
    [SerializeField] Button minus;
    [SerializeField] Button plus;
    TraitsStore playerTraits = null;
    // Start is called before the first frame update
    private void Awake() {
    }
    void Start()
    {
        playerTraits = GameObject.FindWithTag("Player").GetComponent<TraitsStore>();
        minus.onClick.AddListener(()=>Allocate(-1));
        plus.onClick.AddListener(()=>Allocate(+1));
    }

    // Update is called once per frame
    void Update()
    {
        minus.interactable = playerTraits.CanAssignPoints(traitType,-1);
        plus.interactable = playerTraits.CanAssignPoints(traitType,1);
        traitValue.text = playerTraits.GetProposedPoints(traitType).ToString();
    }

    public void Allocate(int points){
        playerTraits.SetTrait(traitType,points);
    }
}
