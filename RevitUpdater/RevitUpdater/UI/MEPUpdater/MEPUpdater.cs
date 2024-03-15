using Serilog;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogManager;
using RevitUpdater.Common.UpdaterBase;

using DevExpress.XtraEditors;

namespace RevitUpdater.UI.MEPUpdater
{
    public partial class MEPUpdater : XtraForm, IUpdater
    {
        #region 프로퍼티

        // TODO : 폼 화면(CopyParams.cs) 출력시 기존처럼 .ShowDialog(Modal - 부모 창 제어 X)하는 방식이 아니라
        //        .Show(Modaless - 부모 창 제어 O)으로 하는 방식으로 수정해야 하면
        //        부모창 제어가 가능하므로 사용자가 Revit 문서가 1개가 아니라 여러 개를 열어서 작업할 수 있으므로
        //        아래처럼 프로퍼티 "RevitDoc"로 구현하면 안 되고,
        //        Command.cs에서 Revit 문서 여러 개를 인자로 한 번에 받도록 구현 해야함. (2024.03.14 jbh) 
        // Modal VS Modaless 차이
        // 참고 URL   - https://blog.naver.com/PostView.naver?blogId=wlsdml1103&logNo=220512538948
        // 참고 2 URL - https://greensul.tistory.com/37
        /// <summary>
        /// Revit 문서 
        /// </summary>
        private Document RevitDoc { get; set; }

        /// <summary>
        /// 업데이터 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        private const string GId = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 카테고리 필터 (배관 - OST_PipeCurves)
        /// </summary>
        private ElementCategoryFilter PipeCurvesCategoryFilter { get; set; }


        /// <summary>
        /// 카테고리 필터 (배관 부속류 - OST_PipeFitting)
        /// </summary>
        private ElementCategoryFilter PipeFittingCategoryFilter { get; set; }


        /// <summary>
        /// 매개변수 값 입력 완료 여부 
        /// </summary>
        private bool IsCompleted { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public MEPUpdater(Document rvDoc, AddInId rvAddInId)
        {
            InitializeComponent();

            InitSetting(rvDoc, rvAddInId);   // 업데이터 초기 셋팅
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        private void InitSetting(Document rvDoc, AddInId rvAddInId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 시작");

                // TODO : 화면 상단 바 버튼 "최소화", "최대화" 비활성화 구현 (2024.03.14 jbh)
                // 참고 URL - https://zelits.tistory.com/690
                this.MinimizeBox = false;                            // 버튼 "최소화" 비활성화
                this.MaximizeBox = false;                            // 버튼 "최대화" 비활성화

                // Behind Code "MEPUpdater.cs"에서 폼 화면(MEPUpdater) Width, Height 고정 
                // 참고 URL - https://kdsoft-zeros.tistory.com/3
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;

                // TextBox (또는 TextEdit) 컨트롤 "textParamName" 높이 조절하기 위해 속성 Properties.AutoHeight = false; 설정 
                // 주의사항 - Properties.AutoHeight = true; 설정시 TextEdit 높이 조절 불가 
                // 참고 URL - https://supportcenter.devexpress.com/ticket/details/q278069/sizing-the-text-area-of-a-textedit-control
                // this.textParamName.Properties.AutoHeight = false;

                // 1. Revit 문서 프로퍼티에 전달 받은 Revit 문서(rvDoc) 할당
                RevitDoc = rvDoc;

                // 2. GUID 생성 
                Guid guId   = new Guid(GId);

                // 3. 업데이터 아이디(Updater_Id) 객체 생성 
                Updater_Id  = new UpdaterId(rvAddInId, guId);

                // 4. 매개변수 값 입력 완료 여부 false 초기화
                IsCompleted = false;

                // 5. 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 
                PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);

                // 6. 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 
                PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion InitSetting

        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL   - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24

        /// <summary>
        /// 콜백 함수 Execute
        /// </summary>
        public void Execute(UpdaterData rvData)
        {
            string targetParamName = string.Empty;   // 값을 할당할 매개변수 이름 
            string currentDateTime = string.Empty;   // 매개변수에 입력할 값(“현재 날짜 시간 조합 문자” )

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show("Revit MEPUpdater", "콜백 함수 Execute 구현 예정...");

                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater Execute 시작");

                // 매개변수 값 입력 완료 여부 확인 
                if (true == IsCompleted)
                {
                    IsCompleted = false;   // 매개변수 값 입력 완료 여부 false 다시 초기화
                    return;                // 콜백함수 Execute 종료 처리 (종료 처리 안 하면 콜백 함수 Execute가 무한으로 실행됨.)
                }





                // targetParamName = this.textParamName.Text;



                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater Execute 종료");

                

            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        /// <summary>
        /// 부가정보 가져오기
        /// </summary>
        public string GetAdditionalInformation()
        {
            return "NA";
        }

        /// <summary>
        /// 업데이터 우선순위 변경하기
        /// </summary>
        public ChangePriority GetChangePriority()
        {
            return ChangePriority.MEPFixtures;
        }

        /// <summary>
        /// 업데이터 아이디 가져오기
        /// </summary>
        public UpdaterId GetUpdaterId()
        {
            return Updater_Id;
        }

        /// <summary>
        /// 업데이터 이름 가져오기 
        /// </summary>
        public string GetUpdaterName()
        {
            return "MEPUpdater";
        }

        #endregion 기본 메소드

        #region btnON_Click

        /// <summary>
        /// 업데이터 + Triggers 등록 
        /// </summary>
        private void btnON_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 등록 구현 예정...");

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using (Transaction transaction = new Transaction(RevitDoc))
                {
                    // transaction.Start(AABIMHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(UpdaterHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 등록 시작");

                    Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 등록 완료");

                    transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }   // 여기서 Dispose (리소스 해제) 처리 

            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion btnON_Click

        #region btnOFF_Click

        /// <summary>
        /// 업데이터 + Triggers 해제 
        /// </summary>
        private void btnOFF_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 해제 구현 예정...");

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using (Transaction transaction = new Transaction(RevitDoc))
                {
                    // transaction.Start(AABIMHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(UpdaterHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 해제 시작");


                    Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 해제 완료");

                    transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }   // 여기서 Dispose (리소스 해제) 처리 
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion btnOFF_Click
    }
}