using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Button))]

public class LevelLoader : MonoBehaviour
{
    private Button button;

    public string LevelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }
    private void onClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);

        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("level is locked");
                break;
            case LevelStatus.Unlocked:
                SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
                SceneManager.LoadScene(LevelName);
                break;
            case LevelStatus.Completed:
                SoundManager.Instance.Play(SoundManager.Sounds.ButtonClick);
                SceneManager.LoadScene(LevelName);
                break;
        }
    }
}
