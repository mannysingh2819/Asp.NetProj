using System;
using System.Net.Http;
using System.Net;
using APFProductUpdateProj.SFDC;
using System.Data;
using APFProductUpdateProj.Classes;


namespace APFProductUpdateProj
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string httpmsg;
        APFProduct apfProduct = new APFProduct();
        protected void Page_Load(object sender, EventArgs e)
        {

            lblmsg.Text = "";
            theDiv.Visible = false;
            sflink.Visible = false;
            divMsg.Visible = false;
        }
        
        protected async void PostRequest()
        {
            httpmsg = "";
            divMsg.Visible = true;
            apfProduct.bc_product_id = Convert.ToInt32(txtproductId.Text);
            apfProduct.l5_directory_code= txtdircode.Text;
            apfProduct.advertiser_id = Convert.ToInt32(txtadvertiserId.Text);
            apfProduct.product_status= txtstatus.Text ;
            apfProduct.l5_heading_code = txtheadingcode.Text;
            apfProduct.l5_directory_issue= Convert.ToInt32(txtdirissue.Text);
            apfProduct.l5_listing_id= Convert.ToInt32(txtlistId.Text) ;
            apfProduct.content = txtcontent.Text;
            apfProduct.attributes= txtlistingId.Text ;
            apfProduct.dir_type = txtdirtype.Text;
            apfProduct.appearances = txtappearanceId.Text;
            apfProduct.main_source_id = Convert.ToInt32(txtmainprodid.Text);
            apfProduct.product_code= txtproductcode.Text;
            apfProduct.publish_ind= drppublishInd.SelectedValue;
            apfProduct.effective_version_ind= drpeffectiveversionInd.SelectedValue;

            
            HttpResponseMessage response = await apfProduct.CreateSoapEnvelop();
            
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var messageContents = await response.Content.ReadAsStringAsync();
                    string msg = messageContents.ToString();
                    httpmsg = response.StatusCode + " Message Sent successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    ClearFields();
                    
                }
                else {
                    httpmsg = response.StatusCode + " An Error Occurred in sending message to APF";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                lblmsg.Text = httpmsg;
                theDiv.Visible = false;
           
        }

        protected Boolean connectSF()
        {
                LoginResult lgResult = apfProduct.loginSF();
                bool login = lgResult.passwordExpired ? true : false;

                if (login)
                {
                    lblmsg.Text = "An error has occurred. Your password has expired.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
           
            QueryResult queryResult = apfProduct.connectToSF("'" + txtproductId.Text + "'");

            if (queryResult.size > 0)
            {

                Print_Line_Item__c pl = (Print_Line_Item__c)queryResult.records[0];
                string PliId = pl.Id;
                lblmsg.Text = "This is not an Orphan product Id. PLI found in salesforce";
                sflink.Visible = true;
                sflink.NavigateUrl= "https://sensissf.my.salesforce.com/" + PliId;
                sflink.Text = "https://sensissf.my.salesforce.com/" + PliId;
                sflink.Target = "_blank";
                sflink.Width = 450;
                sflink.BackColor = System.Drawing.Color.BurlyWood;
                sflink.ToolTip = "Click on this link to go to salesforce";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else
            {
                return true;
            }

        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
                divMsg.Visible = true;
            
                theDiv.Visible = false;
                
            
            if (!IsPostBack)
            {
                string btnscript = "$(document).ready(function () { $('[id*=btnsend]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", btnscript, true);
            }

            bool check ;
            check = connectSF();

            if (check)
            {
                PostRequest();
            }
            else
            {
                ClearFields();
            }


        }
        protected void ClearFields()
        {
            txtadvertiserId.Text = string.Empty;
            txtdircode.Text      = string.Empty;
            txtdirissue.Text     = string.Empty;
            txtlistId.Text       = string.Empty;
            txtlistingId.Text    = string.Empty;
            txtmainprodid.Text   = string.Empty;
            txtproductcode.Text  = string.Empty;
            txtproductId.Text    = string.Empty;
            txtheadingcode.Text  = string.Empty;
        }
        

        
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            divMsg.Visible = true;
            if (!string.IsNullOrEmpty(this.txtproductId.Text)) {
                theDiv.Visible = true;

                DataTable dap = apfProduct.getAttributes(txtproductId.Text);
                DataTable dar = apfProduct.getApperances(txtproductId.Text);
              //DataTable dp  = apfProduct.getDataTable(txtproductId.Text);
                
                apfProduct.attributes = apfProduct.getAttributeString(txtproductId.Text);
                DataTable dp  =  apfProduct.getBulkDataTable(txtproductId.Text, apfProduct.attributes);

                
                if (dp != null && dap != null && dar != null) 
                    {
                    grdview.DataSource = dp;
                    grdview.DataBind();
                    txtdircode.Text                         = dp.Rows[0]["L5_DIRECTORY_CODE"].ToString();
                    txtadvertiserId.Text                    = dp.Rows[0]["ADVERTISER_ID"].ToString();
                    txtheadingcode.Text                     = dp.Rows[0]["L5_HEADING_CODE"].ToString();
                    txtcurrentStatus.Text                   = dp.Rows[0]["PRODUCT_STATUS"].ToString();
                    txtstatus.Text                          = "C";
                    txtdirissue.Text                        = dp.Rows[0]["L5_DIRECTORY_ISSUE"].ToString();
                    txtlistId.Text                          = dp.Rows[0]["L5_LISTING_ID"].ToString();
                    txtlistingId.Text                       = dap.Rows[0]["ATTRIBUTES"].ToString();
                    txtappearanceId.Text                    = dar.Rows[0]["APPEARANCES"].ToString();
                    txtdirtype.Text                         = dp.Rows[0]["DIR_TYPE"].ToString();
                    txtcontent.Text                         = dp.Rows[0]["CONTENT_DATA"].ToString();
                    txtmainprodid.Text                      = dp.Rows[0]["L5_MAIN_PRODUCT_ID"].ToString();
                    txtproductcode.Text                     = dp.Rows[0]["PRODUCT_CODE"].ToString();
                    drppublishInd.SelectedValue             = dp.Rows[0]["PUBLISH_IND"].ToString();
                    drpeffectiveversionInd.SelectedValue    = dp.Rows[0]["EFFECTIVE_VERSION_IND"].ToString();

                }
                else
                {
                    ClearFields();
                    lblmsg.Text = "This Product may already be cancelled in APF.Please check APF";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    theDiv.Visible = false;

                }
            }
        
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}