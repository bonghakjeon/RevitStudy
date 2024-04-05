using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace Lab1PlaceGroup
{
    // TODO : dll 파일 2개 "RevitAPI.dll", "RevitAPIUI.dll" 참조-속성 에 속하는 [로컬복사] 항목 false로 설정 (2024.01.17 jbh)
    //        로컬복사 항목이란? 프로젝트 빌드시 참조dll을 [참조경로]에서 [출력경로]로 복사할지를 설정하는 기능
    // 참고 URL - https://blog.naver.com/haes82/98583862
    [Transaction(TransactionMode.Manual)]       // 트랜잭션 작동 방식 - 수동 
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Get application and document objects
            UIApplication uiapp = commandData.Application;
            Document doc        = uiapp.ActiveUIDocument.Document;

            try
            {
                frmMain main = new frmMain();
                main.ShowDialog();
                // main.Show();

                //Define a reference Object to accept the pick result
                Reference pickedRef = null;

                //Pick a group
                Selection sel = uiapp.ActiveUIDocument.Selection;
                
                GroupPickFilter selFilter = new GroupPickFilter();
                
                pickedRef = sel.PickObject(ObjectType.Element, selFilter, "Please select a group");
                
                Element elem = doc.GetElement(pickedRef);
                Group group  = elem as Group;

                // Get the group's center point
                XYZ origin   = GetElementCenter(group);

                // Get the room that the picked group is located in
                Room room    = GetRoomOfGroup(doc, origin);

                // Get the room's center point  
                XYZ sourceCenter = GetRoomCenter(room);
                /* 
                string coords =
                "X = " + sourceCenter.X.ToString() + "\r\n" +
                "Y = " + sourceCenter.Y.ToString() + "\r\n" +
                "Z = " + sourceCenter.Z.ToString();

                TaskDialog.Show("Source room Center", coords);
                */

                // Pick point
                // XYZ point = sel.PickPoint("Please pick a point to place group");

                // Ask the user to pick target rooms
                RoomPickFilter roomPickFilter = new RoomPickFilter();
                IList<Reference> rooms        = sel.PickObjects(ObjectType.Element, roomPickFilter, "Select target rooms for duplicate furniture group");

                // Place furniture in each of the rooms
                // Place the group
                Transaction trans = new Transaction(doc);
                trans.Start("Lab");

                // doc.Create.PlaceGroup(point, group.GroupType);
                // Calculate the new group's position
                // XYZ groupLocation = sourceCenter + new XYZ(20, 0, 0);
                // doc.Create.PlaceGroup(groupLocation, group.GroupType);
                PlaceFurnitureInRooms(doc, rooms, sourceCenter, group.GroupType, origin);

                trans.Commit();

                return Result.Succeeded;
            }
            // If the user right-clicks or presses Esc, handle the exception
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }

        public XYZ GetElementCenter(Element e)
        {
            BoundingBoxXYZ bounding = e.get_BoundingBox(null);
            XYZ center = (bounding.Max + bounding.Min) * 0.5;
            return center;
        }

        /// Return the room in which the given point is located
        Room GetRoomOfGroup(Document doc, XYZ point)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_Rooms);
            Room room = null;
            foreach (Element elem in collector)
            {
                room = elem as Room;
                if (room != null)
                {
                    // Decide if this point is in the picked room                  
                    if (room.IsPointInRoom(point))
                    {
                        break;
                    }
                }
            }
            return room;
        }

        /// Return a room's center point coordinates.
        /// Z value is equal to the bottom of the room
        public XYZ GetRoomCenter(Room room)
        {
            // Get the room center point
            XYZ boundCenter = GetElementCenter(room);
            LocationPoint locPt = (LocationPoint)room.Location;
            XYZ roomCenter = new XYZ(boundCenter.X, boundCenter.Y, locPt.Point.Z);
            return roomCenter;
        }

        /// Copy the group to each of the provided rooms. The position
        /// at which the group should be placed is based on the target
        /// room's center point: it should have the same offset from
        /// this point as the original had from the center of its room
        public void PlaceFurnitureInRooms(Document doc, IList<Reference> rooms, XYZ sourceCenter, GroupType gt, XYZ groupOrigin)
        {
            XYZ offset = groupOrigin - sourceCenter;
            XYZ offsetXY = new XYZ(offset.X, offset.Y, 0);
            foreach (Reference r in rooms)
            {
                // Room roomTarget = doc.GetElement(r) as Room;
                Room roomTarget = doc.GetElement(r) as Room;
                if (roomTarget != null)
                {
                    XYZ roomCenter = GetRoomCenter(roomTarget);
                    Group group = doc.Create.PlaceGroup(roomCenter + offsetXY, gt); 
                }
            }
        }
    }

    // Filter to constrain picking to model groups. Only model groups
    // are highlighted and can be selected when cursor is hovering.
    public class GroupPickFilter : ISelectionFilter
    {
        public bool AllowElement(Element e)
        {
            // throw new NotImplementedException();
            // 룸만 필터될 수 있도록 처리
            // true로 반환되어야 룸만 필터링 처리 가능
            // TODO : Null 조건부 연산자 ?. 구현 (2024.01.18 jbh)
            // 참고 URL - https://velog.io/@jinuku/C-%EB%B0%8F-.-%EC%97%B0%EC%82%B0%EC%9E%90
            return (e.Category?.Id.Value.Equals((long)BuiltInCategory.OST_IOSModelGroups) == true);
        }

        public bool AllowReference(Reference r, XYZ p)
        {
            return false;
        }
    }

    // Filter to constrain picking to rooms
    public class RoomPickFilter : ISelectionFilter
    {
        public bool AllowElement(Element e)
        {
            // throw new NotImplementedException();
            return (e.Category?.Id.Value.Equals((long)BuiltInCategory.OST_Rooms) == true);
        }

        public bool AllowReference(Reference r, XYZ p)
        {
            // throw new NotImplementedException();
            return false;
        }
    }
}
