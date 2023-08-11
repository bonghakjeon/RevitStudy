﻿//
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit.SDK.Samples.ImportExport.CS
{
   /// <summary>
   /// Data class which stores information of lower priority for exporting PDF format.
   /// </summary>
   public partial class ExportPDFOptionsForm : Form
   {
      /// <summary>
      /// ExportPDFData object
      /// </summary>
      private ExportPDFData m_data;

      // C# 오류 코드 "CS1591" 오류 메시지 "공개된 'ExportPDFData.Combine' 멤버 또는 형식에 대한 XML 주석이 없습니다."
      // 오류 해결 참고 URL - https://ehpub.co.kr/c-10-5-xml-%EB%AC%B8%EC%84%9C-%ED%8C%8C%EC%9D%BC/
      /// <summary>
      /// ExportPDFOptionsForm 생성자
      /// </summary>
      public ExportPDFOptionsForm(ExportPDFData data)
      {
         m_data = data;
         InitializeComponent();
         Initialize();
      }

      /// <summary>
      /// Initialize controls
      /// </summary>
      private void Initialize()
      {
         checkBoxCombineViews.Checked = m_data.Combine;
      }

      private void buttonOK_Click(object sender, EventArgs e)
      {
         m_data.Combine = checkBoxCombineViews.Checked;
      }
   }
}
