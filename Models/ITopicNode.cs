using TreeSyncer.Entities;

namespace TreeSyncer.Models;

public interface ITopicNode
{
    int TopicId { get; }
    CoreContentTopic CoreContentTopic { get; set; }
    TopicNode[] ChildTopicNodes { get; set; }
}