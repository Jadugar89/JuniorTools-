using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MarkupReader;
using BaseHelper.ViewModels.Utility;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using System.IO;
using System.Windows;
using System.Reflection;

namespace BaseHelper.ViewModels
{
    public class XmlReaderViewModel: ViewModelBase, IDropTarget
    {
        public ICommand Btn_OpenDialog { get; }

        public XmlReaderViewModel()
        {

            Btn_OpenDialog = new RelayCommand(OpenDialog);
            var test = App.Current.Windows[0].FindName("jasiu") as Grid;
            Label label = new Label();
            label.Name = "DropDescription";
            label.Content = "Drop File or Press Button to Read File ";
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            //test.Children.Add(label);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && (extension.Equals(".xml") || extension.Equals(".json"));
            }) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && (extension.Equals(".xml") || extension.Equals(".json"));
            }) ? DragDropEffects.Copy : DragDropEffects.None;

            if (dropInfo.Effects == DragDropEffects.Copy)
            {
                var Name = dragFileList.First(x => Path.GetExtension(x) == ".xml"|| Path.GetExtension(x)== ".json");
                CreateTree(Name);
            }
           
        }


        private void OpenDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = ""; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                CreateTree(dialog.FileName);
               
               
            }
        }
        private void CreateTree(string filename)
        { 
            var aMarkups = new Markup(filename).ReadMarkupFile();
            var test = App.Current.Windows[0].FindName("jasiu") as Grid;

            
            if (aMarkups != null && test !=null)
            {
                Type type = aMarkups[0].GetType();
                var lMarkups = aMarkups.Cast<XML_Object>().ToList();
                if (type==typeof(XML_Object))
                {
                    test.Children.Add(new TreeViewCreator<XML_Object>().createTree(lMarkups));
                }
                  
            }
            
        }
    }
}
