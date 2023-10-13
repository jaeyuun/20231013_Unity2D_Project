using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BenchSave : MonoBehaviour
{
    private Transform benchTransform; // ��ġ ��ġ
    public GameObject benchPoint;

    private void Awake()
    {
        benchTransform = gameObject.transform;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("BenchPosX", benchTransform.position.x);
        PlayerPrefs.SetFloat("BenchPosY", benchTransform.position.y);
        PlayerPrefs.SetString("SceneName", $"{SceneManager.GetActiveScene().name}"); // ���� �� ���� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            benchPoint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            benchPoint.SetActive(false);
        }
    }
}
