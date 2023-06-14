# RedisInMemory
In-Memory .NET StackExchange.Redis library.

This library implements the `StackExchange.Redis.IDatabase` interface to provide a test double for your .NET applications using Redis.

It can be used purely in-memory, without any Redis instance. This is useful for simulating Redis use, in test cases, for example.

### Why?

For faster, more reliable and more isolated integration tests.

### Usage

If you are already using StackExchange.Redis, then integration into your project is a 1-line change:

    IDatabase database = new RedisDatabaseInMemory(); //Create the in-memory Redis database
  
Since the `RedisDatabaseInMemory` implements `IDatabase`, it's a simple swap.

If you wish to clear your in-memory state, simply `Dispose` it and re-create it.


### Project State

Not every Redis functionality is implemented so some methods will throw `NotImplementedException`.

**String**
The `String` type is mostly implemented.

**Hash**
The `Hash` type is mostly implemented.

**Set**
The `Set` type is mostly implemented.

**SortedSet**
`SortedSet` type is mostly not implemented.

**List**
The `List` type is somewhat implemented.
