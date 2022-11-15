using TreeSyncer.Entities;

namespace TreeSyncer.Models;

public abstract class TopicNode : ITopicNode
{
    public int TopicId => CoreContentTopic.Id;
    
    public CoreContentTopic CoreContentTopic { get; set; }
    public TopicNode[] ChildTopicNodes { get; set; }

    public IDictionary<string, CoreContentTopic> ToDictionary()
    {
        throw new NotImplementedException();
    }
}