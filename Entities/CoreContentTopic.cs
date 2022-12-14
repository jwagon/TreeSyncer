namespace TreeSyncer.Entities;

public class CoreContentTopic
{        
    public virtual int Id { get; set; }
    //TODO this should not be nullable
    //Doing this here for database updates until we cut over to the new system
    
    // TODO: How do we set this? Will this come in on the sync?
    public virtual int? ProductId { get; set; }
    public virtual int TopicLevel { get; set; }
    public virtual string Name { get; set; }
    public virtual int SortIndex { get; set; }
    public virtual int? CoreContentTopicsRootTopicId { get; set; }
    public virtual int? CoreContentTopicsParentTopicId { get; set; }
    
    
    //Do we still need both of these now that they should be in sync?
    public virtual int? OriginalContentTopicId { get; set; }
    
    
    public virtual string ExternalId { get; set; }
    
    // TODO: We shouldn't be touching this except on inserts
    public virtual DateTime DateCreated { get; set; }
    
    // TODO: This should only get set on an update
    public virtual DateTime DateModified { get; set; }
    
    // TODO: Do we care about maintaing these anymore? What's even in here?
    public virtual int CreatedBy { get; set; }
    public virtual int ModifiedBy { get; set; }
    
    // TODO: I think we can/should get rid of this field
    public virtual int? CanonicalTopicId { get; set; }
    public virtual bool Supplemental { get; set; }
    public virtual bool IsDeleted { get; set; }

    // TODO: This needs to include all columns
    public override bool Equals(object obj)
    {
        var other = obj as CoreContentTopic;

        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return Id == other.Id
               && ProductId == other.ProductId
               && CoreContentTopicsRootTopicId == other.CoreContentTopicsRootTopicId
               && CoreContentTopicsParentTopicId == other.CoreContentTopicsParentTopicId
               && OriginalContentTopicId == other.OriginalContentTopicId;
    }


    // TODO: This needs to include all columns
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = GetType().GetHashCode();
            hashCode = (hashCode * 397) ^ Id;
            if(ProductId.HasValue)
                hashCode = (hashCode * 397) ^ (int)ProductId;
            if (CoreContentTopicsRootTopicId.HasValue)
                hashCode = (hashCode * 397) ^ (int)CoreContentTopicsRootTopicId;
            if (CoreContentTopicsParentTopicId.HasValue)
                hashCode = (hashCode * 397) ^ (int)CoreContentTopicsParentTopicId;
            if (OriginalContentTopicId.HasValue)
                hashCode = (hashCode * 397) ^ (int)OriginalContentTopicId;
            return hashCode;
        }
    }
}