using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MITD.PMS.Presentation.Logic
{
    public class ReportCommandVM : CommandViewModel
    {
        private bool isFolder;
        private string p;
        private DelegateCommand delegateCommand;

        public ReportCommandVM(string p, DelegateCommand delegateCommand):base(p,delegateCommand)
        {
            isFolder = false;
        }
        public ReportCommandVM(string p, DelegateCommand delegateCommand, bool isFolder)
            : base(p, delegateCommand)
        {
            this.isFolder = isFolder;
        }
        public bool IsFolder
        {
            get { return isFolder; }
            set { this.SetField(vm => vm.IsFolder, ref isFolder, value); }
        }
    }
}
