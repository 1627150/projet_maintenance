using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CS2013
{
    public partial class Gestion : System.Web.UI.Page
    {
        ClCompte m_compteConnecte = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "jquerry", "http://code.jquery.com/jquery-latest.js");

            m_compteConnecte = (ClCompte)Session["utilisateur"];

            if (m_compteConnecte == null)
                Response.Redirect("~/Pages/FinSession.aspx");

            if (!IsPostBack)
            {
                BindDestinataires();
                BindModeEnvoi();
            }
        }

        private void BindDestinataires()
        {
            // On bind les types de concentration dans le combobox
            dropDestinataires.DataSource = CreateDataSourceTypeConcentration();
            dropDestinataires.DataTextField = "TITLE";
            dropDestinataires.DataValueField = "TYPE_CONCENTRATION";
            dropDestinataires.DataBind();
            dropDestinataires.SelectedIndex = 0;
        }

        private void BindModeEnvoi()
        {
            // On bind les types de concentration dans le combobox
            dropMode.DataSource = CreateDataSourceModeEnvoi();
            dropMode.DataTextField = "TITLE";
            dropMode.DataValueField = "MODE";
            dropMode.DataBind();
            dropMode.SelectedIndex = 0;
        }

        ICollection CreateDataSourceTypeConcentration()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("TITLE", typeof(String)));
            dataTable.Columns.Add(new DataColumn("TYPE_CONCENTRATION", typeof(int)));

            DataRow dataRowDefault = dataTable.NewRow();
            dataRowDefault[0] = "Tous";
            dataRowDefault[1] = -1;
            dataTable.Rows.Add(dataRowDefault);

            foreach (TypeConcentration type in Enum.GetValues(typeof(TypeConcentration)))
            {
                if (type == TypeConcentration.NULL)
                    continue;

                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = Utilitaire.GetTypeConcentrationString(type);
                dataRow[1] = (int)type;

                dataTable.Rows.Add(dataRow);
            }

            DataRow dataRowManual = dataTable.NewRow();
            dataRowManual[0] = "Mode manuel";
            dataRowManual[1] = 10;
            dataTable.Rows.Add(dataRowManual);

            return new DataView(dataTable);
        }

        ICollection CreateDataSourceModeEnvoi()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("TITLE", typeof(String)));
            dataTable.Columns.Add(new DataColumn("MODE", typeof(int)));

            DataRow dataRowDefault = dataTable.NewRow();
            dataRowDefault[0] = "Courriel";
            dataRowDefault[1] = 1;
            dataTable.Rows.Add(dataRowDefault);
            DataRow dataRowDefault1 = dataTable.NewRow();
            dataRowDefault1[0] = "Message texte";
            dataRowDefault1[1] = 2;
            dataTable.Rows.Add(dataRowDefault1);
            DataRow dataRowDefault2 = dataTable.NewRow();
            dataRowDefault2[0] = "Courriel et message texte";
            dataRowDefault2[1] = 3;
            dataTable.Rows.Add(dataRowDefault2);

            return new DataView(dataTable);
        }

        protected void sendMail(object sender, EventArgs args)
        {
            errorTitle.InnerText = errorContent.InnerText = errorRecipients.InnerText = "";
            bool error = false;

            if (String.IsNullOrEmpty(titreMessage.Text))
            {
                errorTitle.InnerText = "Champs requis";
                error = true;
            }

            if (String.IsNullOrEmpty(message.Text))
            {
                errorContent.InnerText = "Champs requis";
                error = true;
            }

            if (error)
                modal.Show();

            List<string> emailList = new List<string>();
            if (Convert.ToInt32(dropDestinataires.SelectedValue) == 10)
                emailList.AddRange(manualDest.Text.Split(';'));
            else
                emailList = GetEmailList();

            if (emailList.Count == 0)
            {
                errorRecipients.InnerText = "Aucun destinataire parmi la sélection";
                modal.Show();
            }

            List<List<string>> list = Utilitaire.Chunk<string>(emailList, 95);

            foreach (List<string> listeEmail in list)
            {
                MailMessage msg = new MailMessage();
                msg.Body = String.Format(message.Text);
                msg.IsBodyHtml = true;
                msg.Subject = titreMessage.Text;
                msg.To.Add("admcreditsante@gmail.com");

                foreach (string email in listeEmail)
                    msg.Bcc.Add(email);

                new SmtpClient().Send(msg);
            }

            titreMessage.Text = message.Text = "";
        }

        private List<string> GetEmailList()
        {
            switch (Convert.ToInt32(dropMode.SelectedValue))
            {
                case 1:
                    if (Convert.ToInt32(dropDestinataires.SelectedItem.Value) == -1)
                        return ClCourtierCompte.GetInstance().GetAllEMails();
                    else
                        return ClCourtierCompte.GetInstance().GetAllEMailsByTypeConcentration((TypeConcentration)Convert.ToInt32(dropDestinataires.SelectedItem.Value));

                case 2:
                    if (Convert.ToInt32(dropDestinataires.SelectedItem.Value) == -1)
                        return ClCourtierCompte.GetInstance().GetAllEmailPhone();
                    else
                        return ClCourtierCompte.GetInstance().GetAllEMailsPhoneByTypeConcentration((TypeConcentration)Convert.ToInt32(dropDestinataires.SelectedItem.Value));

                case 3:
                    List<string> liste = new List<string>();

                    liste.AddRange(ClCourtierCompte.GetInstance().GetAllEMails());
                    liste.AddRange(ClCourtierCompte.GetInstance().GetAllEmailPhone());

                    return liste;

                default:
                    return new List<string>();
            }
        }

        protected void dropMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32((sender as DropDownList).SelectedValue))
            {
                case 10 :
                    manualDest.Visible = true;
                    dropMode.Visible = false;
                    labelMode.Visible = false;
                    break;
                default :
                    manualDest.Visible = false;
                    dropMode.Visible = true;
                    labelMode.Visible = true;
                    break;
            }

            modal.Show();
        }
    }
}