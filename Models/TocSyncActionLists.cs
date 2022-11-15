namespace TreeSyncer.Models;

public class TocSyncActionLists
{
    public TocSyncActionLists()
    {
        Inserts = Array.Empty<TopicNode>();
        Deletes = Array.Empty<TopicNode>();
        Updates = Array.Empty<TopicNode>();
    }

    public void Add(TocSyncActionLists lists)
    {
        Inserts = Concatenate(Inserts, lists.Inserts);
        Updates = Concatenate(Updates, lists.Updates);
        Deletes = Concatenate(Deletes, lists.Deletes);
    }

    private TopicNode[] Concatenate(TopicNode[] x, TopicNode[] y)
    {
        var z = new TopicNode[x.Length + y.Length];
        x.CopyTo(z, 0);
        y.CopyTo(z, x.Length);

        return z;
    }

    public TopicNode[] Inserts;
    public TopicNode[] Deletes;
    public TopicNode[] Updates;
}