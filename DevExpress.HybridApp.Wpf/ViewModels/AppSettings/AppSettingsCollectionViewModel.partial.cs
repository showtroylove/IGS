using System;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    partial class AppSettingsCollectionViewModel : ISupportFiltering<Employee>, IFilterTreeViewModelContainer<Employee, long> {
        private IDocumentManagerService DocumentManagerService => this.GetRequiredService<IDocumentManagerService>();
        private IDocumentManagerService EditNoteDocumentManagerService => this.GetRequiredService<IDocumentManagerService>("EditNoteDocumentManagerService");

        public void ShowMailMerge() {
            var mailMergeViewModel = MailMergeViewModel<Employee, object>.Create(unitOfWorkFactory, x => x.Employees, SelectedEntity == null ? null as long? : SelectedEntity.Id);
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel, null, this).Show();
        }
        public void ShowPrint(EmployeeReportType employeeReportType) {
            DocumentManagerService.CreateDocument("ReportPreview", ReportPreviewViewModel.Create(GetReport(employeeReportType)), null, this).Show();
        }
        public bool CanShowPrint(EmployeeReportType employeeReportType) {
            return employeeReportType != EmployeeReportType.Profile || SelectedEntity != null;
        }

        private IReportInfo GetReport(EmployeeReportType reportType) {
            switch(reportType) {
                case EmployeeReportType.TaskList:
                    return ReportInfoFactory.EmployeeTaskList(unitOfWorkFactory.CreateUnitOfWork().Tasks.ToList());
                case EmployeeReportType.Profile:
                    return ReportInfoFactory.EmployeeProfile(SelectedEntity);
                case EmployeeReportType.Summary:
                    return ReportInfoFactory.EmployeeSummary(Entities);
                case EmployeeReportType.Directory:
                    return ReportInfoFactory.EmployeeDirectory(Entities);
            }
            throw new ArgumentException("", "reportType");
        }

        public void AddTask() {
            Action<EmployeeTask> initializer = x => {
                x.AssignedEmployeeId = SelectedEntity.Id;
                x.OwnerId = SelectedEntity.Id;
            };
            EditNoteDocumentManagerService.CreateDocument("EmployeeTaskView", null, initializer, this).Show();
        }
        public bool CanAddTask() {
            return SelectedEntity != null;
        }
        public void AddNote() {
            Action<Evaluation> initializer;
            if(SelectedEntity == null)
                initializer = default(Action<Evaluation>);
             else
                initializer = (x) => x.EmployeeId = SelectedEntity.Id;
            EditNoteDocumentManagerService.CreateDocument("EvaluationView", null, initializer, this).Show();
        }
        public bool CanAddNote() {
            return SelectedEntity != null;
        }
        public virtual FilterTreeViewModel<Employee, long> FilterTreeViewModel { get; set; }
        #region ISupportFiltering
        Expression<Func<Employee, bool>> ISupportFiltering<Employee>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
    }
}
