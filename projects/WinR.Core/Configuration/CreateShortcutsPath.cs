using System;
using System.IO;

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
