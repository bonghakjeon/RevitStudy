//
// (C) Copyright 2003-2019 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//
using System;
using System.Collections.Generic;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Revit.SDK.Samples.ModelessForm_ExternalEvent.CS
{
    /// <summary>
    ///   A class with methods to execute requests made by the dialog user.
    /// </summary>
    /// 
    public class RequestHandler : IExternalEventHandler
    {
        // A trivial delegate, but handy
        private delegate void DoorOperation(FamilyInstance e);

        // The value of the latest request made by the modeless form 
        private Request m_request = new Request();

        /// <summary>
        /// A public property to access the current request value
        /// </summary>
        public Request Request
        {
            get { return m_request; }
        }

        /// <summary>
        ///   A method to identify this External Event Handler
        /// </summary>
        public String GetName()
        {
            return "R2014 External Event Sample";
        }


        /// <summary>
        ///  The top method of the event handler. (메서드 "Execute"은 Request 핸들러(또는 이벤트 메서드)를 의미한다. )
        /// </summary>
        /// <remarks>
        ///   This is called by Revit after the corresponding
        ///   external event was raised (by the modeless form)
        ///   and Revit reached the time at which it could call
        ///   the event's handler (i.e. this object)
        /// </remarks>
        /// 메서드 실행 순서 
        /// 1 단계 : ModelessForm_ExternalEvent 프로젝트 파일 -> "Application.cs" 메서드 "OnStartup" 실행 -> 리본 메뉴 등록 메서드 "CreateRibbonSamplePanel" 실행 -> 리소스에 등록된 이미지 파일 Convert 메서드 "convertFromBitmap" 실행 
        /// 2 단계 : Revit 응용 프로그램 실행 -> 1 단계에서 생성된 리본 메뉴 -> 버튼 "ModelessForm" 클릭 
        /// 3 단계 : ModelessForm_ExternalEvent 프로젝트 파일 -> "Command.cs" 메서드 "Execute" 실행 -> Application.thisApp.ShowForm(commandData.Application); 실행 
        /// 4 단계 : "Application.cs" 소스파일 이동 -> 메서드 "ShowForm" 실행 ->  클래스 "ModelessForm" 폼 객체 "m_MyForm" 생성 -> RequestHandler, ExternalEvent 이벤트 객체 생성 
        /// 5 단계 : 윈폼 팝업화면 "ModelessForm" 출력 -> 버튼 클릭 (예) Left / Right -> ModelessForm.cs 비하인드 코드 이동 -> 이벤트 메서드 "btnFlipLeft_Click" 실행 -> 메서드 "MakeRequest( RequestId.FlipLeftRight );" 실행 
        /// 6 단계 : 메서드 "MakeRequest( RequestId.FlipLeftRight );" 실행 -> Request.cs Enum 구조체 멤버 중 메서드 파라미터 "FlipLeftRight" 값 "2" 찾기 ->  값 "2" 찾았으면 다음 단계 진행 
        /// 7 단계 : 메서드 "MakeRequest( RequestId.FlipLeftRight );" 실행후 ->  소스파일 "RequestHandler.cs" 이동 -> 실제 실행하는 메서드 "Execute" 실행 -> switch ~ case문 이동 -> case RequestId.FlipLeftRight: 경우 
        ///          ->  메서드 "ModifySelectedDoors(uiapp, "Flip door Hand", e => e.flipHand());" 실행  -> 명령(Command) 실행 후 폼 복귀("ModelessForm.cs" 이동) -> 메서드 "WakeUp" 실행 -> 메서드 "EnableCommands" 실행 -> 폼 화면 "ModelessForm"이 종료되지 않고 계속해서 화면에 출력되며 살아 있게 된다.
        /// 마지막 단계 : 명령(Command) 실행 결과 Revit 응용 프로그램에 활성화된 문서(doc)에 모델링 된 문(Door)에서 -> 문(Door)이 왼쪽 -> 오른쪽 또는 문(Door)이 오른쪽 -> 왼쪽으로 방향 변환 완료 
        public void Execute(UIApplication uiapp)
        {
            try
            {
                switch (Request.Take())
                {
                    case RequestId.None:
                        {
                            return;  // no request at this time -> we can leave immediately
                        }
                    case RequestId.Delete:
                        {
                            ModifySelectedDoors(uiapp, "Delete doors", e => e.Document.Delete(e.Id));
                            break;
                        }
                    case RequestId.FlipLeftRight:
                        {
                            ModifySelectedDoors(uiapp, "Flip door Hand", e => e.flipHand());
                            break;
                        }
                    case RequestId.FlipInOut:
                        {
                            ModifySelectedDoors(uiapp, "Flip door Facing", e => e.flipFacing());
                            break;
                        }
                    case RequestId.MakeLeft:
                        {
                            ModifySelectedDoors(uiapp, "Make door Left", MakeLeft);
                            break;
                        }
                    case RequestId.MakeRight:
                        {
                            ModifySelectedDoors(uiapp, "Make door Right", MakeRight);
                            break;
                        }
                    case RequestId.TurnOut:
                        {
                            ModifySelectedDoors(uiapp, "Place door Out", TurnOut);
                            break;
                        }
                    case RequestId.TurnIn:
                        {
                            ModifySelectedDoors(uiapp, "Place door In", TurnIn);
                            break;
                        }
                    case RequestId.Rotate:
                        {
                            ModifySelectedDoors(uiapp, "Rotate door", FlipHandAndFace);
                            break;
                        }
                    // TODO: 메서드 "Execute" -> switch ~ case문 로직 -> RequestId.CreateTest 추가 예정 (2023.08.11 jbh)
                    //case RequestId.CreateTest:
                    //    {
                    //        ModifySelectedDoors(uiapp, "", );
                    //    }
                    default:
                        {
                            // some kind of a warning here should
                            // notify us about an unexpected request 
                            break;
                        }
                }
            }
            finally
            {
                // 메서드 "ModifySelectedDoors" 실행 완료 -> finally 문 이동 -> 메서드 "Application.thisApp.WakeFormUp();" 호출 
                Application.thisApp.WakeFormUp();
            }

            return;
        }


        /// <summary>
        ///   The main door-modification subroutine - called from every request 
        /// </summary>
        /// <remarks>
        ///   It searches the current selection for all doors
        ///   and if it finds any it applies the requested operation to them
        /// </remarks>
        /// <param name="uiapp">The Revit application object</param>
        /// <param name="text">Caption of the transaction for the operation.</param>
        /// <param name="operation">A delegate to perform the operation on an instance of a door.</param>
        /// 
        private void ModifySelectedDoors(UIApplication uiapp, String text, DoorOperation operation)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;

            // check if there is anything selected in the active document

            if ((uidoc != null) && (uidoc.Selection != null))
            {
                ICollection<ElementId> selElements = uidoc.Selection.GetElementIds();
                if (selElements.Count > 0)
                {
                    // Filter out all doors from the current selection

                    FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document, selElements);
                    ICollection<Element> doorset = collector.OfCategory(BuiltInCategory.OST_Doors).ToElements();

                    if (doorset != null)
                    {
                        // Since we'll modify the document, we need a transaction
                        // It's best if a transaction is scoped by a 'using' block
                        using (Transaction trans = new Transaction(uidoc.Document))
                        {
                            // The name of the transaction was given as an argument

                            if (trans.Start(text) == TransactionStatus.Started)
                            {
                                // apply the requested operation to every door

                                foreach (FamilyInstance door in doorset)
                                {
                                    operation(door);
                                }

                                trans.Commit();
                            }
                        }
                    }
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////
        //
        // Helpers - simple delegates operating upon an instance of a door

        private void FlipHandAndFace(FamilyInstance e)
        {
            e.flipFacing(); e.flipHand();
        }

        // Note: The door orientation [left/right] is according the common
        // conventions used by the building industry in the Czech Republic.
        // If the convention is different in your county (like in the U.S),
        // swap the code of the MakeRight and MakeLeft methods.

        private static void MakeLeft(FamilyInstance e)
        {
            if (e.FacingFlipped ^ e.HandFlipped) e.flipHand();
        }

        private void MakeRight(FamilyInstance e)
        {
            if (!(e.FacingFlipped ^ e.HandFlipped)) e.flipHand();
        }

        // Note: The In|Out orientation depends on the position of the
        // wall the door is in; therefore it does not necessary indicates
        // the door is facing Inside, or Outside, respectively.
        // The presented implementation is good enough to demonstrate
        // how to flip a door, but the actual algorithm will likely
        // have to be changes in a read-world application.

        private void TurnIn(FamilyInstance e)
        {
            if (!e.FacingFlipped) e.flipFacing();
        }

        private void TurnOut(FamilyInstance e)
        {
            if (e.FacingFlipped) e.flipFacing();
        }

    }  // class

}  // namespace
