﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.RichEdit;
using System.Collections;
using System.IO;
using DevExpress.XtraRichEdit;

namespace DevExpress.DevAV {
    public class RichEditControlMailMergeBehavior : Behavior<RichEditControl> {
        public object ActiveObject {
            get { return (object)GetValue(ActiveObjectProperty); }
            set { SetValue(ActiveObjectProperty, value); }
        }
        public static readonly DependencyProperty ActiveObjectProperty =
            DependencyProperty.Register("ActiveObject", typeof(object), typeof(RichEditControlMailMergeBehavior), new PropertyMetadata(null, (d, e) => ((RichEditControlMailMergeBehavior)d).UpdateActiveRecord()));

        public Stream DocumentTemplate {
            get { return (Stream)GetValue(DocumentTemplateProperty); }
            set { SetValue(DocumentTemplateProperty, value); }
        }
        public static readonly DependencyProperty DocumentTemplateProperty =
            DependencyProperty.Register("DocumentTemplate", typeof(Stream), typeof(RichEditControlMailMergeBehavior), new PropertyMetadata(null, (d, e) => ((RichEditControlMailMergeBehavior)d).UpdateDocumentTemplate()));

        public IEnumerable DataSource {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(RichEditControlMailMergeBehavior), new PropertyMetadata(null, (d, e) => ((RichEditControlMailMergeBehavior)d).UpdateDataSource()));


        public IEnumerable<string> RemoveFields {
            get { return (IEnumerable<string>)GetValue(RemoveFieldsProperty); }
            set { SetValue(RemoveFieldsProperty, value); }
        }
        public static readonly DependencyProperty RemoveFieldsProperty =
            DependencyProperty.Register("RemoveFields", typeof(IEnumerable<string>), typeof(RichEditControlMailMergeBehavior), new PropertyMetadata(null));

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.ApplyTemplate();
            AssociatedObject.Options.MailMerge.ActiveRecord = -1;

            AssociatedObject.Options.MailMerge.ViewMergedData = true;
            AssociatedObject.CustomizeMergeFields += CustomizeMergeFields;
            AssociatedObject.ActiveRecordChanged += ActiveRecordChanged;
            UpdateDataSource();
        }

        private void ActiveRecordChanged(object sender, EventArgs e) {
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.CustomizeMergeFields -= CustomizeMergeFields;
            AssociatedObject.ActiveRecordChanged -= ActiveRecordChanged;
        }

        private void CustomizeMergeFields(object sender, CustomizeMergeFieldsEventArgs e) {
            if(RemoveFields != null)
                e.MergeFieldsNames = e.MergeFieldsNames.Where(fn => !RemoveFields.Any(x => x == fn.Name)).ToArray();
        }

        private void UpdateDataSource() {
            if (AssociatedObject != null) {
                AssociatedObject.ApplyTemplate();
                AssociatedObject.Options.MailMerge.DataSource = DataSource;
                AssociatedObject.Document.Fields.Update();
            }
            UpdateActiveRecord();
        }

        private void UpdateDocumentTemplate() {
            if(AssociatedObject != null) {
                AssociatedObject.ApplyTemplate();
                var index = AssociatedObject.Options.MailMerge.ActiveRecord;
                AssociatedObject.LoadDocumentTemplate(DocumentTemplate, DocumentFormat.Rtf);
                AssociatedObject.Options.MailMerge.ActiveRecord = index;
                AssociatedObject.Document.Fields.Update();
            }
        }

        private void UpdateActiveRecord() {
            if(AssociatedObject != null && DataSource != null) {
                var index = DataSource.Cast<object>().Select((x, i) => new { item = x, index = i }).FirstOrDefault(x => x.item == ActiveObject);
                AssociatedObject.Options.MailMerge.ActiveRecord = index != null ? index.index : -1;
            }
        }
    }
}
