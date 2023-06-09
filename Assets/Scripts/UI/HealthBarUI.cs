using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    public GameObject healthUIPrefab;
    public Transform barPoint;
    public bool alwaysVisible;
    public float visibleTime;
    private float timeLeft;

    private Image healthSlider;
    private Transform UIbar;
    private Transform _camera;
    private CharacterStats currentStats;

#region 生命周期函数
    void Awake() {
        currentStats = GetComponent<CharacterStats>();
        currentStats.UpdateHealthBarOnAttack += UpdateHealthBar;
    }

    void OnEnable() {
        _camera = Camera.main.transform;
        foreach(Canvas canvas in FindObjectsOfType<Canvas>()) {
            if (canvas.name == "HealthBar Canvas") {
                UIbar = Instantiate(healthUIPrefab, canvas.transform).transform;
                // 取得了BarHolder下的绿色血条的子Image
                healthSlider = UIbar.GetChild(0).GetComponent<Image>();
                UIbar.gameObject.SetActive(alwaysVisible);
                break;
            }
        }
    }

    void LateUpdate() {
        if (UIbar != null) {
            UIbar.position = barPoint.position;
            UIbar.forward = -_camera.forward;

            if (timeLeft <= 0 && !alwaysVisible) {
                UIbar.gameObject.SetActive(false);
            }
            else {
                timeLeft -= Time.deltaTime;
            }
        }
    }

#endregion

    void UpdateHealthBar(int currentHealth, int maxHealth) {
        if (currentHealth <= 0) {
            Destroy(UIbar.gameObject);
        }

        UIbar.gameObject.SetActive(true);
        timeLeft = visibleTime;

        float sliderPercent = (float)currentHealth / maxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

}
