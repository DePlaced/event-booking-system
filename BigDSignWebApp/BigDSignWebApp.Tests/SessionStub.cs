using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BigDSignWebApp.Tests
{
    public class SessionStub : ISession
    {
        private Dictionary<string, byte[]> _strings = new Dictionary<string, byte[]>();

        public bool IsAvailable => true;

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => _strings.Keys;

        public void Clear()
        {
            _strings.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _strings.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _strings[key] = value;
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _strings.TryGetValue(key, out value);
        }
    }
}
