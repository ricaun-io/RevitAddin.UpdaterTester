using Autodesk.Revit.DB;
using ricaun.Revit.Mvvm;

namespace RevitAddin.UpdaterTester.Models
{
    public class ElementChangeModel : ObservableObject
    {
        public ElementChangeModel(ElementId elementId, string name, BuiltInCategory builtInCategory)
        {
            ElementId = elementId;
            Name = name;
            BuiltInCategory = builtInCategory;
        }
        public ElementId ElementId { get; }
        public string Name { get; }
        public BuiltInCategory BuiltInCategory { get; }
        public ObservableCollection<ParameterChangeModel> Items { get; } = new ObservableCollection<ParameterChangeModel>();

        public override string ToString()
        {
            return $"[{ElementId}] {Name} \t {BuiltInCategory}";
        }
    }
}