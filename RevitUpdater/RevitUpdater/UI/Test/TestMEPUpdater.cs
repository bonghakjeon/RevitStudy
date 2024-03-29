using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RevitUpdater.Test;

using DevExpress.XtraEditors;

namespace RevitUpdater.UI.Test
{
    // TODO : 테스트 용 CommboBox 컨트롤 "cboState", "cboState2" 구현 (2024.03.26 jbh)
    // 유튜브 참고 URL - https://youtu.be/UDDYd3q5WM4?si=wlXr1DnPKTr8KTvy

    // TODO : 테스트 용 CommboBoxEdit 컨트롤 "cmbNames" 구현 (2024.03.26 jbh)
    // 유튜브 참고 URL - https://youtu.be/x37sps7nJFU?si=8x8lBe5s9dYHLkHF
    public partial class TestMEPUpdater : XtraForm
    {
        public TestMEPUpdater()
        {
            InitializeComponent();
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblValue.Text = cboState.Text;
        }

        private void TestMEPUpdater_Load(object sender, EventArgs e)
        {
            cboState.Items.Add("Arizona");
            cboState.Items.Add("Ohio");
            cboState.SelectedIndex = 1;
            // Clear all item cboState2 
            cboState2.Items.Clear();
            // Init data
            List<States> list = new List<States>();
            list.Add(new States() { ID = "01", Name = "Texas"   });
            list.Add(new States() { ID = "02", Name = "Ohio"    });
            list.Add(new States() { ID = "03", Name = "Utah"    });
            list.Add(new States() { ID = "04", Name = "Vermont" });
            // Set display member and value member for comboBox
            cboState2.DataSource = list;
            cboState2.ValueMember   = "ID";
            cboState2.DisplayMember = "Name";

            cmbNames.SelectedIndex = 0;

        }

        private void cboState2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            States obj = cboState2.SelectedItem as States;
            if(obj != null)
                lblValue.Text = obj.Name;
        }

        private void btnAddNames_Click(object sender, EventArgs e)
        {
            string[] name = new string[] { "Natalie", "Cemal", "Garray", "Tomas", "Bill" };
            for(int i = 0; i < 5; i++)
            {
                cmbNames.Properties.Items.Add(name[i]);
            }
        }

        private void btnAddName_Click(object sender, EventArgs e)
        {
            cmbNames.Properties.Items.Add(txtAddName.Text);
            cmbNames.SelectedItem = txtAddName.Text;
            txtAddName.Text = "";

        }

        private void btnSearchName_Click(object sender, EventArgs e)
        {
            cmbNames.SelectedItem = txtSearchName.Text;
        }
    }
}