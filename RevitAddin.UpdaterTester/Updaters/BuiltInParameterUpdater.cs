using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAddin.UpdaterTester.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddin.UpdaterTester.Updaters
{
    public class BuiltInParameterUpdater : IUpdater
    {
        private UpdaterId updaterId;
        public BuiltInParameterUpdater(AddInId addInId)
        {
            this.updaterId = new UpdaterId(addInId, GetId());
        }
        public void Execute(UpdaterData data)
        {
            var document = data.GetDocument();
            var ids = new List<ElementId>();
            ids.AddRange(data.GetAddedElementIds());
            ids.AddRange(data.GetModifiedElementIds());
            ids.AddRange(data.GetDeletedElementIds());

            var idChangeTypes = ids.ToDictionary(e => e,
                e => GetTriggeredBuiltInParameters(data, e));

            UpdaterTesterView.Clear();
            foreach (var idChangeType in idChangeTypes)
            {
                var id = idChangeType.Key;
                var changes = idChangeType.Value;
                var name = "";
                var category = BuiltInCategory.INVALID;
                if (document.GetElement(id) is Element element)
                {
                    name = element.Name;
                    category = (BuiltInCategory)element.Category.Id.IntegerValue;
                }

                var values = changes
                    .ToDictionary(e => e, e => GetValueParameter(document, id, e));

                UpdaterTesterView.AddItem(id, name, category, values);
                System.Diagnostics.Debug.WriteLine($"{id} [{string.Join(" ", changes)}]");
            }
        }

        private string GetValueParameter(Document document, ElementId elementId, BuiltInParameter builtInParameter)
        {
            var value = "";
            if (document.GetElement(elementId) is Element element)
            {
                value = "NOT FOUND?";
                if (element.get_Parameter(builtInParameter) is Parameter parameter)
                {
                    value = parameter.AsValueString();
                    if (parameter.StorageType == StorageType.String)
                        value = parameter.AsString();
                }
            }

            return value;
        }

        public string GetAdditionalInformation() => "All Parameter Updater Tester";
        public ChangePriority GetChangePriority() => ChangePriority.MEPCalculations;
        public UpdaterId GetUpdaterId() => this.updaterId;
        public string GetUpdaterName() => "UpdaterTester";
        public Guid GetId() => new Guid("85886475-015F-4710-94F8-F4F59B952135");
        public ElementFilter GetElementFilter()
        {
            return new ElementCategoryFilter(BuiltInCategory.INVALID, true);
        }
        private void AddTriggerAllBuiltInParameter()
        {
            var elementFilter = GetElementFilter();
            var changeTypes = GetChangeTypes();
            foreach (var changeType in changeTypes)
            {
                UpdaterRegistry.AddTrigger(GetUpdaterId(), elementFilter, changeType);
            }
        }

        private IEnumerable<ChangeType> GetChangeTypes()
        {
            var values = Enum.GetValues(typeof(BuiltInParameter)).Cast<BuiltInParameter>();
            var changeTypes = values.Select(e => Element.GetChangeTypeParameter(new ElementId(e)));
            return changeTypes;
        }

        private IEnumerable<BuiltInParameter> GetTriggeredBuiltInParameters(UpdaterData data, ElementId elementId)
        {
            var values = Enum.GetValues(typeof(BuiltInParameter)).Cast<BuiltInParameter>();
            var changes = values.Where(e =>
                    data.IsChangeTriggered(elementId, Element.GetChangeTypeParameter(new ElementId(e)))
                );

            return changes.Distinct();
        }

        public void Enable()
        {
            if (UpdaterRegistry.IsUpdaterRegistered(GetUpdaterId()))
                UpdaterRegistry.EnableUpdater(GetUpdaterId());
        }
        public void Disable()
        {
            if (UpdaterRegistry.IsUpdaterRegistered(GetUpdaterId()))
                UpdaterRegistry.DisableUpdater(GetUpdaterId());
        }

        public void Register()
        {
            if (UpdaterRegistry.IsUpdaterRegistered(GetUpdaterId())) return;

            UpdaterRegistry.RegisterUpdater(this, true);

            AddTriggerAllBuiltInParameter();
        }

        public void Unregister()
        {
            if (!UpdaterRegistry.IsUpdaterRegistered(GetUpdaterId())) return;

            UpdaterRegistry.RemoveAllTriggers(GetUpdaterId());
            UpdaterRegistry.UnregisterUpdater(GetUpdaterId());
        }
    }
}