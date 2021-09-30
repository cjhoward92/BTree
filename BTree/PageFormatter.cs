using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace BTree
{
    // TODO: Let's create some custom serialization
    public class PageFormatter : IFormatter
    {
        private SerializationBinder _binder = new DefaultPageSerializationBinder();
        private StreamingContext _context = new StreamingContext(StreamingContextStates.File);

        public SerializationBinder Binder { get => _binder; set => _binder = value; }
        public StreamingContext Context { get => _context; set => _context = value; }
        public ISurrogateSelector SurrogateSelector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            throw new NotImplementedException();
        }

        private class DefaultPageSerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                throw new NotImplementedException();
            }
        }

        private class DefaultPageSurrogateSelector : ISurrogateSelector
        {
            private ISurrogateSelector _nextSelector = null;

            public void ChainSelector(ISurrogateSelector selector)
            {
                this._nextSelector = selector;
            }

            public ISurrogateSelector GetNextSelector()
            {
                return _nextSelector;
            }

            public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
            {
                if (type != typeof(Page))
                {
                    selector = _nextSelector;
                    return null;
                }

                selector = this;
                return new DefaultPageSerializationSurrogate();
            }
        }

        private class DefaultPageSerializationSurrogate : ISerializationSurrogate
        {
            public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }

            public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
            {
                throw new NotImplementedException();
            }
        }
    }
}
