using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
// using System.Windows;

namespace Test
{
    // Revit API 로 개발 시 사용하는 기본 단위는 FEET이다.
    // 이 기본 단위 FEET를 mm 단위로 변경을 시켜주거나 
    // mm 단위를 FEET로 변경할 필요가 있다.
    // 이렇게 단위를 변경하지 않으면 원치 않는 사이즈가 출력되어 오류가 발생한다.
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    /// <summary>
    /// 명령 클래스(Command) - 애드인 실행 
    /// </summary>
    public class Command : IExternalCommand
    {
        /// <summary>
        /// Start
        /// </summary>
        public const string start = "Start";

        public Result Execute(ExternalCommandData commandData,
          ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            try
            {
                Application app = commandData.Application.Application;
                Document doc = commandData.Application.ActiveUIDocument.Document;
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                // 창문의 모든 범례 구성 요소 만들기 
                FilteredElementCollector symbolcollector = new FilteredElementCollector(doc);
                // 창문의 모든 요소 가져오기 (FamilySymbol Id 필요)
                ICollection<Element> symbolcollection = symbolcollector.OfCategory(BuiltInCategory.OST_Windows).OfClass(typeof(FamilySymbol)).ToElements();

                // 이미 만들어진 범례 구성 요소(Object) 선택 및 Reference 클래스 객체 r에 할당하기(값복사)
                Reference r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select");

                Element element = doc.GetElement(r.ElementId);

                ElementId eid = null;

                using (Transaction tr = new Transaction(doc))
                {
                    tr.Start(start);

                    // 창문의 모든 요소 담긴 Collection symbolcollection에서 요소(FamilySymbol 클래스 객체 fs) 하나하나 접근하기 (foreach 반복문)
                    foreach (FamilySymbol fs in symbolcollection)
                    {
                        // ElementTransformUtils.CopyElement 메서드 사용 -> ElementId 클래스 객체 eid에 할당 (값복사)
                        eid = ElementTransformUtils.CopyElement(doc, element.Id, XYZ.Zero).ToList<ElementId>().First<ElementId>();

                        Element newelement = doc.GetElement(eid);

                        // 새로 생긴 범례 구성요소 (newelement)의 id만 FamilySymbol클래스 객체 fs의 Id로 바꿔주기 
                        // BuiltInParameter.LEGEND_COMPONENT - 범례 구성요소 유형 매개변수 의미 
                        newelement.get_Parameter(BuiltInParameter.LEGEND_COMPONENT).Set(fs.Id);

                        // BuiltParameter.LEGEND_COMPONENT_VIEW - 범례 구성요소 뷰 방향 (-7은 앞에서 본 모습 의미)
                        newelement.get_Parameter(BuiltInParameter.LEGEND_COMPONENT_VIEW).Set(-7);
                    }

                    tr.Commit();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return Result.Failed; 
            }
            
            return Result.Succeeded;
        }
    }
}
