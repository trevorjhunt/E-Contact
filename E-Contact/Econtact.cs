using EContact.EContactClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EContact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }

        EContactClass c = new EContactClass();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.Address = txtboxAddress.Text;
            c.PhoneNumber = txtboxPhoneNumber.Text;
            c.Gender = comboboxGender.Text;

            // insert the data into the DB table 
            bool success = c.Insert(c);
            if (success == true)
            {
                MessageBox.Show("New Contact inserted");
                Clear();
            }
            else
                MessageBox.Show("New Contact was not inserted. Try again");

            //Load the data to the data grid view
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void EContact_Load(object sender, EventArgs e)
        {
            //Load the data to the data grid view
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void picboxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // clear the values from the input fields
        public void Clear()
        {
            txtboxContactId.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxAddress.Text = "";
            txtboxPhoneNumber.Text = "";
            comboboxGender.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(txtboxContactId.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.Address = txtboxAddress.Text;
            c.PhoneNumber = txtboxPhoneNumber.Text;
            c.Gender = comboboxGender.Text;

            // update the data into the DB table 
            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact updated");
                Clear();
            }
            else
                MessageBox.Show("Contact was not updated. Try again");

            //Load the data to the data grid view
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        // get the data from the data grid view and load in input text fields 
        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // id the row that was mouse clicked
            int rowIndex = e.RowIndex;
            txtboxContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxPhoneNumber.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboboxGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactId.Text);
            bool success = c.Delete(c);

            if (success == true)
            {
                MessageBox.Show("Contact deleted");

                //Load the data to the data grid view.. refresh view
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                Clear();
            }
            else
                MessageBox.Show("Contact was not deleted. Try again");

        }

        static string my_connection_string = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            // get the value from the text box
            string keyword = txtboxSearch.Text;

            SqlConnection conn = new SqlConnection(my_connection_string);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM EContactDbTable WHERE FirstName LIKE '%" + keyword + "' OR LastName LIKE '%" + keyword + "' OR Address LIKE '%" + keyword + "' ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
