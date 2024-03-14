using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using RevitUpdater.Common.LogManager;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;
using RevitUpdater.Common.UpdaterBase;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace RevitUpdater.Common.ParamsManager
{
    // TODO : 클래스 "ParamsManager.cs" 안에 static 메서드 구현 (2024.02.13 jbh)
    // 참고 URL - https://www.csharpstudy.com/CSharp/CSharp-static.aspx
    public class ParamsManager
    {
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
                ForgeTypeId forgeTypeId = ParameterUtils.GetParameterTypeId(pBuiltInParam);

                builtInParamName = LabelUtils.GetLabelForBuiltInParameter(forgeTypeId, LanguageType.Korean);   // BuiltInParameter 매개변수 이름 할당

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
                                                                                   && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_LIGHTING_PRESET_INDEX
                                                                                   && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_MULTIDAY_PRESET_INDEX
                                                                                   && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_SINGLEDAY_PRESET_INDEX
                                                                                   && builtInParam != BuiltInParameter.VIEW_SOLARSTUDY_STILL_PRESET_INDEX
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
                    ForgeTypeId forgeTypeId = ParameterUtils.GetParameterTypeId(builtInParam);

                    builtInParamName = string.Empty;
                    builtInParamName = LabelUtils.GetLabelForBuiltInParameter(forgeTypeId, LanguageType.Korean);   // BuiltInParameter 매개변수 이름 할당

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

        // TODO : BuiltInCategory 이름 리스트 가져오기 static 메서드 "builtInCategoryNameList" 필요시 오류(오류 원인 - "BuiltInCategory 중 중복된 것과 실제로 사용하지 않는 BuiltInCategory 목록 제거") 보완 및 구현 예정 (2024.03.14 jbh)
        /// <summary>
        /// BuiltInCategory 이름 리스트 가져오기 
        /// </summary>
        public static List<string> builtInCategoryNameList(Array pBuiltInCategories)
        {
            string builtInCategoryName = string.Empty;              // BuiltInCategory 매개변수 이름 
            long builtInCategoryValue = 0;                         // BuiltInCategory 매개변수에 입력할 값

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {

                var test2 = LabelUtils.GetLabelFor(BuiltInCategory.OST_PipeFitting);   // 테스트 코드 

                // Revit 응용 프로그램 안에 내장된 BuiltInCategory를 가지고 Dictionary(key - builtInCategoryName / value - builtInCategoryValue) 만들기
                // BuiltInCategory Dictionary(key - name / value - value) 만들기
                // 참고 URL - https://forum.dynamobim.com/t/setting-built-in-parameter-by-using-a-variable-value/49466/2
                // 참고 2 URL - https://chat.openai.com/c/68245284-54a5-4fa7-acff-8e90c39e1931

                // (또 다른 예시) BuiltInParameter
                // 참고 URL - https://www.revitapidocs.com/2024/fb011c91-be7e-f737-28c7-3f1e1917a0e0.htm
                // 배열에서 List 형변환
                // 참고 URL - https://dongyeopblog.wordpress.com/2016/08/22/c-%EC%96%B4%EB%A0%88%EC%9D%B4%EB%A5%BC-%EB%A6%AC%EC%8A%A4%ED%8A%B8%EB%A1%9C-%EB%B3%80%ED%99%98%ED%95%98%EA%B8%B0-system-array-to-list/

                // 1 단계 : BuiltInCategory 중 중복된 것과 실제로 사용하지 않는 BuiltInCategory 목록 제거 
                // TODO : pBuiltInCategories에 들어있는 BuiltInCategory 값들 중 BuiltInCategory.INVALID 처럼 실제로 사용하지 않는 것들을 Linq 확장 메서드 "Where()" 사용해서 데이터 제거 (2024.03.14 jbh)
                // TODO : pBuiltInCategories에 들어있는 BuiltInCategory 값들 중 중복 데이터가 존재하므로 Linq 확장 메서드 "Distinct()" 사용해서 중복 데이터 제거 (2024.03.14 jbh)
                // 참고 URL - https://developer-talk.tistory.com/215
                List<string> builtInCategoryNameList = pBuiltInCategories.OfType<BuiltInCategory>()
                                                                         .Where(builtInCategory
                                                                                => builtInCategory != BuiltInCategory.OST_StackedWalls_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_MassTags_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_MassSurface_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_MassFloor_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_Mass_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_WallRefPlanes_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_StickSymbols_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_RemovedGridSeg_Obsolete_IdInWrongRange
                                                                                && builtInCategory != BuiltInCategory.OST_StructuralTrussHiddenLines
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemTransitionHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemTerminationHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemRailHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemTopRailHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemHandRailBracketHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemHandRailHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemPanelBracketHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemPanelHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemBalusterHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemPostHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemSegmentHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_RailingSystemHiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_StairStringer2012HiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_StairTread2012HiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_StairLanding2012HiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_StairRun2012HiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_Stairs2012HiddenLines_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_OBSOLETE_ElemArrayHiddenLines
                                                                                && builtInCategory != BuiltInCategory.OST_StructuralFramingSystemHiddenLines_Obsolete
                                                                                && builtInCategory != BuiltInCategory.OST_SteelElementStale
                                                                                && builtInCategory != BuiltInCategory.OST_MEPAncillaries_Obsolete
                                                                                && builtInCategory != BuiltInCategory.OST_ELECTRICAL_AreaBasedLoads_ColorFill_Obsolete
                                                                                && builtInCategory != BuiltInCategory.OST_FabricationServiceElements
                                                                                && builtInCategory != BuiltInCategory.OST_NumberingSchemas
                                                                                && builtInCategory != BuiltInCategory.OST_SplitterProfile
                                                                                && builtInCategory != BuiltInCategory.OST_ElectricalCircuitTags
                                                                                && builtInCategory != BuiltInCategory.OST_DecalType
                                                                                && builtInCategory != BuiltInCategory.OST_DecalElement
                                                                                && builtInCategory != BuiltInCategory.OST_SpotSlopesSymbols
                                                                                && builtInCategory != BuiltInCategory.OST_SpotCoordinateSymbols
                                                                                && builtInCategory != BuiltInCategory.OST_StructuralConnectionHandlerTags_Deprecated
                                                                                && builtInCategory != BuiltInCategory.OST_HostFinTags
                                                                                && builtInCategory != BuiltInCategory.OST_Tags
                                                                                && builtInCategory != BuiltInCategory.OST_RepeatingDetailLines
                                                                                && builtInCategory != BuiltInCategory.OST_RampsIncomplete
                                                                                && builtInCategory != BuiltInCategory.OST_TrussDummy
                                                                                && builtInCategory != BuiltInCategory.OST_Automatic
                                                                                && builtInCategory != BuiltInCategory.OST_SiteRegion)
                                                                         .Select(builtInCategory => LabelUtils.GetLabelFor(builtInCategory))
                                                                         .Distinct()
                                                                         .ToList();


                return builtInCategoryNameList;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // TODO : 오류 발생시 상위 호출자 예외처리 전달 throw
            }
        }

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

                            ForgeTypeId unitTypeId = param.GetUnitTypeId();   // 메서드 "GetUnitTypeId" 사용해서 Revit API 내부에서 사용하는 단위 타입 아이디 (ForgeTypeId) 구하기 

                            var convertParamValue = UnitUtils.ConvertFromInternalUnits(doubleParamValue, unitTypeId);   // Revit API 내부에서 사용하는 단위 Feet -> Millimeters 단위 변환

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
                        TaskDialog.Show(UpdaterHelper.ErrorTitle, $"매개변수 값 입력 실패\r\n매개변수\r\n이름 - {rvParamName}\r\n값 - {rvParamValue}");
                    }
                });

                // 매개변수에 값 입력이 모두 실패한 경우 (리스트 객체 "setCompletedParameters"에 데이터가 존재하지 않는 경우)
                if (setCompletedParameters.Count.Equals((int)EnumExistParameters.NONE))
                {
                    TaskDialog.Show(UpdaterHelper.ErrorTitle, $"확인 요망!\r\n\r\n매개변수\r\n이름 - {rvParamName}\r\n값 입력 실패!\r\n담당자에게 문의하세요.");
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
    }
}
