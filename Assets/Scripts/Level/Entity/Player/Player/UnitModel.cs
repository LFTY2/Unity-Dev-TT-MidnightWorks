using Core.Observer;

namespace Level.Entity.Player.Player
{
    public sealed class UnitModel : Observable
    {
        public float WalkSpeed;
        public float RotateSpeed;

        public UnitModel(float walkSpeed, float rotateSpeed)
        {
            WalkSpeed = walkSpeed;
            RotateSpeed = rotateSpeed;
        }
    }
}