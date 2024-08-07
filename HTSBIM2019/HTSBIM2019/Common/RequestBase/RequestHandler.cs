﻿using Serilog;
using System;
using System.IO;
using System.Linq;   // TODO : 해당 using 문(using System.Linq;)을 사용해야 Linq 확장 메서드 (Where, Select, ToList, ForEach 등등...) 사용 가능 (2024.04.25 jbh)
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.Managers;
using HTSBIM2019.Settings;
using HTSBIM2019.Interface.Request;
using HTSBIM2019.Models.HTSBase.MEPUpdater;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;

namespace HTSBIM2019.Common.RequestBase
{
    public class TestRequestHandler : IExternalEventHandler, IHTSRequest
    {
        #region 프로퍼티

        /// <summary>
        /// Revit 업데이터 + Triggers 등록 여부
        /// </summary>
        public bool IsUpdaterRegistered { get; set; }

        /// <summary>
        /// Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        public UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보
        /// </summary>
        // public BuiltInCategory UpdaterCategory { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보 리스트 
        /// </summary>
        public List<CategoryInfoView> UpdaterCategoryList { get; set; } = new List<CategoryInfoView>();


        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 이름
        /// </summary>
        public string UpdaterCategoryName { get; set; }

        // TODO : 아래 주석친 프로퍼티 카테고리(객체) 필터 프로퍼티 "CategoryFilter" 필요시 참고 (2024.04.19 jbh)
        /// <summary>
        /// 카테고리(객체) 필터 
        /// </summary>
        // public ElementCategoryFilter CategoryFilter { get; set; }

        #endregion 프로퍼티

        #region Execute

        public void Execute(UIApplication app)
        {
            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // CmdTestUpdater.WakeFormUp();
            }
        }

        #endregion Execute

        #region GetName

        public string GetName()
        {
            // throw new NotImplementedException();
            return "TestForm";
        }

        #endregion GetName

        #region Register

