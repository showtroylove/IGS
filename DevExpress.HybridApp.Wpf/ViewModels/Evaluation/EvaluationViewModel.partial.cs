using System;

namespace DevExpress.DevAV.ViewModels {
    partial class EvaluationViewModel {
        protected override Evaluation CreateEntity() {
            var entity = base.CreateEntity();
            entity.CreatedOn = DateTime.Now;
            return entity;
        }
    }
}
