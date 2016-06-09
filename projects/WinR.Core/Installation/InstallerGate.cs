namespace WinR.Core.Installation
{
    using System.Diagnostics;
    using Squirrel;
    using WinR.Core.Configuration;

    class InstallerGate
    {
        //private readonly Actions actions = new Actions();

        internal void OnInstallationAndUpdate()
        {
            // Having exception "Update.exe not found, not a Squirrel-installed app?" Comment next line or just copy Squirrel.exe as Update.exe on 'bin' folder.
            // Note, in most of these scenarios, the app exits after this method completes!
            SquirrelAwareApp.HandleEvents(
            onInitialInstall: v =>
                {
                    new InstallWinRShortCuts().Execute();
                },
            onAppUpdate: v =>
                {
                    new UninstallWinRShortCuts().Execute();
                    new InstallWinRShortCuts().Execute();
                },
            onAppUninstall: v => new UninstallWinRShortCuts().Execute(),
            onFirstRun: () => { });
        }

        internal void Update()
        {
            Debug.WriteLine("Updating ...");
#if DEBUG
            using (var mgr = new UpdateManager(@"c:\lr.m\winR\Releases\"))
            {
#else
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/MarcoMedrano/winR").Result)
            {
#endif
                var res = mgr.UpdateApp().Result;

                if (res != null)
                {
                    Debug.WriteLine("Updated to " + res.Version);
                }
                else
                    Debug.WriteLine("No new version");

            }
        }
    }
}
