using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Settings;
using HTSBIM2019.Models.HTSBase.MEPUpdater;
using HTSBIM2019.Common.Managers;

namespace HTSBIM2019.Utils.MEPUpdater
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.04.02 jbh)

    /// <summary>
    /// 1. MEP 사용 기록 관리 - Utils
    /// </summary>
    public class MEPUpdater : IUpdater
    {
        #region 프로퍼티

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        public UpdaterId Updater_Id { get; set; }

        // Modal VS Modaless 차이
        // 참고 URL   - https://blog.naver.com/PostView.naver?blogId=wlsdml1103&logNo=220512538948
        // 참고 2 URL - https://greensul.tistory.com/37
        /// <summary>
        /// Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        // TODO : 아래 주석친 프로퍼티 "Collector" 필요시 사용 예정 (2024.04.03 jbh)
        /// <summary>
        /// Revit 문서 안에 포함된 객체(Element)를 검색 및 필터링
        /// 참고 URL - https://www.revitapidocs.com/2024/263cf06b-98be-6f91-c4da-fb47d01688f3.htm
        /// </summary>
        // private FilteredElementCollector Collector { get; set; }

        /// <summary>
        /// 카테고리 정보 
        /// </summary>
        public CategoryInfoView CategoryInfo { get; set; }

        /// <summary>
        /// 카테고리 정보 리스트
        /// </summary>
        public List<CategoryInfoView> CategoryInfoList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// MEP Updater 매개변수 리스트
        /// </summary>
        //private List<Updater_Parameters> UpdaterParamList { get; set; } = new List<Updater_Parameters>();
        public List<Updater_Parameters> UpdaterParamList { get; set; } = new List<Updater_Parameters>();

        /// <summary>
        /// MEP Updater 매개변수("객체 생성 날짜", "객체 생성자") 리스트  
        /// </summary>
        //private List<Updater_Parameters> AddParamList { get; set; } = new List<Updater_Parameters>();
        public List<Updater_Parameters> AddParamList { get; set; } = new List<Updater_Parameters>();

        /// <summary>
        /// MEP Updater 매개변수("최종 수정 날짜", "최종 수정자") 리스트  
        /// </summary>
        //private List<Updater_Parameters> ModParamList { get; set; } = new List<Updater_Parameters>();
        public List<Updater_Parameters> ModParamList { get; set; } = new List<Updater_Parameters>();

        // TODO : 매개변수 값 입력 완료 여부 프로퍼티 "IsCompleted" 필요시 사용 예정 (2024.04.04 jbh)
        /// <summary>
        /// 매개변수 값 입력 완료 여부 
        /// </summary>
        // private bool IsCompleted { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public MEPUpdater(AddInId rvAddInId)
        {
            InitSetting(rvAddInId);
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        private void InitSetting(AddInId rvAddInId)
        {
            string dllParentDirPath = string.Empty;              // dll 파일의 부모 폴더 경로 

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater 초기 셋팅 시작");

                // 1. 업데이터 아이디 생성
                Guid guId   = new Guid(HTSHelper.GId);
                Updater_Id  = new UpdaterId(rvAddInId, guId);

                // 2. 매개변수 값 입력 완료 여부 false 초기화 (필요시 아래 코드 주석 해제 후 사용 예정)
                // IsCompleted = false;

                // TODO : 추후 초기화할 대상 생기면 메서드 "InitSetting" 몸체 안에 초기화 로직 구현 예정 (2024.04.02 jbh)
                // 3. MEP Updater 매개변수 리스트 가져오기  
                // dllParentDirPath = DirectoryManager.GetDllParentDirectoryPath(HTSHelper.AssemblyFilePath);  // dll 파일(HTSBIM2019.dll)이 속한 부모 폴더 경로 가져오기 
                dllParentDirPath = AppSetting.Default.DirectoryBase.ParentDirPath;   // dll 파일(HTSBIM2019.dll)이 속한 부모 폴더 경로 가져오기 
                UpdaterParamList = ParamsManager.GetMEPUpdaterParameterList(dllParentDirPath);

                // 4. MEP Updater 매개변수("객체 생성 날짜", "객체 생성자") 데이터 추출하기 
                AddParamList = UpdaterParamList.Where(addParam => addParam.ParamName == HTSHelper.AddDate
                                                               || addParam.ParamName == HTSHelper.AddWorker)
                                               .Select(addParam => addParam)
                                               .ToList();

                // 5. MEP Updater 매개변수("최종 수정 날짜", "최종 수정자") 데이터 추출하기 
                ModParamList = UpdaterParamList.Where(modParam => modParam.ParamName == HTSHelper.LastModDate
                                                               || modParam.ParamName == HTSHelper.LastModWorker)
                                               .Select(modParam => modParam)
                                               .ToList();

                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater 초기 셋팅 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion InitSetting

        #region 기본 메소드 

        #region Execute

        // TODO : 콜백 함수 Execute 구현 (2024.04.02 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL   - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24

        /// <summary>
        /// 콜백 함수 Execute
        /// </summary>
        public void Execute(UpdaterData pData)
        {
            // string workerId        = string.Empty;               // 작업자(사용자) 아이디
            string workerName      = string.Empty;               // 작업자 이름 (로그인 계정 - 영문 또는 한글)
            string currentDateTime = string.Empty;               // 매개변수에 입력할 값("현재 날짜 시간 조합 문자")
            string paramValue      = string.Empty;               // 매개변수에 입력할 값(문자열)

            bool bResult = false;                                // 매개변수 ("객체 생성 날짜", "객체 생성자", "최종 수정 날짜", "최종 수정자")에 매핑된 값 입력 완료 여부 
            
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater 콜백 함수 Execute 시작");

                // 매개변수 값 입력 완료 여부 확인 
                // if (true == IsCompleted)
                // {
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater 콜백 함수 Execute 종료");
                //     IsCompleted = false;   // 매개변수 값 입력 완료 여부 false 다시 초기화
                //     return;                // 콜백함수 Execute 종료 처리 (종료 처리 안 하면 콜백 함수 Execute가 무한으로 실행됨.)
                // }

                RevitDoc = pData.GetDocument();   // UpdaterData 클래스 객체 pData와 연관된 Document 개체 반환

                var addElementIds            = pData.GetAddedElementIds();      // 활성화된 Revit 문서에서 새로 추가된 객체 아이디 리스트(addElementIds) 구하기 
                List<Element> addElements    = addElementIds.Select(addElementId => RevitDoc.GetElement(addElementId)).ToList();        // 새로 추가된 객체 리스트 
                List<string> addElementNames = addElementIds.Select(addElementId => RevitDoc.GetElement(addElementId).Name).ToList();   // 새로 추가된 객체 집합에서 객체 이름만 추출 

                // 활성화된 Revit 문서에서 새로 추가된 객체 아이디 리스트(modElementIds)에 존재하는 요소들 중 타입이 "FamilySymbol"인 요소만 추출 
                // List<FamilySymbol> addFamilySymbols = addElementIds.Where(addElementId => RevitDoc.GetElement(addElementId) as FamilySymbol is not null)
                //                                                    .Select(addElementId => RevitDoc.GetElement(addElementId) as FamilySymbol)
                //                                                    .ToList();

                var modElementIds            = pData.GetModifiedElementIds();   // 활성화된 Revit 문서에서 수정(편집)된 객체 아이디 리스트(modElementIds) 구하기 
                List<Element> modElements    = modElementIds.Select(modElementId => RevitDoc.GetElement(modElementId)).ToList();        // 수정된 객체 리스트 
                List<string> modElementNames = modElementIds.Select(modElementId => RevitDoc.GetElement(modElementId).Name).ToList();   // 수정된 객체 집합에서 객체 이름만 추출

                // 활성화된 Revit 문서에서 수정(편집)된 객체 아이디 리스트(modElementIds)에 존재하는 요소들 중 타입이 "FamilySymbol"인 요소만 추출 
                // List<FamilySymbol> modFamilySymbols = modElementIds.Where(modElementId => RevitDoc.GetElement(modElementId) as FamilySymbol is not null)
                //                                                    .Select(modElementId => RevitDoc.GetElement(modElementId) as FamilySymbol)
                //                                                    .ToList();

                // elementId List -> FamiliySymbol 유형이 존재하면 -> 얘네들만 제외하고 다른 ElementId 요소값만 따로 추출해서 진행 

                // TODO : 클래스 "ElementId" 타입 Collection 객체 "addElementIds", "modElementIds"에 존재하는 타입이 "FamilyInstance"가 아니고 "FamilySymbol"일 경우 
                //        Revit 문서(RevitDoc)에 아직 실제로 생성된 객체는 아니기 때문에 매개변수 "객체 생성 날짜", "객체 생성자", "최종 수정 날짜", "최종 수정자"가 존재하지 않음.
                //        하여 이런 경우엔 메서드 Execute를 종료할 수 있도록 return 처리 진행 (2024.04.17 jbh)
                // if(addFamilySymbols.Count >= (int)EnumExistFamilySymbols.EXIST || modFamilySymbols.Count >= (int)EnumExistFamilySymbols.EXIST) return;



                // TODO : Revit API 사용해서 로그인 정보에 속한 작업자 이름(로그인 계정 - 영문 또는 한글) 가져오기 (2024.04.02 jbh)
                // workerId   = AppSetting.Default.Login.LoginUserId;   // 작업자(사용자) 아이디
                workerName = AppSetting.Default.Login.Username;      // 작업자(사용자) 이름

                currentDateTime = DateTime.Now.ToString();   // 매개변수 "객체 생성 날짜" 또는 "최종 수정 날짜"에 입력할 값 ("현재 날짜 시간 조합 문자") 문자열 변환 후 할당



                // 새로 추가된 객체 아이디 리스트(addElementIds)와 객체 이름 리스트(addElementNames)에 모두 값이 존재하는 경우 
                if (addElementIds.Count >= (int)EnumExistElements.EXIST
                    && addElementNames.Count >= (int)EnumExistElements.EXIST)
                {
                    // TODO : 신규 Triggers 실행시 매개변수 2가지("객체 생성 날짜", "객체 생성자")에 입력할 값으로
                    //        "현재 날짜 시간 조합 문자" "작업자 이름(영문 또는 한글)" 입력하기 (2024.04.02 jbh)
                    // static 메서드 "SetParametersValue" 기능
                    // 1. 매개변수 "객체 생성 날짜", "객체 생성자" 추출하기 
                    // 2. 매개변수에 값 "현재 날짜 시간 조합 문자" "작업자 이름(영문 또는 한글)" 입력하기 
                    // 3. 매개변수에 입력 완료된 값 "현재 날짜 시간 조합 문자" "작업자 이름(영문 또는 한글)" 메시지 출력하기 

                    

                    // 매개변수에 매핑된 값 입력하기  
                    foreach(var addParam in AddParamList)
                    {
                        paramValue = string.Empty;

                        switch(addParam.ParamName)
                        {
                            case HTSHelper.AddDate:
                                paramValue = currentDateTime;
                                break;
                            case HTSHelper.AddWorker:
                                paramValue = workerName;                            
                                break;
                            default:
                                break;
                        }

                        // bResult = ParamsManager.SetParametersValue(addElements, addParam.ParamName, paramValue);
                        ParamsManager.SetParametersValue(addElements, addParam.ParamName, paramValue);

                        // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.04.18 jbh)
                        // if (true == bResult) IsCompleted = true;
                        // if (false == bResult) throw new Exception($"매개변수 {addParam.ParamName} 값 입력 실패!!\r\n담당자에게 문의하세요.");  
                    }
                }

                // 수정된 객체 아이디 리스트(modElementIds)와 객체 이름 리스트(modElementNames)에 모두 값이 존재하는 경우 
                if (modElementIds.Count >= (int)EnumExistElements.EXIST
                    && modElementNames.Count >= (int)EnumExistElements.EXIST)
                {
                    // TODO : 수정/편집 Triggers 실행시 매개변수 2가지("최종 수정 날짜", "최종 수정자")에 입력할 값으로
                    //        "현재 날짜 시간 조합 문자" "작업자 이름(영문 또는 한글)" 입력하기 (2024.04.02 jbh)

                    // 매개변수에 매핑된 값 입력하기  
                    foreach(var modParam in ModParamList)
                    {
                        paramValue = string.Empty;

                        switch (modParam.ParamName)
                        {
                            case HTSHelper.LastModDate:
                                paramValue = currentDateTime;
                                break;
                            case HTSHelper.LastModWorker:
                                paramValue = workerName;
                                break;
                            default:
                                break;
                        }

                        // bResult = ParamsManager.SetParametersValue(modElements, modParam.ParamName, paramValue);
                        ParamsManager.SetParametersValue(modElements, modParam.ParamName, paramValue);

                        // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.04.18 jbh)
                        // if (true == bResult) IsCompleted = true;
                        // if (false == bResult) throw new Exception($"매개변수 {modParam.ParamName} 값 입력 실패!!\r\n담당자에게 문의하세요.");
                    }
                }

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            return;   // 콜백함수 Execute 종료  
        }

        #endregion Execute

        #region GetAdditionalInformation

        /// <summary>
        /// 부가정보 가져오기
        /// </summary>
        public string GetAdditionalInformation()
        {
            return "NA";
        }

        #endregion GetAdditionalInformation

        #region GetChangePriority

        /// <summary>
        /// 업데이터 우선순위 변경하기
        /// </summary>
        public ChangePriority GetChangePriority()
        {
            return ChangePriority.MEPFixtures;
        }

        #endregion GetChangePriority

        #region GetUpdaterId

        /// <summary>
        /// 업데이터 아이디 가져오기
        /// </summary>
        public UpdaterId GetUpdaterId()
        {
            return Updater_Id;
        }

        #endregion GetUpdaterId

        #region GetUpdaterName

        /// <summary>
        /// 업데이터 이름 가져오기 
        /// </summary>
        public string GetUpdaterName()
        {
            return HTSHelper.MEPUpdaterName;
        }

        #endregion GetUpdaterName

        #endregion 기본 메소드 

        #region Sample

        #endregion Sample
    }
}
