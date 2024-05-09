using Serilog;

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Models.HTSBase.MEPUpdater;
//using HTSBIM2019.Common.Extensions;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace HTSBIM2019.Common.Managers
{
    public class CategoryManager
    {
        #region GetCategoryName

        /// <summary>
        /// BuiltInCategory 이름 가져오기 
        /// </summary>
        public static string GetCategoryName(Document rvDoc, BuiltInCategory rvBuiltInCategory)
        {
            string builtInCategoryName = string.Empty;           // BuiltInCategory 이름

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : BuiltInCategory 열거형 구조체 객체 builtInCategory의 이름 가져오기 (2024.04.01 jbh)
                // 참고 URL - https://chat.openai.com/c/32ce8d83-d39a-48d7-af9a-44c408b64fe0
                // string builtInCategoryName = LabelUtils.GetLabelFor(builtInCategory);   // BuiltInCategory 이름 가져오기 
                Categories categories = rvDoc.Settings.Categories;

                Category category = categories.get_Item(rvBuiltInCategory);

                builtInCategoryName = category.Name;   // BuiltInCategory 이름 가져오기 

                // builtInCategoryName = LabelUtils.GetLabelFor(builtInCategory);   // BuiltInCategory 이름 가져오기 

                return builtInCategoryName;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetCategoryName

        #region GetCategory

        /// <summary>
        /// 카테고리 이름에 맞는 BuiltInCategory 가져오기 
        /// </summary>
        public static BuiltInCategory GetCategory(List<Category> rvCategoryList, string rvCategoryName)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // BuiltInCategory 정보 리스트에서 카테고리 이름에 맞는 BuiltInCategory 데이터 중 시퀀스 첫번째 요소 추출하기 (2024.04.19 jbh)
                // 참고 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-C-LINQ-First-FirstOrDefault-Single-SingleOrDefault-%EC%B0%A8%EC%9D%B4%EC%A0%90
                BuiltInCategory builtInCategory = rvCategoryList.Where(category => category.Name.Equals(rvCategoryName))
                                                                .Select(category => (BuiltInCategory)category.Id.IntegerValue)
                                                                .FirstOrDefault();   // 시퀀스 첫번째 요소 추출 

                return builtInCategory;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        /// <summary>
        /// 카테고리 이름에 맞는 BuiltInCategory 가져오기 
        /// </summary>
        // public static BuiltInCategory GetCategory(Document rvDoc, string rvCategoryName)
        // {
        //     var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록
        // 
        //     try
        //     {
        //         // 1 단계 : Revit 문서 내에 내장된 BuiltInCategory 정보 리스트로 가져오기 
        //         // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.oftype?view=net-8.0
        //         // 참고 2 URL - https://chopstick-91.tistory.com/25
        //         List<Category> categories = rvDoc.Settings.Categories.OfType<Category>().ToList();
        // 
        //         // 2 단계 : BuiltInCategory 정보 리스트에서 카테고리 이름에 맞는 BuiltInCategory 데이터 중 시퀀스 첫번째 요소 추출하기 (2024.04.19 jbh)
        //         // 참고 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-C-LINQ-First-FirstOrDefault-Single-SingleOrDefault-%EC%B0%A8%EC%9D%B4%EC%A0%90
        //         BuiltInCategory builtInCategory = categories.Where(category => category.Name.Equals(rvCategoryName))
        //                                                     .Select(category => (BuiltInCategory)category.Id.IntegerValue)
        //                                                     .FirstOrDefault();   // 시퀀스 첫번째 요소 추출 
        // 
        //         return builtInCategory;
        //     }
        //     catch(Exception ex)
        //     {
        //         Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //         throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
        //     }
        // }


        #endregion GetCategory

        #region GetCategoryInfoList

        /// <summary>
        /// BuiltInCategory 정보 리스트 가져오기 
        /// </summary>
        // public static List<CategoryInfoView> GetCategoryInfoList(FilteredElementCollector rvCollector, Document rvDoc)
        //public static List<CategoryInfoView> GetCategoryInfoList(Document rvDoc)
        public static List<CategoryInfoView> GetCategoryInfoList(List<Category> pCategories)
        {
            string categoryName = string.Empty;                  // BuiltInCategory 이름
            int existCount = 0;                                  // 동일 카테고리(BuiltInCategory Category) 갯수(Count) 존재 여부 

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "BuiltInCategory 정보 리스트 가져오기 시작");

                // 1 단계 : Revit 문서 내에 내장된 BuiltInCategory 정보 리스트로 가져오기 
                // Categories categories = rvDoc.Settings.Categories;
                // List<Category> categories = rvDoc.Settings.Categories.OfType<Category>().ToList();
                // List<BuiltInCategory> builtIns = rvDoc.Settings.Categories.OfType<BuiltInCategory>().ToList();

                // 2 단계 : BuiltInCategory 매개변수 데이터 가져오기
                Array builtInCategories = Enum.GetValues(typeof(BuiltInCategory));
                // Array builtInCategoryNames = Enum.GetNames(typeof(BuiltInCategory));

                // TODO : 아래 주석친 테스트 카테고리 리스트 testCategories 필요시 참고 
                // var testCategories = Enum.GetValues(typeof(BuiltInCategory)).OfType<BuiltInCategory>()
                //                                                             .Where(category => category.Equals(BuiltInCategory.OST_PipeCurves)
                //                                                                             || category.Equals(BuiltInCategory.OST_PipeInsulations)
                //                                                                             || category.Equals(BuiltInCategory.OST_PipeFitting)
                //                                                                             || category.Equals(BuiltInCategory.OST_PipeAccessory))
                //                                                             .ToList();

                List<CategoryInfoView> categoryInfoList = new List<CategoryInfoView>();

                foreach(BuiltInCategory builtInCategory in builtInCategories) 
                {
                    //Category category = categories.Where(category => category.Id.IntegerValue == (int)builtInCategory)
                    //                              .FirstOrDefault();

                    Category category = pCategories.Where(category => category.Id.IntegerValue == (int)builtInCategory)
                                                   .FirstOrDefault();

                    if (category is null) continue;   // 카테고리가 존재하지 않는 경우 continue

                    categoryName = string.Empty;
                    categoryName = category.Name;

                    CategoryInfoView categoryInfo = new CategoryInfoView(categoryName, builtInCategory);

                    categoryInfoList.Add(categoryInfo);

                    // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.04.15 jbh)
                    // TODO : 카테고리 정보 리스트 객체 "categoryInfoList"에 Linq 확장 메서드 "Where" , "ToList" 사용해서
                    //        동일한 카테고리(BuiltInCategory Category) 갯수(Count)가 존재하는지 확인
                    // existCount = categoryInfoList.Where(catInfo => (int)catInfo.Category == (int)categoryInfo.Category)
                    //                              .ToList()
                    //                              .Count;

                    // 동일한 카테고리(BuiltInCategory Category)가 존재하지 않는 경우
                    // - 카테고리 정보 리스트 객체 "categoryInfoList"에 데이터 추가
                    // if(existCount == (int)EnumCategoryInfo.NONE) categoryInfoList.Add(categoryInfo);
                }

                // 카테고리 정보 리스트 객체 "categoryInfoList" 카테고리 이름 순으로 정렬(OrderBy)
                List<CategoryInfoView> orderedCategoryInfoList = categoryInfoList// .Where(categoryInfo => false == string.IsNullOrWhiteSpace(categoryInfo.CategoryName))   // BuiltInCategory 이름이 존재하는 경우
                                                                                 // .Select(categoryInfo => categoryInfo)
                                                                                 .OrderBy(categoryInfo => categoryInfo.CategoryName)
                                                                                 .ToList();

                Log.Information(Logger.GetMethodPath(currentMethod) + "BuiltInCategory 정보 리스트 가져오기 완료");

                return orderedCategoryInfoList;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetCategoryInfoList

        #region GetCategoryInfoList

        /// <summary>
        /// Geometry 유형 객체(GeometryElement)에 속한 카테고리 정보 리스트 가져오기 
        /// </summary>
        //public static List<CategoryInfoView> GetCategoryInfoList(FilteredElementCollector rvCollector, List<Element> rvElementList, Options rvGeometryOpt)
        public static List<CategoryInfoView> GetCategoryInfoList(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        {
            string categoryName = string.Empty;                  // 카테고리 이름
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 활성화된 Revit 문서에 존재하는 Geometry 유형 객체(GeometryElement)의 카테고리 추출하기 (2024.03.22 jbh)
                List<Element> geometryList = rvCollector.Where(element => element.Category is not null
                                                                       && element.get_Geometry(rvGeometryOpt) is not null
                                                                       && element.GetType().Name != HTSHelper.ElementType)
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
                    categoryName = string.Empty;
                    categoryName = element.Category.Name;

                    // TODO : BuiltInCategory 열거형 구조체 객체 category 구하기 (2024.04.01 jbh)
                    // 참고 URL - https://chat.openai.com/c/32ce8d83-d39a-48d7-af9a-44c408b64fe0
                    // BuiltInCategory category = element.Category.BuiltInCategory;
                    BuiltInCategory category = (BuiltInCategory)element.Category.Id.IntegerValue;

                    CategoryInfoView categoryInfo = new CategoryInfoView(categoryName, category);

                    categoryInfoList.Add(categoryInfo);

                    // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.04.15 jbh)
                    // TODO : 카테고리 정보 리스트 객체 "categoryInfoList"에 Linq 확장 메서드 "Where" , "ToList" 사용해서
                    //        동일한 카테고리(BuiltInCategory Category) 갯수(Count)가 존재하는지 확인
                    // int existCount = categoryInfoList.Where(catInfo => (int)catInfo.Category == (int)categoryInfo.Category)
                    //                                  .ToList()
                    //                                  .Count;

                    // 동일한 카테고리(BuiltInCategory Category)가 존재하지 않는 경우
                    // - 카테고리 정보 리스트 객체 "categoryInfoList"에 데이터 추가
                    // if(existCount == (int)EnumCategoryInfo.NONE) categoryInfoList.Add(categoryInfo);



                    // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.25 jbh)
                    // ParameterSet paramSet = element.Parameters;

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
                //        익명 타입(Anonymous Type) 사용해서 카테고리 이름(categoryInfo.CategoryName),
                //        카테고리 정보 (categoryInfo.Category) 중복 제거 처리 (2024.03.25 jbh)
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

        #region CreateCategorySet

        /// <summary>
        /// 활성화 된 Revit 문서에 MEP Updater 전용 카테고리셋 생성 
        /// </summary>
        public static void CreateCategorySet(Document rvDoc)
        {
            string paramName = string.Empty;                     // MEP Updater 매개변수 이름
            ParameterType paramType = ParameterType.Invalid;     // 생성할 매개변수 자료형(유형)

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록하기

            try
            {

                TaskDialog.Show(HTSHelper.NoticeTitle, "테스트!!\r\nMEP 사용 기록 관리 매개변수 생성 완료!");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion CreateCategorySet

        #region CreateCategorySet

        /// <summary>
        /// 활성화 된 Revit 문서에 MEP Updater 전용 카테고리셋 생성 
        /// </summary>
        public static void CreateCategorySet(Document rvDoc, List<Updater_Parameters> pUpdaterParamList, LanguageType rvLanguageType)
        {
            string paramName = string.Empty;                        // MEP Updater 매개변수 이름
            ParameterType paramType = ParameterType.Invalid;        // 생성할 매개변수 자료형(유형)
            List<string> updaterCategorySet = new List<string>();   // pUpdaterParamList 리스트에서 언어 타입에 매핑되는 카테고리셋 리스트 

            var currentMethod = MethodBase.GetCurrentMethod();      // 로그 기록시 현재 실행 중인 메서드 위치 기록하기

            try
            {
                // Revit 응용 프로그램에서 활성화된 문서(파일) 경로 (rvDoc.PathName)
                // 참고 URL - https://chat.openai.com/c/36cea428-87b5-4c67-b799-abb50a7e45dd
                Log.Information(Logger.GetMethodPath(currentMethod) + $"Revit 문서 - {rvDoc.PathName} / MEP Updater 매개변수 데이터 생성 시작");


                // 1 단계 : 공유 매개변수 리스트 가져오기 
                // List<SharedParameterElement> sharedParameters = ParamsManager.GetSharedParameterList(rvDoc);

                // 1 단계 : 프로젝트 매개변수 이름 리스트 가져오기 (사용자가 삭제 처리한 프로젝트(공유) 매개변수 제외)
                List<string> projectParamNameList = ParamsManager.GetProjectParameterNameList(rvDoc);


                // Revit 응용 프로그램의 언어 타입(영문, 한글 등등...)에 따라 pUpdaterParamList 리스트에서 언어 타입에 매핑되는 카테고리셋 가져오기 
                // if () mepCategorySet = 
                // else mepCategorySet = 

                // switch ~ case 사용하여 Revit 응용 프로그램의 언어 타입(미국 영문, 영국 영문, 한글 등등...)에 따라
                // pUpdaterParamList 리스트에서 언어 타입에 매핑되는 카테고리셋 리스트 가져오기
                switch(rvLanguageType)
                {
                    case LanguageType.Korean:
                        updaterCategorySet = pUpdaterParamList.Select(updaterParam => updaterParam.KOR_CategorySet).FirstOrDefault();   // FirstOrDefault - 시퀀스 첫번째 요소 가져오기
                        break;
                    case LanguageType.English_USA:
                        updaterCategorySet = pUpdaterParamList.Select(updaterParam => updaterParam.ENU_CategorySet).FirstOrDefault();   // FirstOrDefault - 시퀀스 첫번째 요소 가져오기
                        break;
                    case LanguageType.English_GB:
                        updaterCategorySet = pUpdaterParamList.Select(updaterParam => updaterParam.ENG_CategorySet).FirstOrDefault();   // FirstOrDefault - 시퀀스 첫번째 요소 가져오기
                        break;
                    default:
                        throw new Exception("Revit 언어 타입(영문 또는 한글) 변경 후\r\n프로그램 재실행 해주시기 바랍니다.");
                }


                // 2 단계 : 반복문 사용해서 MEP Updater 매개변수 리스트에 존재하는 모든 매개변수 데이터 방문 
                foreach(var updaterParam in pUpdaterParamList)
                {
                    // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.04.04 jbh)
                    // MEP Updater 매개변수가 존재하는 경우 
                    // Parameter parameter = pAddElement.LookupParameter(updaterParam.paramName);
                    // if(parameter is not null) continue;

                    // 3 단계 : 새로운 카테고리 셋 객체 categorySet 생성 
                    CategorySet categorySet = rvDoc.Application.Create.NewCategorySet();

                    // 반복문 사용해서 카테고리 셋 하위에 존재하는 모든 카테고리 방문 
                    //foreach(var category in updaterParam.KOR_CategorySet)
                    foreach(var category in updaterCategorySet)
                    {
                        // category와 동일한 카테고리가 존재하는지 확인 
                        Category cat = rvDoc.Settings.Categories.get_Item(category);   // string 클래스 객체 "category"와 동일한 카테고리 가져오기 

                        // TODO : if 조건절에 != null 보다 빠른 is not null 연산자 사용 (2024.01.29 jbh)
                        // TODO : CategoryType.Model인 카테고리만 하위에 매개변수가 추가 가능 
                        // TODO : cat.CanAddSubcategory 값이 true면 카테고리 하위에 서브 카테고리 추가 가능 
                        // 참고 URL - https://husk321.tistory.com/405
                        if(cat is not null && cat.CategoryType == CategoryType.Model && cat.CanAddSubcategory) 
                            categorySet.Insert(cat);  // 카테고리 셋에 카테고리 데이터 추가 
                    }

                    // 4 단계 : 카테고리 하위에 추가하려는 MEP Updater 매개변수 이름 가져오기  
                    paramName = string.Empty;
                    paramName = updaterParam.ParamName;

                    paramType = ParameterType.Invalid;   // MEP Updater 매개변수 자료형(유형) 초기화

                    // 5 단계 : MEP Updater 매개변수 자료형(문자) 가져오기
                    if(updaterParam.DataType.Equals(HTSHelper.text)) paramType = ParameterType.Text;

                    else throw new Exception($"생성할 공유 매개변수 유형(T): {HTSHelper.text}아닌\r\n다른 유형으로 오입력!\r\n담당자에게 문의하세요.");

                    // TODO : 공유 매개변수와 프로젝트 매개변수 차이 확인하기 (2024.04.02 jbh) 
                    // 참고 URL - https://blog.naver.com/lewis_han/222226274359

                    // 6 단계 : 기존 공유 매개변수 리스트에 매개변수("객체 생성 날짜", "객체 생성자", "최종 수정 날짜", "최종 수정자") 추가 
                    // TODO : using System.Linq; 추가 및 Linq 확장 메서드 "Where" 사용해서 기존 프로젝트 매개변수 이름 리스트(projectParamNameList)에 새로이 추가 하고자 하는 매개변수 이름과 동일한 프로젝트 매개변수 존재 여부 확인 (2024.04.24 jbh) 
                    // 참고 URL - https://yangbengdictionary.tistory.com/3
                    // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.list-1.exists?view=net-8.0
                    // bool isExist = sharedParameters.Exists(sharedParam => sharedParam.Name.Equals(paramName));
                    // bool isExist = sharedParameters.Exists(sharedParam => sharedParam.Name.Equals(paramName));

                    bool isExist = projectParamNameList.Exists(projectParamName => projectParamName.Equals(paramName));

                    // 6 - 1 : 기존 공유 매개변수 리스트에 새로이 추가 하고자 하는 MEPUpdater 매개변수가 존재하지 않는 경우 
                    if(false == isExist)
                    {
                        CreateParamView createParam = new CreateParamView(categorySet, paramName, BuiltInParameterGroup.INVALID, paramType, false, true, true);

                        ParamsManager.CreateProjectParameter(rvDoc, createParam);   // MEPUpdater 매개변수 생성 (pUserModifiable = false 설정. 왜냐하면 사용자가 화면 상에서 매개변수에 매핑된 값 수정 불가 설정)

                        // var testCategories = Enum.GetValues(typeof(BuiltInCategory)).OfType<BuiltInCategory>()
                        //                                                             .Where(category => category.Equals(BuiltInCategory.OST_PipeCurves)
                        //                                                                             || category.Equals(BuiltInCategory.OST_PipeInsulations)
                        //                                                                             || category.Equals(BuiltInCategory.OST_PipeFitting)
                        //                                                                             || category.Equals(BuiltInCategory.OST_PipeAccessory))
                        //                                                             .ToList();


                        // ParamsManager.CreateProjectParameter(rvDoc, paramName, BuiltInParameterGroup.INVALID, paramType, testCategories, true, true);

                        // ParamsManager.CreateParameter(rvDoc, categorySet, paramName, paramType, false);  // MEPUpdater 매개변수 생성 (pUserModifiable = false 설정. 왜냐하면 사용자가 화면 상에서 매개변수에 매핑된 값 수정 불가 설정)
                        // ParamsManager.CreateParameter(rvDoc, createParam);       // MEPUpdater 매개변수 생성 (pUserModifiable = false 설정. 왜냐하면 사용자가 화면 상에서 매개변수에 매핑된 값 수정 불가 설정)
                    }


                    // TODO : 아래 주석친 코드 필요시 참고 (2024.04.04 jbh)
                    // 6 - 2 : 기존 공유 매개변수 리스트에 새로이 추가 하고자 하는 MEPUpdater 매개변수가 존재하는 경우 
                    // else Log.Information(HTSHelper.NoticeTitle, $"MEP Updater 매개변수 {paramName}\r\n공유 매개변수 목록 중복 추가 불가!");
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + $"Revit 문서 - {rvDoc.PathName} / MEP Updater 매개변수 데이터 생성 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion CreateCategorySet

        #region Sample

        #endregion Sample
    }
}
