using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
 enum VirtualCameraType
 {
  NoCamera = -1,
  FirstPersonCamera = 0,
  ThirdPersonCamera,
 }
 [SerializeField]
 private List<GameObject> _vCameras;
 private int _currentCameraIndex = 0; 
 void Start()
 {
  SetActiveCamera(VirtualCameraType.FirstPersonCamera);
 }
 void Update()
 {
  if (Input.GetKeyDown(KeyCode.C))
  {
   CycleCamera();
  }
 }
 private void CycleCamera()
 {
  _currentCameraIndex = (_currentCameraIndex + 1) % _vCameras.Count;
  SetActiveCamera((VirtualCameraType)_currentCameraIndex);
 }
 private void SetActiveCamera(VirtualCameraType activeCamera)
 {
  if (activeCamera == VirtualCameraType.NoCamera)return;
  for (int i = 0; i < _vCameras.Count; i++)
  {
   _vCameras[i].SetActive(i == (int)activeCamera);
  }
 }
}
