using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    private PlayerInfo playerInfo;
    public int enemyHp;

    private void Awake()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerInfo>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Slash"))
        {
            switch (collision.gameObject.name)
            { // �ٸ� ��ų�� ������ �� �� ���� ���̴� �ɷ� �߰�... todo
                case "Slash":
                case "SlashAlt":
                case "UpSlash":
                case "DownSlash":
                    enemyHp -= playerInfo.playerPower;
                    playerInfo.PlayerGaugeInfo(Skill.Slash);
                    PlayerPrefs.SetInt("SlashAttack", 1); // UIController���� ����� ������
                    break;
            }
        }
    }
}
