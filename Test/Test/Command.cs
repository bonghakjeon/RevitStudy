using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Test
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Form1 form = new Form1(commandData);

            // 윈도우 폼 출력 기본 2가지 방법
            // 1. 메서드 Show 실행 -> Revit(부모창)을 떠나서 독립적인 새창(form)을 띄우기 (Revit(부모창)도 따로 컨트롤 가능/ 독립적인 새창(form) 따로 컨트롤 가능)
            // 단, 해당 창(form)에서 만들어진 결과(명령 또는 데이터 정보)를 Revit(부모창)으로 전달할 수 없다.
            // form.Show(); 실행 -> 해당 창(form)이 종료되던 실행하던 상관 없이 밑에 있는 TaskDialog.Show("확인", "폼 이후 내용입니다."); 메서드가 바로 실행 
            // 해당 창(form)도 띄워지고 메시지 창(TaskDialog.Show)도 같이 실행된다.
            // form.Show();

            // 2. 메서드 ShowDialog 실행 -> 해당 창(form)을 닫기 전까지 Revit 응용 프로그램(부모창)의 컨트롤이 불가능하고 해당 창(form)의 컨트롤만 사용 가능,
            //    그 대신이 해당 창(form)에서 만들어진 결과(명령 또는 데이터 정보)를 Revit 응용 프로그램(부모창)으로 전달할 수 있다.
            //    (해당 창(form)을 닫기 전까지 Revit 응용 프로그램(부모창)과 연결이 끊어지지 않는다.)
            // form.ShowDialog(); 실행 -> 해당 창(form)이 종료되어야 밑에 있는 TaskDialog.Show("확인", "폼 이후 내용입니다."); 메서드가 이어서 실행 
            // 해당 창(form)도 종료되고 나서 메시지 창(TaskDialog.Show)도 같이 실행된다.
            // 해당 창(form)이 실행 중 (움직이거나 클릭하거나 컨트롤하는 등등...)에는 Revit 응용 프로그램(부모창)은 컨트롤이 불가하고 잠겨버린다.(움직이거나 클릭하거나 등등...)
            form.ShowDialog();     

            // 메서드 TaskDialog.Show 실행 -> 창을 띄운다.
            TaskDialog.Show("확인", "폼 이후 내용입니다.");

            #region MyRegion 
            #endregion MyRegion
            #region MyRegion 
            #endregion MyRegion
            #region MyRegion 
            #endregion MyRegion

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
