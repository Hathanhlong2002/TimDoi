using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AddButtton : MonoBehaviour
{
    [SerializeField] 
    private GameObject button;
    [SerializeField] public int num;
    [SerializeField] 
    private Transform panelBackground;
    // Start is called before the first frame update
    void Awake()
    {
        int numberScene=SceneManager.GetActiveScene().buildIndex;
        num=numberScene*4;
        for(int i=0;i<num;i++)
        {
            GameObject buttonTemp=Instantiate(button);
            buttonTemp.name=i.ToString();
            buttonTemp.transform.SetParent(panelBackground,false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
