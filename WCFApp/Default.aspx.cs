using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCFApp.PhoneBookServiceReference;

namespace WCFApp
{
    public partial class Default : System.Web.UI.Page
    {
        ServicePhoneBookClient spc = new ServicePhoneBookClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ServicePhoneBookClient spc = new ServicePhoneBookClient();
                this.GridViewPhoneBook.DataSource = spc.GetAll().Select(p =>
                  new { id = p.Id, name = p.Name, number = p.Number }).ToList();
                this.GridViewPhoneBook.DataBind();
            }
        }

        protected void GridViewPhoneBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PhoneBook p = this.spc.GetById(Convert.ToInt32
                    (this.TextBoxId.Text));
                this.LabelName.Text = p.Name;
                this.LabelNumber.Text = p.Number;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}