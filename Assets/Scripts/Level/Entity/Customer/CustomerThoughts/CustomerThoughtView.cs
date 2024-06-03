using TMPro;
using UnityEngine;

namespace Level.Entity.Customer.CustomerThoughts
{
    public class CustomerThoughtView : MonoBehaviour
    {
        
        [SerializeField] private GameObject[] _thoughts;
        [SerializeField] private SpriteRenderer _needImage;
        [SerializeField] private TMP_Text _needAmountText;
        
        public GameObject[] Thoughts => _thoughts;
        public SpriteRenderer NeedImage => _needImage;
        public TMP_Text NeedAmountText => _needAmountText;
    }
}