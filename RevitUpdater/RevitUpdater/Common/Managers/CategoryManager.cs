using Serilog;

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;
using RevitUpdater.Common.Extensions;

using Autodesk.Revit.DB;

namespace RevitUpdater.Common.Managers
{
    public class CategoryManager
    {
        #region GetbuiltInCategoryNameList

        // TODO : BuiltInCategory 이름 리스트 가져오기 static 메서드 "GetbuiltInCategoryNameList" 필요시 오류(오류 원인 - "BuiltInCategory 중 중복된 것과 실제로 사용하지 않는 BuiltInCategory 목록 제거") 보완 및 구현 예정 (2024.03.14 jbh)
        /// <summary>
        /// BuiltInCategory 이름 리스트 가져오기 
        /// </summary>
        public static List<string> GetbuiltInCategoryNameList()
        {
            string builtInCategoryName = string.Empty;              // BuiltInCategory 매개변수 이름 
            long builtInCategoryValue = 0;                         // BuiltInCategory 매개변수에 입력할 값

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 1 단계 : BuiltInCategory 매개변수 데이터 가져오기
                Array builtInCategories = Enum.GetValues(typeof(BuiltInCategory));

                // var test2 = LabelUtils.GetLabelFor(BuiltInCategory.OST_PipeFitting);   // 테스트 코드 2

                // var test3 = LabelUtils.GetLabelFor(BuiltInCategory.OST_StackedWalls_Obsolete_IdInWrongRange); // 테스트 코드 3

                // Revit 응용 프로그램 안에 내장된 BuiltInCategory를 가지고 Dictionary(key - builtInCategoryName / value - builtInCategoryValue) 만들기
                // BuiltInCategory Dictionary(key - name / value - value) 만들기
                // 참고 URL - https://forum.dynamobim.com/t/setting-built-in-parameter-by-using-a-variable-value/49466/2
                // 참고 2 URL - https://chat.openai.com/c/68245284-54a5-4fa7-acff-8e90c39e1931

                // (또 다른 예시) BuiltInParameter
                // 참고 URL - https://www.revitapidocs.com/2024/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
                // 배열에서 List 형변환
                // 참고 URL - https://dongyeopblog.wordpress.com/2016/08/22/c-%EC%96%B4%EB%A0%88%EC%9D%B4%EB%A5%BC-%EB%A6%AC%EC%8A%A4%ED%8A%B8%EB%A1%9C-%EB%B3%80%ED%99%98%ED%95%98%EA%B8%B0-system-array-to-list/

                // 2 단계 : BuiltInCategory 중 중복된 것과 실제로 사용하지 않는 BuiltInCategory 목록 제거 
                // TODO : builtInCategories에 들어있는 BuiltInCategory 값들 중 BuiltInCategory.INVALID 처럼 실제로 사용하지 않는 것들을
                //        Linq 확장 메서드 "Where()"안에서 메서드 "Category.IsBuiltInCategoryValid(builtInCategory)" 사용해서 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://www.revitapidocs.com/2022/15f903ae-3cdf-52b0-4891-fa2d1002e481.htm

                // TODO : builtInCategories에 들어있는 BuiltInCategory 값들 중 중복 데이터가 존재하므로 Linq 확장 메서드 "Distinct()" 사용해서 중복 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://developer-talk.tistory.com/215
                List<string> builtInCategoryNameList = builtInCategories.OfType<BuiltInCategory>()
                                                                        .Where(builtInCategory  => true == Category.IsBuiltInCategoryValid(builtInCategory))
                                                                        .Select(builtInCategory => LabelUtils.GetLabelFor(builtInCategory))
                                                                        .Distinct()
                                                                        .ToList();


                return builtInCategoryNameList;  // 마지막 단계 : builtInCategoryNameList 리턴
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // TODO : 오류 발생시 상위 호출자 예외처리 전달 throw
            }
        }

        #endregion GetbuiltInCategoryNameList 

        #region GetCategoryInfoList

