using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   [SerializeField]
   LazerScript _lazer;
   [SerializeField] 
   private Transform _shootPoint;
   [SerializeField] 
   private float _shootCoolDown = 0.30f;
   private float _duration;
   float _coolDown;
   private bool ShootReady
   {
       get
       {
           _duration -= Time.deltaTime;
           return _duration <= 0f;
       }
   }
    void Update()
    {
        if (ShootReady && Input.GetMouseButtonDown(0))
        {
            FireLazer();
        }
    }
    void FireLazer()
    {
        _duration += Time.deltaTime;
        Instantiate(_lazer,_shootPoint.position, transform.rotation);
    }
}
