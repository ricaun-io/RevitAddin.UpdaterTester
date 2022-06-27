using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.UpdaterTester.Updaters;
using ricaun.Revit.UI;
using System;

namespace RevitAddin.UpdaterTester.Revit
{
    [Console]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public static BuiltInParameterUpdater Updater;
        public Result OnStartup(UIControlledApplication application)
        {
            Updater = new BuiltInParameterUpdater(application.ActiveAddInId);
            Updater.Register();
            Updater.Disable();

            ribbonPanel = application.CreatePanel("UpdaterTester");
            ribbonPanel.AddPushButton<Commands.Command>("Updater\rTester")
                .SetToolTip("Open Updater Tester Dialog.")
                .SetLargeImage(Properties.Resources.Revit.GetBitmapSource());

            application.Idling += Application_Idling;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            Updater?.Unregister();
            ribbonPanel?.Remove();

            application.Idling -= Application_Idling;
            return Result.Succeeded;
        }

        public static void UpdaterDisable()
        {
            Action += (uiapp) => { Updater?.Disable(); };
        }
        public static event Action<UIApplication> Action;
        private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            if (Action != null)
            {
                Action.Invoke(sender as UIApplication);
                Action = null;
            }
        }
    }

}