using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPCanvas : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private GameObject hpObject;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image hpFill;
    [SerializeField] private Enemy enemy;

    // Update is called once per frame
    void Update()
    {
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        if (enemy.hp < enemy.baseHp)
        {
            hpObject.SetActive(true);
            hpFill.fillAmount = (float)enemy.hp / (float)enemy.baseHp;
        }
        if (enemy.hp == 0)
        {
            Destroy(gameObject);
        }
    }
}
