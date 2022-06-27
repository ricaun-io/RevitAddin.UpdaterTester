using Autodesk.Revit.DB;
using RevitAddin.UpdaterTester.Models;
using ricaun.Revit.Mvvm;
using ricaun.Revit.UI;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RevitAddin.UpdaterTester.Views
{
    public partial class UpdaterTesterView : Window
    {
        #region ViewModel
        public static ObservableCollection<ElementChangeModel> Items { get; set; } = new ObservableCollection<ElementChangeModel>();
        public static void Clear()
        {
            Items.Clear();
        }

        public static void AddItem(ElementId elementId, string name, BuiltInCategory builtInCategory, Dictionary<BuiltInParameter, string> parameterValues)
        {
            ElementChangeModel item = new ElementChangeModel(elementId, name, builtInCategory);
            foreach (var parameterValue in parameterValues)
                item.Items.Add(new ParameterChangeModel(parameterValue.Key, parameterValue.Value));
            Items.Add(item);
        }

        public static void AddItem(ElementId elementId, string name, BuiltInCategory builtInCategory, IEnumerable<BuiltInParameter> parameters)
        {
            ElementChangeModel item = new ElementChangeModel(elementId, name, builtInCategory);
            foreach (var parameter in parameters)
                item.Items.Add(new ParameterChangeModel(parameter));
            Items.Add(item);
        }
        #endregion
        public UpdaterTesterView()
        {
            InitializeComponent();
            InitializeWindow();
            Clear();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.Title = $"UpdaterTester {this.GetType().Assembly.GetName().Version.ToString(3)}";
            this.Icon = Properties.Resources.Revit.GetBitmapSource();
            this.MinWidth = 480;
            this.MaxWidth = 480;
            this.MinHeight = 360;
            this.MaxHeight = 720;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
        }
        #endregion
    }
}