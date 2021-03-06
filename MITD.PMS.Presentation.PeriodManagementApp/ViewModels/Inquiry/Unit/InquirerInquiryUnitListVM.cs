﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MITD.PMS.Presentation.Contracts;
using MITD.PMS.Presentation.PeriodManagementApp;
using MITD.PMS.Presentation.PeriodManagementApp.Views;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class InquirerInquiryUnitListVM : PeriodMgtWorkSpaceViewModel
    {
        #region Fields

        private readonly IPMSController appController;
        private readonly IUnitInquiryServiceWrapper inquiryService;
        private string inquirerEmployeeNo;
        private long periodId;
        #endregion

        #region Properties

        public string InquirerEmployeeNo { get { return inquirerEmployeeNo; } }
        public long PeriodId { get { return periodId; } }

        private ObservableCollection<InquiryUnitDTO> inquirySubjects;
        public ObservableCollection<InquiryUnitDTO> InquirySubjects
        {
            get { return inquirySubjects; }
            set { this.SetField(p => p.InquirySubjects, ref inquirySubjects, value); }
        }

        private InquiryUnitDTO selectedInquirySubject;
        public InquiryUnitDTO SelectedInquirySubject
        {
            get { return selectedInquirySubject; }
            set
            {
                this.SetField(p => p.SelectedInquirySubject, ref selectedInquirySubject, value);
                if (selectedInquirySubject == null) return;
                InquirySubjectCommands = createCommands();
                //todo bz
                if (View != null)
                    ((IUnitsInquiryListView)View).CreateContextMenu(new ReadOnlyCollection<DataGridCommandViewModel>(InquirySubjectCommands));
            }
        }

        private List<DataGridCommandViewModel> inquirySubjectCommands;
        public List<DataGridCommandViewModel> InquirySubjectCommands
        {
            get { return inquirySubjectCommands; }
            private set
            {
                this.SetField(p => p.InquirySubjectCommands, ref inquirySubjectCommands, value);
                if (InquirySubjectCommands.Count > 0) SelectedCommand = InquirySubjectCommands[0];
            }

        }

        private DataGridCommandViewModel selectedCommand;
        public DataGridCommandViewModel SelectedCommand
        {
            get { return selectedCommand; }
            set { this.SetField(p => p.SelectedCommand, ref selectedCommand, value); }
        }


        #endregion

        #region Constructors

        public InquirerInquiryUnitListVM()
        {
            PeriodMgtAppLocalizedResources = new PeriodMgtAppLocalizedResources();
            init();
        }

        public InquirerInquiryUnitListVM(IPMSController appController,
                            IUnitInquiryServiceWrapper inquiryService,
                            IPeriodMgtAppLocalizedResources periodMgtAppLocalizedResources)
        {

            this.appController = appController;
            this.inquiryService = inquiryService;
            PeriodMgtAppLocalizedResources = periodMgtAppLocalizedResources;
            init();


        }

        #endregion

        #region Methods

        void init()
        {
            DisplayName = "لیست واحد های آماده برای ارزیابی";
            InquirySubjects = new ObservableCollection<InquiryUnitDTO>();
            SelectedInquirySubject = new InquiryUnitDTO();
        }

        private List<DataGridCommandViewModel> createCommands()
        {
            if (SelectedInquirySubject != null)
                return CommandHelper.GetControlCommands(this, appController, SelectedInquirySubject.ActionCodes);
            return new List<DataGridCommandViewModel>();
        }

        public void Load(string inquirerEmployeeNoParam, long periodIdParam)
        {
            periodId = periodIdParam;
            if(!string.IsNullOrWhiteSpace(inquirerEmployeeNoParam))
            {
                inquirerEmployeeNo = inquirerEmployeeNoParam;
                refresh();
            }
            else
            {
                appController.ShowMessage("شماره پرسنلی شما در سستم موجود نمی باشد");
            }
        }

        private void refresh()
        {
            ShowBusyIndicator("در حال دریافت اطلاعات...");
            inquiryService.GetInquirerInquirySubjects(
                (res, exp) => appController.BeginInvokeOnDispatcher(() =>
                {
                    HideBusyIndicator();
                    if (exp == null)
                    {
                        var res1 = new List<InquiryUnitDTO>();
                        res.ForEach(c =>
                        {
                            if (res1.SingleOrDefault(d => d.UnitId == c.UnitId) == null)
                            {
                                res1.Add(c);
                            }
                        });

                        InquirySubjects = new ObservableCollection<InquiryUnitDTO>(res1);
                        SelectedInquirySubject = res1.FirstOrDefault();

                        //InquirySubjects = new ObservableCollection<InquiryUnitDTO>(res);
                        //SelectedInquirySubject = res.FirstOrDefault();
                    }
                    else appController.HandleException(exp);
                }), periodId, inquirerEmployeeNo);
        }

        protected override void OnRequestClose()
        {
            base.OnRequestClose();
            appController.Close(this);
        }

        #endregion
    }

}
