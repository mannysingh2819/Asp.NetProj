using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using APFProductUpdateProj.SFDC;
using System.Text;
using APFProductUpdateProj.Classes;
using System.Collections;

namespace APFProductUpdateProj
{
    public partial class CancelApfListing : System.Web.UI.Page 
    {
 
        string httpmsg;
        HttpContent data;
        DataTable ds;
        APFProduct apfProduct = new APFProduct();
        string[] stringArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divGrid.Visible = false;
            }

        }
        public void createBatch(int batchsize, List<string> tempArray, List<string> mainArray)
        {


            for (int i = 0; i < mainArray.Count; i++)
            {
                tempArray.Add(mainArray[i]);

                if (i == batchsize)
                {
                    tempArray.Clear();
                }
            }

        }
        protected void getDataSet()
        {
            apfProduct.filePath = hffilePath.Value;
            string fileContents = string.Empty;
            using (StreamReader str = new StreamReader(apfProduct.filePath))
            {
                fileContents = str.ReadToEnd();
            }
            stringArray = fileContents.Split(new char[] { ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


            ArrayList strArray = new ArrayList();
            int val = 0;
            foreach (string c in stringArray)
            {
                if (c.ToString() == null || c.ToString() == string.Empty || c.ToString() == "")
                {
                    lblmg.Text = "Check the file contents.You have empty fields with no data";
                    lblmg.ForeColor = System.Drawing.Color.Red;
                    divGrid.Visible = false;
                    return;
                }
                if (int.TryParse(c, out val))
                {
                    strArray.Add(c);
                }
            }

            ///instantiating a delegate
            Func<string, DataTable> delgateOracleConn = apfProduct.createOracleConnection;

            string strb = string.Join(",", strArray.ToArray());

            List<string> sp = new List<string>();

            int batchsize = 55;

            List<HttpContent> Listxml = new List<HttpContent>();

            sp = createChunk(stringArray.ToList(), batchsize);

            string listingstring = apfProduct.getListingDataTable(strb);

            ds = delgateOracleConn.Invoke(listingstring);


        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                divGrid.Visible = true;
                lblmg.Text = string.Empty;
                apfProduct.folderPath = ConfigurationManager.AppSettings["FolderPath"];
                apfProduct.filename = Path.GetFileName(fileUpload.PostedFile.FileName);
                apfProduct.filePath = Server.MapPath(apfProduct.folderPath + "/" + apfProduct.filename);
                apfProduct.fileExtn = Path.GetExtension(fileUpload.PostedFile.FileName);
                apfProduct.filesize = fileUpload.PostedFile.ContentLength.ToString();
                hffilePath.Value = apfProduct.filePath;
                lblfileContents.Text = "<br/>File Name :" + apfProduct.filename + "<br/><br/>File Path :" + apfProduct.filePath + "<br/><br/>File Size in Bytes :" + apfProduct.filesize;
                lblfileContents.ForeColor = System.Drawing.Color.BurlyWood;
                if (apfProduct.fileExtn.ToUpper() != ".CSV")
                {
                    lblmg.Text = "Only csv file allowed";
                    divGrid.Visible = false;
                    return;
                }

                DataTable dt = new DataTable();

                fileUpload.SaveAs(apfProduct.filePath);
                string fileContents = string.Empty;
                using (StreamReader str = new StreamReader(apfProduct.filePath))
                {
                    fileContents = str.ReadToEnd();
                }
                stringArray = fileContents.Split(new char[] { ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                //if (stringArray.Count() > 1000)
                //{
                //    lblmg.Text = "Can only process 1000 id\'s in a batch";
                //    lblmg.ForeColor = System.Drawing.Color.Red;
                //    divGrid.Visible = false;
                //    return;
                //}

                ArrayList strArray = new ArrayList();
                int val = 0;
                foreach (string c in stringArray)
                {
                    if (c.ToString() == null || c.ToString() == string.Empty || c.ToString() == "")
                    {
                        lblmg.Text = "Check the file contents.You have empty fields with no data";
                        lblmg.ForeColor = System.Drawing.Color.Red;
                        divGrid.Visible = false;
                        return;
                    }
                    if (int.TryParse(c, out val) == true)
                    {
                        strArray.Add(c);
                    }
                }
                string uri = "https://api.sensis.com.au/apf/xrrv6qytnuvquwt3eckbmjqb/UpdateListingSDF_EWS_WS/ADAPTOR";

                //Instantiating a delegate
                Func<string, DataTable> delgateOracleConn = apfProduct.createOracleConnection;

                string strb = string.Join(",", strArray.ToArray());

                List<string> sp = new List<string>();

                int batchsize = 100;
                List<HttpContent> Listxml = new List<HttpContent>();

                sp = createChunk(strArray.Cast<string>().ToList(), batchsize);

                foreach (string st in sp)
                {
                    string listingstring = apfProduct.getListingDataTable(st);

                ds = delgateOracleConn.Invoke(listingstring);

                if (ds.Rows.Count > 0)
                {
                    string CustId = string.Empty;
                    StringBuilder sbldr = new StringBuilder();

                    foreach (DataRow dw in ds.Rows)
                    {
                        sbldr.Append(dw["customer_id"].ToString());
                        sbldr.Append(",");
                    }
                    CustId = sbldr.ToString().Substring(0, sbldr.ToString().LastIndexOf(","));

                    string checksf = CheckCustomerIdInSF(CustId);

                   if (string.IsNullOrEmpty(checksf))
                    {
                            lblmg.Text = "The customer Id for this listing Id doesn't exists in salesforce hence it cann't be cancelled";
                            lblmg.ForeColor = System.Drawing.Color.Red;
                    }

                    DataRow[] filtereddatarow = ds.Select("customer_id IN (" + checksf + ")");

                    if (ds.Rows.Count > 0 && filtereddatarow.Length > 0)
                    {
                            foreach (DataRow dw in filtereddatarow)
                            {
                                apfProduct.listing_id = Convert.ToInt32(dw["listing_id"]);
                                apfProduct.listing_version = Convert.ToInt32(dw["listing_version"]);
                                apfProduct.atn_npa = dw["atn_npa"].ToString();
                                apfProduct.atn_cop = dw["atn_cop"].ToString();
                                apfProduct.atn_line_no = dw["atn_line_no"].ToString();
                                apfProduct.listed_tn_npa = dw["listed_tn_npa"].ToString();
                                apfProduct.listed_tn_cop = dw["listed_tn_cop"].ToString();
                                apfProduct.listed_tn_line_no = dw["listed_tn_line_no"].ToString();
                                apfProduct.listed_name = dw["listed_name"].ToString();
                                apfProduct.honorary_title = dw["honorary_title"].ToString();
                                apfProduct.lineage_title = dw["lineage_title"].ToString();
                                apfProduct.designation = dw["designation"].ToString();
                                apfProduct.dial_inst = dw["dial_inst"].ToString();
                                apfProduct.right_justified_text = dw["right_justified_text"].ToString();
                                apfProduct.cross_reference_text = dw["cross_reference_text"].ToString();
                                apfProduct.ali_code = dw["ali_code"].ToString();
                                apfProduct.effective_date = dw["effective_date"].ToString();
                                apfProduct.customer_id = Convert.ToInt32(dw["customer_id"]);
                                apfProduct.main_listing_id = Convert.ToInt32(dw["main_listing_id"]);
                                apfProduct.listing_status = dw["listing_status"].ToString();
                                apfProduct.lni = dw["lni"].ToString();
                                apfProduct.style_code = dw["style_code"].ToString();
                                apfProduct.brg_ind = dw["brg_ind"].ToString();
                                apfProduct.tn_type = dw["tn_type"].ToString();
                                apfProduct.omit_address_ind = dw["omit_address_ind"].ToString();
                                apfProduct.advance_ind = dw["advance_ind"].ToString();
                                apfProduct.adv_completion_date = dw["adv_completion_date"].ToString();
                                apfProduct.publish_ind = dw["publish_ind"].ToString(); ;
                                apfProduct.pla = dw["pla"].ToString();
                                apfProduct.directive_text = dw["directive_text"].ToString();
                                apfProduct.rcf_ind = dw["rcf_ind"].ToString();
                                apfProduct.open_reason = dw["open_reason"].ToString();
                                apfProduct.transaction_code = dw["transaction_code"].ToString();
                                apfProduct.disconnect_reason = dw["disconnect_reason"].ToString();
                                apfProduct.so_type = dw["so_type"].ToString();
                                apfProduct.associate_ind = dw["associate_ind"].ToString();
                                apfProduct.yp_scoping_type = dw["yp_scoping_type"].ToString();
                                apfProduct.follow_street_num = dw["follow_street_num"].ToString();
                                apfProduct.follow_str_type_sfx = dw["follow_str_type_sfx"].ToString();
                                apfProduct.street_num_prefix = dw["street_num_prefix"].ToString();
                                apfProduct.street_no = dw["street_no"].ToString();
                                apfProduct.street_name = dw["street_name"].ToString();
                                apfProduct.street_type_code = dw["street_type_code"].ToString();
                                apfProduct.street_directional = dw["street_directional"].ToString();
                                apfProduct.apartment = dw["apartment"].ToString();
                                apfProduct.locality_id = dw["locality_id"].ToString();
                                apfProduct.loc_name_override = dw["loc_name_override"].ToString();
                                apfProduct.state_name_override = dw["state_name_override"].ToString();
                                apfProduct.country_name = dw["country_name"].ToString();
                                apfProduct.post_code = dw["post_code"].ToString();
                                apfProduct.po_box = dw["po_box"].ToString();
                                apfProduct.optional_address = dw["optional_address"].ToString();
                                apfProduct.optional_addr_type = dw["optional_addr_type"].ToString();
                                apfProduct.postal_addr_type = dw["postal_addr_type"].ToString();
                                apfProduct.po_locality_id = dw["po_locality_id"].ToString();
                                apfProduct.po_post_code = dw["po_post_code"].ToString();

                                string sb = apfProduct.CancelListingXml();
                                data = new StringContent(sb, Encoding.UTF8, "application/soap+xml");
                                Listxml.Add(data);
                            }
                        }
                    }
                }
                PostRequest(uri, Listxml);
                grdCsv.DataSource = ds;
                grdCsv.DataBind();

            }
        }
        private List<string> createChunk(List<string> myArray, int batchsize)
        {
            List<string> LstStr = new List<string>();
            string str = "";
            int numrows = myArray.Count;
            StringBuilder sbr = new StringBuilder();
            if (numrows < batchsize)
            {
                for (int k = 0; k < numrows; k++)
                {
                    sbr.Append("");
                    sbr.Append(myArray[k]);
                    sbr.Append(",");
                  
                }
                str = sbr.ToString();
                str = str.Substring(0, str.LastIndexOf(","));
                LstStr.Add(str);
            }
            else
            {
                for (int i = 0; i < myArray.Count; i++)
                {
                    while (numrows >= batchsize)
                    {
                        for (int j = 0; j < batchsize; j++)
                        {
                            sbr.Append("'");
                            sbr.Append(myArray[i]);
                            sbr.Append("',");
                            i++;
                            numrows = numrows - 1;
                        }
                        str = sbr.ToString();
                        str = str.Substring(0, str.LastIndexOf(","));
                        LstStr.Add(str);
                        str = string.Empty;
                    }

                    if (numrows < batchsize)
                    {
                        for (int k = i; k < myArray.Count; k++)
                        {
                            sbr.Append("'");
                            sbr.Append(myArray[k]);
                            sbr.Append("',");
                             i++;
                        }
                        if (!string.IsNullOrEmpty(str))
                        {
                            str = sbr.ToString();
                            str = str.Substring(0, str.LastIndexOf(","));
                            LstStr.Add(str);
                        }
                    }
                }
            }
            return LstStr;
        }
        private DataTable CreateTableFromOutputStream(string outputStreamText, string tableName)
        {
            //Process output and return
            string[] split = outputStreamText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length >= 2)
            {
                int iteration = 0;

                DataTable table = null;

                foreach (string values in split)
                {
                    if (iteration == 0)
                    {
                        //if (Regex.IsMatch(values, @"\d"))
                        //{

                        //    lblmg.Text = "Enter a Column Header BC_product_Id in the CSV file";
                        //    lblmg.ForeColor = System.Drawing.Color.Red;
                        //    divGrid.Visible = false;
                        //    return null;
                        //}
                        string[] columnNames = SplitString(values);

                        table = new DataTable(tableName);

                        List<DataColumn> columnList = new List<DataColumn>();

                        foreach (string columnName in columnNames)
                        {
                            columnList.Add(new DataColumn(columnName));
                        }
                        table.Columns.AddRange(columnList.ToArray());
                    }
                    else
                    {
                        string[] fields = SplitString(values);
                        if (table != null)
                        {
                            table.Rows.Add(fields);
                        }
                    }
                    iteration++;
                }
                return table;
            }
            return null;
        }

        private string[] SplitString(string inputString)
        {
            System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace | System.Text.RegularExpressions.RegexOptions.Multiline)
                        | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Regex reg = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);
            MatchCollection coll = reg.Matches(inputString);
            string[] items = new string[coll.Count];
            int i = 0;
            foreach (Match m in coll)
            {
                items[i++] = m.Groups[0].Value.Trim('"').Trim(',').Trim('"').Trim();
            }
            return items;
        }

        async void PostRequest(string url, List<HttpContent> xmldata)
        {
            httpmsg = "";
            string msg = "";
            foreach (HttpContent xmld in xmldata)
            {
                HttpResponseMessage response = await apfProduct.CreateSoapEnvelop(xmld, "cancelListing");
                var messageContents = await response.Content.ReadAsStringAsync();
                msg = messageContents.ToString();
                if (msg.Contains("The Selected Listing was not found in the database"))
                {
                    httpmsg = " An Error Occurred in sending message to APF Status Code = " + msg;
                    lblmg.ForeColor = System.Drawing.Color.Red;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    httpmsg = response.Content.ToString() + " Message Sent successfully" + response;
                    lblmg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    httpmsg = " An Error Occurred in sending message to APF Status Code = " + msg;
                    lblmg.ForeColor = System.Drawing.Color.Red;

                }
                lblmg.Text = httpmsg;
                System.Threading.Thread.Sleep(2000);
            }

        }
        protected string CheckCustomerIdInSF(string CustomerNumber)
        {
            LoginResult lgResult = apfProduct.loginSF();
            bool login = lgResult.passwordExpired ? true : false;

            if (login)
            {
                lblmg.Text = "An error has occurred. Your password has expired.";
                lblmg.ForeColor = System.Drawing.Color.Red;

            }

            QueryResult queryResult = apfProduct.CheckCustomerIdInSF(CustomerNumber);
            if (queryResult.size > 0)
            {
                sObject[] Sobj = (sObject[])queryResult.records;
                StringBuilder sd = new StringBuilder();
                foreach (sObject c in Sobj)
                {
                    sd.Append(((Account)c).Legacy_Customer_Number__c);
                    sd.Append(",");
                }
                return sd.ToString().Substring(0, sd.ToString().LastIndexOf(","));
            }
            else
            {
                return "0";
            }

        }

        protected void grdCsv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCsv.PageIndex = e.NewPageIndex;
            getDataSet();
            grdCsv.DataSource = ds;
            grdCsv.DataBind();
        }
    }

}