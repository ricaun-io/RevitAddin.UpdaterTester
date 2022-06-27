using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.UpdaterTester.Views;
using System;

namespace RevitAddin.UpdaterTester.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        static UpdaterTesterView updaterTesterView;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            if (updaterTesterView == null)
            {
                App.Updater.Enable();
                updaterTesterView = new UpdaterTesterView();
                updaterTesterView.Closed += (s, e) => { updaterTesterView = null; };

                updaterTesterView.Closed += (s, e) => { App.UpdaterDisable(); };
                updaterTesterView.Show();
            }
            updaterTesterView.Activate();
            return Result.Succeeded;
        }
    }
}
