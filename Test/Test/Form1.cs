using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace Test
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public const string start = "Start";
        ExternalCommandData commandData;
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;
        UIDocument uidoc;

        public Form1(ExternalCommandData commanddata)
        {
            InitializeComponent();

            // 폼 객체 생성시 -> 생성자 실행 -> commandData, app, doc, uidoc 초기 셋팅 
            commandData = commanddata;
            app = commandData.Application.Application;
            doc = commandData.Application.ActiveUIDocument.Document;
            uidoc = commandData.Application.ActiveUIDocument;
        }

        /// <summary>
        /// 벽 만드는 이벤트 메서드 simpleButton1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // OfClass
                // 중요 - 프로젝트 "Test"에 포함되어 있는 어떤 객체(level1) 를 필터링
                // 기능 - 해당 프로젝트에 열린 문서(doc) 또는 뷰에서 필요한 객체를 필터링해서 가져옴. (새로 구현할 데이터 모델에 필요한 데이터셋 대상)
                // 해당 문서(doc)에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc);
                // 해당 뷰에서 필요한 객체 필터링 해서 가져옴 - FilteredElementCollector collector = new FilteredElementCollector(doc, viewId);
                FilteredElementCollector collector = new FilteredElementCollector(doc);

                // 해당 collector(문서(doc) 전체가 대상)에서 "Level"에 해당하는 type 을 가진 객체만 전부다 collection으로 가지고 오기 
                // 현재 문서(doc)에서 Level 타입의 객체를 모두 collection으로 가져오기
                ICollection<Element> collection = collector.OfClass(typeof(Level)).ToElements();

                Level level = collection.First<Element>() as Level;

                XYZ pt0 = new XYZ(0, 0, 0);
                XYZ pt1 = new XYZ(10, 0, 0);

                Line line = Line.CreateBound(pt1, pt0);

                using (Transaction transaction = new Transaction(doc))
                {
                    transaction.Start("Start");

                    // 메서드 form.ShowDialog 실행
                    // 해당 창(form)에서 만들어진 결과(명령 또는 데이터 정보)를 Revit 응용 프로그램(부모창)으로 전달할 수 있다.
                    // 따라서 명령 또는 데이터 정보(예) 벽 만들기 를 전달할 수 있는 메서드 Wall.Create(doc, line, level.Id, true); 실행시
                    // Revit 응용 프로그램(부모창)으로 명령어 또는 데이터 정보를 전달하여 
                    // Revit 응용 프로그램(부모창)에서 벽을 만들 수 있다.
                    Wall.Create(doc, line, level.Id, true);      // 벽 만들기 

                    transaction.Commit();
                }
            }
            // 벽을 만들지 못할 경우 - 오류 메시지 출력
            // C# 오류 코드 "CS0136" 오류 메시지 "이름이 'e'인 지역 또는 매개 변수는 이 범위에서 선언될 수 없습니다. 해당 이름이 지역 또는 매개 변수를 정의하기 위해 바깥쪽 지역 범위에서 사용되었습니다."
            // 해당 오류는 catch 문 파라미터 "Exception e"와 이벤트 메서드 "simpleButton1_Click" 파라미터 "EventArgs e"의 변수 이름이 "e"로 똑같아서 발생하는 오류임.
            // 해당 오류 해결하려면 catch 문 파라미터 "Exception e" -> "Exception ex"로 변경해야함.
            // 참고 URL - https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=jhwang2u&logNo=1771432
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Wall.Create(doc, line, level.Id, true);  메서드 실행시 null Excetion이 발생하면 오류 메시지 출력  
                // Revit 응용 프로그램과 상관없는 독립적인 새창(form.Show();)이 띄워져 있는 상태이기 때문에
                // Revit 응용 프로그램쪽으로 명령(Wall.Create(doc, line, level.Id, true);)을 전달하지 못하고 있을 경우 출력되는 오류 메시지이다. 
                MessageBox.Show("벽 생성 실패", "확인");   
            }

        }
    }
}