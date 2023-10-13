using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        if (PlayerPrefs.GetInt("PlayerHp").Equals(0))
        {
            PlayerPrefs.SetInt("PlayerDead", 1); // �÷��̾��� � ��ȥ�� ������
            StartCoroutine(DeadAnimation_Co());
        }
    }

    private IEnumerator DeadAnimation_Co()
    {
        animator.SetTrigger("Dead");
        // ��ƼŬ ���ÿ� ����
        // �÷��̾� ��ġ�� � ��ȥ ����... todo
        yield return new WaitForSeconds(2f);
        PlayerInfo.PlayerMoneyInfo(PlayerPrefs.GetInt("PlayerMoney"), true); // �÷��̾ ���� ��ŭ �� ������
        PlayerInfo.PlayerHpInfo(5, false);
        // �񵿱� �ε��� �ڷ�ƾ �߰�.. todo
        PlayerPrefs.SetInt("PlayerHp", 5);
        // �÷��̾ ������ ������ ��ġ�� �̵�
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
        /*transform.position = new Vector2(PlayerPrefs.GetFloat("BenchPosX"), PlayerPrefs.GetFloat("BenchPosY"));
        animator.SetTrigger("Resurrection");*/
    }
}
