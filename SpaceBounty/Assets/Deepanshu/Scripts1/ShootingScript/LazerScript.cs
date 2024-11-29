using System;
using UnityEngine;
public class LazerScript : MonoBehaviour 
{
   [SerializeField] [Range(1000f, 20000f)]
   float speed = 10000f;
   [SerializeField]
   int _damage = 10;
   [SerializeField]
   float _range = 3f;
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
   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent<InteractionFoundation>(out var interactableFoundation))
      {
         if (GameManager.Instance != null)
         {
            interactableFoundation.Interact(GameManager.Instance);
         }
      }
      
      if (other.GetComponent<Scrap>() != null)
      {
         Destroy(gameObject); 
         Destroy(other.gameObject);
         AudioManagerScript.Instance.PlayAudio(AudioManagerScript.AudioType.D1, 0.5f);
      }
   }
}
