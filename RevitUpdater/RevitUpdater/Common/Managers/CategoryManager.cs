using Serilog;

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;

using Autodesk.Revit.DB;

namespace RevitUpdater.Common.Managers
{
    public class CategoryManager
    {
        #region GetCategoryInfoList

        /// <summary>
        /// Geometry 유형 객체(GeometryElement)에 속한 카테고리 정보 리스트 가져오기 
        /// </summary>
        public static List<CategoryInfoView> GetCategoryInfoList(List<Element> rvElementList, Options rvGeometryOpt)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 활성화된 Revit 문서에 존재하는 Geometry 유형 객체(GeometryElement)의 카테고리 추출하기 (2024.03.22 jbh)
                List<Element> geometryList = rvElementList.Where(element => element.Category is not null
                                                                         && element.get_Geometry(rvGeometryOpt) is not null
                                                                         && element.GetType().Name != UpdaterHelper.ElementType)
                                                          .Select(element => element)
                                                          .ToList();

                List<CategoryInfoView> categoryInfoList = new List<CategoryInfoView>();


                foreach (Element element in geometryList)
                {
                    string mainCategoryName       = element.Category.Name;
                    BuiltInCategory mainCategory  = element.Category.BuiltInCategory;

                    CategoryInfoView categoryInfo = new CategoryInfoView(mainCategoryName, mainCategory);

                    // TODO : 카테고리 정보 리스트 객체 "categoryInfoList"에 Linq 확장 메서드 "Where" , "ToList" 사용해서
                    //        동일한 카테고리(BuiltInCategory mainCategory) 갯수(Count)가 존재하는지 확인
                    int existCount = categoryInfoList.Where(catInfo => (int)catInfo.mainCategory == (int)categoryInfo.mainCategory)
                                                     .ToList()
                                                     .Count;

                    // 동일한 카테고리(BuiltInCategory mainCategory)가 존재하지 않는 경우
                    // - 카테고리 정보 리스트 객체 "categoryInfoList"에 데이터 추가
                    if (existCount == (int)EnumCategoryInfo.NONE) categoryInfoList.Add(categoryInfo);



                    // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.25 jbh)
                    // ParameterSet paramSet   = element.Parameters;

                    // foreach (Parameter param in paramSet)
                    // {
                    //     // TODO : 메서드 "GetGroupTypeId", static 메서드 "LabelUtils.GetLabelForGroup" 사용해서 특정 객체에 속한 매개변수 그룹 이름 구하기 (2024.03.25 jbh)
                    //     ForgeTypeId paramGroup = param.Definition.GetGroupTypeId();
                    //     string paramGroupName  = LabelUtils.GetLabelForGroup(paramGroup);
                    // 
                    //     CategoryInfoView categoryInfo = new CategoryInfoView(mainCategoryName, paramGroupName);
                    //     categoryInfoList.Add(categoryInfo);
                    // }
                }

                // 카테고리 정보 리스트 객체 "categoryInfoList" 메인 카테고리 이름 순으로 정렬(OrderBy)
                List<CategoryInfoView> orderedCategoryInfoList = categoryInfoList.OrderBy(categoryInfo => categoryInfo.mainCategoryName)
                                                                                 .ToList();

                return orderedCategoryInfoList;

                // TODO : C# Linq 확장 메서드 Distinct() 사용해서 사용자 정의 클래스 CategoryInfoView 리스트 객체 categoryInfoList에서 
                //        익명 타입(Anonymous Type) 사용해서 메인 카테고리 이름(categoryInfo.mainCategoryName),
                //        메인 카테고리 정보 (categoryInfo.mainCategory) 중복 제거 처리 (2024.03.25 jbh)
                // 참고 URL - https://developer-talk.tistory.com/560
                // var testList = categoryInfoList.Select(categoryInfo => new { categoryInfo.mainCategoryName, categoryInfo.mainCategory })
                //                                .Distinct()
                //                                .OrderBy(categoryInfo => categoryInfo.mainCategoryName)
                //                                .ToList();
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetCategoryInfoList
    }
}
