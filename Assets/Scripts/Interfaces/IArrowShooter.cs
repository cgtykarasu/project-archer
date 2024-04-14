using UnityEngine;

namespace Interfaces
{
    public interface IArrowShooter
    {
        bool Charging { get; }
        bool Shoot { get; }
        
    }

    public class KeyboardArrowShooter : IArrowShooter
    {
        public bool Charging => Input.GetKey(KeyCode.Space);
        public bool Shoot => Input.GetKeyUp(KeyCode.Space);
    }
}