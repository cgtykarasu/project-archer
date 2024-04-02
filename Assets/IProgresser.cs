using System;

namespace DefaultNamespace
{
    public interface IProgresser
    {
        // IObservable<float> Progress { get; }
        float Progress { get; }
    }
}