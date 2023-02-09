using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour
{
    public static ScreenEffect Instance;
    Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitEffect(Color targetColor)
    {
        Color color = targetColor;
        image.color = color;
        color.a = 0.25f;
        image.DOColor(color,0.2f).OnComplete( () => {
            color.a = 0;
            image.DOColor(color,0.2f);
        });
    }

    public void LoseOP()
    {
        Color black = Color.black;
        black.a = 0;
        image.color = black;
        black.a = 1;
        image.DOColor(black, 1f).SetDelay(2f).OnComplete(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
