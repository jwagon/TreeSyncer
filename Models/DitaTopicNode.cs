namespace TreeSyncer.Models;

public class DitaTopicNode : TopicNode
{
    public string ParentTopicExternalId { get; set; }
    
    public IList<int> QuestionIds { get; set; }
    
}