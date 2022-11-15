namespace TreeSyncer.Interfaces;

public interface ICoreTocLoader<T> where T : TopicNode
{
    T[] Load();
}