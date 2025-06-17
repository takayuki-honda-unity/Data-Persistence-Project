using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField PlayerNameInput;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI playerNameText;

    // Start is called before the first frame update
    void Start()
    {
        // DataManagerから値を取得してTextMeshProに表示
        if (DataManager.Instance != null)
        {
            // スコアがnullまたは空の場合は"0"を設定
            if (string.IsNullOrEmpty(DataManager.Instance.score))
            {
                DataManager.Instance.score = "0";
            }
            // プレイヤーネームがnullまたは空の場合は"名無し"を設定
            if (string.IsNullOrEmpty(DataManager.Instance.playerName))
            {
                DataManager.Instance.playerName = "名無し";
            }

            scoreText.text = $"Current High Score: {DataManager.Instance.score}";
            playerNameText.text = $"Name: {DataManager.Instance.playerName?.Replace("\n", "").Replace("\r", "").Trim()}";
        }

        // InputFieldのonEndEditイベントにリスナーを追加
        if (PlayerNameInput != null)
        {
            PlayerNameInput.onEndEdit.AddListener(OnPlayerNameInputEndEdit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveInputPlayerName()
    {
        if (PlayerNameInput != null)
        {
            DataManager.Instance.playerName = PlayerNameInput.text; // テキストを保存
            Debug.Log("保存されたテキスト: " + DataManager.Instance.playerName);
        }
    }
    private void OnPlayerNameInputEndEdit(string input)
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.playerName = input;
            Debug.Log("保存されたテキスト: " + DataManager.Instance.playerName);
        }
    }
}
