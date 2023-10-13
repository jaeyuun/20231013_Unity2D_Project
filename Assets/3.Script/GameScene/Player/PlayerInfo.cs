using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("�׽�Ʈ �� üũ")]
    public bool isTest; // ������ ��... todo

    public int playerHp;
    public int playerGauge;
    public int playerMoney;
    public int playerPower;
    public int playerDead;

    public int[] playerSkill; // ��ų ������� �ƴ��� Ȯ���ϴ� ����, 0�̸� ���� �� 1�̸� �ִ� ��
    private int playerSkillCount = 1; // ��ų�� �� ����

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerHp"))
        {
            PlayerPrefs.SetInt("PlayerHp", 5);
        }
        if (!PlayerPrefs.HasKey("PlayerGauge"))
        {
            PlayerPrefs.SetInt("PlayerGauge", 0);
        }
        if (!PlayerPrefs.HasKey("PlayerMoney"))
        {
            PlayerPrefs.SetInt("PlayerMoney", 0);
        }
        if (!PlayerPrefs.HasKey("PlayerPower"))
        {
            PlayerPrefs.SetInt("PlayerPower", 5);
        }
        if (!PlayerPrefs.HasKey("PlayerDead"))
        {
            PlayerPrefs.SetInt("PlayerDead", 0); // false 0, true 1
        }

        // ������ ��... todo
        PlayerPrefs.SetInt("PlayerHp", 5);
        PlayerPrefs.SetInt("PlayerGauge", 0);
        PlayerPrefs.SetInt("PlayerMoney", 0);
        PlayerPrefs.SetInt("PlayerPower", 5);

        playerHp = PlayerPrefs.GetInt("PlayerHp");
        playerGauge = PlayerPrefs.GetInt("PlayerGauge");
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        playerPower = PlayerPrefs.GetInt("PlayerPower");
        playerDead = PlayerPrefs.GetInt("PlayerDead");

        for (int i = 0; i < playerSkillCount; i++)
        {
            playerSkill[i] = new int();
        }
    }

    public static void PlayerHpInfo(int skillPower = 1, bool isMinus = true)
    {
        int playerHp = PlayerPrefs.GetInt("PlayerHp");
        if (isMinus)
        {
            playerHp -= skillPower;
        } else
        {
            playerHp += skillPower;
        }
        PlayerPrefs.SetInt("PlayerHp", playerHp);
    }

    public static void PlayerGaugeInfo(Skill skillName)
    { // Enemy�� hit���� ����
        int playerGauge = PlayerPrefs.GetInt("PlayerGauge");
        switch (skillName)
        {
            case Skill.Slash:
            /*case Skill.SlashAlt:*/
            case Skill.UpSlash:
            case Skill.DownSlash:
                playerGauge++;
                break;
            case Skill.Focus: // ���߿� ��ų�� ���� �󸶳� ������ �����ϴ��� �߰�... todo
                playerGauge--;
                break;
        }

        int maxGauge;
        if (PlayerPrefs.GetInt("PlayerDead").Equals(0))
        { // � ��ȥ�� �������� ���� ��
            maxGauge = 4;
        }
        else { // � ��ȥ�� ������ ��
            maxGauge = 3;
        }
        if (playerGauge > maxGauge)
        {
            PlayerPrefs.SetInt("PlayerGauge", maxGauge);
        }
        else if (playerGauge < 0)
        {
            PlayerPrefs.SetInt("PlayerGauge", 0);
        }
        else
        {
            PlayerPrefs.SetInt("PlayerGauge", playerGauge);
        }
    }

    public static void PlayerMoneyInfo(int geo, bool isMinus = true)
    {
        int playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        if (isMinus)
        {
            playerMoney -= geo;
        } else
        {
            playerMoney += geo;
        }
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
    }
}
