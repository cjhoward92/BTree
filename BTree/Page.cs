using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BTree
{
    [Serializable]
    public sealed class Page : ISerializable
    {
        // TODO: Support variable sizes?
        private static byte _pageSize = 8; // 8kb total page size

        public Page() { }

        // Special constructor required by serialization process
        private Page(SerializationInfo info, StreamingContext context)
        {
            ValidateContextState(context);

            // TODO: deserialize
        }

        private void ValidateContextState(StreamingContext context)
        {
            if ((context.State | StreamingContextStates.File) != StreamingContextStates.File)
            {
                throw new InvalidOperationException("No support for deserializing pages outside of files");
            }
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        // https://github.com/postgres/postgres/blob/master/src/include/storage/bufpage.h#L144
        [Serializable]
        private struct PageHeader
        {
            public ulong pd_lsn { get; set; }
            public ushort pd_checksum { get; set; }
            public ushort pd_flags { get; set; }
            public ushort pd_lower { get; set; }
            public ushort pd_upper { get; set; }
            public ushort pd_special { get; set; }
            public ushort pd_pagesize_version { get; set; }
            // TODO: transaction id and itemiddata - cannot find typedefs to know proper size
        }
    }
}
