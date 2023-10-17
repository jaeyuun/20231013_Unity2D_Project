using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    private GameLoad gameLoad;
    private GameData gameData;

    public int playerHp;
    public int playerGauge;
    public int playerMoney;
    public int playerPower;
    public bool playerDead;
    
    public float playerX;
    public float playerY;
    public string sceneName;

    public Vector2 playerTransform;

    public int[] playerSkill; // ��ų ������� �ƴ��� Ȯ���ϴ� ����, 0�̸� ���� �� 1�̸� �ִ� ��
    private int playerSkillCount = 1; // ��ų�� �� ����

    private void Awake()
    {
        gameLoad = GameObject.FindGameObjectWithTag("SceneLoader").transform.GetComponent<GameLoad>();
        gameData = gameLoad.Load("save");

        playerX = gameData.playerX;
        playerY = gameData.playerY;
        sceneName = gameData.sceneName;

        playerHp = gameData.playerHp;
        playerGauge = gameData.playerGauge;
        playerMoney = gameData.playerMoney;
        playerPower = gameData.playerPower;
        playerDead = gameData.playerDead;

        for (int i = 0; i < playerSkillCount; i++)
        {
            playerSkill[i] = new int();
        }

        playerTransform = new Vector2(playerX, playerY);
        transform.position = playerTransform;
    }

    public GameData GetGameData(bool isBench = true)
    {
        gameData.playerHp = playerHp;
        gameData.playerGauge = playerGauge;
        gameData.playerMoney = playerMoney;
        gameData.playerPower = playerPower;
        gameData.playerDead = playerDead;

        if (isBench)
        {
            gameData.playerX = playerX;
            gameData.playerY = playerY;
        }
        gameData.sceneName = sceneName;

        return gameData;
    }

    public void PlayerHpInfo(int skillPower = 1, bool isMinus = true)
    {
        if (isMinus)
        {
            playerHp -= skillPower;
        } else
        {
            playerHp += skillPower;
        }
    }

    public void PlayerGaugeInfo(Skill skillName)
    { // Enemy�� hit���� ����
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
        if (playerDead)
        { // � ��ȥ�� �������� ���� ��
            maxGauge = 4;
        }
        else { // � ��ȥ�� ������ ��
            maxGauge = 3;
        }

        if (playerGauge > maxGauge)
        {
            playerGauge = maxGauge;
        }
        else if (playerGauge < 0)
        {
            playerGauge = 0;
        }
    }

    public void PlayerMoneyInfo(int geo, bool isMinus = true)
    {
        if (isMinus)
        {
            playerMoney -= geo;
        } else
        {
            playerMoney += geo;
        }
    }
}
