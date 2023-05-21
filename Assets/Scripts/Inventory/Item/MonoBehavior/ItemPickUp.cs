using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour {
    public ItemData_SO itemData;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // TODO: 将物品添加到背包
            InventoryManager.Instance.bagData.AddItem(itemData);
            InventoryManager.Instance.inventoryContainer.RefreshUI();
            // TODO: 装备武器并删除
            // GameManager.Instance.playerStats.EquipWeapon(itemData);
            Destroy(gameObject);
        }
    }
}