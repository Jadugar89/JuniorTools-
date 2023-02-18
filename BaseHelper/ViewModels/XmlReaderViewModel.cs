using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using System.IO;
using System.Windows;
using BaseHelper.Services;

namespace BaseHelper.ViewModels
{
    public class XmlReaderViewModel: ViewModelBase, IDropTarget
    {
        public IRelayCommand BtnOpenDialog { get; }

        private List<TreeViewItem> markupTreeView;
        private readonly IMarkupReaderService markupReaderService;

        public List<TreeViewItem> MarkupTreeView
        {
            get { return markupTreeView; }
            set { markupTreeView = value;
                OnPropertyChanged(nameof(MarkupTreeView));
            }
        }


        public XmlReaderViewModel(IMarkupReaderService markupReaderService)
        {

            BtnOpenDialog = new RelayCommand(OpenDialog);
            this.markupReaderService = markupReaderService;
        }

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
                var name = dragFileList.First(x => Path.GetExtension(x) == ".xml"|| Path.GetExtension(x)== ".json");
                if(name!=null) MarkupTreeView = markupReaderService.GetTreeViewItem(name).ToList();
            }
           
        }


        private void OpenDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = ""; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "XML documents (.xml)|*.xml"; // Filter files by extension

            // Show open file dialog box
            bool? name = dialog.ShowDialog();

            // Process open file dialog box results
            if (name == true)
            {

                if (dialog.FileName != null) MarkupTreeView = markupReaderService.GetTreeViewItem(dialog.FileName).ToList();


            }
        }
       
    }
}
