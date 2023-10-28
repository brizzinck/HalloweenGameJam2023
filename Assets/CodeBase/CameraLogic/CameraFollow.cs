using UnityEngine;
using Cinemachine;
namespace CodeBase.CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float _distance;
    [SerializeField] private float _offsetY;
    
    private Transform _following;
        /*private void LateUpdate()
        {
          if (_following == null)
            return;
          Vector3 position = new Vector3(0, 0, -_distance) + FollowingPointPosition();
          transform.position = position;
        }*/

    public void Follow(GameObject following)
    {
            cinemachineVirtualCamera.Follow = following.transform;
    }
    private Vector3 FollowingPointPosition()
    {
      Vector3 followingPosition = _following.position;
      followingPosition.y += _offsetY;

      return followingPosition;
    }
  }
}