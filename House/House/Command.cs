using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Selection;

namespace House
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

        /// <summary>
        /// 명령 클래스(Command) 안의 실행 메서드 "Execute"
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, 
          ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
            // 메서드 "Execute" 실행시 실행되는 코드 
            Application app = commandData.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            // 현재 문서(doc)에서 어떤 클릭을 하거나 Dynamo의 Selection (해당 문서에서 어떤걸 가져 오는 것 ) 같은 기능들이 모여있는 객체 "uidoc"
            UIDocument uidoc = commandData.Application.ActiveUIDocument;     

            // 중요 - 프로젝트 "House"에 포함되어 있는 어떤 객체를 필터링
            // 기능 - 해당 문서(doc) 또는 뷰에서 필요한 객체를 필터링해서 가져옴. (새로 구현할 데이터 모델에 필요한 데이터셋 대상)
            // 해당 문서(doc)에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc);
            // 해당 뷰에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc, viewId);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            // 해당 collector(문서(doc) 전체가 대상)에서 "Level"에 해당하는 type 을 가진 객체만 전부다 collection으로 가지고 오기 
            // 현재 문서(doc)에서 Level 타입의 객체를 모두 collection으로 가져오기
            ICollection<Element> collection = collector.OfClass(typeof(Level)).ToElements();

            // collection에 들어있는 레벨 중에 첫 번째 레벨 (Level 1 - 1층을 의미) 추출
            Level level = collection.First<Element>() as Level;

            // curve - Revit에서 벽을 그릴 때 선(line) 형태로 작성(또는 입력)
            // 여기서 말하는 curve는 Line 클래스 객체 line을 의미함.
            // 사각형 벽 형태 구조물 생성 
            double x = 20000;     // X 좌표가 사용되는 단위 - mm(밀리미터) 
            double y = 10000;     // Y 좌표가 사용되는 단위 - mm(밀리미터) 

            // x, y 좌표값 단위 mm(밀리미터) -> FEET로 변경 
            x = MmToFeet(x);
            y = MmToFeet(y);

            // 사각형 벽 형태 구조물에 필요한 3차원 포인트(X,Y,Z 좌표) 4개(pt0, pt1, pt2, pt3) 생성 
            XYZ pt0 = new XYZ(0, 0, 0);     
            XYZ pt1 = new XYZ(x, 0, 0);
            XYZ pt2 = new XYZ(x, y, 0);
            XYZ pt3 = new XYZ(0, y, 0);

            // 3차원 포인트(X,Y,Z 좌표) 4개(pt0, pt1, pt2, pt3) 사용해서 선 4개 만들기  
            Line line = Line.CreateBound(pt1, pt0);
            Line line2 = Line.CreateBound(pt2, pt1);
            Line line3 = Line.CreateBound(pt3, pt2);
            Line line4 = Line.CreateBound(pt0, pt3);

            // 벽을 계속 써야 하므로 연산처리(Transaction) 시작하기 전에 벽(Wall) 객체 4개 생성 
            Wall wall1 = null;
            Wall wall2 = null;
            Wall wall3 = null;
            Wall wall4 = null;

            // 해당 "House" 프로젝트가 무언가 변화되거나 연산처리(객체 생성, 정보 변경 및 삭제 등등... )를 해야할 때, Transaction을 실행 해야함.
            // 이 Transaction을 실행하지 않고서는 연산처리(객체 생성, 정보 변경 및 삭제 등등... )가 거부된다.
            using (Transaction transaction = new Transaction(doc))
            {
                // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                transaction.Start(start);  // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                // 경고라던가 핸들링할 수 있는 클래스 (FailureHandlingOptions) 호출 
                FailureHandlingOptions failOpt = transaction.GetFailureHandlingOptions();
                failOpt.SetFailuresPreprocessor(new WarningDiscard());
                transaction.SetFailureHandlingOptions(failOpt);

                // 구글 크롬 검색어 "revit api wall create" 입력
                // 참고 URL - https://www.revitapidocs.com/2017/4a42066c-bc44-0f99-566c-4e8327bc3bfa.htm
                // 메서드 "Create" 파라미터 bool structural - 구조여부 확인 
                wall1 = Wall.Create(doc, line, level.Id, true);     // 해당 "House" 프로젝트의 신규 객체(Wall1) 생성 
                wall2 = Wall.Create(doc, line2, level.Id, true);    // 해당 "House" 프로젝트의 신규 객체(Wall2) 생성 
                wall3 = Wall.Create(doc, line3, level.Id, true);    // 해당 "House" 프로젝트의 신규 객체(Wall3) 생성 
                wall4 = Wall.Create(doc, line4, level.Id, true);    // 해당 "House" 프로젝트의 신규 객체(Wall4) 생성 

                transaction.Commit();      // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
            }

            FamilySymbol doors = GetSymbol(doc, "단일_패널", "1100x2050mm");

            // 패밀리 타입(FamilySymbol) 클래스 객체 "fs" null 체크 
            if (doors != null)
            {
                CreateDoor(doc, wall1, doors);
                CreateDoor(doc, wall2, doors);
                CreateDoor(doc, wall3, doors);
                CreateDoor(doc, wall4, doors);
            }

            System.Windows.Forms.MessageBox.Show("창문을 만드려면 벽을 2번 클릭하세요.", "안내", System.Windows.Forms.MessageBoxButtons.OK);

            FamilySymbol windows = GetSymbol(doc, "미닫이", "1000x1200mm");

            // 패밀리 인스턴스 객체(windows) 만들 때 들어가는 포인트 객체 생성 및 X, Y, Z 좌표 값 할당
            XYZ point = uidoc.Selection.PickPoint("Select Point");      // X, Y, Z 좌표 값 가져오기 (Dynamo에서 Dynamo의 Selection 버튼 (해당 문서에서 어떤걸 가져 오는 것) 클릭하는 기능과 똑같이 실행된다.

            Reference r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select Wall");
            Element wallelement = doc.GetElement(r.ElementId); // Reference 클래스 객체 r에 속하는 프로퍼티 "ElementId"를 메서드 파라미터로 받아서 메서드 "GetElement" 호출 

            // 해당 "House" 프로젝트가 무언가 변화되거나 연산처리(객체 생성, 정보 변경 및 삭제 등등... )를 해야할 때, Transaction을 실행 해야함.
            // 이 Transaction을 실행하지 않고서는 연산처리(객체 생성, 정보 변경 및 삭제 등등... )가 거부된다.
            using (Transaction transaction = new Transaction(doc))
            {
                // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                transaction.Start(start);  // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                // 패밀리 타입(FamilySymbol) 클래스 객체 "fs" 활성화 여부 체크 
                if (!windows.IsActive) windows.Activate();     // 객체 "fs"이 한번도 활성화 되어 있지 않았다면 활성화 시켜라

                // 기존 활성화된 문서(doc)에 문(패밀리 인스턴스) 객체 wi 생성(Create)
                FamilyInstance wi = doc.Create.NewFamilyInstance(point, windows, wallelement, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                // wi.LookupParameter("씰 높이").Set(MmToFeet(800));    
                // 매개변수 이름 "씰 높이" 활용해서 속성값(높이) "800" 할당 
                wi.get_Parameter(BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM).Set(MmToFeet(800)); // BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM는 매개변수 "씰 높이" 의미

                transaction.Commit();      // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
            }

            // CurveArray carry = new CurveArray(); // 바닥 

            // Curve 클래스 밑에 Line(선)이 존재하고 이 Curve 클래스 객체 4개(curve, curve2, curve3, curve4)를 가지고 바닥(floor) 생성 
            Curve curve = Line.CreateBound(pt0, pt1);   
            Curve curve2 = Line.CreateBound(pt1, pt2);
            Curve curve3 = Line.CreateBound(pt2, pt3);
            Curve curve4 = Line.CreateBound(pt3, pt0);

            CurveLoop curveloop = new CurveLoop();

            curveloop.Append(curve);
            curveloop.Append(curve2);
            curveloop.Append(curve3);
            curveloop.Append(curve4);

            // Level level = doc.ActiveView.GenLevel;
            FilteredElementCollector col = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).OfClass(typeof(FloorType));
            FloorType floorType = col.FirstElement() as FloorType;

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start(start);

                IList<CurveLoop> curveLoops = new List<CurveLoop>();
                curveLoops.Add(curveloop);

                // 바닥 객체 floor 생성 및 값 할당 
                // 참고 URL - https://stackoverflow.com/questions/75986187/new-floor-create-method-revit-2023-apis-with-python
                // 참고 2 URL - https://youtu.be/O_Pzvs6E6Pk
                Floor floor = Floor.Create(doc, curveLoops, floorType.Id, level.Id);

                transaction.Commit();
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// 패밀리 인스턴스(문(Door), 창문(Window) 등등...) 객체 리턴 메서드 
        /// </summary>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public FamilySymbol GetSymbol(Document doc, string familyName, string symbolName)
        {
            FamilySymbol fs = null;        // 패밀리 타입(FamilySymbol) 클래스 객체 "fs" 선언 

            // 현재 문서(doc)에서 필요한 패밀리 타입(FamilySymbol) 모두 필터링 해서 가져옴
            FilteredElementCollector symbolcollector = new FilteredElementCollector(doc);
            ICollection<Element> symbolcollection = symbolcollector.OfClass(typeof(FamilySymbol)).ToElements();

            // 기존 활성화된 문서(doc)에 패밀리 인스턴스(문(Door), 창문(Window) 등등...) 객체 생성(Create)
            // doc.Create.NewFamilyInstance();

            foreach (Element element in symbolcollection)
            {
                FamilySymbol symbol = element as FamilySymbol;

                // symbolcollection에 속한 요소(Item)인 element들 중
                // FamilySymbol 클래스 객체 symbol의 familyName은 "단일_패널"이고
                // symbolName은 "1100x2050mm"인 2가지 조건을 만족하는 FamilySymbol클래스 객체(symbol)만 뽑아내기 
                if (symbol.FamilyName.Equals(familyName) && symbol.Name.Equals(symbolName))
                {
                    fs = symbol;
                    break;      // foreach 반복문 종료 
                }
            }

            return fs;
        }

        /// <summary>
        /// 문 만드는 메서드
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="wall"></param>
        /// <param name="fs"></param>
        public void CreateDoor(Document doc, Wall wall, FamilySymbol fs)
        {
            LocationCurve locCurve = wall.Location as LocationCurve;
            // 벽을 그릴 때, 밑에 그렸던 선(Curve wallCurve) 정보 추출
            Curve wallCurve = locCurve.Curve;

            // 벽을 그릴 때, 밑에 그렸던 선(Curve wallCurve)의 중간점 구하기 
            XYZ startPt = wallCurve.GetEndPoint(0);  // 선의 시작점 (Start)은 GetEndPoint(0)을 의미 (0은 인덱스 0을 의미)
            XYZ endPt = wallCurve.GetEndPoint(1);    // 선의   끝점 (End)은 GetEndPoint(1)을 의미   (1은 인덱스 1을 의미)
            XYZ centerPt = (startPt + endPt) / 2;    // 중간점(pt) 구하기

            // 해당 "House" 프로젝트가 무언가 변화되거나 연산처리(객체 생성, 정보 변경 및 삭제 등등... )를 해야할 때, Transaction을 실행 해야함.
            // 이 Transaction을 실행하지 않고서는 연산처리(객체 생성, 정보 변경 및 삭제 등등... )가 거부된다.
            using (Transaction transaction = new Transaction(doc))
            {
                // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                transaction.Start(start);  // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                // 패밀리 타입(FamilySymbol) 클래스 객체 "fs" 활성화 여부 체크 
                if (!fs.IsActive) fs.Activate();     // 객체 "fs"이 한번도 활성화 되어 있지 않았다면 활성화 시켜라

                // 기존 활성화된 문서(doc)에 문(패밀리 인스턴스) 객체 생성(Create)
                doc.Create.NewFamilyInstance(centerPt, fs, wall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                transaction.Commit();      // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
            }
        }

        /// <summary>
        /// 단위 mm(밀리미터) -> FEET 변경 
        /// </summary>
        /// <param name="mmValue"></param>
        /// <returns></returns>
        public double MmToFeet(double mmValue)
        {
            double _mmToFeet = 0.0032808399;
            return mmValue * _mmToFeet;
        }
    }

    #region BarWaringDiscard

    /// <summary>
    /// 경고창 관련 클래스(WarningDiscard)
    /// </summary>
    public class WarningDiscard : IFailuresPreprocessor 
    { 
        FailureProcessingResult IFailuresPreprocessor.PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            IList<FailureMessageAccessor> fmas = failuresAccessor.GetFailureMessages();

            if (fmas.Count == 0)
            {
                return FailureProcessingResult.Continue;
            }

            bool isResolved = false;

            // fma - 실패 메세지를 받아오는 FailureMessageAccessor 클래스 객체 fma
            foreach (FailureMessageAccessor fma in fmas)
            {
                failuresAccessor.DeleteWarning(fma);    // 실패(또는 경고) 메시지 삭제 (DeleteWarning)
            }

            return FailureProcessingResult.Continue;    // 경고창 무시하고 확인 누른 상황으로 리턴 
        }
    }

    #endregion BarWaringDiscard
}
