using Level.Entity.Customer.CustomerThoughts;
using UnityEngine;
using UnityEngine.AI;

namespace Level.Entity.Customer
{
    public class CustomerView : MonoBehaviour
    {
        [SerializeField] private Transform _localTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private CustomerThoughtView _customerThought;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public CustomerThoughtView CustomerThought => _customerThought;

        private CustomerAnimationType _currentType;

        internal void Walk(int inventories)
        {
            SetLayerWeight(inventories);
            PlayWalkAnimation();
        }

        private void PlayWalkAnimation()
        {
            PlayAnimation(CustomerAnimationType.Walk, GetRandomTime());
        }

        internal void Idle(int inventories)
        {
            SetLayerWeight(inventories);
            PlayIdleAnimation();
        }

        private void SetLayerWeight(int inventories)
        {
            if (inventories > 0)
                _animator.SetLayerWeight(1, 1f);
            else _animator.SetLayerWeight(1, 0f);
        }

        private void PlayIdleAnimation()
        {
            PlayAnimation(CustomerAnimationType.Idle, GetRandomTime());
        }
        
        float GetRandomTime()
        {
            return Random.Range(0f, 1f);
        }

        private void PlayAnimation(CustomerAnimationType animationType, float timeValue)
        {
            _currentType = animationType;

            var nameHash = Animator.StringToHash(_currentType.ToString());
            _animator.PlayInFixedTime(nameHash, 0, timeValue);

            _animator.Update(0);
        }
    }
}