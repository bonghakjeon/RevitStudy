using Serilog;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Models.HTSBase.MEPUpdater;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Common.Managers
{
    // TODO : 클래스 "ParamsManager.cs" 안에 static 메서드 구현 (2024.02.13 jbh)
    // 참고 URL - https://www.csharpstudy.com/CSharp/CSharp-static.aspx
    public class ParamsManager
    {
        #region GetBuiltInParameterName

        /// <summary>
        /// BuiltInParameter 이름 가져오기 
        /// </summary>
        public static string GetBuiltInParameterName(BuiltInParameter pBuiltInParam)
        {
            string builtInParamName = string.Empty;              // BuiltInParameter 매개변수 이름 
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : BuiltInParameter -> ForgeTypeId 구하기 (2024.03.14 jbh)
                //        아래 URL 주소와 연동된 PDF 문서 5 Page -> "ParameterUtils.GetParameterTypeId(BuiltInParameter)" 참고
                // 참고 URL - https://thebuildingcoder.typepad.com/files/revit_platform_api_changes_and_additions_2022.pdf
                // ForgeTypeId forgeTypeId = ParameterUtils.GetParameterTypeId(pBuiltInParam);

                

                // builtInParamName = LabelUtils.GetLabelForBuiltInParameter(forgeTypeId, LanguageType.Korean);   // BuiltInParameter 매개변수 이름 할당

                builtInParamName = LabelUtils.GetLabelFor(pBuiltInParam);

                return builtInParamName;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                // TODO : 오류 발생시 상위 호출자 예외처리 전달 throw 구현 (2024.01.29 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetBuiltInParameterName

        #region GetBuiltInParameterList

        /// <summary>
        /// BuiltInParameter 리스트 가져오기 
        /// </summary>
        public static List<BuiltInParamView> GetBuiltInParameterList(Array pBuiltInParameters)
        {
            string builtInParamName = string.Empty;              // BuiltInParameter 매개변수 이름 
            long builtInParamValue = 0;                         // BuiltInParameter 매개변수에 입력할 값

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Revit 응용 프로그램 안에 내장된 BuiltInParameter를 가지고 Dictionary(key - builtInNames / value - builtInValues) 만들기
                // BuiltInParameter Dictionary(key - name / value - value) 만들기
                // 참고 URL   - https://forum.dynamobim.com/t/setting-built-in-parameter-by-using-a-variable-value/49466/2
                // 참고 2 URL - https://chat.openai.com/c/68245284-54a5-4fa7-acff-8e90c39e1931

                // BuiltInParameter
                // 참고 URL - https://www.revitapidocs.com/2024/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm

                // 배열에서 List 형변환
                // 참고 URL - https://dongyeopblog.wordpress.com/2016/08/22/c-%EC%96%B4%EB%A0%88%EC%9D%B4%EB%A5%BC-%EB%A6%AC%EC%8A%A4%ED%8A%B8%EB%A1%9C-%EB%B3%80%ED%99%98%ED%95%98%EA%B8%B0-system-array-to-list/

                // 1 단계 : BuiltInParameter 매개변수 중 중복된 매개변수와 실제로 사용하지 않는 매개변수 목록 제거 
                // TODO : pBuiltInParameters에 들어있는 BuiltInParameter 값들 중 BuiltInParameter.INVALID 처럼 실제로 사용하지 않는 매개변수들을 Linq 확장 메서드 "Where()" 사용해서 매개변수 데이터 제거 (2024.03.14 jbh)
                // TODO : pBuiltInParameters에 들어있는 BuiltInParameter 값들 중 중복 매개변수 데이터가 존재하므로 Linq 확장 메서드 "Distinct()" 사용해서 중복 매개변수 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://developer-talk.tistory.com/215
                List<BuiltInParameter> builtInParamList = pBuiltInParameters.OfType<BuiltInParameter>()
                                                                            .Where(builtInParam
                                                                                   => builtInParam != BuiltInParameter.INVALID
                                                                                   && builtInParam != BuiltInParameter.STRUCTURAL_CONNECTION_SYMBOL
                                                                                   && builtInParam != BuiltInParameter.STAIRS_LANDINGTYPE_TREADRISER_TYPE
                                                                                   && builtInParam != BuiltInParameter.SPACE_PEOPLE_ACTIVITY_LEVEL_PARAM
                                                                                   && builtInParam != BuiltInParameter.RBS_DUCT_FITTING_LOSS_TABLE_PARAM
                                                                                   && builtInParam != BuiltInParameter.SPACING_JUSTIFICATION
                                                                                   && builtInParam != BuiltInParameter.SPACING_APPEND
                                                                                   // && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_LIGHTING_PRESET_INDEX
                                                                                   // && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_MULTIDAY_PRESET_INDEX
                                                                                   // && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_SINGLEDAY_PRESET_INDEX
                                                                                   // && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_STILL_PRESET_INDEX
                                                                                   && builtInParam != BuiltInParameter.SURFACE_PATTERN_ID_PARAM)
                                                                            .Distinct()
                                                                            .ToList();


                // 2 단계 : BuiltInParameter 매개변수 데이터 담을 리스트 객체 "paramDatas" 생성
                List<BuiltInParamView> paramDatas = new List<BuiltInParamView>();

                // 3 단계 : foreach 반복문 사용해서 BuiltInParameter 매개변수들의 이름 및 값 구해서 리스트 객체 "paramDatas"에 데이터 추가 
                foreach (BuiltInParameter builtInParam in builtInParamList)
                {
                    // TODO : BuiltInParameter -> ForgeTypeId 구하기 (2024.02.07 jbh)
                    //        아래 URL 주소와 연동된 PDF 문서 5 Page -> "ParameterUtils.GetParameterTypeId(BuiltInParameter)" 참고
                    // 참고 URL - https://thebuildingcoder.typepad.com/files/revit_platform_api_changes_and_additions_2022.pdf
                    // ForgeTypeId forgeTypeId = ParameterUtils.GetParameterTypeId(builtInParam);

                    builtInParamName = string.Empty;
                    // builtInParamName = LabelUtils.GetLabelForBuiltInParameter(forgeTypeId, LanguageType.Korean);   // BuiltInParameter 매개변수 이름 할당

                    builtInParamName = LabelUtils.GetLabelFor(builtInParam);


                    builtInParamValue = 0;
                    builtInParamValue = (long)builtInParam;   // BuiltInParameter 매개변수에 입력할 값 할당 

                    BuiltInParamView builtInParamData = new BuiltInParamView(builtInParamName, builtInParamValue);


                    paramDatas.Add(builtInParamData);   // 리스트 객체 "paramDatas"에 데이터 "builtInParamData" 추가

                    // TODO : 아래 주석친 테스트 필요시 참고 (2024.03.12 jbh)
                    // test.name = string.Empty;
                    // test.name = LabelUtils.GetLabelFor(builtInParam);
                    // builtInParamData.paramName = LabelUtils.GetLabelForBuiltInParameter(forgeTypeId, LanguageType.Korean);   // BuiltInParameter 매개변수 이름 할당
                }

                return paramDatas;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion GetBuiltInParameterList

        #region GetMEPUpdaterParameterList

        /// <summary>
        /// MEP Updater 매개변수 목록 조회 
        /// </summary>
        public static List<Updater_Parameters> GetMEPUpdaterParameterList(string pDllFilePath)
        {
            // int index = 0;                                       // 어셈블리(.dll) 파일의 상위 폴더 인덱스 
            string parentDirPath = string.Empty;                 // 어셈블리(.dll) 파일의 상위 부모 폴더 경로 
            string jsonPath   = string.Empty;                    // JSON 파일 경로

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 추후 필요시 해당 메서드 "GetMEPUpdaterParameterList" 안에 로그 기록 추가하기 (2024.04.03 jbh)

                // TODO : JSON 파일 "Updater_Parameters.json" 인코딩 방식 유니코드 UTF-8 방식 변환 후 다른 이름으로 저장 진행(2024.04.03 jbh)
                // 참고 URL - https://blog.naver.com/sehoon95/220826740411
                // 3. 해당 상위 디렉토리 하위 폴더 "Json" 안에 존재하는 Json 파일 "Updater_Parameters.json" 경로 가져오기 
                // jsonPath = @"D:\bhjeon\HTSBIM2019\HTSBIM2019\Json\Updater_Parameters.json";
                // jsonPath = dirPath + @"\Json\Updater_Parameters.json";

                StringBuilder sb = new StringBuilder(pDllFilePath);
                sb.Append(HTSHelper.MEPUpdaterJsonFilePath);
                // sb.Append(@"\Json");
                // sb.Append(@"\Updater_Parameters.json");
                // sb.Append(@"\Test.json");

                jsonPath = sb.ToString();

                // 2 단계 : JSON 데이터 읽어오기 
                // TODO : JSON 데이터 -> byte[] 배열 SerializeToUtf8Bytes() 변환 구현 (2024.01.24 jbh)
                // 참고 URL - https://developer-talk.tistory.com/213
                string json = File.ReadAllText(jsonPath);

                // 3 단계 : NUGET 패키지 "Newtonsoft" 사용해서 2단계에서 읽어온 JSON 데이터를 사용자 정의 클래스 "Updater_Parameters" 리스트 객체 "datas"로 변환 (2024.04.04 jbh)
                // 참고 URL   - https://stackoverflow.com/questions/55998104/how-to-convert-jarray-to-list
                // 참고 2 URL - https://s-engineer.tistory.com/337
                // 참고 3 URL - https://chat.openai.com/c/c16076e7-951e-400f-83a8-4a5e336d6e96
                JObject jObj = JObject.Parse(json);
                JArray jArray = jObj[HTSHelper.UPDATER_ParameterList].Value<JArray>();
                List<Updater_Parameters> updaterParameters = jArray.ToObject<List<Updater_Parameters>>();

                return updaterParameters;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion GetMEPUpdaterParameterList

        #region GetSharedParameterList

        /// <summary>
        /// 공유 매개변수 리스트 가져오기 
        /// </summary>
        public static List<SharedParameterElement> GetSharedParameterList(Document rvDoc)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Revit 문서(rvDoc) 안에 포함된 객체(Element)만 필터링 처리
                // 주의사항 - 메서드 "WhereElementIsElementType", "WhereElementIsNotElementType" 둘 다 테스트해서 사용해 보고 둘 중 어느 것으로 사용할지 진행
                // FilteredElementCollector collector = new FilteredElementCollector(rvDoc).WhereElementIsElementType();         // 객체 유형(Element Types)인 객체(Element)만 필터링 
                FilteredElementCollector collector = new FilteredElementCollector(rvDoc).WhereElementIsNotElementType();   // 객체 유형(Element Types)이 아닌 객체(Element)만 필터링



                // 공유 매개변수 리스트 객체 "sharedParameterList"에 데이터 할당 
                // 참고 URL - https://bimextension.com/retrieve-all-shared-parameters-in-a-revit-project/
                List<SharedParameterElement> sharedParameterList = collector.OfClass(typeof(SharedParameterElement))
                                                                            .Cast<SharedParameterElement>()
                                                                            .ToList();

                return sharedParameterList;   // 마지막 단계 : 공유 매개변수 리스트 객체 "sharedParameterList" 반환
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion GetSharedParameterList

        #region CreateParameter

        /// <summary>
        /// 활성화된 Revit 문서 카테고리 하위 MEPUpdater 전용 매개변수 생성(추가) 
        /// 메서드 파라미터 "rvDataType" 넣어서 새로 구현하기 
        /// </summary>
        public static void CreateParameter(Document rvDoc, CategorySet rvCatSet, string rvParamName, ParameterType rvParamType, bool pUserModifiable)
        {
            // Autodesk Revit에는 공유 매개변수 파일이 없으므로 공유 매개변수 파일 객체에 액세스 하기 전에
            // string 클래스 객체 "oriFile"에 공유 매개변수 파일 전체 경로 프로퍼티 "SharedParametersFilename" 할당하기 
            // 참고 URL - https://www.revitapidocs.com/2018/d6b43cc8-9521-9ab3-569e-5e0c7a0205a8.htm
            string oriFile = rvDoc.Application.SharedParametersFilename;

            string tempFile = Path.GetTempFileName() + HTSHelper.TextFile;  // 임시 파일의 전체 파일 경로 반환 및 string 클래스 객체 "tempFile"에 할당 

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2024.01.26 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : using 구문 사용해서 임시 파일 생성(File.Create(tempFile))  (2024.01.24 jbh)
                // 참고 URL - https://www.csharpstudy.com/latest/CS8-using.aspx
                // 임시 파일(tempFile) 생성
                using (File.Create(tempFile))
                {

                }  // 중괄호 { } 범위 벗어난 여기서 Dispose() 호출됨.


                // 새로운 공유 매개변수 정의를 생성 하기 위해 ExternalDefinitionCreationOptions 옵션 클래스 객체 opt 생성 
                // ExternalDefinitionCreationOptions opt = new ExternalDefinitionCreationOptions(p_ParamName, SpecTypeId.String.Text);
                // 임시 파일 경로 값(tempFile)을 공유 매개변수 파일 전체 경로 프로퍼티 "SharedParametersFilename" 에 할당하기 
                rvDoc.Application.SharedParametersFilename = tempFile;

                // 1 단계 : app.OpenSharedParameterFile() 메서드를 통해 공유 매개변수 파일 열기 
                //          DefinitionFile 클래스 객체(defFile)는 디스크에 존재하는 공유 매개변수 파일 의미
                // 참고 URL - https://www.revitapidocs.com/2019/3a345800-4ee3-04ef-5a67-94a1b9840c27.htm
                // 메서드 "OpenSharedParameterFile" 사용해서 디스크에 존재하는 공유 매개변수 파일 열기 
                // 참고 URL - https://www.revitapidocs.com/2016/e7698cec-f599-3078-f2e2-84e8d90f2b44.htm
                DefinitionFile defFile = rvDoc.Application.OpenSharedParameterFile();

                // 2 단계 : DefinitionGroup 클래스 객체 "defGroup" 생성 및 새로운 공유 매개변수 그룹 "TempDefintionGroup" 할당
                //          새로운 공유 매개변수 그룹이란? 디스크에 새로운 공유 매개변수 정의를 보관하는데 사용되는 컨테이너를 의미함.
                DefinitionGroup defGroup = defFile.Groups.Create("TempDefintionGroup");


                // 새로운 공유 매개변수 정의를 생성 하기 위해 ExternalDefinitionCreationOptions 옵션 클래스 객체 opt 생성 
                //ExternalDefinitionCreationOptions opt = new ExternalDefinitionCreationOptions(rvParamName, SpecTypeId.String.Text);
                ExternalDefinitionCreationOptions opt = new ExternalDefinitionCreationOptions(rvParamName, rvParamType);
                //opt.Visible = visible;
                opt.Visible = true;        // 새로운 공유 매개변수가 사용자에게 표시 되도록 true 할당

                // TODO : 프로퍼티 "UserModifiable" 사용해서 사용자가 새로 생성한 공유 매개변수에 매핑된 값을 화면상에서 수정 여부 설정 구현 (2024.04.04 jbh)
                // 참고 URL - https://www.revitapidocs.com/2018/c0343d88-ea6f-f718-2828-7970c15e4a9e.htm
                // 참고 2 URL - https://www.revitapidocs.com/2018/99e14a83-f976-2465-6464-ed3f8a159000.htm
                opt.UserModifiable = pUserModifiable;     


                // 3 단계 : 외부 정의 클래스 ExternalDefinition 객체 exDef에 defGroup.Definitions.Create(opt) as ExternalDefinition 할당 
                //          위에서 생성한 공유 매개변수 정의 보관 객체 "defGroup"에 속하는 프로퍼티 "Definitions" 사용
                //          -> 지정된 옵션(opt) 사용해서 새로운 외부 매개변수 정의 생성 (defGroup.Definitions.Create(opt))
                ExternalDefinition exDef = defGroup.Definitions.Create(opt) as ExternalDefinition;

                // app.SharedParametersFilename에 oriFile 할당 후 새로 생성한 공유 매개변수 파일 전체 경로가 존재하는지 확인
                rvDoc.Application.SharedParametersFilename = oriFile;
                File.Delete(tempFile);    // 임시 파일(tempFile) 삭제 


                // 4 단계 : ElementBinding 클래스 객체 "binding" null 초기화 
                ElementBinding binding = null;

                // 5 단계 : 객체 "binding"에 메서드 "NewTypeBinding" 또는 "NewInstanceBinding" 사용해서 새로운 공유 매개변수가 바인딩 될 객체 생성 
                // 메서드 "NewTypeBinding" 사용시 탭 "관리" -> 버튼 "프로젝트 매개변수" -> 매개변수 클릭 (예)AIS_걸레받이마감
                // 팝업화면 "매개변수 특성" 출력 -> 항목 "매개변수 데이터"에 속하는  매개변수 타입(AIS_Parameters.json - "paramType")은 "유형"로 체크된다.
                // binding = rvDoc.Application.Create.NewTypeBinding(rvCatSet);   

                // 메서드 "NewInstanceBinding" 사용시 탭 "관리" -> 버튼 "프로젝트 매개변수" -> 매개변수 클릭 (예)AIS_걸레받이마감
                // 팝업화면 "매개변수 특성" 출력 -> 항목 "매개변수 데이터"에 속하는 매개변수 타입(AIS_Parameters.json - "paramType")은 "인스턴스"로 체크된다.
                binding = rvDoc.Application.Create.NewInstanceBinding(rvCatSet);

                // TODO : 점심 먹고나서 아래 부터 로직 분석 진행하기 (2024.01.24 jbh)
                // 6 단계 : Document.ParameterBindings 개체를 사용 문서에 새로운 공유 매개변수 바인딩 및 정의 추가 (map.Insert(exDef, binding))
                // 새로운 공유 매개변수 바인딩은 매개변수 정의가 하나 이상의 카테고리 내의 요소에 바인딩되는 방식 의미
                // BindingMap map = (new UIApplication(rvApp)).ActiveUIDocument.Document.ParameterBindings;
                // return map.Insert(exDef, binding);
                BindingMap map = rvDoc.ParameterBindings;

                // 7 단계 : 카테고리셋 하위의 카테고리에 세움터 파라미터 (속성명, 속성값) 추가
                //          (예) 속성명 "AIS_분류코드", 속성값 "L지상"(타입 - 코드(== 텍스트))
                // return map.Insert(exDef, binding);
                map.Insert(exDef, binding);
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion CreateParameter

        #region SetParametersValue

        /// <summary>
        /// 매개변수에 값 입력하기
        /// </summary>
        public static bool SetParametersValue(List<Element> pElementList, string rvParamName, string rvParamValue)
        {
            bool bResult = false;                                // 매개변수에 입력할 값 할당 완료 여부 false로 초기화

            List<SetParamInfoView> setCompletedParameters = new List<SetParamInfoView>();     // 매개변수 값 할당 완료된 객체(이름, 값) 리스트 객체 생성 

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                // 1 단계 : 매개변수 리스트 구하기 
                List<Parameter> targetParameters = pElementList.FindAll(element => element.LookupParameter(rvParamName) is not null)
                                                               .Select(element => element.LookupParameter(rvParamName))
                                                               .ToList();

                // 2 단계 : 매개변수가 존재하지 않는 경우 
                if (targetParameters.Count.Equals((int)EnumExistParameters.NONE))
                {
                    // TODO : 테스트 하면서 추후 값을 입력하려는 매개변수가 존재하지 않는 경우 메시지 박스 (TaskDialog.Show)로 출력할지 아니면 오류 처리 (throw new Exception)로 진행할 지 결정 후 로직 다시 수정하기 (2024.03.14 jbh)
                    // TaskDialog.Show(AABIMHelper.NoticeTitle, $"매개변수 {rvParamName}가 존재하지 않습니다.\r\n다시 선택하시기 바랍니다.");
                    // return;
                    throw new Exception($"Revit 문서의 모든 객체(Element)에\r\n매개변수 - {rvParamName}\r\n이/가 존재하지 않습니다.\r\n다시 확인 바랍니다.");
                }

                // TODO : 그리디 알고리즘(욕심쟁이 탐욕법) 참고해서 foreach 문에서 리스트 "targetParameters"에 속한 요소(매개변수)를 방문하여
                //        해당 매개변수(rvParamName)에 값(rvParamValue) 입력하기 
                // 그리디 알고리즘
                // 유튜브
                // 참고 URL   - https://youtu.be/5OYlS2QQMPA?si=2Q5uu_kxwEnXp-gm
                // 참고 2 URL - https://youtu.be/_TG0hVYJ6D8?si=FSuaEYrW7t-Bou3d

                // 3 단계 : foreach 문에서 리스트 "targetParameters"에 속한 요소(매개변수) 방문 
                targetParameters.ForEach(param => {
                    // 4 단계 : 매개변수의 값 자료형 찾아서 메서드 파라미터 rvParamValue를 형변환(casting) 및 해당 매개변수(rvParamName와 동일한 이름)에 값 입력하기
                    switch (param.StorageType)
                    {
                        case StorageType.Integer:   // "dataType": "예/아니요" 인 경우 
                            int intParamValue = Int32.Parse(rvParamValue);
                            bResult = param.Set(intParamValue);   // 매개변수에 값 입력

                            break;

                        case StorageType.Double:    // "dataType": "번호" 인 경우
                            double doubleParamValue = Double.Parse(rvParamValue);

                            // ForgeTypeId unitTypeId = param.GetUnitTypeId();   // 메서드 "GetUnitTypeId" 사용해서 Revit API 내부에서 사용하는 단위 타입 아이디 (ForgeTypeId) 구하기 
                            // var convertParamValue = UnitUtils.ConvertFromInternalUnits(doubleParamValue, unitTypeId);   // Revit API 내부에서 사용하는 단위 Feet -> Millimeters 단위 변환

                            var convertParamValue = UnitUtils.ConvertFromInternalUnits(doubleParamValue, DisplayUnitType.DUT_MILLIMETERS);   // Revit API 내부에서 사용하는 단위 Feet -> Millimeters 단위 변환
                            bResult = param.Set(convertParamValue);   // 매개변수에 값 입력

                            break;

                        case StorageType.String:    // "dataType": "문자" 인 경우 
                            bResult = param.Set(rvParamValue);   // 매개변수에 값 입력

                            break;

                            // TODO : 매개변수의 값 자료형에 "StorageType.ElementId", "StorageType.None" 추가될 경우 아래 case : 레이블 구현 예정 (2024.03.12 jbh)
                            // case StorageType.ElementId:   // ElementId인 경우 
                            //     ElementId elementId = testParam.AsElementId();
                            //     bResult = param.Set(elementId);
                            //     break;
                            // case StorageType.None:   // invalid인 경우 
                            //     // throw new Exception("invalid 값 복사 오류");
                            //     break;
                    }

                    // 매개변수에 값 입력 완료한 경우 
                    if (true == bResult)
                    {
                        SetParamInfoView setCompletedParameter = new SetParamInfoView(rvParamName, rvParamValue);

                        setCompletedParameters.Add(setCompletedParameter);
                    }
                    // TODO : 추후 필요시 매개변수에 값 입력 실패한 경우 로그 및 메시지에 매개변수 값 입력 실패한 객체 이름 추가 여부 확인 후 추가 예정 (2024.03.14 jbh)
                    // 매개변수에 값 입력 실패한 경우 
                    else
                    {
                        Log.Error(Logger.GetMethodPath(currentMethod) + $"매개변수 값 입력 실패\r\n매개변수\r\n이름 - {rvParamName}\r\n값 - {rvParamValue}");
                        TaskDialog.Show(HTSHelper.ErrorTitle, $"매개변수 값 입력 실패\r\n매개변수\r\n이름 - {rvParamName}\r\n값 - {rvParamValue}");
                    }
                });

                // 매개변수에 값 입력이 모두 실패한 경우 (리스트 객체 "setCompletedParameters"에 데이터가 존재하지 않는 경우)
                if (setCompletedParameters.Count.Equals((int)EnumExistParameters.NONE))
                {
                    TaskDialog.Show(HTSHelper.ErrorTitle, $"확인 요망!\r\n\r\n매개변수\r\n이름 - {rvParamName}\r\n값 입력 실패!\r\n담당자에게 문의하세요.");
                    throw new Exception($"확인 요망!\r\n\r\n매개변수\r\n이름 - {rvParamName}\r\n값 입력 실패!\r\n담당자에게 문의하세요.");
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + $"매개변수 값 입력 완료\r\n\r\n매개변수\r\n이름 - {rvParamName}\r\n값 - {rvParamValue}");

                // TODO : 아래 테스트용 결과 메시지 필요시 사용 예정 (2024.02.21 jbh) 
                // TaskDialog.Show(UpdaterHelper.CompletedTitle, $"매개변수 값 입력 완료\r\n\r\n매개변수\r\n이름 - {rvParamName}\r\n값 - {rvParamValue}");

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                return false;
            }
        }

        #endregion SetParametersValue

        #region ClashCheck_InRealTime

        // TODO : 실시간 간섭 체크 메서드 "ClashCheck_InRealTime" 필요시 구현 예정 (2024.03.13 jbh)
        // 참고 소스 파일 - "RevitBox2023" -> 폴더 "MEPBox" -> CreashMEPCopy.cs
        // 참고 메서드 - CreashMEPCopy.cs -> 실시간 간섭 체크 메서드 "CreashChkNoneCopy" 
        /// <summary>
        /// 실시간 간섭 체크
        /// </summary>
        public static bool ClashCheck_InRealTime(Document rvDoc, Element pElement)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                //List<Solid>

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                // throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
                return false;
            }
        }

        #endregion ClashCheck_InRealTime
    }
}
