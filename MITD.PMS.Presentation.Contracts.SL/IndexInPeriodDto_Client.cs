using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class IndexInPeriodDto :TreeElementDto
    {
        private ObservableCollection<IndexInPrdFieldDto> indexInPrdFields;
        public ObservableCollection<IndexInPrdFieldDto> IndexInPrdFields
        {
            get
            {
                return indexInPrdFields;
            }

            set { this.SetField(p => p.IndexInPrdFields, ref indexInPrdFields, value); }

        }
    }


}
