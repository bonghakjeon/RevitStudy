using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;

namespace OnIdlingEvent
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        UIApplication uiApp = null;
        Document doc = null;

        TextNote textNote = null;
        String oldDateTime = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiApp = new UIApplication(commandData.Application.Application);
            doc = commandData.Application.ActiveUIDocument.Document;
            using (Transaction t = new Transaction(doc, "Text Note Creation"))
            {
                t.Start();

                TextNoteOptions options = new TextNoteOptions();
                options.HorizontalAlignment = HorizontalTextAlignment.Center;
                options.TypeId = doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
                textNote = TextNote.Create(doc, doc.ActiveView.Id, XYZ.Zero, DateTime.Now.ToString(), options);

                t.Commit();
            }
            oldDateTime = DateTime.Now.ToString();

            uiApp.Idling += new EventHandler<Autodesk.Revit.UI.Events.IdlingEventArgs>(idleUpdate);

            return Result.Succeeded;
        }

        public void idleUpdate(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            UIApplication uiApp = sender as UIApplication;
            Document doc = uiApp.ActiveUIDocument.Document;
            if (oldDateTime != DateTime.Now.ToString())
            {
                using (Transaction transaction = new Transaction(doc, "Text Note Update"))
                {
                    transaction.Start();
                    textNote.Text = DateTime.Now.ToString();
                    transaction.Commit();
                }
                oldDateTime = DateTime.Now.ToString();
            }
        }
    }
}
