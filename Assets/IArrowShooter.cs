using UnityEngine;

public interface IArrowShooter
{
    bool Charging { get; }
    bool Shoot { get; }
        
}

public class KeyboardArrowShooter : IArrowShooter
{
    public bool Charging => Input.GetKey(KeyCode.Space);
    public bool Shoot => Input.GetKeyUp(KeyCode.Space);
        
    //
    //
    // AsyncReactiveProperty<float> tension = new AsyncReactiveProperty<float>(0);
    //
    //
    // public KeyboardArrowShooter(float tensionIncreasePerSecond)
    // {
    //     UniTaskAsyncEnumerable.EveryUpdate()
    //         .Where(_ => Input.GetKey(KeyCode.Space))
    //         .Subscribe(_ => { tension.Value = tensionIncreasePerSecond * Time.deltaTime; });
    // }
}