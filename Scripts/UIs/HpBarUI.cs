using System;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private Health health;
    public Camera MainCamera;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }
    private void OnEnable()
    {
        fillImage.fillAmount = 1;
        health.OnChangeHealth += UpdateHpBar;
    }

    void Update()
    {
        RotateHpBarToCam();
    }

    private void OnDisable()
    {
        health.OnChangeHealth -= UpdateHpBar;
    }

    void UpdateHpBar()
    {
        fillImage.fillAmount = health.GetHealthPercentage();
    }

    void RotateHpBarToCam()
    {
        transform.LookAt(transform.position - MainCamera.transform.forward,  MainCamera.transform.up);
    }
}
