using TreeSyncer.Models;

namespace TreeSyncer.TocLoaders;

public interface ICoreTocLoader<T> where T : TopicNode
{
    T Load();
}