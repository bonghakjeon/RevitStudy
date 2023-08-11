//
// (C) Copyright 2003-2019 by Autodesk, Inc. All rights reserved.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.

//
// AUTODESK PROVIDES THIS PROGRAM 'AS IS' AND WITH ALL ITS FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable. 

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

using Autodesk;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace Revit.SDK.Samples.ModelessForm_ExternalEvent.CS
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// </summary>
    public class Application : IExternalApplication
    {
        // class instance
        internal static Application thisApp = null;
        // ModelessForm instance
        private ModelessForm m_MyForm;

        #region IExternalApplication Members
        /// <summary>
        /// Implements the OnShutdown event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            if (m_MyForm != null && m_MyForm.Visible)
            {
                m_MyForm.Close();
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Implements the OnStartup event
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            m_MyForm = null;   // no dialog needed yet; the command will bring it
            thisApp = this;  // static access to this application instance

            CreateRibbonSamplePanel(application); // ���� �޴� ��� 

            return Result.Succeeded;
        }

        /// <summary>
        /// ���� �޴� ���
        /// </summary>
        private void CreateRibbonSamplePanel(UIControlledApplication application)
        {
            try
            {
                // 1 �ܰ� : ���� �� "ModelessForm_ExternalEvent" ����
                string tabName = "ModelessForm_ExternalEvent";
                application.CreateRibbonTab(tabName);   // ���� URL - https://www.revitapidocs.com/2024/8ce17489-75ee-ae81-306d-58f9c505c80c.htm

                // 2 �ܰ� : ���� �� "House" �ȿ� ���ϴ� ���� �г� "Print ModelessForm" ���� 
                string panelName = "Print ModelessForm";
                RibbonPanel panel = application.CreateRibbonPanel(tabName, panelName);

                // 3 �ܰ� : ���� �г� "Print ModelessForm"�ȿ� ���ϴ� ���� ��ư "ModelessForm" ����
                SplitButtonData buttonData = new SplitButtonData("ModelessForm", "ModelessForm"); // buttonData ���� �� buttonData �̸� "ModelessForm" ����
                SplitButton button = panel.AddItem(buttonData) as SplitButton;                    // ���� �г� "Print ModelessForm"�� buttonData �߰� �� SplitButton (button) ���� 

                // ������ �ܰ� : Command�� ���� ���� ���(namespace "Revit.SDK.Samples.ModelessForm_ExternalEvent.CS" -> class "Command")�� �Ǵ� ���� ��ư "ModelessForm" ��ü ���� 
                // "Revit.SDK.Samples.ModelessForm_ExternalEvent.CS.Command" - ���� ����� ���۵Ǵ� ��ġ (namespace "Revit.SDK.Samples.ModelessForm_ExternalEvent.CS" -> class "Command")
                // ModelessForm_ExternalEvent.dll ���� ������ ��, Debug - Any CPU ���� ������ �ϹǷ� ModelessForm_ExternalEvent.dll ������ �Ʒ� ���� ��η� �����ȴ�.
                // D:\bhjeon\RevitStudy\Revit 2024 SDK\Samples\ModelessDialog\ModelessForm_ExternalEvent\CS\bin\Debug\ModelessForm_ExternalEvent.dll
                PushButton pushButton = button.AddPushButton(new PushButtonData("ModelessForm", "ModelessForm", 
                    @"D:\bhjeon\RevitStudy\Revit 2024 SDK\Samples\ModelessDialog\ModelessForm_ExternalEvent\CS\bin\Debug\ModelessForm_ExternalEvent.dll", 
                    "Revit.SDK.Samples.ModelessForm_ExternalEvent.CS.Command"));

                // ���� ��ư "ModelessForm" ������(�̹���) ���(����) 
                // House ������Ʈ ���� -> ���� -> PresentationCore.dll ���� �߰�  
                // ���� ��ư "ModelessForm" ������ �̹��� ������ (32 X 32) - ���� Revit�� �����ϴ� �ٸ� ���� �� �ȿ� ���ϴ� ��ư�� ����� 32 X 32 �̱� ����
                pushButton.LargeImage = convertFromBitmap(Revit.SDK.Samples.ModelessForm_ExternalEvent.CS.Properties.Resources.ModelessForm);  // ������ ����
                pushButton.Image      = convertFromBitmap(Revit.SDK.Samples.ModelessForm_ExternalEvent.CS.Properties.Resources.ModelessForm);  // ������ ����
                pushButton.ToolTip    = "ModelessForm ȭ���� ����մϴ�.";                                                                     // ���� ���� 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// ���ҽ��� ��ϵ� �̹��� ����(.png, .jpg ���...)�� BitmapSource���� convert ���ִ� �޼��� 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        BitmapSource convertFromBitmap(Bitmap bitmap)
        {
            // House ������Ʈ ���� -> ���� -> WindowsBase.dll ���� �߰�  
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
               bitmap.GetHbitmap(),
               IntPtr.Zero,
               Int32Rect.Empty,
               BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        ///   This method creates and shows a modeless dialog, unless it already exists.
        /// </summary>
        /// <remarks>
        ///   The external command invokes this on the end-user's request
        /// </remarks>
        /// 
        public void ShowForm(UIApplication uiapp)
        {
            // If we do not have a dialog yet, create and show it
            if (m_MyForm == null || m_MyForm.IsDisposed)
            {
                // A new handler to handle request posting by the dialog
                RequestHandler handler = new RequestHandler();

                // External Event for the dialog to use (to post requests)
                ExternalEvent exEvent = ExternalEvent.Create(handler);

                // We give the objects to the new dialog;
                // The dialog becomes the owner responsible fore disposing them, eventually.
                m_MyForm = new ModelessForm(exEvent, handler);
                m_MyForm.Show();
            }
        }


        /// <summary>
        ///   Waking up the dialog from its waiting state.
        /// </summary>
        /// 
        public void WakeFormUp()
        {
            if (m_MyForm != null)
            {
                m_MyForm.WakeUp();
            }
        }
        #endregion
    }
}
