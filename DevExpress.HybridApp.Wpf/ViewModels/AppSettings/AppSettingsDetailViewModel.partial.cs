using System;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    partial class AppSettingsDetailViewModel
    {
        private EmployeeContactsViewModel contacts;

        private string _firstName;
        private string _lastName;

        public void OnLoaded() {
            _firstName = Entity.FirstName;
            _lastName = Entity.LastName;
        }
        protected override bool SaveCore() {
            if(Entity.FirstName != _firstName || Entity.LastName != _lastName)
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
            MessageBoxService.ShowMessage($"Schedule meeting with {Entity.FullName}?", "Meeting", MessageButton.YesNoCancel);
        }
        public void AddTask() {
            Action<EmployeeTask> initializer = x => {
                x.AssignedEmployeeId = Entity.Id;
                x.OwnerId = Entity.Id;
            };
            this.GetRequiredService<IDocumentManagerService>("AddNoteDocumentManagerService").CreateDocument("EmployeeTaskView", null, initializer, this).Show();
        }
        public EmployeeContactsViewModel Contacts => contacts ?? (contacts = EmployeeContactsViewModel.Create().SetParentViewModel(this));

        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            Contacts.Entity = Entity;
        }
        protected override string GetTitle() {
            return Entity.FullName;
        }

        private IDocumentManagerService DocumentManagerService => this.GetRequiredService<IDocumentManagerService>();
    }
}
