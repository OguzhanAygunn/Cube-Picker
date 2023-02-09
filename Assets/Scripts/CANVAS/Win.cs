using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public static Win Instance;
    public GameObject other;
    Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
    }
    void Start()
    {
        
    }
    public void ActiveOP()
    {
        Color color = image.color;
        color.a = 0.90f;
        image.DOColor(color,1f).SetDelay(1.5f).OnComplete( () => {
            other.transform.DOScale(Vector3.one,1f).SetEase(Ease.OutElastic);
        });
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
