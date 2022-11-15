using System.Reflection;
using TreeSyncer.Entities;
using TreeSyncer.Interfaces;
using TreeSyncer.Models;
using TreeSyncer.TocLoaders;

namespace TreeSyncer.Services;

public static class ArrayExtensions
{
    public static int Push<T>(this T[] source, T value)
    {
        var index = Array.IndexOf(source, default(T));

        if (index != -1)
        {
            source[index] = value;
        }

        return index;
    }
}

public class CoreTocSyncer : ICoreTocSyncer
{
    private MmsTopicNode _subjectTree;
    private DitaTopicNode _targetTree;
    
    private ICoreTocLoader<MmsTopicNode> _mmsCoreTocLoader;
    private ICoreTocLoader<DitaTopicNode> _ditaCoreTocLoader;
    private IDictionary<string, CoreContentTopic> _subjectTreeDict;
    private IDictionary<string, CoreContentTopic> _targetTreeDict;

    public interface INotifiier
    {
        public void Notify(IList<string> usernames, string msg);
    }
    public CoreTocSyncer(
        ICoreTocLoader<MmsTopicNode> mmsCoreTocLoader
        , ICoreTocLoader<DitaTopicNode> ditaCoreTocLoader
    )
    {
        _mmsCoreTocLoader = mmsCoreTocLoader;
        _ditaCoreTocLoader = ditaCoreTocLoader;

        _subjectTree = new MmsTopicNode();
        _targetTree = new DitaTopicNode();
    }
    
    public void Sync()
    {
        _subjectTree = _mmsCoreTocLoader.Load();
        _targetTree = _ditaCoreTocLoader.Load();
        
        // TODO: These should either be part of the loader, or a separate class
        // for building CoreContentTopics from the DITA manifest
        SetSortIndexes(_targetTree);
        SetParentTopicExternalIds(_targetTree);
        SetTopicLevels(_targetTree);

        IEnumerable<int> x  = new List<int>();

        var y = x.GetEnumerator().MoveNext();
        var z = x.GetEnumerator().Current;
        
        // TODO: Here, we need to build a dictionary
        _subjectTreeDict = _subjectTree.ToDictionary();
        _targetTreeDict = _targetTree.ToDictionary();

        var actionLists = BuildActions(_subjectTree, _targetTree);
        ExecuteSyncActions(actionLists);
    }

    private void SetParentTopicExternalIds(DitaTopicNode targetTree)
    {
        throw new NotImplementedException();
    }

    // TODO: Make this a separate class
    private void SetSortIndexes(TopicNode node, int sortIndex = 0)
    {
        node.CoreContentTopic.SortIndex = sortIndex++;
        foreach (var topicNode in node.ChildTopicNodes)
        {
            topicNode.CoreContentTopic.SortIndex = sortIndex++;
            SetSortIndexes(topicNode, sortIndex);
        }
    }
    
    private void SetTopicLevels(DitaTopicNode targetTree)
    {
        throw new NotImplementedException();
    }

    private void ExecuteSyncActions(TocSyncActionLists actionLists)
    {
        // Here, we have to make sure to set the parent IDs correctly
        // To do that, we
        // TODO: We also must execute inserts in order of topic level, deletes in order of reverse topic level and finally updates
        throw new NotImplementedException();
    }

    private TocSyncActionLists BuildActions(ITopicNode subjectTree, ITopicNode targetTree)
    {
        var accumulatedActionLists = new TocSyncActionLists();

        foreach (var targetNode in targetTree.ChildTopicNodes)
        {
            var subjectNode = subjectTree.ChildTopicNodes.FirstOrDefault(x => x.TopicId == targetNode.TopicId);
            if (subjectNode != null)
            {
                if (!targetNode.Equals(subjectNode))
                    accumulatedActionLists.Updates.Push(targetNode);
                
                accumulatedActionLists.Add(BuildActions(subjectNode, targetNode));
            }
            else
            {
                // TODO: First we have to check the whole tree to see if we just need to update the topic level, parent and root
                // So maybe this means we build a dictionary keyed by external ID for quick lookup
                accumulatedActionLists.Inserts.Push(targetNode);
            }
        }

        foreach (var subjectNode in subjectTree.ChildTopicNodes)
        {
            var targetNode = targetTree.ChildTopicNodes.FirstOrDefault(x => x.TopicId == subjectNode.TopicId);
            if (targetNode == null)
                // TODO: Similarly, here we need to check the whole tree to see if this is just a move, and need to update †opic level, parent and root
                accumulatedActionLists.Deletes.Push(targetNode);
        }
        
        return accumulatedActionLists;
    }
}