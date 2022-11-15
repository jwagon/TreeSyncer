using TreeSyncer.Models;

namespace TreeSyncer;

public interface ITopicNode
{
    int TopicId { get; }
    CoreContentTopic CoreContentTopic { get; set; }
    TopicNode[] ChildTopicNodes { get; set; }
}

public abstract class TopicNode : ITopicNode
{
    public int TopicId => CoreContentTopic.Id;
    public CoreContentTopic CoreContentTopic { get; set; }
    public TopicNode[] ChildTopicNodes { get; set; }
}

public class MmsTopicNode : TopicNode
{
    /*
*  - A list of inserts
*  - A list of updates
*  - A list of deletes
*/   
    
}
public class DitaTopicNode : TopicNode
{
    public IList<int> QuestionIds { get; set; }
    
}