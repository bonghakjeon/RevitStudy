using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestProcessStartInfo
{
    // Revit API 로 개발 시 사용하는 기본 단위는 FEET이다.
    // 이 기본 단위 FEET를 mm 단위로 변경을 시켜주거나 
    // mm 단위를 FEET로 변경할 필요가 있다.
    // 이렇게 단위를 변경하지 않으면 원치 않는 사이즈가 출력되어 오류가 발생한다.
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        /// <summary>
        /// Start
        /// </summary>
        public const string start = "Start";

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                //Get application and document objects
                UIApplication uiapp = commandData.Application;
                Document doc = uiapp.ActiveUIDocument.Document;

                using (Transaction transaction = new Transaction(doc))
                {
                    // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(start);  // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    MessageBox.Show("ProcessStartInfo 클래스 사용 파이썬 코드 테스트");

                    

                    // TODO : IronPython을 사용하지 않고, 파이썬 코드를 "ProcessStartInfo" 클래스 사용해서 C# 코드에서 실행하도록 구현(2023.09.27 jbh)
                    var psi = new ProcessStartInfo();



                    transaction.Commit();      // 해당 "House" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return Result.Failed;
            }
        }
            
    }
}
