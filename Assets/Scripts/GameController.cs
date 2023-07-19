using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite imageQues;
    private List<Button> listButton=new List<Button>();
    [SerializeField] Sprite[] SourceSprites;
    [SerializeField] List<Sprite> listSprite=new List<Sprite>();
    public bool firstGuess,secondGuess;
    public int firstIndex,secondIndex,totalGuess,CorrectGuess;
    public int NoofGuess=0;
    private int numberScene;
    private string firstname,secondname;
    [SerializeField] Text levelGame;
    [SerializeField] Text Scoretext;
    [SerializeField] Text Counttext;
    [SerializeField] GameObject dialogPanel;
    private void Awake() 
    {
        SourceSprites=Resources.LoadAll<Sprite>("Texture/");
        dialogPanel=GameObject.FindGameObjectWithTag("Dialog");
        
    }
    void Start()
    {
        numberScene=SceneManager.GetActiveScene().buildIndex;
        levelGame.text="Màn: "+numberScene.ToString();
        Counttext.text="Đoán Sai: "+NoofGuess.ToString();
        GetButtons();
        totalGuess=listButton.Count/2;
        NoofGuess=0;
        AddListener();
        AddSprites();
        Shuffle(listSprite);
        dialogPanel.SetActive(false);
    }
    void AddSprites()
    {
        int size=listButton.Count;
        int index=0;
        for(int i=0;i<size;i++)
        {
            if(i==size/2)
            {
                index=0;
            }
            listSprite.Add(SourceSprites[index]);
            index++;
        }
    }
    void GetButtons()
    {
        GameObject[] gameobject=GameObject.FindGameObjectsWithTag("Button");
        for(int i=0;i<gameobject.Length;i++)
        {
            listButton.Add(gameobject[i].GetComponent<Button>());
            listButton[i].image.sprite=imageQues;
        }
    }
    void AddListener()
    {
        foreach (Button button in listButton)
        {
            button.onClick.AddListener(()=>CheckGuess());
        }
    }
    void CheckGuess()
    {
        string name=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if(!firstGuess)
        {
            firstGuess=true;
            firstIndex=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstname=listSprite[firstIndex].name;
            listButton[firstIndex].image.sprite=listSprite[firstIndex];
            // Debug.Log("'1'");
        }
        else if(!secondGuess)
        {
            secondGuess=true;
            secondIndex=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondname=listSprite[secondIndex].name;
            listButton[secondIndex].image.sprite=listSprite[secondIndex];
            // Debug.Log("2");
            NoofGuess++;

            StartCoroutine(CheckSameAndiff());
        }
        
    }
    private IEnumerator CheckSameAndiff()
    {
        yield return new WaitForSeconds(0.5f);
        if(firstname==secondname && firstIndex!=secondIndex)
        {
            CorrectGuess++;
            listButton[firstIndex].interactable=false;
            listButton[secondIndex].interactable=false;
            listButton[firstIndex].image.color=new Color(0,0,0,0);
            listButton[secondIndex].image.color=new Color(0,0,0,0);
            Scoretext.text="Điểm :" +CorrectGuess.ToString();
            CheckFinished();
        }
        else
        {
            
            listButton[firstIndex].image.sprite=imageQues;
            listButton[secondIndex].image.sprite=imageQues;
            Counttext.text="Đoán Sai: "+NoofGuess.ToString();
        }
        firstGuess=false;
        secondGuess=false;
    }
    void Shuffle(List<Sprite> listSpr)
    {  
        for(int i=0;i<listSpr.Count;i++)
        {
            Sprite temp=listSpr[i];
            int random=Random.Range(i,listSpr.Count);
            listSpr[i]=listSpr[random];
            listSpr[random]=temp;
        }

    }
    void CheckFinished()
    {
        if(CorrectGuess==totalGuess)
        {
            dialogPanel.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
