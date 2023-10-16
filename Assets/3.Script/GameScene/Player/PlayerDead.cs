using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private SceneLoader sceneLoader;
    private Animator animator;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        playerInfo = transform.GetComponent<PlayerInfo>();
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").transform.GetComponent<SceneLoader>();
    }

    private void Update()
    {
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        if (playerInfo.playerHp == 0)
        {
            playerInfo.playerDead = true; // �÷��̾��� � ��ȥ�� ������
            StartCoroutine(DeadAnimation_Co());
            playerInfo.getGameData(false);
        }
    }

    private IEnumerator DeadAnimation_Co()
    {

        animator.SetTrigger("Dead");
        PlayerPrefs.SetInt("DeadTime", 1);
        // ��ƼŬ ���ÿ� ����
        // �÷��̾� ��ġ�� � ��ȥ ����... todo
        yield return new WaitForSeconds(1f);
        sceneLoader.DeadLoadingTrue();
        playerInfo.PlayerMoneyInfo(playerInfo.playerMoney, true); // �÷��̾ ���� ��ŭ �� ������
        yield return new WaitForSeconds(2f);
        // �񵿱� �ε��� �ڷ�ƾ �߰�.. todo
        if (PlayerPrefs.GetInt("DeadTime").Equals(1))
        {
            playerInfo.PlayerHpInfo(5, false);
            PlayerPrefs.SetInt("DeadTime", 0);
        }
        sceneLoader.DeadLodingFalse();
        // �÷��̾ ������ ������ ��ġ�� �̵�
    }
}
