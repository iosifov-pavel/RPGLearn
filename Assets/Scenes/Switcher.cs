using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject creatingMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;
    // Start is called before the first frame update
    void Start()
    {
        Back();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back(){
        creatingMenu.SetActive(false);
        loadMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void NewGameButton(){
        creatingMenu.SetActive(true);
        mainMenu.SetActive(false);
        loadMenu.SetActive(false);
    }

    public void LoadGame(){
        creatingMenu.SetActive(false);
        mainMenu.SetActive(false);
        loadMenu.SetActive(true);
    }
}
