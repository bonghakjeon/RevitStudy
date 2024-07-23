using Serilog;
using System;
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.Converters;

using Autodesk.Revit.UI;

namespace HTSBIM2019.Common.RibbonBase
{
    // TODO : 비쥬얼스튜디오 2019 언어 버전 9.0 변경 (2024.04.05 jbh)
    // 참고 URL - https://forum.dotnetdev.kr/t/c-ft-net-framework/4894
    // 참고 2 URL - https://karupro.tistory.com/114

    public class Ribbon
    {
        #region 프로퍼티 

        #endregion 프로퍼티 

        #region CreateRibbonControl

        /// <summary>
        /// 리본 컨트롤(메뉴) 등록(생성)
        /// </summary>
        public static void CreateRibbonControl(UIControlledApplication rvUIControlledApp)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 로그 기록 "리본 메뉴 등록" 시작 기록 
                Log.Information(Logger.GetMethodPath(currentMethod) + "리본 메뉴 등록 시작");

                // TODO : 리본 탭, 리본 패널, 리본 버튼 구현하기 (2024.01.22 jbh)
                // 참고 URL - https://www.revitapidocs.com/2024/8ce17489-75ee-ae81-306d-58f9c505c80c.htm
                // 참고 2 URL - https://thebuildingcoder.typepad.com/blog/2013/02/adding-a-button-to-existing-ribbon-panel.html
                // 참고 3 URL - https://archi-lab.net/create-your-own-tab-and-buttons-in-revit/

                // 1 단계 : 리본 탭 "HTS BIM" 생성
                rvUIControlledApp.CreateRibbonTab(RibbonHelper.tabName);

                // 2 단계 : 리본 패널 ("(주)상상진화", "Updater BIM") 생성
                // List<RibbonPanel> PanelList = new List<RibbonPanel>();
                // PanelList.Add(application.CreateRibbonPanel(RibbonHelper.tabName, RibbonHelper.panelHTSBIM));

                Dictionary<string, RibbonPanel> PanelDic = new Dictionary<string, RibbonPanel>();
                PanelDic.Add(RibbonHelper.panelImagineBuilder, rvUIControlledApp.CreateRibbonPanel(RibbonHelper.tabName, RibbonHelper.panelImagineBuilder));
                PanelDic.Add(RibbonHelper.panelUpdater, rvUIControlledApp.CreateRibbonPanel(RibbonHelper.tabName, RibbonHelper.panelUpdater));


                // 3 단계 : 리본 패널 ("HTS") 버튼 "홈페이지" 추가 ((주)상상진화 기업 로고 이미지 추가) (2024.04.15 jbh)
                // 버튼 기업 홈페이지
                PushButtonData pdbLogo = new PushButtonData(RibbonHelper.기업홈페이지, RibbonHelper.기업홈페이지, HTSHelper.AssemblyFilePath, RibbonHelper.path_기업홈페이지);
                pdbLogo.LargeImage = BitmapConverter.ConvertFromBitmap(HTSBIM2019.Properties.Resources.Logo); // 아이콘 셋팅

                PanelDic[RibbonHelper.panelImagineBuilder].AddItem(pdbLogo);

                List<RibbonItem> ButtonList = new List<RibbonItem>();

                // 4 단계 : 리본 패널 ("Updater BIM") 버튼 추가 
                // 1. MEP 사용 기록 관리
                PushButtonData pbdMEPUpdater = new PushButtonData(RibbonHelper.MEP_사용기록관리, RibbonHelper.MEP_사용기록관리, HTSHelper.AssemblyFilePath, RibbonHelper.path_MEP_사용기록관리);

                // 2. (주)상상진화 기술지원 문의
                PushButtonData pbdTechnicalSupport = new PushButtonData(RibbonHelper.상상진화_기술지원문의, RibbonHelper.상상진화_기술지원문의, HTSHelper.AssemblyFilePath, RibbonHelper.path_상상진화_기술지원문의);

                // 3. 이미지 편집 
                PushButtonData pbdImageEditor = new PushButtonData(RibbonHelper.이미지편집, RibbonHelper.이미지편집, HTSHelper.AssemblyFilePath, RibbonHelper.path_이미지편집);


                // 4. (주)상상진화 기업 홈페이지
                // PushButtonData pbdCompanyHomePage = new PushButtonData(RibbonHelper.기업홈페이지, RibbonHelper.기업홈페이지, HTSHelper.AssemblyFilePath, RibbonHelper.path_상상진화_기업홈페이지); 


                // TODO : 추후 버튼 여러 개 구현해서 버튼 리스트 객체 "ButtonList"에 데이터 추가시 아래 주석친 코드 참고 (2024.04.05 jbh) 
                // ButtonList.AddRange(PanelDic[RibbonHelper.panelUpdater].AddStackedItems(pbdMEPUpdater, pbdTechnicalSupport, pbdCompanyHomePage));
                ButtonList.AddRange(PanelDic[RibbonHelper.panelUpdater].AddStackedItems(pbdMEPUpdater, pbdTechnicalSupport, pbdImageEditor));

                // 로그 기록 "리본 메뉴 등록" 완료 기록 
                Log.Information(Logger.GetMethodPath(currentMethod) + "리본 메뉴 등록 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 예외처리 throw 
            }
        }

        #endregion CreateRibbonControl

        #region Sample

        #endregion Sample
    }
}
