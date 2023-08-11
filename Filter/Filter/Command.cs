using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

// 유튜브 REVIT API - ﻿FilteredElementCollector 객체 필터링
// 참고 URL - https://youtu.be/yt7dnfkL6l4
// 참고 2 URL - https://blog.naver.com/sojunbeer/221762270373
namespace Filter
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
        public const string levelName = "1F";
        public const string levelName2 = "2F";
        public const string levelName3 = "지붕";

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Application app = commandData.Application.Application;
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;


            Level level1 = null;

            // Level 1 - OfClass
            // 중요 - 프로젝트 "TestFilter"에 포함되어 있는 어떤 객체(level1) 를 필터링
            // 기능 - 해당 프로젝트에 열린 문서(doc) 또는 뷰에서 필요한 객체를 필터링해서 가져옴. (새로 구현할 데이터 모델에 필요한 데이터셋 대상)
            // 해당 문서(doc)에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc);
            // 해당 뷰에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc, viewId);
            FilteredElementCollector collector = new FilteredElementCollector(doc);

            // 해당 collector(문서(doc) 전체가 대상)에서 "Level"에 해당하는 type 을 가진 객체만 전부다 list로 가지고 오기 
            // 현재 문서(doc)에서 Level 타입의 객체를 모두 list로 가져오기
            // Level 타입의 객체를 모두 Element로 받아서 levels list에 Element 추가하기 
            // OfClass(typeof(Level))로 필터링한 객체들을 ToElements 메서드 사용해서 Element로 바꿔서 가지고 와라 
            // 결국 List 안에 들어 있는 것은 Element이다.
            IList<Element> levels = collector.OfClass(typeof(Level)).ToElements();

            // list levels 안에 존재하는 요소들(element)에 접근하기 (반복문 foreach 사용)
            foreach (Element element in levels)
            {
                // Level 타입 객체를 Level로 변환(as)하면 정상적으로 Level 클래스 객체로 리턴 -> Level l에 할당.
                // LevelType을 Level로 변환(as)하면 null로 리턴 -> Level l에 할당.
                Level l = element as Level;

                if (l == null) continue;       // Level l 이 Level 타입 객체가 아닐 경우 실행 (이 코드가 없으면 에러 발생)

                if (l.Name.Equals(levelName))  // element 요소의 프로퍼티 "Name"과 string 객체 "levelName"이 같다면 
                {
                    level1 = l;
                    break;
                }
            }

            // Level 2 - OfCategory
            // 중요 - 프로젝트 "TestFilter"에 포함되어 있는 어떤 객체를 필터링
            // 기능 - 해당 프로젝트에 열린 문서(doc) 또는 뷰에서 필요한 객체를 필터링해서 가져옴. (새로 구현할 데이터 모델에 필요한 데이터셋 대상)
            // 해당 문서(doc)에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            // 해당 뷰에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector2 = new FilteredElementCollector2(doc, viewId);
            Level level2 = null;
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);

            // 해당 collector(문서(doc) 전체가 대상)에서 "BuiltInCategory.OST_Levels"에 해당하는 Category를 가진 객체만 전부다 list로 가지고 오기 
            // 현재 문서(doc)에서 "BuiltInCategory.OST_Levels"에 해당하는 Category를 객체만 모두 list로 가져오기
            // BuiltInCategory.OST_Levels 이라는 Category를 모두 Element로 받아서 levels2 list에 Element 추가하기 
            // BuiltInCategory.OST_Levels 이라는 Category로 필터링한 객체들을 ToElements 메서드 사용해서 Element로 바꿔서 가지고 와라 
            // 결국 List 안에 들어 있는 것은 Element이다.
            // 주의사항 - BuiltInCategory.OST_Levels 이라는 Category에 해당하는게 Level 뿐만이 아니라 LevelType 등등 가져와 진다.
            //            따라서 아래 코드처럼 collector2.OfCategory(BuiltInCategory.OST_Levels)로 1차로 Category 객체를 추출한 후 2차로 OfClass(typeof(Level))를 써서 
            //            Level 타입의 객체를 모두 Element로 받아서(ToElements()) levels2 list에 Element 추가해야 한다.
            IList<Element> levels2 = collector2.OfCategory(BuiltInCategory.OST_Levels).OfClass(typeof(Level)).ToElements();

            // list levels 안에 존재하는 요소들(element)에 접근하기 (반복문 foreach 사용)
            foreach (Level element in levels2)
            {

                // C# 삼항 연산자 참고 URL - https://spaghetti-code.tistory.com/51
                // C# 데이터 타입 확인(GetType()) 참고 URL - https://developer-talk.tistory.com/204
                //Level l = (element.GetType().Name.Equals("LevelType")) ?  null : element as Level;
                Level l = element as Level;

                if (l == null) continue;

                if (l.Name.Equals(levelName2)) // element 요소의 프로퍼티 "Name"과 string 객체 "levelName"이 같다면 
                {
                    level2 = l;
                    break;
                }
            }

            // Level 3 - 람다식 활용 => 
            // 주석 설명 안에 있는 코드를 주석 바로 아래 소스코드 두 줄로 요약 가능 
            // FilteredElementCollector collector3 = new FilteredElementCollector(doc);
            // IList<Element> levels = collector3.OfClass(typeof(Level)).ToElements();

            // foreach (Element element in levels)
            // {
            //     Level l = element as Level;

            //     if (l == null) continue;       // Level l 이 Level 타입 객체가 아닐 경우 실행 (이 코드가 없으면 에러 발생)

            //     if (l.Name.Equals(levelName))  // element 요소의 프로퍼티 "Name"과 string 객체 "levelName"이 같다면 
            //     {
            //         level1 = l;
            //         break;
            //     }
            // }
            FilteredElementCollector collector3 = new FilteredElementCollector(doc).OfClass(typeof(Level));

            // Cast 진행해서 Level로 변환, x(대리자, collector3안에 들어있는 객체를 x로(element로) 받는다.(입력 파라미터 의미))
            // 람다식에서 실제로 실행되는 문장 - x.Name == levelName3 조건을 만족하는 Level 클래스 객체를 
            // Level level3 에 준다.
            Level level3 = collector3.Cast<Level>().FirstOrDefault<Level>(x => x.Name == levelName3);        // Level 3 - 람다식 활용 => 


            TaskDialog.Show("확인", "Level 1 : " + level1.Name + " / Level 2 : " + level2.Name + " / Level 3 : " + level3.Name);

            return Result.Succeeded;
        }
    }
}
