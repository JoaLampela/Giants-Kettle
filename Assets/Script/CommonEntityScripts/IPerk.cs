using System;
public interface IPerk
{
    string StringProperty { get; }

    void Method();
}

[Serializable]
public class IPerkContainer : IUnifiedContainer<IPerk> { }
