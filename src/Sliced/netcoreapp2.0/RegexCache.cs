


#pragma warning disable

#if !NET7_0_OR_GREATER && FeatureMemory

namespace System.Text.RegularExpressions;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Threading;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]

/// <summary>Cache used to store Regex instances used by the static methods on Regex.</summary>
internal sealed class RegexCache
{
    
    
    
    
    
    

    /// <summary>The default maximum number of items to store in the cache.</summary>
    const int DefaultMaxCacheSize = 15;
    /// <summary>The maximum number of cached items to examine when we need to replace an existing one in the cache with a new one.</summary>
    /// <remarks>This is a somewhat arbitrary value, chosen to be small but at least as large as DefaultMaxCacheSize.</remarks>
    const int MaxExamineOnDrop = 30;

    /// <summary>A read-through cache of one element, representing the most recently used regular expression.</summary>
    static volatile Node? s_lastAccessed;
    /// <summary>The thread-safe dictionary storing all the items in the cache.</summary>
    /// <remarks>
    /// The concurrency level is initialized to 1 as we're using our own global lock for all mutations, so we don't need ConcurrentDictionary's
    /// striped locking.  Capacity is initialized to 31, which is the same as (the private) ConcurrentDictionary.DefaultCapacity.
    /// </remarks>
    static readonly ConcurrentDictionary<Key, Node> s_cacheDictionary = new ConcurrentDictionary<Key, Node>(concurrencyLevel: 1, capacity: 31);
    /// <summary>A list of all the items in the cache.  Protected by <see cref="SyncObj"/>.</summary>
    static readonly List<Node> s_cacheList = new List<Node>(DefaultMaxCacheSize);
    /// <summary>Random number generator used to examine a subset of items when we need to drop one from a large list.  Protected by <see cref="SyncObj"/>.</summary>
    static readonly Random s_random = new Random();
    /// <summary>The current maximum number of items allowed in the cache.  This rarely changes.  Mostly protected by <see cref="SyncObj"/>.</summary>
    static int s_maxCacheSize = DefaultMaxCacheSize;

    /// <summary>Lock used to protect shared state on mutations.</summary>
    static object SyncObj => s_cacheDictionary;

    /// <summary>Gets or sets the maximum size of the cache.</summary>
    public static int MaxCacheSize
    {
        get
        {
            lock (SyncObj)
            {
                return s_maxCacheSize;
            }
        }
        set
        {
            Debug.Assert(value >= 0);

            lock (SyncObj)
            {
                
                s_maxCacheSize = value;

                if (value == 0)
                {
                    
                    s_cacheDictionary.Clear();
                    s_cacheList.Clear();
                    s_lastAccessed = null;
                }
                else if (value < s_cacheList.Count)
                {
                    
                    
                    
                    
                    
                    s_lastAccessed = s_cacheList[0];
                    for (int i = value; i < s_cacheList.Count; i++)
                    {
                        s_cacheDictionary.TryRemove(s_cacheList[i].Key, out _);
                    }
                    s_cacheList.RemoveRange(value, s_cacheList.Count - value);

                    Debug.Assert(s_cacheList.Count == value);
                    Debug.Assert(s_cacheDictionary.Count == value);
                }
            }
        }
    }

    public static Regex GetOrAdd(string pattern)
    {
        
        
        

        Key key = new Key(pattern, RegexOptions.None, Regex.InfiniteMatchTimeout);

        Regex? regex = Get(key);
        if (regex is null)
        {
            regex = new Regex(pattern);
            Add(key, regex);
        }

        return regex;
    }

    public static Regex GetOrAdd(string pattern, RegexOptions options, TimeSpan matchTimeout)
    {
        Key key = new Key(pattern, options, matchTimeout);

        Regex? regex = Get(key);
        if (regex is null)
        {
            regex = new Regex(pattern, options, matchTimeout);
            Add(key, regex);
        }

        return regex;
    }

    static Regex? Get(Key key)
    {
        long lastAccessedStamp = 0;

        
        
        
        if (s_lastAccessed is Node lastAccessed)
        {
            if (key.Equals(lastAccessed.Key))
            {
                return lastAccessed.Regex;
            }

            
            
            
            lastAccessedStamp = Volatile.Read(ref lastAccessed.LastAccessStamp);
        }

        
        if (s_maxCacheSize != 0 && 
            s_cacheDictionary.TryGetValue(key, out Node? node))
        {
            
            
            
            
            
            
            
            Volatile.Write(ref node.LastAccessStamp, lastAccessedStamp + 1);

            
            s_lastAccessed = node;

            
            return node.Regex;
        }

        
        return null;
    }

    static void Add(Key key, Regex regex)
    {
        lock (SyncObj)
        {
            Debug.Assert(s_cacheList.Count == s_cacheDictionary.Count);

            
            
            
            
            
            if (s_maxCacheSize == 0 || s_cacheDictionary.TryGetValue(key, out _))
            {
                return;
            }

            
            if (s_cacheList.Count == s_maxCacheSize)
            {
                int itemsToExamine;
                bool useRandom;

                if (s_maxCacheSize <= MaxExamineOnDrop)
                {
                    
                    
                    itemsToExamine = s_cacheList.Count;
                    useRandom = false;
                }
                else
                {
                    
                    
                    
                    
                    
                    itemsToExamine = MaxExamineOnDrop;
                    useRandom = true;
                }

                
                int minListIndex = useRandom ? s_random.Next(s_cacheList.Count) : 0;
                long min = Volatile.Read(ref s_cacheList[minListIndex].LastAccessStamp);

                
                for (int i = 1; i < itemsToExamine; i++)
                {
                    int nextIndex = useRandom ? s_random.Next(s_cacheList.Count) : i;
                    long next = Volatile.Read(ref s_cacheList[nextIndex].LastAccessStamp);
                    if (next < min)
                    {
                        minListIndex = nextIndex;
                        min = next;
                    }
                }

                
                s_cacheDictionary.TryRemove(s_cacheList[minListIndex].Key, out _);
                s_cacheList.RemoveAt(minListIndex);
            }

            
            var node = new Node(key, regex);
            s_lastAccessed = node;
            s_cacheList.Add(node);
            s_cacheDictionary.TryAdd(key, node);

            Debug.Assert(s_cacheList.Count <= s_maxCacheSize);
            Debug.Assert(s_cacheList.Count == s_cacheDictionary.Count);
        }
    }

    /// <summary>Used as a key for <see cref="Node"/>.</summary>
    internal readonly struct Key : IEquatable<Key>
    {
        readonly string _pattern;
        readonly RegexOptions _options;
        readonly TimeSpan _matchTimeout;

        public Key(string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            Debug.Assert(pattern != null, "Pattern must be provided");

            _pattern = pattern;
            _options = options;
            _matchTimeout = matchTimeout;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) =>
            obj is Key other && Equals(other);

        public bool Equals(Key other) =>
            _pattern.Equals(other._pattern) &&
            _options == other._options &&
            _matchTimeout == other._matchTimeout;

        public override int GetHashCode() =>
            
            
            _pattern.GetHashCode() ^ (int)_options;
    }

    /// <summary>Node for a cached Regex instance.</summary>
    sealed class Node(Key key, Regex regex)
    {
        /// <summary>The key associated with this cached instance.</summary>
        public readonly Key Key = key;
        /// <summary>The cached Regex instance.</summary>
        public readonly Regex Regex = regex;
        /// <summary>A "time" stamp representing the approximate last access time for this Regex.</summary>
        public long LastAccessStamp;
    }
}
#endif