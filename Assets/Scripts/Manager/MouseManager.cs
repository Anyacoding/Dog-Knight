using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class MouseManager : Singleton<MouseManager> {
    private RaycastHit hitInfo;
    public event Action<Vector3> OnMouseClicked;
    public event Action<GameObject> OnEnemyClicked;
    public Texture2D point, doorWay, attack, target, arrow;

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update() {
        // 如果是跟UI交互，设置鼠标图标然后那么直接返回
        if (InteractWithUI()) {
            Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
            return;
        }
        SetCursorTexture();
        MouseControl();
    }

    // 每一帧都发射射线来更新hitInfo
    void SetCursorTexture() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 切换鼠标贴图
        if (Physics.Raycast(ray, out hitInfo)) {
            switch (hitInfo.collider.gameObject.tag) {
                case "Ground": {
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                }  
                case "Enemy": {
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                } 
                case "Attackable": {
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                }
                case "Portal": {
                    Cursor.SetCursor(doorWay, new Vector2(16, 16), CursorMode.Auto);
                    break;
                }
                case "Item": {
                    Cursor.SetCursor(point, new Vector2(16, 16), CursorMode.Auto);
                    break;
                }
                default: {
                    Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    break;
                }
            }
        }
    }

    void MouseControl() {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null) {
            if (hitInfo.collider.gameObject.CompareTag("Ground")) {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Portal")) {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Enemy")) {
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
            if (hitInfo.collider.gameObject.CompareTag("Attackable")) {
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
            if (hitInfo.collider.gameObject.CompareTag("Item")) {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }


    bool InteractWithUI() {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