        /// <summary>
        /// 업데이터 + Triggers 등록
        /// </summary>
        public void Register(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, List<CategoryInfoView> rvCategoryInfoList)
        {
            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion Register

        #region Remove

        /// <summary>
        /// 업데이터 + Triggers 제거(해제)
        /// </summary>
        public void Remove(Document rvDoc, UpdaterId rvUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion Remove
    }

    #region MEPUpdaterRequestHandler

    /// <summary>
    /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 요청 핸들러
    /// </summary>
    public class MEPUpdaterRequestHandler : IExternalEventHandler, IHTSRequest
    {
        #region 프로퍼티 

        /// <summary>
        /// Modaless 폼(.Show()) 최근 요청 값에 엑세스 하기위한 프로퍼티 (읽기 전용)
        /// </summary>
        public MEPUpdaterRequest Request { get { return _Request; } }
        private MEPUpdaterRequest _Request = new MEPUpdaterRequest();

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 등록 여부
        /// </summary>
        public bool IsUpdaterRegistered { get; set; }

        /// <summary>
        /// Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        /// <summary>
        /// MEP 업데이터 아이디
        /// </summary>
        public UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리
        /// </summary>
        //public BuiltInCategory UpdaterCategory { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보 리스트 
        /// </summary>
        public List<CategoryInfoView> UpdaterCategoryList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 이름
        /// </summary>
        public string UpdaterCategoryName { get; set; }

        /// <summary>
        /// MEP Updater 매개변수 리스트
        /// </summary>
        public List<Updater_Parameters> UpdaterParamList { get; set; } = new List<Updater_Parameters>();

        /// <summary>
        /// Revit 애플리케이션 언어 타입(영문, 한글 등등...)
        /// </summary>
        public LanguageType RevitLanguageType { get; set; }

        /// <summary>
        /// MEPUpdater Request 유형 열거형 구조체 프로퍼티
        /// </summary>
        public EnumMEPUpdaterRequestId RequestIdValue { get; set; }

        // TODO : 아래 주석친 프로퍼티 카테고리(객체) 필터 프로퍼티 "CategoryFilter" 필요시 참고 (2024.04.19 jbh)
        /// <summary>
        /// 카테고리(객체) 필터 (카테고리(객체) 대상 - 배관, 배관 부속류, 단열재, 배관 밸브류)
        /// </summary>
        // public ElementCategoryFilter CategoryFilter { get; set; }

        // TODO : 아래 주석친 프로퍼티 카테고리 필터 (배관 - OST_PipeCurves) 필요시 참고 (2024.03.27 jbh)
        /// <summary>
        /// 카테고리 필터 (배관 - OST_PipeCurves)
        /// </summary>
        // private ElementCategoryFilter PipeCurvesCategoryFilter { get; set; }


        // TODO : 아래 주석친 프로퍼티 카테고리 필터 (배관 부속류 - OST_PipeFitting) 필요시 참고 (2024.03.27 jbh)
        /// <summary>
        /// 카테고리 필터 (배관 부속류 - OST_PipeFitting)
        /// </summary>
        // private ElementCategoryFilter PipeFittingCategoryFilter { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public MEPUpdaterRequestHandler()
        {

        }

        #endregion 생성자

        #region GetName

        /// <summary>
        /// 해당 외부 이벤트 핸들러(MEPUpdaterRequestHandler)(이벤트 메서드) 이름으로 식별 
        /// </summary>
        public string GetName()
        {
            return HTSHelper.MEPUpdaterFormName;
        }

        #endregion GetName

        #region Execute

        /// <summary>
        /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 이벤트 메서드(외부 이벤트 핸들러 - MEPUpdaterRequestHandler) 
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication rvUIApp)
        {
            // bool isUpdaterRegistered = false;                         // Revit MEP 업데이터 + Triggers 등록 여부
                                                                      
            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 시작");

                // 1. Revit MEP 업데이터 + Triggers 등록 여부 false 초기화
                IsUpdaterRegistered = false;

                // 2. Revit 문서 프로퍼티 "RevitDoc" 할당 
                RevitDoc = rvUIApp.ActiveUIDocument.Document;    // 활성화된 Revit 문서 

                // 3. Revit 애플리케이션 언어 타입(영문, 한글 등등...) 할당 
                RevitLanguageType = RevitDoc.Application.Language;

                // 4. MEP 업데이터 가져오기
                IUpdater mepUpdater = AppSetting.Default.UpdaterBase.MEPUpdater;

                // 5. MEP 업데이터 아이디 가져오기
                Updater_Id = AppSetting.Default.UpdaterBase.MEPUpdater.Updater_Id;

                // TODO : UpdaterCategoryList.Clear(); 메소드 호출시 AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList 값 또한 0으로 초기화 되는 원인 파악 및 해결하기 (2024.04.19 jbh)
                //        UpdaterCategoryList.Clear(); 메소드 호출 하지 말고 아래 처럼 AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList 값을 직접 매핑하기 (2024.04.22 jbh)
                // 6. 카테고리 TreeView 컨트롤(treeViewCategory)에서 선택한 카테고리 정보 리스트 가져오기 
                // UpdaterCategoryList.Clear();
                // UpdaterCategoryList = AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList;
                UpdaterCategoryList = AppSetting.Default.UpdaterBase.MEPUpdaterForm.CategoryInfoList;

                UpdaterParamList = AppSetting.Default.UpdaterBase.MEPUpdater.UpdaterParamList;

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using(Transaction transaction = new Transaction(RevitDoc))
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 시작");

                    // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(HTSHelper.Start);  // 해당 "HTSBIM2019" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작


                    // 카테고리 ComboBox 컨트롤(comboBoxCategory)에서 선택한 카테고리 정보 가져오기 
                    // UpdaterCategory = AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfo.Category; // rvMEPUpdater.CategoryInfo.Category;

                    // 카테고리 이름 가져오기 
                    // UpdaterCategoryName = CategoryManager.GetCategoryName(RevitDoc, UpdaterCategory);

                    // 카테고리 ComboBox 컨트롤(comboBoxCategory)에서 선택한 카테고리 객체(UpdaterBuiltInCategory)만 필터링 처리
                    // CategoryFilter = new ElementCategoryFilter(UpdaterCategory);


                    // TODO : 아래 주석친 테스트 코드 필요시 사용 예정 (2024.04.01 jbh)
                    // HTS Revit 업데이터 Command 아이디 값 프로퍼티 "Updater_Id" 할당 
                    // AddInId addInId = rvUIApp.ActiveAddInId;         // HTS Revit 업데이터 Command 아이디
                    // Guid guId = new Guid(HTSHelper.GId);
                    // Updater_Id = new UpdaterId(addInId, guId);

                    // 인터페이스 "IUpdater" 상속 받는 폼 객체 "MEPUpdater" 찾기 
                    // MEPUpdaterForm mepUpdaterForm = (MEPUpdaterForm)FormManager.GetModalessForm(typeof(IUpdater), typeof(MEPUpdaterForm));

                    // Revit MEP 업데이터 + Triggers가 등록되어 있는 경우 true 리턴 / Revit MEP 업데이터 + Triggers가 이미 해제되어 있는 경우 false 리턴 
                    IsUpdaterRegistered = UpdaterRegistry.IsUpdaterRegistered(Updater_Id, RevitDoc);   // Revit 문서(rvDoc)에 해당 rvUpdaterId를 가진 업데이터가 등록된 경우 

                    // TODO : 부모 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어할 수 있도록 구현 예정 (2024.03.15 jbh)
                    //EnumMEPUpdaterRequestId requestIdValue = Request.Take();

                    RequestIdValue = Request.Take();

                    // switch(requestIdValue)
                    switch(RequestIdValue)
                    {
                        case EnumMEPUpdaterRequestId.NONE:   // 요청이 없는 경우 -> 즉시 종료
                            return;

                        case EnumMEPUpdaterRequestId.REGISTER:   // 등록 요청 
                            // TODO : 매개변수 4가지("객체 생성 날짜", "최종 수정 날짜", "객체 생성자", "최종 수정자") 생성 로직 추가하기 (2024.04.02 jbh)
                            //        버튼 "ON"을 중복 클릭시 매개변수가 존재할 경우에는 생성 안 함.
                            // 주의사항 - 생성한 매개변수에 매핑된 데이터 값을 사용자가 화면에서 수정하지 못하도록 설정 구현 
                            // 프로퍼티 "UserModifiable" 설명
                            // 사용자가 이 매개변수의 값을 수정할 수 있는지 여부를 나타냅니다.
                            // 참고 URL - https://www.revitapidocs.com/2018/c0343d88-ea6f-f718-2828-7970c15e4a9e.htm
                            CategoryManager.CreateCategorySet(RevitDoc, UpdaterParamList, RevitLanguageType);

                            // TODO : 업데이터가 기존에 이미 등록된 경우 업데이터는 유지하고 Triggers만 추가 
                            if (true == IsUpdaterRegistered) UpdaterManager.RegisterTriggers(Updater_Id, UpdaterCategoryList); // UpdaterManager.RegisterTriggers(Updater_Id, CategoryFilter, UpdaterCategoryName);   // RemoveMEP(RevitDoc, updater_Id);

                            // 업데이터가 등록되지 않은 경우 업데이터 + Triggers 모두 등록할 수 있도록 메서드 "RegisterMEP" 호출
                            // RegisterMEP(mepUpdaterForm, RevitDoc, updater_Id);
                            else Register(mepUpdater, Updater_Id, RevitDoc, UpdaterCategoryList);   // Register(mepUpdater, Updater_Id, RevitDoc, CategoryFilter, UpdaterCategoryName);
                            break;

                        case EnumMEPUpdaterRequestId.REMOVE:   // 제거(해제) 요청
                            if(true == IsUpdaterRegistered) Remove(RevitDoc, Updater_Id);   // RemoveMEP(RevitDoc, Updater_Id);
                            // Revit MEP 업데이터 + Triggers가 이미 해제되어 있는 경우 
                            else TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 + Triggers 이미 해제 완료되었습니다.");
                            break;

                        default:
                            TaskDialog.Show(HTSHelper.NoticeTitle, $"요청 아이디{RequestIdValue}이/가 존재하지 않습니다.\r\n담당자에게 문의하시기 바랍니다.");
                            break;
                    }

                    transaction.Commit();    // 해당 "HTSBIM2019" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋

                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 완료");
                }   // 여기서 Dispose (리소스 해제) 처리 

                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                AppSetting.Default.UpdaterBase.UpdaterLoadingForm.CloseLoadingForm();   // 업데이터 + Triggers 등록 대기처리 화면 종료
                CmdMEPUpdater.WakeFormUp();
            }
        }

        #endregion Execute

        #region Register

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 등록 
        /// </summary>
        //public void Register(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, ElementCategoryFilter rvElementCategoryFilter)
        //public void Register(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, ElementCategoryFilter rvElementCategoryFilter, string rvBuiltInCategoryName)
        //public void Register(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, ElementCategoryFilter rvElementCategoryFilter, List<CategoryInfoView> rvCategoryInfoList)
        public void Register(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, List<CategoryInfoView> rvCategoryInfoList)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 시작");

                // 해당 메서드 "UpdaterRegistry.RegisterUpdater" 호출시 파라미터로 넘길 인자로 넘길 때, 
                // 인터페이스 IUpdater를 상속 받는 MEPUpdater 클래스 객체를 넘기도록 구현하기 

                // 1. Revit MEP 업데이터 등록 
                // TODO : static 메서드 "UpdaterManager.RegisterUpdater" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
                // UpdaterManager.RegisterUpdater(rvMEPUpdaterForm, rvDoc, rvUpdaterId);
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");

                UpdaterRegistry.RegisterUpdater(rvMEPUpdater, rvDoc);   // 인터페이스 IUpdater를 상속받는 폼 객체에 업데이터 등록

                // TODO : 경고 메시지 "SKID TEST 파일이 현재 설치되지 않은 타사 업데이터 HTSBIM2019 : MEPUpdater에서 수정되었습니다.
                //        파일을 계속 편집할 경우 HTSBIM2019: MEPUpdater에서 유지 관리하는 데이터가 제대로 업데이트되지 않습니다.
                //        따라서 나중에 HTSBIM2019: MEPUpdater이(가) 있을 때 SKID TEST을(를) 열면 문제가 발생할 수 있습니다." 출력되지 않도록
                //        메서드 "UpdaterRegistry.SetIsUpdaterOptional" 호출 구현 (2024.05.09 jbh)
                // 참고 URL - https://adndevblog.typepad.com/aec/2012/05/avoid-the-missing-third-party-updater-dialog.html
                UpdaterRegistry.SetIsUpdaterOptional(rvUpdaterId, true);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");
                // TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 등록 완료");

                // 카테고리 TreeView 컨트롤(treeViewCategory)에서 선택한 
                // 카테고리 객체 (BupdaterBuiltInCategory)만 필터링 처리 및 MEP Triggers 등록
                //UpdaterManager.RegisterTriggers(rvUpdaterId, rvElementCategoryFilter, rvCategoryInfoList);
                UpdaterManager.RegisterTriggers(rvUpdaterId, rvCategoryInfoList);


                // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.04.19 jbh)
                // BuiltInCategory updaterBuiltInCategory = AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfo.Category; // rvMEPUpdater.CategoryInfo.Category;

                // 카테고리 ComboBox 컨트롤(comboBoxCategory)에서 선택한
                // 카테고리 객체 (BupdaterBuiltInCategory)만 필터링 처리 및 MEP Triggers 등록 
                // MEPCategoryFilter = new ElementCategoryFilter(updaterBuiltInCategory);
                // UpdaterManager.RegisterTriggers(rvUpdaterId, rvDoc, rvElementCategoryFilter);
                //UpdaterManager.RegisterTriggers(rvUpdaterId, rvElementCategoryFilter, rvBuiltInCategoryName);


                // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.27 jbh)
                // 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 및 MEP Triggers 등록 
                // PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
                // UpdaterManager.RegisterTriggers(rvUpdaterId, PipeCurvesCategoryFilter);

                // 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 및 MEP Triggers 등록 
                //PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);
                //UpdaterManager.RegisterTriggers(rvUpdaterId, PipeFittingCategoryFilter);   

                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }


        /// <summary>
        /// Revit MEP 업데이터 + Triggers 등록 
        /// </summary>
        //private void RegisterMEP(MEPUpdaterForm rvMEPUpdaterForm, Document rvDoc, UpdaterId rvUpdaterId)
        //private void RegisterMEP(IUpdater rvMEPUpdater, Document rvDoc, UpdaterId rvUpdaterId)
        //private void RegisterMEP(IUpdater rvMEPUpdater, UpdaterId rvUpdaterId, Document rvDoc, ElementCategoryFilter rvElementCategoryFilter)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
        //        // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
        //        // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
        //        using(Transaction transaction = new Transaction(rvDoc))
        //        {
        //            // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
        //            transaction.Start(HTSHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

        //            Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 시작");

        //            // 해당 메서드 "UpdaterRegistry.RegisterUpdater" 호출시 파라미터로 넘길 인자로 넘길 때, 
        //            // 인터페이스 IUpdater를 상속 받는 MEPUpdater 클래스 객체를 넘기도록 구현하기 

        //            // 1. Revit MEP 업데이터 등록 
        //            // TODO : static 메서드 "UpdaterManager.RegisterUpdater" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        //            // UpdaterManager.RegisterUpdater(rvMEPUpdaterForm, rvDoc, rvUpdaterId);
        //            Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");

        //            UpdaterRegistry.RegisterUpdater(rvMEPUpdater, rvDoc);   // 인터페이스 IUpdater를 상속받는 폼 객체에 업데이터 등록

        //            Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");
        //            TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 등록 완료");

        //            // BuiltInCategory updaterBuiltInCategory = AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfo.Category; // rvMEPUpdater.CategoryInfo.Category;

        //            // 2. 카테고리 ComboBox 컨트롤(comboBoxCategory)에서 선택한
        //            // 카테고리 객체 (BupdaterBuiltInCategory)만 필터링 처리 및 MEP Triggers 등록 
        //            // MEPCategoryFilter = new ElementCategoryFilter(updaterBuiltInCategory);
        //            UpdaterManager.RegisterTriggers(rvUpdaterId, rvDoc, rvElementCategoryFilter);

        //            // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.27 jbh)
        //            // 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 및 MEP Triggers 등록 
        //            // PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
        //            // UpdaterManager.RegisterTriggers(rvUpdaterId, PipeCurvesCategoryFilter);

        //            // 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 및 MEP Triggers 등록 
        //            //PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);
        //            //UpdaterManager.RegisterTriggers(rvUpdaterId, PipeFittingCategoryFilter);   

        //            Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 완료");

        //            transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
        //        }   // 여기서 Dispose (리소스 해제) 처리 
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
        //    }
        //}

        #endregion Register

        #region Remove

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 해제 
        /// </summary>
        public void Remove(Document rvDoc, UpdaterId rvUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
                using(Transaction transaction = new Transaction(rvDoc))
                {
                    // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(HTSHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    // TODO : static 메서드 "UpdaterManager.RemoveSetting" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
                    // UpdaterManager.RemoveSetting(rvDoc, rvUpdaterId);   // Revit MEP 업데이터 + Triggers 해제 

                    Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 시작");

                    UpdaterRegistry.RemoveAllTriggers(rvUpdaterId);            // 지정된 rvUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
                    UpdaterRegistry.UnregisterUpdater(rvUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 rvUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)

                    Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 완료");
                    TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 + Triggers 해제 완료");

                    transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }   // 여기서 Dispose (리소스 해제) 처리 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 해제 
        /// </summary>
        //private void RemoveMEP(MEPUpdater rvMEPUpdaterForm, Document rvDoc, UpdaterId rvUpdaterId)
        //private void RemoveMEP(Document rvDoc, UpdaterId rvUpdaterId)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
        //        // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
        //        // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
        //        using(Transaction transaction = new Transaction(rvDoc))
        //        {
        //            // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
        //            transaction.Start(HTSHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

        //            // TODO : static 메서드 "UpdaterManager.RemoveSetting" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        //            // UpdaterManager.RemoveSetting(rvDoc, rvUpdaterId);   // Revit MEP 업데이터 + Triggers 해제 

        //            Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 시작");

        //            UpdaterRegistry.RemoveAllTriggers(rvUpdaterId);            // 지정된 rvUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
        //            UpdaterRegistry.UnregisterUpdater(rvUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 rvUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)

        //            Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 완료");
        //            TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 + Triggers 해제 완료");

        //            transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
        //        }   // 여기서 Dispose (리소스 해제) 처리 
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
        //    }
        //}

        #endregion Remove
    }

    #endregion MEPUpdaterRequestHandler

    #region ImageEditorRequestHandler

    /// <summary>
    /// 이미지 편집(ImageEditorForm) Modaless 폼(.Show()) 형식에 의해 발생하는 외부 요청 핸들러
    /// </summary>
    public class ImageEditorRequestHandler : IExternalEventHandler
    {
        #region 프로퍼티

        /// <summary>
        /// MEPUpdater Request 유형 열거형 구조체 프로퍼티
        /// </summary>
        private ImageEditorRequestId RequestIdValue { get; set; }

        /// <summary>
        /// Modaless 폼(.Show()) 최근 요청 값에 엑세스 하기위한 프로퍼티 (읽기 전용)
        /// </summary>
        public ImageEditorRequest Request { get { return _Request; } }
        private ImageEditorRequest _Request = new ImageEditorRequest();

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        private UIDocument UIDoc { get; set; }

        /// <summary>
        /// Revit 문서 
        /// </summary>
        private Document RevitDoc { get; set; }

        // TODO : 필요시 프로퍼티 "TaskErrorDialog" 사용 예정 (2024.07.03 jbh)
        /// <summary>
        /// 오류 메시지(이미지 파일 경로) 출력 
        /// </summary>
        // private TaskDialog TaskErrorDialog { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public ImageEditorRequestHandler()
        {

        }

        #endregion 생성자

        #region GetName

        /// <summary>
        /// 해당 외부 이벤트 핸들러(ImageEditorRequestHandler)(이벤트 메서드) 이름으로 식별 
        /// </summary>
        public string GetName()
        {
            return HTSHelper.ImageEditorFormName;
        }

        #endregion GetName

        #region Execute

        /// <summary>
        /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 이벤트 메서드(외부 이벤트 핸들러 - ImageEditorRequestHandler) 
        /// </summary>
        public void Execute(UIApplication rvUIApp)
        {
            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 시작");

                // Revit 문서 프로퍼티 "UIDoc" 할당
                UIDoc = rvUIApp.ActiveUIDocument;    // 활성화된 Revit 문서 
                RevitDoc = UIDoc.Document;           // Revit 문서 

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using (Transaction transaction = new Transaction(RevitDoc))
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 시작");

                    // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(HTSHelper.Start);  // 해당 "RevitBox2025" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    RequestIdValue = Request.Take();

                    switch (RequestIdValue)
                    {
                        case ImageEditorRequestId.NONE:           // 요청이 없는 경우 -> 즉시 종료
                            return;

                        case ImageEditorRequestId.SelectElement:  // 객체 선택 요청 
                            SelectElement(UIDoc, RevitDoc);
                            break;

                        case ImageEditorRequestId.InsertImage:    // 이미지 삽입 요청 
                            InsertImage(RevitDoc);
                            break;

                        default:
                            TaskDialog.Show(HTSHelper.NoticeTitle, $"요청 아이디{RequestIdValue}이/가 존재하지 않습니다.\r\n담당자에게 문의하시기 바랍니다.");
                            break;
                    }

                    transaction.Commit();    // 해당 "RevitBox2025" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋

                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 완료");
                }   // 여기서 Dispose (리소스 해제) 처리 

                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                CmdImageEditor.WakeFormUp();
            }
        }

        #endregion Execute

        #region SelectElement

        /// <summary>
        /// 객체 선택 
        /// </summary>
        private void SelectElement(UIDocument rvUIDoc, Document rvDoc)
        {
            bool result = false;                                      // 객체 선택 작업 완료 여부 초기화
            string selectedImageFilePath = string.Empty;              // 열려있는 Revit 문서에서 선택된 이미지 객체(이미지 파일)의 실제 경로 초기화
            string selectedImageFileName = string.Empty;              // 열려있는 Revit 문서에서 선택된 이미지 객체(이미지 파일) 이름 초기화

            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "객체 선택 작업 시작");

                // TODO : 아래 주석친 코드 필요시 참고 (2024.07.10 jbh)
                // TaskDialog.Show(HTSHelper.NoticeTitle, "열려있는 Revit 도면에서\r\n이미지 객체를 선택하세요.");

                Reference reference = rvUIDoc.Selection.PickObject(ObjectType.Element);

                Element element = rvDoc.GetElement(reference);   // 열려있는 Revit 도면에서 선택한 객체 가져오기

                ElementId categoryId = element.Category.Id;      // 열려있는 Revit 도면에서 선택한 객체의 카테고리 아이디 가져오기
                ElementId elementId = element.GetTypeId();       // 열려있는 Revit 도면에서 선택한 객체 아이디 가져오기

