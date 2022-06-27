using Autodesk.Revit.DB;
using ricaun.Revit.Mvvm;

namespace RevitAddin.UpdaterTester.Models
{
    public class ParameterChangeModel : ObservableObject
    {
        public ParameterChangeModel(BuiltInParameter builtInParameter, string value = "")
        {
            BuiltInParameter = builtInParameter;
            Valeu = value;
        }
        public BuiltInParameter BuiltInParameter { get; }
        public string Valeu { get; }

        public override string ToString()
        {
            return $"{BuiltInParameter} \t {Valeu}";
        }
    }
}