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
    private int playerDead;

    private void Start()
    {
        playerHp = PlayerPrefs.GetInt("PlayerHp");
        playerMoney = PlayerPrefs.GetInt("PlayerMoney");
        playerGauge = PlayerPrefs.GetInt("PlayerGauge");
        playerDead = PlayerPrefs.GetInt("PlayerDead");
    }

    private void Update()
    {
        // �÷��̾ ����� ���� �ٲ���� ��
        if (playerHp != PlayerPrefs.GetInt("PlayerHp") && PlayerPrefs.GetInt("PlayerHp") >= 0)
        {
            UIHpSet();
        }
        if (playerGauge != PlayerPrefs.GetInt("PlayerGauge"))
        {
            UIGaugeSoulSet();
        }
        if (playerMoney != PlayerPrefs.GetInt("PlayerMoney"))
        {
            UIGeoSet();
        }
        if (playerDead != PlayerPrefs.GetInt("PlayerDead"))
        {
            UIGaugeSoulOnOff();
        }
    }

    private void UIHpSet()
    {
        bool isHurt = ((playerHp - PlayerPrefs.GetInt("PlayerHp")) >= 0) ? true : false; // 0���� ������ ȸ��, �ƴ϶�� ��ħ
        string animeName = string.Empty;
        playerHp = PlayerPrefs.GetInt("PlayerHp");
        if (isHurt)
        {
            animeName = "Hurt";
            HpAnimation(playerHp, animeName);
        } else
        {
            animeName = "Recovery";
            HpAnimation(playerHp - 1, animeName);
        }
    }

    private void UIGeoSet()
    {
        int geo = PlayerPrefs.GetInt("PlayerMoney");
        geoText.text = (geo < 1000) ? $"{geo}" : $"{geo % 1000},{geo / 1000}";
    }

    private void UIGaugeSoulOnOff()
    {
        if (PlayerPrefs.GetInt("PlayerDead").Equals(0))
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
        int gauge = PlayerPrefs.GetInt("PlayerGauge");
        bool isUsed = ((playerGauge - PlayerPrefs.GetInt("PlayerGauge")) >= 0) ? true : false; // 0���� ������ ����, �ƴ϶�� ���
        // �װ��� ��ȥ�� ������� �� gaugeSoul[0]�� �ִϸ��̼� �߰����ֱ�... todo
        // soul gauge
        playerGauge = PlayerPrefs.GetInt("PlayerGauge");

        if (playerGauge <= 0)
        {
            gaugeSoul[1].SetActive(false);
        } else
        {
            gaugeSoul[1].SetActive(true);
        }

        // eyesImage setActive
        if (gauge < 2)
        {
            eyeImage.SetActive(false);
        } else
        {
            eyeImage.SetActive(true);
        }

        // full soul
        if (gauge == 4)
        {
            gaugeSoul[2].SetActive(true);
        } else
        {
            gaugeSoul[2].SetActive(false);
        }

        string animeName = string.Empty;
        int slashAttack = PlayerPrefs.GetInt("SlashAttack"); // UIController���� ����� ������, ����ϰ� ������ 0���� �ٲ��ֱ�
        if (slashAttack == 1)
        { // �����÷� �������� ��
            PlayerPrefs.SetInt("SlashAttack", 0);
             if (gauge != 1)
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

    // �񵿱� �ε��� �߰�... todo, Load�� Save�� Ȱ��
}
