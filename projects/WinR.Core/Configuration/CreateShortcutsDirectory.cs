using System;
using System.IO;

namespace WinR.Core.Configuration
{
    class CreateShortcutsDirectory
    {
        private readonly OperationResult result = new OperationResult();

        internal OperationResult Execute()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), WinRAssemblyInfo.Product + "/" + WinRAssemblyInfo.DefaultShourtcutsFolder);
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                this.result.Success = true;
            }
            catch (Exception e)
            {
                this.result.Success = false;
                this.result.Message = e.Message;
            }

            return this.result;
        }
    }
}
