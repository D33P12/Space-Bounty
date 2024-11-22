using UnityEngine;
public class LazerScript : MonoBehaviour
{
   [SerializeField] [Range(1000f, 20000f)]
   float speed = 10000f;
   [SerializeField]
   int _damage = 10;
   [SerializeField]
   float _range = 5f;
   private Rigidbody _rigidbody;
   private float _duration;
   bool OutOfPower
   {
      get
      {
         _duration -= Time.deltaTime;
         return _duration <= 0f;
      }
   }
   private void Awake()
   {
      _rigidbody = GetComponent<Rigidbody>();
   }
   private void OnEnable()
   {
      _rigidbody.AddForce(speed * transform.right);
      _duration = _range;
   }
   void Update()
   {
      if (OutOfPower) Destroy(gameObject);
   }
   void onCollisionEnter(Collision collision)
   {
     
   }
}