        /// <summary>
        /// Geometry 유형 객체(GeometryElement)에 속한 카테고리 정보 리스트 가져오기 
        /// </summary>
        //public static List<CategoryInfoView> GetCategoryInfoList(FilteredElementCollector rvCollector, List<Element> rvElementList, Options rvGeometryOpt)
        public static List<CategoryInfoView> GetCategoryInfoList(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 활성화된 Revit 문서에 존재하는 Geometry 유형 객체(GeometryElement)의 카테고리 추출하기 (2024.03.22 jbh)
                List<Element> geometryList = rvCollector.Where(element => element.Category is not null
                                                                       && element.get_Geometry(rvGeometryOpt) is not null
                                                                       && element.GetType().Name != UpdaterHelper.ElementType)
                                                        .Select(element => element)
                                                        .ToList();

                // TODO : 아래 주석 친 테스트 코드 필요시 참고 
                // TODO : 활성화된 Revit 문서에 존재하는 Geometry 유형 객체(GeometryElement)의 카테고리 추출하기 (2024.03.22 jbh)
                // List<Element> geometryList = rvElementList.Where(element => element.Category is not null
                //                                                          && element.get_Geometry(rvGeometryOpt) is not null
                //                                                          && element.GetType().Name != UpdaterHelper.ElementType)
                //                                           .Select(element => element)
                //                                           .ToList();

                List<CategoryInfoView> categoryInfoList = new List<CategoryInfoView>();


                foreach(Element element in geometryList)
                {
                    string categoryName       = element.Category.Name;
                    BuiltInCategory category  = element.Category.BuiltInCategory;

                    CategoryInfoView categoryInfo = new CategoryInfoView(categoryName, category);

                    // TODO : 카테고리 정보 리스트 객체 "categoryInfoList"에 Linq 확장 메서드 "Where" , "ToList" 사용해서
                    //        동일한 카테고리(BuiltInCategory Category) 갯수(Count)가 존재하는지 확인
                    int existCount = categoryInfoList.Where(catInfo => (int)catInfo.Category == (int)categoryInfo.Category)
                                                     .ToList()
                                                     .Count;

                    // 동일한 카테고리(BuiltInCategory Category)가 존재하지 않는 경우
                    // - 카테고리 정보 리스트 객체 "categoryInfoList"에 데이터 추가
                    if(existCount == (int)EnumCategoryInfo.NONE) categoryInfoList.Add(categoryInfo);



                    // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.25 jbh)
                    // ParameterSet paramSet   = element.Parameters;

                    // foreach(Parameter param in paramSet)
                    // {
                    //     // TODO : 메서드 "GetGroupTypeId", static 메서드 "LabelUtils.GetLabelForGroup" 사용해서 특정 객체에 속한 매개변수 그룹 이름 구하기 (2024.03.25 jbh)
                    //     ForgeTypeId paramGroup = param.Definition.GetGroupTypeId();
                    //     string paramGroupName  = LabelUtils.GetLabelForGroup(paramGroup);
                    // 
                    //     CategoryInfoView categoryInfo = new CategoryInfoView(categoryName, paramGroupName);
                    //     categoryInfoList.Add(categoryInfo);
                    // }
                }

                // 카테고리 정보 리스트 객체 "categoryInfoList" 카테고리 이름 순으로 정렬(OrderBy)
                List<CategoryInfoView> orderedCategoryInfoList = categoryInfoList.OrderBy(categoryInfo => categoryInfo.CategoryName)
                                                                                 .ToList();

                return orderedCategoryInfoList;

                // TODO : C# Linq 확장 메서드 Distinct() 사용해서 사용자 정의 클래스 CategoryInfoView 리스트 객체 categoryInfoList에서 
                //        익명 타입(Anonymous Type) 사용해서 카테고리 이름(categoryInfo.categoryName),
                //        카테고리 정보 (categoryInfo.category) 중복 제거 처리 (2024.03.25 jbh)
                // 참고 URL - https://developer-talk.tistory.com/560
                // var testList = categoryInfoList.Select(categoryInfo => new { categoryInfo.CategoryName, categoryInfo.Category })
                //                                .Distinct()
                //                                .OrderBy(categoryInfo => categoryInfo.CategoryName)
                //                                .ToList();
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetCategoryInfoList

        #region GetBuiltInCategory

        /// <summary>
        /// 메서드 파라미터로 받은 Enum 열거형 구조체 BuiltInCategory 카테고리 영어 이름과 매핑되는 BuiltInCategory 가져오기 
        /// </summary>
        public static BuiltInCategory GetBuiltInCategory(string rvCategoryName)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 1 단계 : BuiltInCategory 카테고리 데이터 가져오기
                Array builtInCategories = Enum.GetValues(typeof(BuiltInCategory));



                // 2 단계 : BuiltInCategory 중 중복된 것과 실제로 사용하지 않는 BuiltInCategory 목록 제거 
                // TODO : builtInCategories에 들어있는 BuiltInCategory 값들 중 BuiltInCategory.INVALID 처럼 실제로 사용하지 않는 것들을
                //        Linq 확장 메서드 "Where()"안에서 메서드 "Category.IsBuiltInCategoryValid(builtInCategory)" 사용해서 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://www.revitapidocs.com/2022/15f903ae-3cdf-52b0-4891-fa2d1002e481.htm

                // TODO : builtInCategories에 들어있는 BuiltInCategory 값들 중 중복 데이터가 존재하므로 Linq 확장 메서드 "Distinct()" 사용해서 중복 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://developer-talk.tistory.com/215
                List<BuiltInCategory> builtInCategoryList = builtInCategories.OfType<BuiltInCategory>()
                                                                             .Where(builtInCategory => true == Category.IsBuiltInCategoryValid(builtInCategory))
                                                                             .Select(builtInCategory => builtInCategory)
                                                                             .Distinct()
                                                                             .ToList();

                // 3 단계 : 메서드 파라미터 rvCategoryName 이름과 동일한 BuiltInCategory 찾기 
                BuiltInCategory builtInCategory = builtInCategoryList.Where(builtInCategory => LabelUtils.GetLabelFor(builtInCategory).Equals(rvCategoryName))
                                                                     .Select(builtInCategory => builtInCategory)
                                                                     .FirstOrDefault();

                //builtInCategoryList.ForEach(builtInCategory =>
                //                   {
                //                       ForgeTypeId forgeTypeId = Category.IsBuiltInCategoryValid()
                //                   });


                //var testList = ParamsManager.GetbuiltInCategoryNameList(builtInCategories);


                return builtInCategory;   // 마지막 단계 : BuiltInCategory 리턴 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 
            }
        }

        #endregion GetBuiltInCategory

        #region GetBuiltInCategory

        // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.03.26 jbh)
        /// <summary>
        /// 메서드 파라미터로 받은 Enum 열거형 구조체 BuiltInCategory 카테고리 영어 이름과 매핑되는 BuiltInCategory 가져오기 
        /// </summary>
        //public static BuiltInCategory GetBuiltInCategory(string rvCategoryName)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        // 메서드 Parse 호출 -> Enum 열거형 구조체 멤버변수 열거형 영어 이름(string) -> Enum 구조체 멤버변수 형변환
        //        BuiltInCategory builtInCategory = EnumParseExtensions<BuiltInCategory>.Parse(rvCategoryName);

        //        return builtInCategory;   // BuiltInCategory 리턴 
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달 
        //    }
        //}

        #endregion GetBuiltInCategory
    }
}
