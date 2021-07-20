using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] Transform loadContainer;
    [SerializeField] GameObject saveFilePrefab;
    SavingWrapper wrapper;

    private void OnEnable() {
        wrapper = FindObjectOfType<SavingWrapper>();
        ChildsHelper.DeleteChilds(loadContainer.gameObject);
        foreach(string saveFile in wrapper.ListSaves()){
            Button loadFile = Instantiate(saveFilePrefab,loadContainer).GetComponent<Button>();
            Text name = loadFile.GetComponentInChildren<Text>();
            name.text = saveFile;
            loadFile.onClick.AddListener(()=>wrapper.LoadGame(saveFile));
        }
    }
}
