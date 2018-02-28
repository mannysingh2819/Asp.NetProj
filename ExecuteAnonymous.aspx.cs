using System;
using APFProductUpdateProj.Classes;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace APFProductUpdateProj
{
    public partial class ExecuteAnonymous : System.Web.UI.Page
    {
        APFProduct apfProduct = new APFProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            divout.InnerHtml = "";
            if (!Page.IsPostBack)

            {
                txteanonymous.Text = string.Empty;
            }

        }

        protected async void btnExecute_Click(object sender, EventArgs e)
        {
            string compiled = "";
            if (txteanonymous.Text != null && txteanonymous.Text != "")
            {
                divout.InnerHtml = "";
                string output = await apfProduct.connectRestAPIAsync(txteanonymous.Text);

                if (output == "OK")
                {
                    JObject obj = JObject.Parse(output);

                    string line = (string)obj["line"];
                    string column = (string)obj["column"];
                    compiled = (string)obj["compiled"];
                    string success = (string)obj["success"];
                    string compileProblem = (string)obj["compileProblem"];
                    string exceptionStackTrace = (string)obj["exceptionStackTrace"];
                    string exceptionMessage = (string)obj["exceptionMessage"];

                    if (compiled.ToLower() == "false")
                    {
                        divout.InnerHtml = @"Results <br /><span style=""color: Red;"">An Error Occurred at " + line + " and column " + column + " " + compileProblem + " " + exceptionMessage + " " + exceptionStackTrace + "</span>";
                        return;
                    }
                    else
                    {
                        string log = await apfProduct.rawlogs();
                        string str = log;
                        string[] strc = log.Split(new char[] { '\n', '\r' });
                        divout.InnerHtml = "Results <br />";
                        foreach (string st in strc)
                        {
                            divout.InnerHtml += @"<span style=""color: teal;text-align: left;"">" + st + "</span><br />";
                        }
                    }
                }
                if(compiled == "false")
                {
                    divout.InnerHtml = @"<span style=""color: Red;text-align: left;"">An Error Occured HTTP reason Phrase == >" + output + "</span><br />";
                }

            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txteanonymous.Text = string.Empty;
        }
    }
}