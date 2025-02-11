using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }

    public static void ApplySlowTime(float time = 0.08f)
    {
        Time.timeScale = 0f;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, time).SetUpdate(true);
    }

}
