// See https://aka.ms/new-console-template for more information

using System.Collections.ObjectModel;
using TreeSyncer;
using TreeSyncer.Services;
using TreeSyncer.TocLoaders;

/*
 * The first thing we should do after reading the manifest into a tree is to recursively traverse the list and set
 * sort indexes.
 * 
 * The overall algorithm here should read both ToCs into memory structures and recursively move downwards in the tree,
 * synchronizing each level of the tree, cycling through each item of that level, synchronizing that subtree, etc.
 * We should also return a list of updates, where an item is simply different, like if it's name changed.
 *
 * Actually really what we'd be doing would be building ordered lists of actions to sync trees, so that we could
 * execute those later.
 *
 * This would probably have to involve manual transaction management in order to avoid keeping long-running transactions.
 *
 * Each node of the tree would have:
 *  - Topic ID
 *  - Topic Name
 *  - Parent Topic ID
 *  - SortIndex
 *  - A list of child nodes
 *  - A list of inserts
 *  - A list of updates
 *  - A list of deletes
 *  - A list of question IDs
 *
 * This structure implies that we need a certain set of data in the "manifest" file with the ToC, so that we can
 * capture things like whether a topic is deleted, supplemental, its name has changed, etc.
 *
 * ## After
 * Question: How do we account for things that moved from one place to another? Could we just accumulate a list of
 * inserts and deletes for each node's child list, and build actions based on the various cases:
 *
 * ### Inserts
 * - (1) If an item in an insert list has a corresponding item in any delete list in the tree, we build an update action
 * in the master action lists (we'd have master insert, delete and update lists) that just updated the parent of this
 * topic.
 * - (2) If an item in an insert list doesn't have a corresponding delete anywhere, then it just gets added to the master
 * insert action list.
 *
 * ### Deletes
 * - (3) If an item in a delete list has a corresponding item in any insert list in the tree, we do nothing, since it'll
 * get handled later by rule (1), above (remember, we're recursing *downwards* in the hierarchy).
 * - (4) If an item in an a delete list doesn't have a corresponding insert anywhere, then it just gets added to the master
 * delete action list.
 *
 * ### Updates
 * - (5) These just get added to the master update lists, since presumably they're just name changes
 * 
 * ## Executing the master actions
 * - We'd first execute all the inserts in order
 * - Then execute all the updates in order
 * - Then we'd execute all the deletes in *reverse* order (or, we could use Stacks or Queues)
 *
 * ## Question Syncing
 * Then later, we'd cycle through the tree recursively, syncing question IDs . . .
 */

var syncer = new CoreTocSyncer(new DbCoreTocLoader(), new XmlCoreTocLoader());
syncer.Sync();

Console.WriteLine("Hello, World!");