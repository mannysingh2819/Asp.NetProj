using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using APFProductUpdateProj.SFDC;
using System.Text;
using APFProductUpdateProj.Classes;

namespace APFProductUpdateProj
{
    public partial class bulkload : System.Web.UI.Page
    {
        string httpmsg;
        HttpContent data;
        APFProduct apfProduct = new APFProduct();
        string[] stringArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            divGrid.Visible = false;
            
        }


        protected string getData(String[] str)
        {
            string prodid = null;
            StringBuilder sb = new StringBuilder();
            
            foreach(string s in str)
            {
                prodid=sb.Append("'" + s + "'").ToString();
                prodid +=sb.Append(",");
            }
            return prodid;
     
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                divGrid.Visible = true;
                string folderPath = Server.MapPath(ConfigurationManager.AppSettings["FolderPath"]);
                string filename = Path.GetFileName(fileUpload.PostedFile.FileName);
                string filePath = Path.Combine(folderPath, filename);
                string fileExtn = Path.GetExtension(filePath);
                string filesize = fileUpload.PostedFile.ContentLength.ToString();
                lblfileContents.Text = "<br/>File Name :" + filename + "<br/><br/>File Path :" + filePath + "<br/><br/>File Size in Bytes :" + filesize;
                lblfileContents.ForeColor = System.Drawing.Color.BurlyWood;
                if (fileExtn.ToUpper() != ".CSV")
                {
                    lblmg.Text = "Only csv file allowed";
                    divGrid.Visible = false;
                    return;
                }

                DataTable dt = new DataTable();

                fileUpload.SaveAs(filePath);

                string fileContents = string.Empty;
                using (StreamReader str = new StreamReader(filePath))
                {
                    fileContents = str.ReadToEnd();
                }
                    stringArray = fileContents.Split(new char[] { ',' });

                foreach (string c in stringArray)
                {
                    if (c.ToString() == null || c.ToString() == string.Empty || c.ToString() == "")
                    {
                        lblmg.Text = "Check the file contents.You have empty fields with no data";
                        lblmg.ForeColor = System.Drawing.Color.Red;
                        divGrid.Visible = false;
                        return;
                    }

                }


                dt = CreateTableFromOutputStream(fileContents, "tbl");

                   
                if (dt != null) { 
                    string qrystr = "";
                    List<string> ss = new List<string>();
                    foreach (DataRow de in dt.Rows)
                    {

                        ss.Add(string.Join(",", de.ItemArray.Select(c => c.ToString()).ToArray()));
                    }
                    foreach (string st in ss)
                    {
                        qrystr += "'" + st + "',";
                    }
                    string sp = qrystr.Substring(0, qrystr.LastIndexOf(','));

                    DataTable ds = apfProduct.getDataTable(sp);

                    string uri = "https://api.sensis.com.au/apf/xrrv6qytnuvquwt3eckbmjqb/UpdateProductSDF_EWS_WS/ADAPTOR";

                    List<HttpContent> Listxml = new List<HttpContent>();
                    foreach (DataRow dw in ds.Rows)
                    {
                        data = new StringContent(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""> <env:Header/> <env:Body> <UpdateProductSDF xmlns=""http://amdocs/iam/ADAPTOR_WS/UpdateProductSDF_1""> <ROOT xmlns=""http://amdocs/iam/bc/CreUpdProductInput""> <Event xmlns=""""> <Type/> <ID/> </Event> <DATA xmlns=""""> <staticProductEntity> <advertiserId>" + dw["ADVERTISER_ID"] + "</advertiserId> <publishType>P</publishType> <bcProductId>" + dw["BC_PRODUCT_ID"] + "</bcProductId> <productCode>" + dw["PRODUCT_CODE"] + "</productCode> <status>" + "C" + "</status> <publishInd>" + dw["PUBLISH_IND"] + "</publishInd> <baseProductId></baseProductId> <seniorityDate><dateTimeString></dateTimeString></seniorityDate> <ctcrSourceProgId>SDFUPDATE</ctcrSourceProgId> <appearance> <GroupID>1</GroupID> <CUSTOM/> </appearance> <attribute> <attributeCode>LISTINGPRN</attributeCode> <attributeValue>" + dw["ATTRIBUTE_VALUE"] + "</attributeValue> </attribute> <CUSTOM> <effectiveVersionInd>" + dw["EFFECTIVE_VERSION_IND"] + "</effectiveVersionInd> <reasonText></reasonText> <L5_HEADING_CODE>" + dw["L5_HEADING_CODE"] + "</L5_HEADING_CODE> <L5_LISTING_ID>" + dw["L5_LISTING_ID"] + "</L5_LISTING_ID> <L5_DIRECTORY_CODE>" + dw["L5_DIRECTORY_CODE"] + "</L5_DIRECTORY_CODE> <L5_DIRECTORY_ISSUE>" + dw["L5_DIRECTORY_ISSUE"] + "</L5_DIRECTORY_ISSUE> <L5_MAIN_PRODUCT_ID>" + dw["MAIN_SOURCE_ID"] + "</L5_MAIN_PRODUCT_ID> </CUSTOM> </staticProductEntity> </DATA> </ROOT> </UpdateProductSDF> </env:Body> </env:Envelope>", Encoding.UTF8, "application/soap+xml");
                        Listxml.Add(data);
                    }
                    if (!connectSF(sp))
                    {
                        divGrid.Visible = false;
                        return;
                    }
                    else
                    {
                        PostRequest(uri, Listxml);
                    }
                    grdCsv.DataSource = ds;
                    grdCsv.DataBind();
                }
                
            }  
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
                        if(Regex.IsMatch(values, @"\d"))
                        {
                            
                            lblmg.Text = "Enter a Column Header BC_product_Id in the CSV file";
                            lblmg.ForeColor = System.Drawing.Color.Red;
                            divGrid.Visible = false;
                            return null;
                        }
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

            foreach (HttpContent xmld in xmldata)
                {
                    HttpResponseMessage response = await apfProduct.CreateSoapEnvelop(xmld, "cancelProduct");
              
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var messageContents = await response.Content.ReadAsStringAsync();
                        string msg = messageContents.ToString();
                        httpmsg = response.StatusCode + " Message Sent successfully";
                        lblmg.ForeColor = System.Drawing.Color.Green;
                         }
                      else
                        {
                        httpmsg = response.StatusCode + " An Error Occurred in sending message to APF";
                        lblmg.ForeColor = System.Drawing.Color.Red;

                        }
                    lblmg.Text = httpmsg;
                    System.Threading.Thread.Sleep(2000);
            }
   
        }

        protected Boolean connectSF(string bcProductId)
        {
         
            QueryResult queryResult = apfProduct.connectToSF(bcProductId);

            if (queryResult.size > 0)
            {
                sObject[] pl = queryResult.records;
                lblmg.Text = "The following Pli exists in Salesforce.Please check the csv file <br /><br />";
                StringBuilder sb = new StringBuilder(@"<html><table style=""border:thin;border-collapse:collapse;border-color:black;color:black;""><th>Print_Line_Item__c</th>");

                foreach (sObject p in pl)
                {
                    string PliId = ((Print_Line_Item__c)p).Id;
                    
                    sb.Append(@"<tr style=""border:solid;border-collapse:collapse;border-color:black;color:black;""><td><a href=""https://sensissf.my.salesforce.com/"" target=""_blank""" + PliId + ">https://sensissf.my.salesforce.com/" + PliId + "</a></td></tr>");
                }

                sb.Append("</table></html>");
                lblmg.Text = lblmg.Text + sb;
                
                //sflink.Visible = true;
                //sflink.NavigateUrl = "https://sensissf.my.salesforce.com/" + PliId;
                //sflink.Text = "https://sensissf.my.salesforce.com/" + PliId;
                //sflink.Target = "_blank";
                //sflink.Width = 450;
                //sflink.BackColor = System.Drawing.Color.BurlyWood;
                //sflink.ToolTip = "Click on this link to go to salesforce";
                lblmg.ForeColor = System.Drawing.Color.Red;
                return false;

            }
            else
            {
                return true;
            }

        }
    }
}

       
    