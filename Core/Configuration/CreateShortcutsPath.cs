using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WinR.Core.Configuration
{
    class CreateShortcutsPath
    {
        private readonly OperationResult result = new OperationResult();

        internal OperationResult Execute(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }

            return result;
        }
    }
}