#if (R2024 || R2025)
                // 열려있는 Revit 문서에서 선택한 객체(element)가 이미지 객체(이미지 파일 - BuiltInCategory.OST_RasterImages)인 경우
                if(categoryId.Value.Equals((long)BuiltInCategory.OST_RasterImages))
#else
                // 열려있는 Revit 문서에서 선택한 객체(element)가 이미지 객체(이미지 파일 - BuiltInCategory.OST_RasterImages)인 경우
                if (categoryId.IntegerValue.Equals((int)BuiltInCategory.OST_RasterImages))
#endif
                {
                    // Element selectElement = rvDoc.GetElement(element.GetTypeId());
                    Element selectElement = rvDoc.GetElement(elementId);   // 열려있는 Revit 도면에서 선택한 객체 가져오기 
                    Parameter param = selectElement.get_Parameter(BuiltInParameter.RASTER_SYMBOL_FILENAME);
                    selectedImageFilePath = param.AsString();

                    // 열려있는 Revit 문서에서 선택한 이미지 객체(이미지 파일 - BuiltInCategory.OST_RasterImages)가 실제로 존재하는 경우 
                    if (false == string.IsNullOrWhiteSpace(selectedImageFilePath)
                       && true == File.Exists(selectedImageFilePath))
                    {
                        result = true;
                        CmdImageEditor.SetSelectedImageFilePath(result, selectedImageFilePath);
                        Log.Information(Logger.GetMethodPath(currentMethod) + "객체 선택 작업 완료");
                    }

                    // 열려있는 Revit 문서에서 선택한 이미지 객체(이미지 파일)가 실제로 존재하지 않는 경우
                    else
                    {
                        selectedImageFileName = Path.GetFileName(selectedImageFilePath);   // 이미지 파일 이름(확장자 포함) 가져오기
                        //result = false;
                        //ImageInsertCommand.SetSelectedImageFilePath(result, string.Empty);
                        // throw new Exception($"{selectedImageFileName} 파일 존재 안 함!\r\n다시 확인 바랍니다.\r\n* 파일 경로 *\r\n{selectedImageFilePath}");
                        throw new Exception($"{selectedImageFileName} 파일 존재 안 함!\r\n다시 확인 바랍니다.");
                    }
                }

                // 열려있는 Revit 문서에서 선택한 객체가 이미지 객체(이미지 파일)가 아닌 경우
                else
                {
                    //TaskDialog.Show(HTSHelper.ErrorTitle, "잘못된 객체 입니다.\r\n이미지 객체를 선택하세요.");
                    throw new Exception("잘못된 객체 입니다.\r\n이미지 객체를 선택하세요.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                result = false;
                CmdImageEditor.SetSelectedImageFilePath(result, string.Empty);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
            }
        }

        #endregion SelectElement

        #region InsertImage

        /// <summary>
        /// 이미지 삽입 
        /// </summary>
        private void InsertImage(Document rvDoc)
        {
            bool result = false;                                      // 이미지 삽입 작업 완료 여부 초기화 
            string insertedImageFilePath = string.Empty;              // 열려있는 Revit 문서에 삽입하고자 하는 이미지 파일의 실제 경로
            string insertedImageFileName = string.Empty;              // 열려있는 Revit 문서에 삽입하고자 하는 이미지 파일 이름

            var currentMethod = MethodBase.GetCurrentMethod();        // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                insertedImageFilePath = CmdImageEditor.GetInsertedImageFilePath();

                // 열려있는 Revit 문서에 삽입하고자 하는 이미지 파일이 사용자 PC 로컬에 저장된 경우 
                if(false == string.IsNullOrWhiteSpace(insertedImageFilePath)
                   && true == File.Exists(insertedImageFilePath))
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 작업 시작");

#if (R2016 || R2017 || R2018 || R2019 || R2020)
                    ImageImportOptions importOptions = new ImageImportOptions();
                    Element element;
                    
                    rvDoc.Import(insertedImageFilePath, importOptions, rvDoc.ActiveView, out element);

#else               // Revit 2021 버전 이후 
                    ImageTypeOptions typeOptions = new ImageTypeOptions(insertedImageFilePath, false, ImageTypeSource.Import);

                    ImageType imageType = ImageType.Create(rvDoc, typeOptions);

                    ImagePlacementOptions placementOptions = new ImagePlacementOptions();

                    placementOptions.PlacementPoint = BoxPlacement.Center;
                    placementOptions.Location = new XYZ(0, 0, 0);

                    ImageInstance imageInstance = ImageInstance.Create(rvDoc, rvDoc.ActiveView, imageType.Id, placementOptions);
#endif
                    result = true;
                    CmdImageEditor.SetInsertedImageFile(result);

                    Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 작업 완료");
                }
                // Revit 문서에 삽입하고자 하는 이미지 파일이 PC에 저장되어 있지 않은 경우 
                else
                {
                    insertedImageFileName = Path.GetFileName(insertedImageFilePath);   // 이미지 파일 이름(확장자 포함) 가져오기
                    //throw new Exception($"{insertedImageFileName} 파일 존재 안 함!\r\n다시 확인 바랍니다.\r\n* 파일 경로 *\r\n{insertedImageFilePath}");
                    throw new Exception($"{insertedImageFileName} 파일 존재 안 함!\r\n다시 확인 바랍니다.");
                }
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                result = false;
                CmdImageEditor.SetInsertedImageFile(result);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
            }
        }

        #endregion InsertImage

        #region Sample

        #endregion Sample
    }


    #endregion ImageEditorRequestHandler

    #region Sample

    #endregion Sample
}
