﻿using System;
using System.Data;
using System.Windows.Forms;

namespace LinenSys
{
    public partial class frmUpdateLinen : Form
    {
        frmMainMenu parent;
        DataTable dt = new DataTable();


        public frmUpdateLinen(frmMainMenu Parent)
        {
            InitializeComponent();
            parent = Parent;
        }

        private void btnGetLinen_Click(object sender, EventArgs e)
        {
            grpLinen.Visible = false;
            cboLinenNames.Visible = false;

            if (txtLinenCode.Text.Equals(""))
            {
                MessageBox.Show("Linen Code must be entered", "Error");
                txtLinenCode.Focus();
                return;
            }

            cboLinenNames.Items.Clear();
            dt.Clear();

            dt = Linen.getMatchingNames(dt, txtLinenCode.Text.ToUpper());

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["LINEN_STATUS"].ToString().Equals("A"))
                {
                    cboLinenNames.Items.Add(dt.Rows[i]["LINEN_NAME"].ToString().Trim());
                }
            }

            if (cboLinenNames.Items.Count == 0)
            {
                MessageBox.Show("No active linen matching linen code was found, please re-enter");
                txtLinenCode.Focus();
                return;
            }

            cboLinenNames.Visible = true;
        }

        private void btnUpdateLinen_Click(object sender, EventArgs e)
        {
            //validate data
            float check;
            int checkInt;

            if (txtLinenName.Text.Equals("") && txtHirePrice.Text.Equals("") && txtCleaningPrice.Text.Equals("") && txtRejectPrice.Text.Equals("") && txtPackSize.Text.Equals(""))
            {
                MessageBox.Show("At least one field must be entered", "Error");
                txtLinenName.Focus();
                return;
            }

            foreach (char item in txtLinenName.Text)
            {
                if (char.IsDigit(item))
                {
                    MessageBox.Show("Linen Name must only contain letters", "Error");
                    txtLinenName.Focus();
                    return;
                }
            }

            if (!float.TryParse(txtHirePrice.Text, out check))
            {
                MessageBox.Show("Hire Price must be numeric", "Error");
                txtHirePrice.Focus();
                return;
            }

            if (!float.TryParse(txtCleaningPrice.Text, out check))
            {
                MessageBox.Show("Cleaning Price must be numeric", "Error");
                txtCleaningPrice.Focus();
                return;
            }

            if (!float.TryParse(txtRejectPrice.Text, out check))
            {
                MessageBox.Show("Reject Price must be numeric", "Error");
                txtRejectPrice.Focus();
                return;
            }

            if (!int.TryParse(txtPackSize.Text, out checkInt))
            {
                MessageBox.Show("Pack Size must be numeric", "Error");
                return;
            }

            else
            {
                Linen updatedLinen = new Linen();
                updatedLinen.setLinen_code(txtLinenCodeForUpdate.Text);

                if(!txtLinenName.Text.Equals(""))
                {
                    updatedLinen.setLinen_name(txtLinenName.Text);
                }
                if (!txtHirePrice.Text.Equals(""))
                {
                    updatedLinen.setHire_price(Convert.ToDouble(txtHirePrice.Text));
                }
                if (!txtCleaningPrice.Text.Equals(""))
                {
                    updatedLinen.setCleaning_price(Convert.ToDouble(txtCleaningPrice.Text));
                }
                if (!txtRejectPrice.Text.Equals(""))
                {
                    updatedLinen.setReject_price(Convert.ToDouble(txtRejectPrice.Text));
                }
                if (!txtPackSize.Text.Equals(""))
                {
                    updatedLinen.setPack_size(Convert.ToInt32(txtPackSize.Text));
                }

                updatedLinen.updateLinen();

                String updateLinen;
                updateLinen = "\nLinen Name: " + txtLinenName.Text + "\nLinen Code: " + txtLinenCodeForUpdate.Text +
                             "\nHire Price: " + txtHirePrice.Text + "\nCleaning Price: " +
                             txtCleaningPrice.Text + "\nReject Price: " + txtRejectPrice.Text+ "\nPack Size: " +txtPackSize.Text;

                MessageBox.Show("The updated lined has been saved to the system." + updateLinen, "Update Linen");

                txtLinenName.Clear();
                txtLinenCode.Clear();
                txtHirePrice.Clear();
                txtCleaningPrice.Clear();
                txtRejectPrice.Clear();
                txtPackSize.Clear();
                return;
            }
        }

        private void cboLinenNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpLinen.Visible = true;

            txtLinenName.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["LINEN_NAME"].ToString().Trim();
            txtHirePrice.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["HIRE_PRICE"].ToString();
            txtCleaningPrice.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["CLEANING_PRICE"].ToString();
            txtRejectPrice.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["REJECT_PRICE"].ToString();
            txtPackSize.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["PACK_SIZE"].ToString();
            txtLinenCodeForUpdate.Text = dt.Rows[Convert.ToInt32(cboLinenNames.SelectedIndex)]["LINEN_CODE"].ToString();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            parent.Show();
        }

        private void frmUpdateLinen_Load(object sender, EventArgs e){
        }
    }
}
