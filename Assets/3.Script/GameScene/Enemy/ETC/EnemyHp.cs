using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int enemyHp;

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
                    enemyHp -= PlayerPrefs.GetInt("PlayerPower");
                    PlayerPrefs.SetInt("SlashAttack", 1); // UIController���� ����� ������
                    PlayerInfo.PlayerGaugeInfo(Skill.Slash);
                    break;
            }
        }
    }
}
