using System.IO; // �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json; // �������� ��

public class GameSave : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerInfo = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerInfo>();
        }
    }

    public void Save(bool exFile = true)
    {
        GameData data = new GameData();
        if (exFile)
        {
            data = playerInfo.GetGameData(); 
        }

        string fileName = "save";

        if (!fileName.Contains(".json"))
        { // ���� �̸��� .json ������ ���Ե��� �ʾҴٸ�
            fileName += ".json";
        }
        fileName = Path.Combine(Application.streamingAssetsPath, fileName); // ��� ����
        string toJson = JsonConvert.SerializeObject(data, Formatting.Indented); // json�� dictionary�� ����
        File.WriteAllText(fileName, toJson);
    }
}
