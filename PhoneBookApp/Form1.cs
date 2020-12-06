
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhoneBookApp.ServiceReference1;


namespace PhoneBookApp
{
    public partial class Form1 : Form
    {

        private ServicePhoneBookClient WcfClient = new ServicePhoneBookClient();
        
        public Form1()
        {
            InitializeComponent();
             WcfClient.GetAll();
            dataGridView1.Columns[0].Visible = false;
        }



        private void RemoveTxt()
        {
            txtName.Text = "";
            txtNumber.Text = "";
        }

        private async void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                PhoneBook phoneBook = new PhoneBook();
                //phoneBook.Id = Convert.ToInt32(txtId.Text);
                phoneBook.Name = txtName.Text;
                phoneBook.Number = txtNumber.Text;

                PhoneBook[] db = WcfClient.GetAll();
                bool x = false;
                for (int i = 0; i <db.Length; i++)
                {
                    if(db[i].Number != txtNumber.Text)
                    {
                        x = true;
                        break;
                    }
                    else
                    {
                        MessageBox.Show("You can't add record because it exists");
                        break;
                    }
                }
                if (x)
                {
                    await Task.Run(() =>
                    {
                        WcfClient.AddPhoneBook(phoneBook);
                        return;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RemoveTxt();
        }

        private async void btnDelate_Click(object sender, EventArgs e)
        {

            try
            {
                PhoneBook phoneBook = new PhoneBook();
                phoneBook.Id = Convert.ToInt32(txtId.Text);
                //phoneBook.Name = txtName.Text;
                //phoneBook.Number = txtNumber.Text;

                await Task.Run(() =>
                {
                    WcfClient.DelatePhoneBook(phoneBook.Id);
                });
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            dataGridView1.DataSource = WcfClient.GetAll();
            RemoveTxt();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            PhoneBook phoneBook = new PhoneBook();

            phoneBook.Id = Convert.ToInt32(txtId.Text);
            phoneBook.Name = txtName.Text;
            phoneBook.Number = txtNumber.Text;

           

            await Task.Run(() =>
            {
                WcfClient.UpdatePhoneBook(phoneBook);

            });
                dataGridView1.DataSource = WcfClient.GetAll();

            RemoveTxt();
        }

        private void btnSelectbyID_Click(object sender, EventArgs e)
        {
            int searchId = Convert.ToInt32(txtId.Text);

            PhoneBook phoneBook = WcfClient.GetById(searchId);

            if (phoneBook == null)
            {
                MessageBox.Show("There is no record with the specified id!");

                return;
            }
            BindingSource bi = new BindingSource();
            bi.DataSource = phoneBook;
            dataGridView1.DataSource = bi;
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = WcfClient.GetAll();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string searchNumber = txtNumber.Text;
            PhoneBook phoneBook = WcfClient.GetByNumber(searchNumber);

            if (phoneBook == null)
            {
                MessageBox.Show("There is no record with the specified PhoneNumber!");

                return;
            }

            BindingSource bi = new BindingSource();
            bi.DataSource = phoneBook;
            dataGridView1.DataSource = bi;

            RemoveTxt();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string searchName = txtName.Text;

            dataGridView1.DataSource = WcfClient.GetByName(searchName);
            RemoveTxt();
        }

        private  void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PhoneBook phonebook = new PhoneBook();
            phonebook.Id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            phonebook.Name = (string)dataGridView1.CurrentRow.Cells[1].Value.ToString();
            phonebook.Number = (string)dataGridView1.CurrentRow.Cells[2].Value.ToString();


            try
            {
                txtId.Text = phonebook.Id.ToString();
                txtName.Text = phonebook.Name;
                txtNumber.Text = phonebook.Number;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

       
    }
}
