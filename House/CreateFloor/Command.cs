using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateFloor
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    /// <summary>
    /// 명령 클래스(Command) - 애드인 실행 
    /// </summary>
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Room sroom = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element).ElementId) as Room;

            SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
            IList<IList<BoundarySegment>> bs = sroom.GetBoundarySegments(opt);

            List<CurveLoop> curveLoops = new List<CurveLoop>();
            foreach (IList<BoundarySegment> itemList in bs)
            {
                CurveLoop cl = new CurveLoop();
                foreach (BoundarySegment item in itemList)
                {
                    cl.Append(item.GetCurve());
                }
                curveLoops.Add(cl);
            }

            Level level = doc.ActiveView.GenLevel;
            FilteredElementCollector col = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).OfClass(typeof(FloorType));
            FloorType floorType = col.FirstElement() as FloorType;

            // Floor.Create();
            using (Transaction trans = new Transaction(doc, "df"))
            {
                trans.Start();

                Floor floor = Floor.Create(doc, curveLoops, floorType.Id, level.Id);

                trans.Commit();
            }

            return Result.Succeeded;
        }
    }
}
