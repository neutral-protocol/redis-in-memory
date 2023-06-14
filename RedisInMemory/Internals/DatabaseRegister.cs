using System;
using System.Collections.Generic;
using Neutral.TestingUtilities.RedisInMemory.Internals.Types;
using Neutral.TestingUtilities.RedisInMemory.Internals.Types.SortedSet;

namespace Neutral.TestingUtilities.RedisInMemory.Internals
{

    internal sealed class DatabaseRegister : IDisposable
    {
        internal static DatabaseRegister Instance = new DatabaseRegister();

        private Dictionary<string, DatabaseInstanceData> _dbData = new Dictionary<string, DatabaseInstanceData>();
        private static readonly object LockObj = new object();

        private DatabaseRegister()
        {
        }

        internal void RemoveInstanceData(string dbIdentifier)
        {
            lock (LockObj)
            {
                if (_dbData.ContainsKey(dbIdentifier))
                {
                    _dbData.Remove(dbIdentifier);
                }
            }
        }

        internal DatabaseInstanceData GetDatabaseInstanceData(string dbIdentifier)
        {
            //Check if this db is already registered, and register it for notifications if necessary
            lock (LockObj)
            {
                if (!_dbData.ContainsKey(dbIdentifier))
                {
                    _dbData.Add(dbIdentifier, new DatabaseInstanceData());
                }
            }

            return _dbData[dbIdentifier];
        }

        public void Dispose()
        {
            foreach (var db in _dbData)
            {
                db.Value.Dispose();
            }

            _dbData = new Dictionary<string, DatabaseInstanceData>();
        }
    }

    /// <summary>
    /// Holds details for each Redis database.
    /// </summary>
    internal class DatabaseInstanceData : IDisposable
    {
        /// <summary>
        /// Memory cache for this database
        /// </summary>
        public ObjMemCache MemoryCache { get; private set; }

        internal MemoryStrings MemoryStrings { get; private set; }
        internal MemoryHashes MemoryHashes { get; private set; }
        internal MemorySets MemorySets { get; private set; }
        internal MemorySortedSet MemorySortedSets { get; private set; }

        internal DatabaseInstanceData()
        {
            MemoryCache = new ObjMemCache();
            MemoryStrings = new MemoryStrings(MemoryCache);
            MemoryHashes = new MemoryHashes(MemoryCache);
            MemorySets = new MemorySets(MemoryCache);
            MemorySortedSets = new MemorySortedSet(MemoryCache);
        }

        public void Dispose()
        {
            MemoryCache.Dispose();
        }
    }
}