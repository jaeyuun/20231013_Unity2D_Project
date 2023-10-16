using System.IO; // �����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json; // �������� ��

public class GameSave : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerInfo = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerInfo>();
        }
    }

    public void Save(bool exFile = true)
    {
        GameData data;
        if (exFile)
        {
            data = playerInfo.getGameData();
        } else
        {
            data = new GameData();
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
