using System;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    public partial class EmployeeTaskViewModel {
        protected override EmployeeTask CreateEntity() {
            var entity = base.CreateEntity();
            entity.StartDate = DateTime.Now;
            entity.DueDate = DateTime.Now + new TimeSpan(48, 0, 0);
            return entity;
        }
        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            this.RaisePropertyChanged(vm => vm.ReminderTime);
        }
        public DateTime? ReminderTime {
            get {
                return Entity?.ReminderDateTime;
            }
            set {
                if(Entity == null || value == null || Entity.ReminderDateTime == null)
                    return;
                var reminderDateTime = (DateTime)Entity.ReminderDateTime;
                Entity.ReminderDateTime = new DateTime(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, ((DateTime)value).Hour, ((DateTime)value).Minute, reminderDateTime.Second);
            }
        }
        protected override string GetTitle() {
            return Entity.Owner != null ? Entity.Owner.FullName : string.Empty;
        }
    }
}
