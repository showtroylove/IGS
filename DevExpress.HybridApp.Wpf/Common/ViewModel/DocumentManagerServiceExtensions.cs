using System;
using System.Linq;
using DevExpress.Mvvm;

namespace DevExpress.DevAV.Common.ViewModel
{
    /// <summary>
    /// Provides the extension methods that are used to implement the IDocumentManagerService interface.
    /// </summary>
    public static class DocumentManagerServiceExtensions {

        /// <summary>
        /// Creates and shows a document containing a single object view model for the existing entity.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to create and show the document.</param>
        /// <param name="parentViewModel">An object that is passed to the view model of the created view.</param>
        /// <param name="primaryKey">An entity primary key.</param>
        public static void ShowExistingEntityDocument<TEntity, TPrimaryKey>(this IDocumentManagerService documentManagerService, object parentViewModel, TPrimaryKey primaryKey) {
            var document = FindEntityDocument<TEntity, TPrimaryKey>(documentManagerService, primaryKey) ?? CreateDocument<TEntity>(documentManagerService, primaryKey, parentViewModel);
            document?.Show();
        }

        /// <summary>
        /// Creates and shows a document containing a single object view model for new entity.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to create and show the document.</param>
        /// <param name="parentViewModel">An object that is passed to the view model of the created view.</param>
        /// <param name="newEntityInitializer">An optional parameter that provides a function that initializes a new entity.</param>
        public static void ShowNewEntityDocument<TEntity>(this IDocumentManagerService documentManagerService, object parentViewModel, Action<TEntity> newEntityInitializer = null) {
            var document = CreateDocument<TEntity>(documentManagerService, newEntityInitializer != null ? newEntityInitializer : x => DefaultEntityInitializer(x), parentViewModel);
            document?.Show();
        }

        /// <summary>
        /// Searches for a document that contains a single object view model editing entity with a specified primary key.
        /// </summary>
        /// <param name="documentManagerService">An instance of the IDocumentManager interface used to find a document.</param>
        /// <param name="primaryKey">An entity primary key.</param>
        public static IDocument FindEntityDocument<TEntity, TPrimaryKey>(this IDocumentManagerService documentManagerService, TPrimaryKey primaryKey) => documentManagerService == null ? null : (from document in documentManagerService.Documents let entityViewModel = document.Content as ISingleObjectViewModel<TEntity, TPrimaryKey> where entityViewModel != null && Equals(entityViewModel.PrimaryKey, primaryKey) select document).FirstOrDefault();

        private static void DefaultEntityInitializer<TEntity>(TEntity entity) { }

        private static IDocument CreateDocument<TEntity>(IDocumentManagerService documentManagerService, object parameter, object parentViewModel) 
        {
            return documentManagerService?.CreateDocument(typeof(TEntity).Name + "View", parameter, parentViewModel);
        }
    }
}