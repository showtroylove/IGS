using System;
using System.Linq;
using System.IO;

namespace DevExpress.DevAV.ViewModels {
    partial class ProductViewModel {
        private static double[] ZoomFactors = new[] { 0.5, 0.6, 0.7, 0.8, 0.9, 1, 2, 3, 4, 5 };
        private int zoomFactorIndex = 5;

        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            ZoomFactor = 1;
        }
        public virtual Stream PdfDocument { get; set; }
        public virtual double ZoomFactor { get; set; }
        public virtual void ZoomIn() {
            if(zoomFactorIndex != ZoomFactors.Count() - 1)
                zoomFactorIndex++;
            ZoomFactor = ZoomFactors[zoomFactorIndex];
        }
        public virtual void ZoomOut() {
            if(zoomFactorIndex != 0)
                zoomFactorIndex--;
            ZoomFactor = ZoomFactors[zoomFactorIndex];
        }
        protected override Product CreateEntity() {
            var entity = base.CreateEntity();
            entity.ProductionStart = DateTime.Now;
            entity.CurrentInventory = 1;
            return entity;
        }
        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            PdfDocument = Entity != null && Entity.Catalog != null && Entity.Catalog.Count != 0 ? Entity.Catalog[0].PdfStream : null;
        }
    }
}
