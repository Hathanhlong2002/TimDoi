using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : MonoBehaviour
{
    public LevelSelectMenu menu;
    public Sprite lockSprite;
    public Sprite defaultSprite;
    public Text levelText;
    private int level=0;
    private Button button;
    private Image image;

    void OnEnable()
    {
        button=GetComponent<Button>();
        image=GetComponent<Image>();
    }
    public void SetUp(int level, bool isUnlock)
    {
        this.level=level;
        levelText.text=level.ToString();
        if(isUnlock)
        {
            image.sprite=defaultSprite;
            button.enabled=true;
            levelText.gameObject.SetActive(true);
        }
        else
        {
            image.sprite=lockSprite;
            button.enabled=false;
            levelText.gameObject.SetActive(false);
        }
    }
    public void OnClick()
    {
        menu.StartLevel(level);
    }
}
