using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBound : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private GameObject player; // �ٿ�� ��ų ������Ʈ
    [SerializeField] private float boundForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ExcecuteBounding(collision);
        }
        if (!isPlayer)
        {
            if (collision.gameObject.CompareTag("Land"))
            {
                ExcecuteBounding(collision);
            }
        }
    }

    private void ExcecuteBounding(Collision2D collision)
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        ContactPoint2D cp = collision.GetContact(0); // �浹 �� �浹�Ǵ� ������ ���� ��ȯ, trigger������ ����� �� ����
        Vector2 dir = playerPosition - cp.point; // ���� �������� �÷��̾��� ����
        player.transform.GetComponent<Rigidbody2D>().AddForce((dir).normalized * boundForce);
    }
}
