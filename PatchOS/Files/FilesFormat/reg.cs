using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchOS.Files.FilesFormat
{
    internal class reg : FORMAT
    {
        internal override string FileType() => "REG";

        internal override Bitmap icon() => null;

        internal override void Execute(string path, string x = null, string y = null)
        {
            throw new NotImplementedException();
        }
    }
}
