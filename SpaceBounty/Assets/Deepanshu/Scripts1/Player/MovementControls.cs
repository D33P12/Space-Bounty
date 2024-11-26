using UnityEngine;
public class MovementControls : MovementBaseControl
{
    public float _deadZoneRadius = 0.1f;
    Vector2 ScreenMid => new Vector2(Screen.width*0.5f, Screen.height*0.5f);
    public override float YawAmount
    {
        get
        {
           Vector3 mousePos = Input.mousePosition; 
           float yaw = (mousePos.x - ScreenMid.x)/ScreenMid.x;
           if (Mathf.Abs(yaw) > _deadZoneRadius)
           {
               return yaw;
           }
           else
           {
               return 0f;
           }
        }
    }
    public override float PitchAmount
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            float pitch = (mousePos.y - ScreenMid.y)/ScreenMid.y;
            if (Mathf.Abs(pitch) > _deadZoneRadius)
            {
                return pitch;
            }
            else
            {
                return 0f;
            }
        }
    }
    public override float RollAmount
    {
        get
        {
            if (Input.GetKey(KeyCode.A))
            {
                return 1f;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
    public override float ThrustAmount =>Input.GetAxis("Vertical");
}
