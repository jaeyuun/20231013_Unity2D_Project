using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // hp, geo, gauge UI�� ��Ÿ���ִ� ��ũ��Ʈ
    [SerializeField] private GameObject[] hp;
    [SerializeField] private GameObject[] gaugeSoul;
    [SerializeField] private GameObject eyeImage;
    [SerializeField] private Text geoText;

    private Animator hpAnimator;
    private Animator gaugeAnimator;

    private int playerHp;
    private int playerGauge;
    private int playerMoney;
    private bool playerDead;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerInfo>();
        playerHp = playerInfo.playerHp;
        playerMoney = playerInfo.playerMoney;
        playerGauge = playerInfo.playerGauge;
        playerDead = playerInfo.playerDead;
    }

    private void Start()
    {
        SetStartUI();
    }

    private void Update()
    {
        // �÷��̾ ����� ���� �ٲ���� ��
        UIHpSet();
        UIGaugeSoulSet();
        UIGeoSet();
        UIGaugeSoulOnOff();
    }

    private void SetStartUI()
    {
        // Hp
        for (int i = playerHp; i < hp.Length; i++)
        {
            hpAnimator = hp[i].transform.GetComponent<Animator>();
            hpAnimator.Play("Hp_no");
        }
        // Geo
        geoText.text = playerMoney.ToString();
        // Gauge
        gaugeAnimator = gaugeSoul[1].transform.GetComponent<Animator>();
        if (playerGauge <= 0)
        {
            gaugeSoul[1].SetActive(false);
        }
        else
        {
            gaugeSoul[1].SetActive(true);
        }

        // eyesImage setActive
        if (playerGauge < 2)
        {
            eyeImage.SetActive(false);
        }
        else
        {
            eyeImage.SetActive(true);
        }

        // full soul
        if (playerGauge == 4)
        {
            gaugeSoul[2].SetActive(true);
        }
        else
        {
            gaugeSoul[2].SetActive(false);
        }

        switch (playerGauge)
        {
            case 1:
                gaugeAnimator.Play("GaugeSmall");
                break;
            case 2:
                gaugeAnimator.Play("GaugeMiddle");
                break;
            case 3:
            case 4:
                gaugeAnimator.Play("GaugeMiddle");
                break;
        }
    }

    private void UIHpSet()
    {
        if (playerHp != playerInfo.playerHp && playerInfo.playerHp >= 0)
        {
            bool isHurt = (playerHp - playerInfo.playerHp) >= 0; // 0���� ������ ȸ��, �ƴ϶�� ��ħ
            string animeName = string.Empty;
            playerHp = playerInfo.playerHp;
            if (isHurt)
            {
                animeName = "Hurt";
                HpAnimation(playerHp, animeName);
            }
            else
            {
                animeName = "Recovery";
                HpAnimation(playerHp - 1, animeName);
            }
        }
    }

    private void UIGeoSet()
    {
        if (playerMoney != playerInfo.playerMoney)
        {
            int geo = playerInfo.playerMoney;
            geoText.text = geo < 1000 ? $"{geo}" : $"{geo % 1000},{geo / 1000}";
        }
    }

    private void UIGaugeSoulOnOff()
    {
        if (!playerDead)
        { // ���ȥ�� �������� ���� ��
            GaugeAnimation(0, "SoulOn");
        }
        else
        { // ���ȥ�� ������ ��
            GaugeAnimation(0, "SoulOff");
        }
    }

    private void UIGaugeSoulSet()
    {
        if (playerGauge != playerInfo.playerGauge)
        {
            Debug.Log(playerGauge);
            Debug.Log(playerInfo.playerGauge);
            bool isUsed = (playerGauge - playerInfo.playerGauge) >= 0; // 0���� ������ ����, �ƴ϶�� ���
            // �װ��� ��ȥ�� ������� �� gaugeSoul[0]�� �ִϸ��̼� �߰����ֱ�... todo
            playerGauge = playerInfo.playerGauge;

            if (playerGauge <= 0)
            {
                gaugeSoul[1].SetActive(false);
            }
            else
            {
                gaugeSoul[1].SetActive(true);
            }

            // eyesImage setActive
            if (playerGauge < 2)
            {
                eyeImage.SetActive(false);
            }
            else
            {
                eyeImage.SetActive(true);
            }

            // full soul
            if (playerGauge == 4)
            {
                gaugeSoul[2].SetActive(true);
            }
            else
            {
                gaugeSoul[2].SetActive(false);
            }

            string animeName = string.Empty;
            int slashAttack = PlayerPrefs.GetInt("SlashAttack"); // UIController���� ����� ������, ����ϰ� ������ 0���� �ٲ��ֱ�
            if (slashAttack == 1)
            { // �����÷� �������� ��
                PlayerPrefs.SetInt("SlashAttack", 0);
                if (playerGauge != 1)
                {
                    GaugeAnimation(1, "Attack");
                }
            }
            // ��Ŀ�� ��ų�� ������� ��
            if (isUsed)
            {
                animeName = "UsedSoul";
                GaugeAnimation(1, animeName);
            }
            else
            {
                animeName = "NextSoul";
                GaugeAnimation(1, animeName);
            }
        }
    }

    private void HpAnimation(int i, string animeName)
    {
        hpAnimator = hp[i].transform.GetComponent<Animator>();
        hpAnimator.SetBool($"{animeName}", true);

        if (hpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hp_No"))
        {
            hpAnimator.SetBool("Hurt", false);
        }
        else if (hpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hp"))
        {
            hpAnimator.SetBool("Recovery", false);
        }
    }

    private void GaugeAnimation(int i, string animeName)
    {
        // ������ ����ϰų� ������ ��
        gaugeAnimator = gaugeSoul[i].transform.GetComponent<Animator>();
        gaugeAnimator.SetTrigger($"{animeName}");
    }
}
