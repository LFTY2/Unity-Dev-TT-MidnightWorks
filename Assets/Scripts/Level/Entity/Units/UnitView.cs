using UnityEngine;
using UnityEngine.AI;

namespace Level.Entity.Units
{
    public enum AnimationType
    {
        Walk,
        Idle,
        CashRegister,
    }
    
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Transform _localTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        private AnimationType _currentType;
        internal void Walk(int inventories)
        {
            SetLayerWeight(inventories);
            PlayWalkAnimation();
        }

        private void PlayWalkAnimation()
        {
            PlayAnimation(AnimationType.Walk, GetRandomTime());
        }

        internal void Idle(int inventories)
        {
            SetLayerWeight(inventories);
            PlayIdleAnimation();
        }

        public void SetLayerWeight(int inventories)
        {
            if (inventories > 0)
                _animator.SetLayerWeight(1, 1f);
            else _animator.SetLayerWeight(1, 0f);
        }

        private void PlayIdleAnimation()
        {
            PlayAnimation(AnimationType.Idle, GetRandomTime());
        }

   

        internal void CashRegister()
        {
            PlayAnimation(AnimationType.CashRegister, GetRandomTime());
        }
        
        
        float GetRandomTime()
        {
            return Random.Range(0f, 1f);
        }
        

        private void PlayAnimation(AnimationType animationType, float timeValue)
        {
            _currentType = animationType;

            var nameHash = Animator.StringToHash(_currentType.ToString());
            _animator.PlayInFixedTime(nameHash, 0, timeValue);

            _animator.Update(0);
        }
        
    }
}