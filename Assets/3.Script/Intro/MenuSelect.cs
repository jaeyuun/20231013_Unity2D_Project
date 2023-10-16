using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class MenuSelect : MonoBehaviour
{
    [SerializeField] private GameObject saveLoad;

    // ���콺�� Ű���带 �� �޴� ����
    public void SaveFileLoad()
    {
        if (!File.Exists(Application.streamingAssetsPath))
        {
            saveLoad.SetActive(true);
        }
    }
}
