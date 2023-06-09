using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager> {
    public class DragData {
        public SlotUI originalSlotUI;
        public RectTransform originalParent;
    }

    // TODO: 将来改成template data来实现保存和加载
    [Header("InventoryData")]
    public InventoryData_SO bagDataTemplate;
    public InventoryData_SO bagData;
    public InventoryData_SO actionDataTemplate;
    public InventoryData_SO actionData;
    public InventoryData_SO equipmentDataTemplate;
    public InventoryData_SO equipmentData;

    [Header("Container")]
    public ContainerUI bagUI;
    public ContainerUI actionUI;
    public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;

    [Header("UI Panel")]
    public GameObject bagPanel;
    public GameObject statesPanel;
    private bool isOpen = false;

    [Header("States Text")]
    public Text healthText;
    public Text attackText;

    [Header("Tool Tip")]
    public ItemToolTip toolTip;


    #region 生命周期函数
    protected override void Awake() {
        base.Awake();
        if (bagDataTemplate != null) {
            bagData = Instantiate(bagDataTemplate);
        }
        if (actionDataTemplate != null) {
            actionData = Instantiate(actionDataTemplate);
        }
        if (equipmentDataTemplate != null) {
            equipmentData = Instantiate(equipmentDataTemplate);
        }
    }

    private void Start() {
        bagUI.RefreshUI();
        actionUI.RefreshUI();
        equipmentUI.RefreshUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            isOpen = !isOpen;
            bagPanel.SetActive(isOpen);
            statesPanel.SetActive(isOpen);
        }
        UpdateStatesText(GameManager.Instance.playerStats.MaxHealth, GameManager.Instance.playerStats.attackData.minDamage);
    }
#endregion

#region 检查拖拽物品是否在slot范围内
    public bool CheckInventoryUI(Vector3 position) {
        for (int i = 0; i < bagUI.slotHolders.Count; ++i) {
            RectTransform t = bagUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position)) {
                return true;
            }
        }
        return false;
    }

    public bool CheckActionUI(Vector3 position) {
        for (int i = 0; i < actionUI.slotHolders.Count; ++i) {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position)) {
                return true;
            }
        }
        return false;
    }

    public bool CheckEquipmentUI(Vector3 position) {
        for (int i = 0; i < equipmentUI.slotHolders.Count; ++i) {
            RectTransform t = equipmentUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position)) {
                return true;
            }
        }
        return false;
    }
#endregion

    public void UpdateStatesText(int health, int attack) {
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
    }
}
