using System;
using UnityEngine;
public class GameManager : MonoBehaviour
{
 private void Start()
 {
  Cursor.lockState = CursorLockMode.Confined;
  Cursor.visible = false;
 }
}
