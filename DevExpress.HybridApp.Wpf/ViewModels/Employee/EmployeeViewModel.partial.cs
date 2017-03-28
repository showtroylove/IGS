using System;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeViewModel {
        private EmployeeContactsViewModel contacts;

        private string firstName;
        private string lastName;

        public void OnLoaded() {
            firstName = Entity.FirstName;
            lastName = Entity.LastName;
        }
        protected override bool SaveCore() {
            if(Entity.FirstName != firstName || Entity.LastName != lastName)
                Entity.FullName = Entity.FirstName + " " + Entity.LastName;
            return base.SaveCore();
        }
        public void ShowMailMerge() {
            var mailMergeViewModel = MailMergeViewModel<Employee, object>.Create(UnitOfWorkFactory, getRepositoryFunc, Entity.Id);
            DocumentManagerService.CreateDocument("EmployeeMailMergeView", mailMergeViewModel, null, this).Show();
        }
        public void ShowProfile() {
            DocumentManagerService.CreateDocument("ReportPreview", ReportPreviewViewModel.Create(ReportInfoFactory.EmployeeProfile(Entity)), null, this).Show();
        }
        public void ShowMeeting() {
            MessageBoxService.ShowMessage(string.Format("Schedule meeting with {0}?", Entity.FullName), "Meeting", MessageButton.YesNoCancel);
        }
        public void AddTask() {
            Action<EmployeeTask> initializer = x => {
                x.AssignedEmployeeId = Entity.Id;
                x.OwnerId = Entity.Id;
            };
            this.GetRequiredService<IDocumentManagerService>("AddNoteDocumentManagerService").CreateDocument("EmployeeTaskView", null, initializer, this).Show();
        }
        public EmployeeContactsViewModel Contacts {
            get {
                if(contacts == null)
                    contacts = EmployeeContactsViewModel.Create().SetParentViewModel(this);
                return contacts;
            }
        }
        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            Contacts.Entity = Entity;
        }
        protected override string GetTitle() {
            return Entity.FullName;
        }

        private IDocumentManagerService DocumentManagerService { get { return this.GetRequiredService<IDocumentManagerService>(); } }
    }
}
