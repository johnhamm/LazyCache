using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NETSTANDARD2_0
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
#endif


namespace LazyCache
{
#if NETSTANDARD2_0
	public class ObjectCache : MemoryCache
	{
		public ObjectCache( IOptions<MemoryCacheOptions> optionsAccessor ) : base( optionsAccessor )
		{

		}

		public object this[ string key ]
		{
			get { return this.Get( key ); }
		}

		public bool Contains(string key )
		{
			object result;
			return this.TryGetValue( key , out result );
		}

		public int GetCount()
		{
			return this.Count;
		}

		//
		// Summary:
		//     When overridden in a derived class, inserts a cache entry into the cache, specifying
		//     a key and a value for the cache entry, and information about how the entry will
		//     be evicted.
		//
		// Parameters:
		//   key:
		//     A unique identifier for the cache entry.
		//
		//   value:
		//     The object to insert.
		//
		//   policy:
		//     An object that contains eviction details for the cache entry. This object provides
		//     more options for eviction than a simple absolute expiration.
		//
		//   regionName:
		//     Optional. A named region in the cache to which the cache entry can be added,
		//     if regions are implemented. The default value for the optional parameter is null.
		//
		// Returns:
		//     If a cache entry with the same key exists, the specified cache entry's value;
		//     otherwise, null.
		public object AddOrGetExisting( string key , object value , CacheItemPolicy policy )
		{
			object result;
			return !this.TryGetValue( key , out result ) ? value : result;
		}


	}

	public class CacheItemPolicy : MemoryCacheEntryOptions
	{
		//public PostEvictionCallbackRegistration RemovedCallback { get { return this.PostEvictionCallbacks[ 0 ]; } }
	}

#endif
}
