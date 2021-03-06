﻿using System;
using System.Data;
using System.Windows.Forms;

namespace LinenSys
{
    public partial class frmRemoveLinen : Form
    {
        DataTable dt = new DataTable();

        frmMainMenu parent;
        public frmRemoveLinen(frmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
        }

        private void btnGetLinen_Click(object sender, EventArgs e)
        {
            lblLinenName.Visible = false;
            cboLinenName.Visible = false;
            btnRemoveLinen.Visible = false;

            if (txtLinenCode.Text.Equals(""))
            {
                MessageBox.Show("Linen Code must be entered", "Error");
                txtLinenCode.Focus();
                return;
            }

            cboLinenName.Items.Clear();
            dt.Clear();

            dt = Linen.getMatchingNames(dt, txtLinenCode.Text.ToUpper());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LINEN_STATUS"].ToString().Equals("A"))
                {
                    cboLinenName.Items.Add(dt.Rows[i]["LINEN_NAME"]);
                }
            }

            if (cboLinenName.Items.Count == 0)
            {
                MessageBox.Show("No active linen matching linen code was found, please re-enter");
                txtLinenCode.Focus();
                return;
            }

            cboLinenName.Visible = true;
            lblLinenName.Visible = true;
        }

        private void btnRemoveLinen_Click(object sender, EventArgs e)
        {
            Linen linenToRemove = new Linen();
            linenToRemove.setLinen_code(dt.Rows[Convert.ToInt32(cboLinenName.SelectedIndex)]["LINEN_CODE"].ToString());
            linenToRemove.removeLinen();

            MessageBox.Show("Selected linen has been removed", "Removed");
            cboLinenNames.Items.Clear();
            txtLinenCode.Clear();
            return;
        }

        private void frmRemoveLinen_Load(object sender, EventArgs e)
        {

        }

        private void cboLinenName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveLinen.Visible = true;
        }
    }
}
